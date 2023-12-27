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

    public OverlayView OverlayView
    {
        get => GetValue(OverlayViewProperty);
        set => SetValue(OverlayViewProperty, value);
    }

    public Toolbox()
    {
        InitializeComponent();

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
    }

    private bool _captured;
    private Point _start;
    private Control? _control;
    private HashSet<Visual> _ignored;
    private XamlItem? _xamlItem;

    private void ContainerOnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        //Console.WriteLine($"PointerPressed: {sender}");

        _captured = true;
        _start = e.GetPosition(e.Source as Control);
    }

    private void ContainerOnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        //Console.WriteLine($"PointerReleased: {sender}");
        
        if (_control is not null)
        {
            Drop(e, _ignored, true);
        }

        _captured = false;
        _control = null;
        _xamlItem = null;
    }

    private void Drop(PointerEventArgs e, HashSet<Visual> ignored, bool insert)
    {
        if (insert)
        {
            if (_control is not null)
            {
                (OverlayView.Child as Canvas).Children.Remove(_control);
            }
        }

        var root = this.GetVisualRoot() as Interactive;
        if (root is null)
        {
            return;
        }
        var descendants = root.GetLogicalDescendants().Cast<Visual>();

        var position = e.GetPosition(root);

        var toolBoxViewModel = DataContext as ToolBoxViewModel;
        if (toolBoxViewModel is null)
        {
            return;
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

        var target = visuals.FirstOrDefault() as Control;

#if DEBUG
        //Console.WriteLine($"Drop: {target}");
#endif

        if (insert && target is not null && _control is not null && _xamlItem is not null)
        {
            if (!toolBoxViewModel.TryGetXamlItem(target, out var targetXamlItem))
            {
                toolBoxViewModel.RemoveControl(_control);
                return;
            }

            if (target is Panel panel)
            {
                if (targetXamlItem.ChildrenProperty is not null)
                {
                    // TODO:
                    panel.Children.Add(_control);

                    targetXamlItem.TryAddChild(_xamlItem);
                    toolBoxViewModel.AddControl(_control, _xamlItem);

                    // TODO:
                    toolBoxViewModel.Debug(targetXamlItem);
                }
            }
            else if (target is ContentControl contentControl)
            {
                if (targetXamlItem.ContentProperty is not null)
                {
                    // TODO:
                    contentControl.Content = _control;

                    targetXamlItem.TrySetContent(_xamlItem);
                    toolBoxViewModel.AddControl(_control, _xamlItem);

                    // TODO:
                    toolBoxViewModel.Debug(targetXamlItem);
                }
            }
            else
            {
                // TODO:
                toolBoxViewModel.RemoveControl(_control);
            }
        }
    }

    private void ContainerOnPointerMoved(object? sender, PointerEventArgs e)
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
                var toolBoxItem = (sender as ListBoxItem).Content as XamlItem;

                _xamlItem = toolBoxItem.Clone();

                try
                {
                    _control = XamlItemControlFactory.CreateControl(_xamlItem);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }

                if (_control is not null)
                {
                    (OverlayView.Child as Canvas).Children.Add(_control);

                    _ignored = new HashSet<Visual>(new Visual[] {OverlayView, _control});
                }
            }
        }

        if (_control is not null)
        {
            var location = (e.Source as Control).TranslatePoint(position, OverlayView.Child as Canvas);
            Canvas.SetLeft(_control, location.Value.X);
            Canvas.SetTop(_control, location.Value.Y);
        }

        if (_control is not null)
        {
            Drop(e, _ignored, false);
        }

        //Console.WriteLine($"PointerMoved: {sender}");
    }

    private void ToolboxListBoxOnContainerClearing(object? sender, ContainerClearingEventArgs e)
    {
        //Console.WriteLine($"ContainerClearing: {e.Container}");

        e.Container.RemoveHandler(Control.PointerPressedEvent, ContainerOnPointerPressed);
        e.Container.RemoveHandler(Control.PointerReleasedEvent, ContainerOnPointerReleased);
        e.Container.RemoveHandler(Control.PointerMovedEvent, ContainerOnPointerMoved);
    }

    private void ToolboxListBoxOnContainerIndexChanged(object? sender, ContainerIndexChangedEventArgs e)
    {
        //Console.WriteLine($"ContainerIndexChanged: {e.Container}");
    }
}

