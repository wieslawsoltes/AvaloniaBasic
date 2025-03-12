using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace ResizingControlDemo.Controls;

public class ResizingAdornerControl : TemplatedControl
{
    private double _left;
    private double _top;
    private double _width;
    private double _height;
    private Panel? AdornerPanel;
    private Canvas? AdornerCanvas;
    private Thumb? MoveThumb;
    private Thumb? TopLeftThumb;
    private Thumb? TopRightThumb;
    private Thumb? BottomLeftThumb;
    private Thumb? BottomRightThumb;

    public static readonly StyledProperty<Canvas?> EditorCanvasProperty = 
        AvaloniaProperty.Register<ResizingAdornerControl, Canvas?>(nameof(EditorCanvas));

    public static readonly StyledProperty<Visual?> AdornedElementProperty =
        AvaloniaProperty.Register<ResizingAdornerControl, Visual?>(nameof(AdornedElement));

    public static readonly AttachedProperty<bool> IsResizingEnabledProperty =
        AvaloniaProperty.RegisterAttached<ResizingAdornerControl, Visual, bool>("IsResizingEnabled", true, true);

    public static readonly AttachedProperty<bool> IsResizingVisibleProperty =
        AvaloniaProperty.RegisterAttached<ResizingAdornerControl, Visual, bool>("IsResizingVisible", false, true);

    public static readonly AttachedProperty<bool> IsResizingSelectedProperty =
        AvaloniaProperty.RegisterAttached<ResizingAdornerControl, Visual, bool>("IsResizingSelected", false, true);
  
    public static readonly AttachedProperty<ResizingHostControl?> ResizingHostControlProperty =
        AvaloniaProperty.RegisterAttached<ResizingAdornerControl, Visual?, ResizingHostControl?>("ResizingHostControl", null, true);

    [ResolveByName]
    public Canvas? EditorCanvas
    {
        get => GetValue(EditorCanvasProperty);
        set => SetValue(EditorCanvasProperty, value);
    }

    public Visual? AdornedElement
    {
        get => GetValue(AdornedElementProperty);
        set => SetValue(AdornedElementProperty, value);
    }

    public static bool GetIsResizingEnabled(Visual visual)
    {
        return visual.GetValue(IsResizingEnabledProperty);
    }

    public static void SetIsResizingEnabled(Visual visual, bool isResizingEnabled)
    {
        visual.SetValue(IsResizingEnabledProperty, isResizingEnabled);
    }

    public static bool GetIsResizingVisible(Visual visual)
    {
        return visual.GetValue(IsResizingVisibleProperty);
    }

    public static void SetIsResizingVisible(Visual visual, bool isResizingVisible)
    {
        visual.SetValue(IsResizingVisibleProperty, isResizingVisible);
    }

    public static bool GetIsResizingSelected(Visual visual)
    {
        return visual.GetValue(IsResizingSelectedProperty);
    }

    public static void SetIsResizingSelected(Visual visual, bool isResizingSelected)
    {
        visual.SetValue(IsResizingSelectedProperty, isResizingSelected);
    }
    
    public static ResizingHostControl? GetResizingHostControl(Visual visual)
    {
        return visual.GetValue(ResizingHostControlProperty);
    }

    public static void SetResizingHostControl(Visual visual, ResizingHostControl? resizingHostControl)
    {
        visual.SetValue(ResizingHostControlProperty, resizingHostControl);
    }

