using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.VisualTree;

namespace BoundsDemo;

public partial class Toolbox : UserControl
{
    public static readonly StyledProperty<OverlayView> OverlayViewProperty = 
        AvaloniaProperty.Register<Toolbox, OverlayView>(nameof(OverlayView));

    private bool _captured;
    private Point _start;
    private Control? _control;
    private HashSet<Visual> _ignored;
    private XamlItem? _xamlItem;

    public OverlayView OverlayView
    {
        get => GetValue(OverlayViewProperty);
        set => SetValue(OverlayViewProperty, value);
    }

    public Toolbox()
    {
        InitializeComponent();

        _ignored = new HashSet<Visual>();

        ToolboxListBox.ContainerPrepared += ToolboxListBoxOnContainerPrepared;
        ToolboxListBox.ContainerClearing += ToolboxListBoxOnContainerClearing;
        ToolboxListBox.ContainerIndexChanged += ToolboxListBoxOnContainerIndexChanged;
    }

    private void ToolboxListBoxOnContainerPrepared(object? sender, ContainerPreparedEventArgs e)
    {
        //Console.WriteLine($"ContainerPrepared: {e.Container}");

        e.Container.AddHandler(Control.PointerPressedEvent, ContainerOnPointerPressed, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        e.Container.AddHandler(Control.PointerReleasedEvent, ContainerOnPointerReleased, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        e.Container.AddHandler(Control.PointerMovedEvent, ContainerOnPointerMoved, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        e.Container.AddHandler(Control.PointerCaptureLostEvent, ContainerOnPointerCaptureLost, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
    }

    private void ToolboxListBoxOnContainerClearing(object? sender, ContainerClearingEventArgs e)
    {
        //Console.WriteLine($"ContainerClearing: {e.Container}");

        e.Container.RemoveHandler(Control.PointerPressedEvent, ContainerOnPointerPressed);
        e.Container.RemoveHandler(Control.PointerReleasedEvent, ContainerOnPointerReleased);
        e.Container.RemoveHandler(Control.PointerMovedEvent, ContainerOnPointerMoved);
        e.Container.RemoveHandler(Control.PointerCaptureLostEvent, ContainerOnPointerCaptureLost);
    }

    private void ToolboxListBoxOnContainerIndexChanged(object? sender, ContainerIndexChangedEventArgs e)
    {
        //Console.WriteLine($"ContainerIndexChanged: {e.Container}");
    }

    private void ContainerOnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        //Console.WriteLine($"PointerPressed: {sender}");

        Pressed(e);
    }

    private void Pressed(PointerPressedEventArgs e)
    {
        _captured = true;
        _start = e.GetPosition(e.Source as Control);
    }

    private void ContainerOnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        //Console.WriteLine($"PointerReleased: {sender}");

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
        //Console.WriteLine($"PointerCaptureLost: {sender}");

        Clean();
    }

    private void ContainerOnPointerMoved(object? sender, PointerEventArgs e)
    {
        //Console.WriteLine($"PointerMoved: {sender}");

        Moved(sender, e);
    }

    private void Moved(object? sender, PointerEventArgs e)
    {
        if (!_captured)
        {
            return;
        }

        var position = e.GetPosition(e.Source as Control);
        var delta = _start - position;

        if (Math.Abs(delta.X) > 3 || Math.Abs(delta.Y) > 3)
        {
            if (_control is null)
            {
                CreatePreview(sender);
                AddPreview();
            }
        }

        if (_control is not null)
        {
            MovePreview(e, position);

            Drop(e, _ignored, false);
        }
    }

    private Control? GetTarget(PointerEventArgs e, HashSet<Visual> ignored)
    {
        var root = this.GetVisualRoot() as Interactive;
        if (root is null)
        {
            return null;
        }
        var descendants = root.GetLogicalDescendants().Cast<Visual>();

        var position = e.GetPosition(root);

        var toolBoxViewModel = DataContext as ToolBoxViewModel;
        if (toolBoxViewModel is null)
        {
            return null;
        }

        var visuals = descendants
            .OfType<Control>()
            .Where(visual =>
            {
                if (!toolBoxViewModel.TryGetXamlItem(visual, out _))
                {
                    return false;
                }

                //if (!ignored.Contains(visual) && OverlayView.GetEnableHitTest(visual))
                if (!ignored.Contains(visual))
                {
                    var transformedBounds = visual.GetTransformedBounds();
                    return transformedBounds is not null
                           && transformedBounds.Value.Contains(position);
                }

                return false;
            })
            .Reverse();

        return visuals.FirstOrDefault() as Control;
    }
    
    private void Drop(PointerEventArgs e, HashSet<Visual> ignored, bool insert)
    {
        if (insert)
        {
            RemovePreview();
        }

        var target = GetTarget(e, ignored);
#if DEBUG
        //Console.WriteLine($"Drop: {target}");
#endif

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
        var toolBoxViewModel = DataContext as ToolBoxViewModel;
        if (toolBoxViewModel is null)
        {
            return;
        }

        if (!toolBoxViewModel.TryGetXamlItem(target, out var targetXamlItem))
        {
            toolBoxViewModel.RemoveControl(control);
            return;
        }

        if (target is Panel panel)
        {
            if (targetXamlItem.ChildrenProperty is not null)
            {
                // TODO:
                panel.Children.Add(control);

                targetXamlItem.TryAddChild(xamlItem);
                toolBoxViewModel.AddControl(control, xamlItem);

                // TODO:
                toolBoxViewModel.Debug(targetXamlItem);
            }
        }
        else if (target is ContentControl contentControl)
        {
            if (targetXamlItem.ContentProperty is not null)
            {
                // TODO:
                contentControl.Content = control;

                targetXamlItem.TrySetContent(new XamlItemXamlValue(xamlItem));
                toolBoxViewModel.AddControl(control, xamlItem);

                // TODO:
                toolBoxViewModel.Debug(targetXamlItem);
            }
        }
        else
        {
            // TODO:
            toolBoxViewModel.RemoveControl(control);
        }
    }

    private void CreatePreview(object? sender)
    {
        try
        {
            var toolBoxViewModel = DataContext as ToolBoxViewModel;
            if (toolBoxViewModel is null)
            {
                return;
            }

            var toolBoxItem = (sender as ListBoxItem).Content as XamlItem;

            _xamlItem = XamlItemFactory.Clone(toolBoxItem, toolBoxViewModel.IdManager);

            _control = XamlItemControlFactory.CreateControl(_xamlItem, isRoot: true, writeUid: true);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }

    private void AddPreview()
    {
        if (_control is not null)
        {
            (OverlayView.Child as Canvas).Children.Add(_control);

            _ignored = new HashSet<Visual>(new Visual[] {OverlayView, _control});
        }
    }

    private void MovePreview(PointerEventArgs e, Point position)
    {
        var location = (e.Source as Control).TranslatePoint(position, OverlayView.Child as Canvas);

        Canvas.SetLeft(_control, location.Value.X);
        Canvas.SetTop(_control, location.Value.Y);
    }

    private void RemovePreview()
    {
        if (_control is not null)
        {
            (OverlayView.Child as Canvas).Children.Remove(_control);
        }
    }
}

