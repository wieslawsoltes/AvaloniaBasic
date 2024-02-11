using System;
using Avalonia;
using Avalonia.Input;

namespace FormsBuilder;

public class RectangleTool : Tool
{
    /*
    private Point _startPoint;
    private Point _endPoint;
    private bool _captured;
    private Rect _selectionRect;

    public override void OnPointerPressed(IToolContext context, object? sender, PointerPressedEventArgs e)
    {
        var point = e.GetPosition(null);


            context.OverlayView.Hover(null);
            context.OverlayView.Select(null);
            context.OverlayView.ClearSelection();
            _startPoint = point;
            _endPoint = _startPoint;

            _moveMode = false;
            _selectionRect = UpdateRectSelection(context, _startPoint, _endPoint, _ignored);

        e.Pointer.Capture(context.Host);
        _captured = true;
        context.Host.Focus();
    }

    public override void OnPointerReleased(IToolContext context, object? sender, PointerReleasedEventArgs e)
    {
        if (e.Pointer.Captured is not null && _captured)
        {

                _endPoint = e.GetPosition(null);
                context.OverlayView.Selection(_startPoint, _endPoint);
                _selectionRect = UpdateRectSelection(context, _startPoint, _endPoint, _ignored);
                context.OverlayView.ClearSelection();

            e.Pointer.Capture(null);
            _captured = false;
        }
    }

    public override void OnPointerMoved(IToolContext context, object? sender, PointerEventArgs e)
    {
        if (e.Pointer.Captured is not null && _captured)
        {

                _endPoint = e.GetPosition(null);
                context.OverlayView.Selection(_startPoint, _endPoint);
                _selectionRect = UpdateRectSelection(context, _startPoint, _endPoint, _ignored);

        }
    }

    public override void OnPointerExited(IToolContext context, object? sender, PointerEventArgs e)
    {
        _captured = false;

        context.OverlayView.Hover(null);
        e.Pointer.Capture(null);
        context.OverlayView.ClearSelection();
    }

    public override void OnPointerCaptureLost(IToolContext context, object? sender, PointerCaptureLostEventArgs e)
    {
        _captured = false;

        context.OverlayView.Hover(null);
        e.Pointer.Capture(null);
        context.OverlayView.ClearSelection();
    }

    private static Rect GetSelectionRect(Point startPoint, Point endPoint)
    {
        var topLeft = new Point(
            Math.Min(startPoint.X, endPoint.X),
            Math.Min(startPoint.Y, endPoint.Y));
        var bottomRight = new Point(
            Math.Max(startPoint.X, endPoint.X),
            Math.Max(startPoint.Y, endPoint.Y));
        return new Rect(topLeft, bottomRight);
    }
    */
}
