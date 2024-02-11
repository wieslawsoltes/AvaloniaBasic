using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.VisualTree;
using ReactiveUI;

namespace FormsBuilder;

public interface IToolboxViewModel
{
    void AttachToContainer(Control container);
    void DetachFromContainer(Control container);
}

public class ToolboxViewModel : ReactiveObject, IToolboxViewModel
{
    private readonly Control _host;
    private readonly OverlayView _overlayView;
    private readonly IXamlEditorViewModel _xamlEditorViewModel;
    private bool _captured;
    private Point _start;
    private Control? _control;
    private HashSet<Visual> _ignored;
    private XamlItem? _xamlItem;

    public ToolboxViewModel(Control host, OverlayView overlayView, IXamlEditorViewModel xamlEditorViewModel)
    {
        _host = host;
        _overlayView = overlayView;
        _xamlEditorViewModel = xamlEditorViewModel;
        _ignored = new HashSet<Visual>();
    }

    public void AttachToContainer(Control container)
    {
        container.AddHandler(InputElement.PointerPressedEvent, ContainerOnPointerPressed, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        container.AddHandler(InputElement.PointerReleasedEvent, ContainerOnPointerReleased, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        container.AddHandler(InputElement.PointerMovedEvent, ContainerOnPointerMoved, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        container.AddHandler(InputElement.PointerCaptureLostEvent, ContainerOnPointerCaptureLost, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        container.AddHandler(Gestures.HoldingEvent, ContainerOnHolding);
    }

    public void DetachFromContainer(Control container)
    {
        container.RemoveHandler(InputElement.PointerPressedEvent, ContainerOnPointerPressed);
        container.RemoveHandler(InputElement.PointerReleasedEvent, ContainerOnPointerReleased);
        container.RemoveHandler(InputElement.PointerMovedEvent, ContainerOnPointerMoved);
        container.RemoveHandler(InputElement.PointerCaptureLostEvent, ContainerOnPointerCaptureLost);
        container.RemoveHandler(Gestures.HoldingEvent, ContainerOnHolding);
    }

    private void ContainerOnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        Pressed(e);
    }

    private void Pressed(PointerPressedEventArgs e)
    {
        if (e.Pointer.Type == PointerType.Mouse)
        {
            _captured = true;
            _start = e.GetPosition(e.Source as Control);
        }
    }

    private void ContainerOnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        Released(e);
    }

    private void Released(PointerReleasedEventArgs e)
    {
        if (_control is not null)
        {
            RemovePreview();
            Drop(e, _ignored);
        }

        Clean();
    }

    private void Clean()
    {
        _captured = false;
        _control = null;
        _xamlItem = null;
    }

    private void ContainerOnPointerCaptureLost(object? sender, PointerCaptureLostEventArgs e)
    {
        Clean();
    }

    private void ContainerOnHolding(object? sender, HoldingRoutedEventArgs e)
    {
        if (e.PointerType != PointerType.Mouse)
        {
            _captured = true;
            _start = e.Position;
        }
    }

    private void ContainerOnPointerMoved(object? sender, PointerEventArgs e)
    {
        Moved(sender, e);
    }

    private void Moved(object? sender, PointerEventArgs e)
    {
        if (!_captured)
        {
            return;
        }

        if (e.Pointer.Type != PointerType.Mouse)
        {
            e.PreventGestureRecognition();
        }

        var position = e.GetPosition(e.Source as Control);
        var delta = _start - position;

        if (Math.Abs(delta.X) > 3 || Math.Abs(delta.Y) > 3)
        {
            if (_control is null)
            {
                e.PreventGestureRecognition();

                CreatePreview(sender);
                AddPreview();
            }
        }

        if (_control is not null)
        {
            e.PreventGestureRecognition();
            
            MovePreview(e, position);
        }
    }

    private void CreatePreview(object? sender)
    {
        try
        {
            if (sender is ListBoxItem listBoxItem && listBoxItem.Content is XamlItem toolBoxItem)
            {
                _xamlItem = XamlItemFactory.Clone(toolBoxItem, _xamlEditorViewModel.IdManager);
                _control = XamlItemControlFactory.CreateControl(_xamlItem, isRoot: true, writeUid: true);
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }

    private void AddPreview()
    {
        if (_control is not null && _overlayView.Child is Canvas canvas)
        {
            canvas.Children.Add(_control);

            _ignored = new HashSet<Visual>(new Visual[] {_overlayView, _control});
        }
    }

    private void MovePreview(PointerEventArgs e, Point position)
    {
        if (_control is not null && _overlayView.Child is Canvas canvas && e.Source is Control source)
        {
            var location = source.TranslatePoint(position, canvas);
            if (location is not null)
            {
                Canvas.SetLeft(_control, location.Value.X);
                Canvas.SetTop(_control, location.Value.Y);
            }
        }
    }

    private void RemovePreview()
    {
        if (_control is not null && _overlayView.Child is Canvas canvas)
        {
            canvas.Children.Remove(_control);
        }
    }

    private void Drop(PointerEventArgs e, HashSet<Visual> ignored)
    {
        if (_control is null || _xamlItem is null)
        {
            return;
        }

        if (_host.GetVisualRoot() is not Interactive root)
        {
            return;
        }

        var target = GetTarget(root, e.GetPosition(root), ignored);
        if (target is null)
        {
            return;
        }

        var position = e.GetPosition(target);

        position = SnapHelper.SnapPoint(position, 6, 6, true);

        _xamlEditorViewModel.RemoveControl(_control);

        _xamlEditorViewModel.InsertXamlItem(target, _control, _xamlItem, position);
    }

    private Control? GetTarget(Interactive root, Point position, HashSet<Visual> ignored)
    {
        var descendants = root.GetLogicalDescendants().Cast<Visual>();

        return _xamlEditorViewModel.HitTest(descendants, position, ignored);
    }
}
