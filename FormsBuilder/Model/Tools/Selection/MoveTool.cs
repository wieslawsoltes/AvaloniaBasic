using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Input;

namespace FormsBuilder;

public class MoveTool : Tool
{
    private readonly HashSet<Visual> _ignored;
    private Point _movePoint;
    private Point _startPoint;
    private Point _endPoint;
    private bool _captured;
    private Rect _selectionRect;
    private bool _moveMode;

    public MoveTool(IToolContext context)
    {
        _ignored = new HashSet<Visual>(new Visual[] {context.OverlayService.Overlay});
    }

    public override void OnPointerPressed(IToolContext context, object? sender, PointerPressedEventArgs e)
    {
        var point = e.GetPosition(null);

        if (_selectionRect != default && _selectionRect.Contains(point))
        {
            context.XamlSelection.BeginMoveSelection();
            _moveMode = true;
            _movePoint = point;
        }
        else
        {
            context.XamlSelection.Hover(null);
            context.XamlSelection.Select(null);
            context.XamlSelection.ClearSelection();
            _startPoint = point;
            _endPoint = _startPoint;

            _moveMode = false;
            _selectionRect = UpdateRectSelection(context, _startPoint, _endPoint, _ignored);

            if (_selectionRect.Contains(point))
            {
                context.XamlSelection.BeginMoveSelection();
                _moveMode = true;
                _movePoint = point;
            }
        }

        e.Pointer.Capture(context.Host);
        _captured = true;
        context.Host.Focus();
    }

    public override void OnPointerReleased(IToolContext context, object? sender, PointerReleasedEventArgs e)
    {
        if (e.Pointer.Captured is null || !_captured)
        {
            return;
        }
        
        if (_moveMode)
        {
            context.XamlSelection.EndMoveSelection();
            _moveMode = false;
        }
        else
        {
            _endPoint = e.GetPosition(null);
            context.XamlSelection.Selection(_startPoint, _endPoint);
            _selectionRect = UpdateRectSelection(context, _startPoint, _endPoint, _ignored);
            context.XamlSelection.ClearSelection();
        }

        e.Pointer.Capture(null);
        _captured = false;
    }

    public override void OnPointerMoved(IToolContext context, object? sender, PointerEventArgs e)
    {
        if (e.Pointer.Captured is null || !_captured)
        {
            return;
        }
        
        if (_moveMode)
        {
            var point = e.GetPosition(null);
            var delta = point - _movePoint;

            context.XamlSelection.MoveSelection(delta);

            //_movePoint = point;

            var selected = context.XamlSelection.Selected;
            if (selected is not null)
            {
                _selectionRect = RectHelper.GetSelectionRectUnion(selected);
            }
        }
        else
        {
            _endPoint = e.GetPosition(null);
            context.XamlSelection.Selection(_startPoint, _endPoint);
            _selectionRect = UpdateRectSelection(context, _startPoint, _endPoint, _ignored);
        }
    }

    public override void OnPointerExited(IToolContext context, object? sender, PointerEventArgs e)
    {
        _captured = false;

        context.XamlSelection.Hover(null);
        e.Pointer.Capture(null);
        context.XamlSelection.ClearSelection();
    }

    public override void OnPointerCaptureLost(IToolContext context, object? sender, PointerCaptureLostEventArgs e)
    {
        _captured = false;

        context.XamlSelection.Hover(null);
        e.Pointer.Capture(null);
        context.XamlSelection.ClearSelection();
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
            context.XamlSelection.HitTestMode, 
            ignored, 
            x => 
            {
                var transformedRect = x.Bounds.TransformToAABB(x.Transform);
                return rect.Intersects(transformedRect);
            }).Reverse().Skip(1).ToList();

        context.XamlSelection.Select(visuals);

        return RectHelper.GetSelectionRectUnion(visuals);
    }
}