    public static double Snap(double value, double snap)
    {
        if (snap == 0.0)
        {
            return value;
        }
        var c = value % snap;
        var r = c >= snap / 2.0 ? value + snap - c : value - c;
        return r;
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        AdornerPanel = e.NameScope.Find<Panel>("AdornerPanel");
        AdornerCanvas = e.NameScope.Find<Canvas>("AdornerCanvas");
        MoveThumb = e.NameScope.Find<Thumb>("MoveThumb");
        TopLeftThumb = e.NameScope.Find<Thumb>("TopLeftThumb");
        TopRightThumb = e.NameScope.Find<Thumb>("TopRightThumb");
        BottomLeftThumb = e.NameScope.Find<Thumb>("BottomLeftThumb");
        BottomRightThumb = e.NameScope.Find<Thumb>("BottomRightThumb");

        MoveThumb.AddHandler(PointerPressedEvent, MoveThumb_OnPointerPressed, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        
        MoveThumb.DragStarted += MoveThumb_OnDragStarted;
        MoveThumb.DragDelta += MoveThumb_OnDragDelta;
        MoveThumb.DragCompleted += MoveThumb_OnDragCompleted;

        TopLeftThumb.DragStarted += TopLeftThumb_OnDragStarted;
        TopLeftThumb.DragDelta += TopLeftThumb_OnDragDelta;
        TopLeftThumb.DragCompleted += TopLeftThumb_OnDragCompleted;

        TopRightThumb.DragStarted += TopRightThumb_OnDragStarted;
        TopRightThumb.DragDelta += TopRightThumb_OnDragDelta;
        TopRightThumb.DragCompleted += TopRightThumb_OnDragCompleted;

        BottomLeftThumb.DragStarted += BottomLeftThumb_OnDragStarted;
        BottomLeftThumb.DragDelta += BottomLeftThumb_OnDragDelta;
        BottomLeftThumb.DragCompleted += BottomLeftThumb_OnDragCompleted;

        BottomRightThumb.DragStarted += BottomRightThumb_OnDragStarted;
        BottomRightThumb.DragDelta += BottomRightThumb_OnDragDelta;
        BottomRightThumb.DragCompleted += BottomRightThumb_OnDragCompleted;
    }

    private enum DragDirection
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight,
        Move,
    }

    private void DragStarted(object? sender, DragDirection direction)
    {
        var thumb = sender as Thumb;
        var adornedElement = AdornedElement as Control;

        var left = Canvas.GetLeft(adornedElement);
        if (double.IsNaN(left))
        {
            left = adornedElement.Bounds.Left;
        }

        var top = Canvas.GetTop(adornedElement);
        if (double.IsNaN(top))
        {
            top = adornedElement.Bounds.Top;
        }

        var width = adornedElement.Width;
        if (double.IsNaN(width))
        {
            width = adornedElement.Bounds.Width;
        }
        
        var height = adornedElement.Height;
        if (double.IsNaN(height))
        {
            height = adornedElement.Bounds.Height;
        }

        _left = left;
        _top = top;
        _width = width;
        _height = height;

        // Console.WriteLine($"DragStarted={direction} {adornedElement.GetType().Name} left={_left} top={_top} width={_width} height={_height}");
    }

