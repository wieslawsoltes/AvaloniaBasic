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

    public Visual? Hover;

    public Visual? Selected;

    public HitTestMode HitTestMode = HitTestMode.Logical;

    public override void Render(DrawingContext context)
    {
        base.Render(context);

        if (Selected is not null)
        {
            RenderVisual(Selected, context,  new ImmutablePen(Colors.Blue.ToUInt32()));
        }

        if (Hover is not null)
        {
            RenderVisual(Hover, context,  new ImmutablePen(Colors.Red.ToUInt32()));

            var formattedText = new FormattedText(Hover.GetType().Name, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, Typeface.Default, 12, Brushes.Red);
            context.DrawText(formattedText, new Point(5, 5));
        }

        var formattedTextMode = new FormattedText($"[V] [L] Mode: {HitTestMode}, [H] Toggle HitTest", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, Typeface.Default, 12, Brushes.Blue);
        context.DrawText(formattedTextMode, new Point(5, Bounds.Height - 20));
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
