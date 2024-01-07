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

namespace BoundsDemo;

public class ToolboxViewModel : ReactiveObject
{
    private readonly Control _host;
    private readonly OverlayView _overlayView;
    private bool _captured;
    private Point _start;
    private Control? _control;
    private HashSet<Visual> _ignored;
    private XamlItem? _xamlItem;

    public ToolboxViewModel(Control host, OverlayView overlayView)
    {
        _host = host;
        _overlayView = overlayView;
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
            Drop(e, _ignored, true);
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

            Drop(e, _ignored, false);
        }
    }

    private Control? GetTarget(PointerEventArgs e, HashSet<Visual> ignored)
    {
        var root = _host.GetVisualRoot() as Interactive;
        if (root is null)
        {
            return null;
        }
        var descendants = root.GetLogicalDescendants().Cast<Visual>();

        var position = e.GetPosition(root);

        var mainViewModel = _host.DataContext as MainViewViewModel;
        if (mainViewModel is null)
        {
            return null;
        }

        var visuals = descendants
            .OfType<Control>()
            .Where(visual =>
            {
                if (!mainViewModel.TryGetXamlItem(visual, out _))
                {
                    return false;
                }

                if (!ignored.Contains(visual))
                {
                    var transformedBounds = visual.GetTransformedBounds();
                    return transformedBounds is not null
                           && transformedBounds.Value.Contains(position);
                }

                return false;
            })
            .Reverse();

        return visuals.FirstOrDefault();
    }
    
    private void Drop(PointerEventArgs e, HashSet<Visual> ignored, bool insert)
    {
        if (insert)
        {
            RemovePreview();
        }

        var target = GetTarget(e, ignored);

        if (insert)
        {
            if (target is not null && _control is not null && _xamlItem is not null)
            {
                Insert(target, _control, _xamlItem);
            }
        }
    }

    private void Insert(Control target, Control control, XamlItem xamlItem)
    {
        var mainViewModel = _host.DataContext as MainViewViewModel;
        if (mainViewModel is null)
        {
            return;
        }

        mainViewModel.InsertXamlItem(target, control, xamlItem);
    }

    private void CreatePreview(object? sender)
    {
        try
        {
            if (_host.DataContext is not MainViewViewModel mainViewModel)
            {
                return;
            }

            if (sender is ListBoxItem listBoxItem && listBoxItem.Content is XamlItem toolBoxItem)
            {
                _xamlItem = XamlItemFactory.Clone(toolBoxItem, mainViewModel.IdManager);
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
}
