using System.Collections.Generic;
using Avalonia;
using Avalonia.Input;

namespace FormsBuilder;

public class RectangleTool : Tool
{
    private readonly HashSet<Visual> _ignored;
    private Point _startPoint;
    private Point _endPoint;
    private bool _captured;
    private Rect _selectionRect;

    public RectangleTool(IToolContext context)
    {
        _ignored = new HashSet<Visual>(new Visual[] {context.OverlayView});
    }

    public override void OnPointerPressed(IToolContext context, object? sender, PointerPressedEventArgs e)
    {
        var point = e.GetPosition(null);

        _startPoint = point;
        _endPoint = _startPoint;
        _selectionRect = RectHelper.GetSelectionRect(_startPoint, _endPoint);

        e.Pointer.Capture(context.Host);
        _captured = true;
 
        context.OverlayView.Hover(null);
        context.OverlayView.Select(null);
        context.OverlayView.ClearSelection();

        context.Host.Focus();
    }

    public override void OnPointerReleased(IToolContext context, object? sender, PointerReleasedEventArgs e)
    {
        if (e.Pointer.Captured is not null && _captured)
        {
            e.Pointer.Capture(null);
            _captured = false;

            _endPoint = e.GetPosition(null);
            _selectionRect = RectHelper.GetSelectionRect(_startPoint, _endPoint);
  
            context.OverlayView.Selection(_startPoint, _endPoint);
            context.OverlayView.ClearSelection();
        }
    }

    public override void OnPointerMoved(IToolContext context, object? sender, PointerEventArgs e)
    {
        if (e.Pointer.Captured is null || !_captured)
        {
            return;
        }

        _endPoint = e.GetPosition(null);
        _selectionRect = RectHelper.GetSelectionRect(_startPoint, _endPoint);

        context.OverlayView.Selection(_startPoint, _endPoint);
    }

    public override void OnPointerExited(IToolContext context, object? sender, PointerEventArgs e)
    {
        _captured = false;
        e.Pointer.Capture(null);

        context.OverlayView.Hover(null);
        context.OverlayView.ClearSelection();
    }

    public override void OnPointerCaptureLost(IToolContext context, object? sender, PointerCaptureLostEventArgs e)
    {
        _captured = false;
        e.Pointer.Capture(null);

        context.OverlayView.Hover(null);
        context.OverlayView.ClearSelection();
    }
}
