using System.Diagnostics;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using Avalonia.Xaml.Interactions.Events;
using AvaloniaBasicDemo.Utilities;

namespace AvaloniaBasicDemo.Behaviors;

public class ControlDragBehavior : PointerEventsBehavior<Control>
{
    public static readonly StyledProperty<Canvas?> PreviewCanvasProperty = 
        AvaloniaProperty.Register<ControlDragBehavior, Canvas?>(nameof(PreviewCanvas));

    private Control? _dragArea;
    private Point _start;
    private Point _position;

    [ResolveByName]
    public Canvas? PreviewCanvas
    {
        get => GetValue(PreviewCanvasProperty);
        set => SetValue(PreviewCanvasProperty, value);
    }

    public ControlDragBehavior()
    {
        RoutingStrategies = RoutingStrategies.Tunnel | RoutingStrategies.Bubble;
    }

    protected override void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (AssociatedObject is null)
        {
            return;
        }

        var root = AssociatedObject.GetVisualRoot();
        var point = e.GetPosition(root);
        var dragArea = FindDragArea(point);
        if (dragArea is null)
        {
            return;
        }

        var enableDrag = DragSettings.GetEnableDrag(AssociatedObject);
        if (!enableDrag)
        {
            return;
        }

        _dragArea = dragArea;

        if (_dragArea.Parent is Canvas canvas)
        {
            _start = e.GetPosition(canvas);

            var left = Canvas.GetLeft(_dragArea);
            var top = Canvas.GetTop(_dragArea);
            _position = new Point(left, top);
        }

        e.Pointer.Capture(AssociatedObject);
        e.Handled = true;

        Debug.WriteLine($"Control: {dragArea}, Parent: {dragArea.Parent}");
    }

    protected override void OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (AssociatedObject is null)
        {
            return;
        }

        if (_dragArea is null)
        {
            return;
        }

        var enableDrag = DragSettings.GetEnableDrag(AssociatedObject);
        if (!enableDrag)
        {
            return;
        }

        _dragArea = null;
        e.Handled = true;
        e.Pointer.Capture(null);
    }

    protected override void OnPointerMoved(object? sender, PointerEventArgs e)
    {
        if (AssociatedObject is null)
        {
            return;
        }

        if (_dragArea is null)
        {
            return;
        }

        var enableDrag = DragSettings.GetEnableDrag(AssociatedObject);
        if (!enableDrag)
        {
            return;
        }

        if (_dragArea.Parent is Canvas canvas)
        {
            var position = e.GetPosition(canvas);
            var delta = position - _start;

            position = new Point(_position.X + delta.X, _position.Y + delta.Y);
            position = SnapPoint(position);

            Canvas.SetLeft(_dragArea, position.X);
            Canvas.SetTop(_dragArea, position.Y);
        }

        e.Handled = true;
    }

    private Point SnapPoint(Point point)
    {
        if (AssociatedObject is null)
        {
            return point;
        }

        IAvaloniaObject snapObject = AssociatedObject;

        var snapToGrid = DragSettings.GetSnapToGrid(snapObject);
        var snapX = DragSettings.GetSnapX(snapObject);
        var snapY = DragSettings.GetSnapY(snapObject);
        var snappedPoint = Snap.SnapPoint(point, snapX, snapY, snapToGrid);

        return snappedPoint;
    }
    
    private Control? FindDragArea(Point point)
    {
        var visuals = AssociatedObject
            .GetVisualDescendants()
            .Where(x => x.TransformedBounds is not null && x.TransformedBounds.Value.Contains(point))
            .Reverse();

        var dragAreas = visuals.OfType<Control>().Where(DragSettings.GetIsDragArea).ToList();

        var dragArea = dragAreas.FirstOrDefault();

        return dragArea;
    }
}
