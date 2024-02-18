using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Input;

namespace FormsBuilder;

public class PointerTool : Tool
{
    private readonly HashSet<Visual> _ignored;

    public PointerTool(IToolContext context)
    {
        _ignored = new HashSet<Visual>(new Visual[] {context.OverlayService.Overlay});
    }

    public override void OnPointerPressed(IToolContext context, object? sender, PointerPressedEventArgs e)
    {
        var point = e.GetPosition(null);
        var visuals = context.HitTest(context.Host, context.XamlSelection.HitTestMode, _ignored, x => x.Contains(point));
        var first = visuals.FirstOrDefault();
        context.XamlSelection.Select(first is null ? Enumerable.Empty<Visual>() : Enumerable.Repeat(first, 1));
        context.Host.Focus();
    }

    public override void OnPointerMoved(IToolContext context, object? sender, PointerEventArgs e)
    {
        var point = e.GetPosition(null);
        var visuals = context.HitTest(context.Host, context.XamlSelection.HitTestMode, _ignored, x => x.Contains(point));
        context.XamlSelection.Hover(visuals.FirstOrDefault());
    }
    
    public override void OnPointerExited(IToolContext context, object? sender, PointerEventArgs e)
    {
        context.XamlSelection.Hover(null);
        e.Pointer.Capture(null);
        context.XamlSelection.ClearSelection();
    }

    public override void OnPointerCaptureLost(IToolContext context, object? sender, PointerCaptureLostEventArgs e)
    {
        context.XamlSelection.Hover(null);
        e.Pointer.Capture(null);
        context.XamlSelection.ClearSelection();
    }
}
