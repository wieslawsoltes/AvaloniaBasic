using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Input;

namespace FormsBuilder;

public class SelectionTool : Tool
{
    private readonly HashSet<Visual> _ignored;
    private Point _movePoint;
    private Point _startPoint;
    private Point _endPoint;
    private bool _captured;
    private Rect _selectionRect;
    private bool _moveMode;

    public SelectionTool(IToolContext context)
    {
        _ignored = new HashSet<Visual>(new Visual[] {context.OverlayView});
    }

    public override void OnPointerPressed(IToolContext context, object? sender, PointerPressedEventArgs e)
    {
        var point = e.GetPosition(null);

        if (_selectionRect != default && _selectionRect.Contains(point))
        {
            context.OverlayView.BeginMoveSelection();
            _moveMode = true;
            _movePoint = point;
        }
        else
        {
            context.OverlayView.Hover(null);
            context.OverlayView.Select(null);
            context.OverlayView.ClearSelection();
            _startPoint = point;
            _endPoint = _startPoint;

            _moveMode = false;
            _selectionRect = UpdateRectSelection(context, _startPoint, _endPoint, _ignored);
        }

        e.Pointer.Capture(context.Host);
        _captured = true;
        context.Host.Focus();
    }

    public override void OnPointerReleased(IToolContext context, object? sender, PointerReleasedEventArgs e)
    {
        if (e.Pointer.Captured is not null && _captured)
        {
            if (_moveMode)
            {
                context.OverlayView.EndMoveSelection();
                _moveMode = false;
            }
            else
            {
                _endPoint = e.GetPosition(null);
                context.OverlayView.Selection(_startPoint, _endPoint);
                _selectionRect = UpdateRectSelection(context, _startPoint, _endPoint, _ignored);
                context.OverlayView.ClearSelection();
            }

            e.Pointer.Capture(null);
            _captured = false;
        }
    }

    public override void OnPointerMoved(IToolContext context, object? sender, PointerEventArgs e)
    {
        if (e.Pointer.Captured is not null && _captured)
        {
            if (_moveMode)
            {
                var point = e.GetPosition(null);
                var delta = point - _movePoint;

                context.OverlayView.MoveSelection(delta);

                //_movePoint = point;

                var selected = context.OverlayView.GetSelected();
                if (selected is not null)
                {
                    _selectionRect = RectHelper.GetSelectionRectUnion(selected);
                }
            }
            else
            {
                _endPoint = e.GetPosition(null);
                context.OverlayView.Selection(_startPoint, _endPoint);
                _selectionRect = UpdateRectSelection(context, _startPoint, _endPoint, _ignored);
            }
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

    private static Rect UpdateRectSelection(IToolContext context, Point startPoint, Point endPoint, HashSet<Visual> ignored)
    {
        if (context.Host is null)
        {
            return new Rect();
        }

        // TODO:
        var rect = RectHelper.GetSelectionRect(startPoint, endPoint);
        var visuals = context.HitTest(
            context.Host, 
            context.OverlayView.HitTestMode, 
            ignored, 
            x => 
            {
                var transformedRect = x.Bounds.TransformToAABB(x.Transform);
                return rect.Intersects(transformedRect);
            }).Reverse().Skip(1).ToList();

        context.OverlayView.Select(visuals);

        return RectHelper.GetSelectionRectUnion(visuals);
    }
}