    private void DragDelta(object? sender, VectorEventArgs e, DragDirection direction)
    {
        var thumb = sender as Thumb;
        var adornedElement = AdornedElement as Control;

        var left = direction switch
        {
            DragDirection.TopLeft => _left + e.Vector.X,
            DragDirection.TopRight => _left,
            DragDirection.BottomLeft => _left + e.Vector.X,
            DragDirection.BottomRight => _left,
            DragDirection.Move => _left + e.Vector.X,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };

        var top = direction switch
        {
            DragDirection.TopLeft => _top + e.Vector.Y,
            DragDirection.TopRight => _top + e.Vector.Y,
            DragDirection.BottomLeft => _top,
            DragDirection.BottomRight => _top,
            DragDirection.Move => _top + e.Vector.Y,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };

        var width = direction switch
        {
            DragDirection.TopLeft => _width - e.Vector.X,
            DragDirection.TopRight => _width + e.Vector.X,
            DragDirection.BottomLeft => _width - e.Vector.X,
            DragDirection.BottomRight => _width + e.Vector.X,
            DragDirection.Move => _width,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };

        var height = direction switch
        {
            DragDirection.TopLeft => _height - e.Vector.Y,
            DragDirection.TopRight => _height - e.Vector.Y,
            DragDirection.BottomLeft => _height + e.Vector.Y,
            DragDirection.BottomRight => _height + e.Vector.Y,
            DragDirection.Move => _height,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };

        if (width < 0.0)
        {
            switch (direction)
            {
                case DragDirection.TopLeft:
                case DragDirection.TopRight:
                case DragDirection.BottomLeft:
                case DragDirection.BottomRight:
                    width = Math.Abs(width);
                    left -= width;
                    break;
                case DragDirection.Move:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        if (height < 0.0)
        {
            switch (direction)
            {
                case DragDirection.TopLeft:
                case DragDirection.TopRight:
                case DragDirection.BottomLeft:
                case DragDirection.BottomRight:
                    height = Math.Abs(height);
                    top -= height;
                    break;
                case DragDirection.Move:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }
  
#if true
        left = Snap(left, 8.0);
        top = Snap(top, 8.0);
        width = Snap(width, 8.0);
        height = Snap(height, 8.0);
#endif
     
        // TODO: Clamp width and height between min/max values
#if false
        var minHeight = adornedElement.MinHeight;
        var maxHeight = adornedElement.MaxHeight;
        var minWidth = adornedElement.MinWidth;
        var maxWidth = adornedElement.MaxWidth;

        height = Math.Clamp(height, minHeight, maxHeight);
        width = Math.Clamp(width, minWidth, maxWidth);
#endif

        switch (direction)
        {
            case DragDirection.TopLeft:
                break;
            case DragDirection.TopRight:
                _width = width;
                break;
            case DragDirection.BottomLeft:
                _height = height;
                break;
            case DragDirection.BottomRight:
                _width = width;
                _height = height;
                break;
            case DragDirection.Move:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }

        Canvas.SetLeft(adornedElement, left);
        Canvas.SetTop(adornedElement, top);
        adornedElement.Width = width;
        adornedElement.Height = height;

        // Console.WriteLine($"DragDelta={direction} {adornedElement.GetType().Name} Vector={e.Vector} width={width} height={height}");
    }

    private void DragCompleted(object? sender, DragDirection direction)
    {
        var thumb = sender as Thumb;
        var adornedElement = AdornedElement as Control;

        // Console.WriteLine($"DragCompleted={direction} {adornedElement.GetType().Name}");
    }

    private void Drop(object? sender, VectorEventArgs e)
    {
        var thumb = sender as Thumb;
        var control = AdornedElement as Control;

        var editorCanvas = EditorCanvas;
        if (editorCanvas is null)
        {
            return;
        }
 
        var canvasPoint = this.TranslatePoint(new Point(e.Vector.X, e.Vector.Y), editorCanvas).Value;
        var isAddedToChild = false;

        foreach (var child in editorCanvas.Children)
        {
            if (child == control)
            {
                continue;
            }

            var childPoint = editorCanvas.TranslatePoint(canvasPoint, child).Value;

            // Panel.Children
            if (child is Panel panel
                && panel.GetTransformedBounds().Value.Bounds.Contains(childPoint))
            {
                if (control.Parent == panel)
                {
                    isAddedToChild = true;
                    break;
                }

                if (panel is Canvas)
                {
                    var left = Canvas.GetLeft(control);
                    var top = Canvas.GetTop(control);
                    var position = (control.Parent as Visual).TranslatePoint(new Point(left, top), panel).Value;
                    Canvas.SetLeft(control, position.X);
                    Canvas.SetTop(control, position.Y);
                }

                RemoveFromParent(control);

                panel.Children.Add(control);
                isAddedToChild = true;

                break;
            }

            // ContentControl.Content
            if (child is ContentControl contentControl
                && child.GetTransformedBounds().Value.Bounds.Contains(childPoint))
            {
                if (control.Parent == contentControl)
                {
                    isAddedToChild = true;
                    break;
                }

                RemoveFromParent(control);

                contentControl.Content = control;
                isAddedToChild = true;
                    
                break;
            }

            // Decorator.Child
            if (child is Decorator decorator
                && child.GetTransformedBounds().Value.Bounds.Contains(childPoint))
            {
                if (control.Parent == decorator)
                {
                    isAddedToChild = true;
                    break;
                }

                RemoveFromParent(control);

                decorator.Child = control;
                isAddedToChild = true;
                    
                break;
            }
        }

        if (!isAddedToChild 
            && control.Parent != editorCanvas 
            && editorCanvas.GetTransformedBounds().Value.Bounds.Contains(canvasPoint))
        {
            var left = Canvas.GetLeft(control);
            var top = Canvas.GetTop(control);
            var position = (control.Parent as Visual).TranslatePoint(new Point(left, top), editorCanvas).Value;
            Canvas.SetLeft(control, position.X);
            Canvas.SetTop(control, position.Y);

            RemoveFromParent(control);

            editorCanvas.Children.Add(control);
        }
    }

    private static void RemoveFromParent(Control control)
    {
        if (control.Parent is Panel childPanel)
        {
            childPanel.Children.Remove(control);
        }

        if (control.Parent is ContentControl childContentControl)
        {
            childContentControl.Content = null;
        }

        if (control.Parent is Decorator childDecorator)
        {
            childDecorator.Child = null;
        }
    }

    // TopLeft
    
    private void TopLeftThumb_OnDragStarted(object? sender, VectorEventArgs e)
    {
        DragStarted(sender, DragDirection.TopLeft);
    }

    private void TopLeftThumb_OnDragDelta(object? sender, VectorEventArgs e)
    {
        DragDelta(sender, e, DragDirection.TopLeft);
    }

    private void TopLeftThumb_OnDragCompleted(object? sender, VectorEventArgs e)
    {
        DragCompleted(sender, DragDirection.TopLeft);
    }

    // TopRight
    
    private void TopRightThumb_OnDragStarted(object? sender, VectorEventArgs e)
    {
        DragStarted(sender, DragDirection.TopRight);
    }

    private void TopRightThumb_OnDragDelta(object? sender, VectorEventArgs e)
    {
        DragDelta(sender, e, DragDirection.TopRight);
    }

    private void TopRightThumb_OnDragCompleted(object? sender, VectorEventArgs e)
    {
        DragCompleted(sender, DragDirection.TopRight);
    }

    // BottomLeft
    
    private void BottomLeftThumb_OnDragStarted(object? sender, VectorEventArgs e)
    {
        DragStarted(sender, DragDirection.BottomLeft);
    }

    private void BottomLeftThumb_OnDragDelta(object? sender, VectorEventArgs e)
    {
        DragDelta(sender, e, DragDirection.BottomLeft);
    }

    private void BottomLeftThumb_OnDragCompleted(object? sender, VectorEventArgs e)
    {
        DragCompleted(sender, DragDirection.BottomLeft);
    }

    // BottomRight

    private void BottomRightThumb_OnDragStarted(object? sender, VectorEventArgs e)
    {
        DragStarted(sender, DragDirection.BottomRight);
    }

    private void BottomRightThumb_OnDragDelta(object? sender, VectorEventArgs e)
    {
        DragDelta(sender, e, DragDirection.BottomRight);
    }

    private void BottomRightThumb_OnDragCompleted(object? sender, VectorEventArgs e)
    {
        DragCompleted(sender, DragDirection.BottomRight);
    }

    // MoveThumb

    private void MoveThumb_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        var resizingHostControl = this.GetValue(ResizingHostControlProperty);

        var selectedResizingAdornerControl = resizingHostControl.GetValue(ResizingHostControl.SelectedResizingAdornerControlProperty);
        if (selectedResizingAdornerControl is not null)
        {
            selectedResizingAdornerControl.SetCurrentValue(ResizingAdornerControl.IsResizingSelectedProperty, false);
        }

        SetCurrentValue(IsResizingSelectedProperty, true);

        if (resizingHostControl is not null)
        {
            resizingHostControl.SetCurrentValue(ResizingHostControl.SelectedResizingAdornerControlProperty, this);
        }
    }

    private void MoveThumb_OnDragStarted(object? sender, VectorEventArgs e)
    {
        DragStarted(sender, DragDirection.Move);
    }

    private void MoveThumb_OnDragDelta(object? sender, VectorEventArgs e)
    {
        DragDelta(sender, e, DragDirection.Move);
    }

    private void MoveThumb_OnDragCompleted(object? sender, VectorEventArgs e)
    {
        DragCompleted(sender, DragDirection.Move);

        Drop(sender, e);
    }
}
