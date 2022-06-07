using System.Diagnostics;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace AvaloniaBasicDemo.Behaviors;

public class ControlDragBehavior : PointerEventsBehavior<Control>
{
    public static readonly StyledProperty<Canvas?> PreviewCanvasProperty = 
        AvaloniaProperty.Register<ControlDragBehavior, Canvas?>(nameof(PreviewCanvas));

    private Control? _dragArea;
    private Point _start;

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
        var root = AssociatedObject.GetVisualRoot();
        var point = e.GetPosition(root);
        var dragArea = FindDragArea(point);
        if (dragArea is { })
        {
            _dragArea = dragArea;
            _start = e.GetPosition(AssociatedObject);
            e.Pointer.Capture(AssociatedObject);
            e.Handled = true;

            Debug.WriteLine($"Control: {dragArea}, Parent: {dragArea.Parent}");
        }
    }

    protected override void OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (_dragArea is { })
        {
            _dragArea = null;
            e.Handled = true;
            e.Pointer.Capture(null);
        }
    }

    protected override void OnPointerMoved(object? sender, PointerEventArgs e)
    {
        if (_dragArea is { })
        {
            if (_dragArea.Parent is Canvas)
            {
                var point = e.GetPosition(AssociatedObject);
                var delta = point - _start;

                var left = Canvas.GetLeft(_dragArea);
                var top = Canvas.GetTop(_dragArea); 

                Canvas.SetLeft(_dragArea, left + delta.X);
                Canvas.SetTop(_dragArea, top + delta.Y);

                _start = point;
            }

            e.Handled = true;
        }
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
