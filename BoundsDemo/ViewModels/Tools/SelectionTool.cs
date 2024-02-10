using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Input;

namespace BoundsDemo;

public class SelectionTool : Tool
{
    private readonly HashSet<Visual> _ignored;
    private Point _startPoint;
    private Point _endPoint;
    private bool _captured;
    
    public SelectionTool(IToolContext context)
    {
        _ignored = new HashSet<Visual>(new Visual[] {context.OverlayView});
    }

    public override void OnPointerPressed(IToolContext context, object? sender, PointerPressedEventArgs e)
    {
        context.OverlayView.Hover(null);
        context.OverlayView.Select(null);
        context.OverlayView.ClearSelection();
        _startPoint = e.GetPosition(null);
        _endPoint = _startPoint;
        e.Pointer.Capture(context.Host);
        _captured = true;
        UpdateRectSelection(context);
        context.Host.Focus();
    }

    public override void OnPointerReleased(IToolContext context, object? sender, PointerReleasedEventArgs e)
    {
        if (e.Pointer.Captured is not null && _captured)
        {
            _endPoint = e.GetPosition(null);
            context.OverlayView.Selection(_startPoint, _endPoint);
            UpdateRectSelection(context);
            e.Pointer.Capture(null);
            _captured = false;
            context.OverlayView.ClearSelection();
        }
    }

    public override void OnPointerMoved(IToolContext context, object? sender, PointerEventArgs e)
    {
        if (e.Pointer.Captured is not null && _captured)
        {
            _endPoint = e.GetPosition(null);
            context.OverlayView.Selection(_startPoint, _endPoint);
            UpdateRectSelection(context);
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

    private Rect GetSelectionRect()
    {
        var topLeft = new Point(
            Math.Min(_startPoint.X, _endPoint.X),
            Math.Min(_startPoint.Y, _endPoint.Y));
        var bottomRight = new Point(
            Math.Max(_startPoint.X, _endPoint.X),
            Math.Max(_startPoint.Y, _endPoint.Y));
        return new Rect(topLeft, bottomRight);
    }

    private void UpdateRectSelection(IToolContext context)
    {
        if (context.Host is null)
        {
            return;
        }

        // TODO:
        var rect = GetSelectionRect();
        var visuals = context.HitTest(
            context.Host, 
            context.OverlayView.HitTestMode, 
            _ignored, 
            x => 
            {
                var transformedRect = x.Bounds.TransformToAABB(x.Transform);
                return rect.Intersects(transformedRect);
            }).Reverse().Skip(1);

        context.OverlayView.Select(visuals);
#if false
        Console.WriteLine("[HitTest]");
        foreach (var visual in visuals)
        {
            Console.WriteLine($"{visual}");
        }
#endif
    }
}
