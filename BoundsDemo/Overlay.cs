using System;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.VisualTree;

namespace BoundsDemo;

public enum HitTestMode
{
    Logical,
    Visual,
}

public class Overlay : Control
{
    public static readonly AttachedProperty<bool> EnableHitTestProperty =
        AvaloniaProperty.RegisterAttached<Overlay, Visual, bool>("EnableHitTest", false, true);

    public static bool GetEnableHitTest(Visual element)
    {
        return element.GetValue(EnableHitTestProperty);
    }

    public static void SetEnableHitTest(Visual element, bool value)
    {
        element.SetValue(EnableHitTestProperty, value);
    }

    public event EventHandler<EventArgs>? HoveredChanged;

    public event EventHandler<EventArgs>? SelectedChanged;

    public Visual? Hovered { get; set; }

    public Visual? Selected { get; set; }

    public HitTestMode HitTestMode  { get; set; } = HitTestMode.Logical;

    protected virtual void OnHoveredChanged(EventArgs e)
    {
        HoveredChanged?.Invoke(this, e);
    }

    protected virtual void OnSelectedChanged(EventArgs e)
    {
        SelectedChanged?.Invoke(this, e);
    }

    public void Hover(Visual? visual)
    {
        Hovered = visual;
        InvalidateVisual();
        OnHoveredChanged(EventArgs.Empty);
    }

    public void Select(Visual? visual)
    {
        Selected = visual;
        InvalidateVisual();
        OnSelectedChanged(EventArgs.Empty);
    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);

        if (Selected is not null)
        {
            RenderVisual(Selected, context, new ImmutablePen(Colors.Blue.ToUInt32()));

            if (Hovered is null)
            {
                DrawName(context, Selected.GetType().Name);
            }
        }

        if (Hovered is not null)
        {
            RenderVisual(Hovered, context, new ImmutablePen(Colors.Red.ToUInt32()));
            DrawName(context, Hovered.GetType().Name);
        }

        DrawHelp(context);
    }

    private void DrawHelp(DrawingContext context)
    {
        var helpText = $"[V] [L] Mode: {HitTestMode}, [H] Toggle HitTest, [R] Toggle Reverse Order";

        var formattedTextMode = new FormattedText(
            helpText, 
            CultureInfo.CurrentCulture, 
            FlowDirection.LeftToRight, 
            Typeface.Default, 
            12, 
            Brushes.Blue);
        context.DrawText(formattedTextMode, new Point(5, Bounds.Height - 12 - 5));
    }

    private static void DrawName(DrawingContext context, string name)
    {
        var formattedText = new FormattedText(
            name, 
            CultureInfo.CurrentCulture, 
            FlowDirection.LeftToRight, 
            Typeface.Default, 
            12, 
            Brushes.Red);
        context.DrawText(formattedText, new Point(5, 5));
    }

    private static void RenderVisual(Visual visual, DrawingContext context, IPen? pen)
    {
        var transformedBounds = visual.GetTransformedBounds();
        if (transformedBounds is not null)
        {
            using var _ = context.PushTransform(transformedBounds.Value.Transform);
            context.DrawRectangle(null, pen, transformedBounds.Value.Bounds);
        }
    }
}
