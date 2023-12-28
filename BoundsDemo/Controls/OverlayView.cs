using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.VisualTree;

namespace BoundsDemo;

public class OverlayView : Decorator
{
    public OverlayView()
    {
        Child = new Canvas();
    }

    public HitTestMode HitTestMode  { get; set; } = HitTestMode.Logical;

    public void Hover(Visual? visual)
    {
        if (DataContext is MainViewViewModel mainViewModel)
        {
            mainViewModel.Hover(visual);
            InvalidateVisual();
        }
    }

    public void Select(Visual? visual)
    {
        if (DataContext is MainViewViewModel mainViewModel)
        {
            mainViewModel.Select(visual);
            InvalidateVisual();
        }
    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);

        if (DataContext is not MainViewViewModel mainViewModel)
        {
            return;
        }

        var selected = mainViewModel.Selected;
        var hovered = mainViewModel.Hovered;

        if (selected is not null)
        {
            RenderVisual(selected, context, new ImmutablePen(Colors.CornflowerBlue.ToUInt32()));
            RenderVisualThumbs(selected, context, new ImmutableSolidColorBrush(Colors.White), new ImmutablePen(Colors.CornflowerBlue.ToUInt32()));

            if (hovered is null)
            {
                // DrawName(context, selected.GetType().Name);
            }
        }

        if (hovered is not null)
        {
            RenderVisual(hovered, context, new ImmutablePen(Colors.CornflowerBlue.ToUInt32()));
            // DrawName(context, hovered.GetType().Name);
        }

        // DrawHelp(context);
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
            Brushes.CornflowerBlue);

        context.DrawText(formattedTextMode, new Point(10, Bounds.Height - 12 - 5));
    }

    private void DrawName(DrawingContext context, string name)
    {
        var formattedText = new FormattedText(
            name, 
            CultureInfo.CurrentCulture, 
            FlowDirection.LeftToRight, 
            Typeface.Default, 
            12, 
            Brushes.CornflowerBlue);

        context.DrawText(formattedText, new Point(Bounds.Width - formattedText.Width - 10, Bounds.Height - 12 - 5));
    }

    private static void RenderVisual(Visual visual, DrawingContext context, IPen? pen)
    {
        var transformedBounds = visual.GetTransformedBounds();
        if (transformedBounds is null)
        {
            return;
        }

        using var _ = context.PushTransform(transformedBounds.Value.Transform);
        context.DrawRectangle(null, pen, transformedBounds.Value.Bounds);
    }

    private static void RenderVisualThumbs(Visual visual, DrawingContext context, IBrush? brush, IPen? pen)
    {
        var transformedBounds = visual.GetTransformedBounds();
        if (transformedBounds is null)
        {
            return;
        }

        using var _ = context.PushTransform(transformedBounds.Value.Transform);
        var bounds = transformedBounds.Value.Bounds;
        var selection = new VisualSelection(bounds, 3);
        context.DrawRectangle(brush, pen, selection.TopLeft);
        context.DrawRectangle(brush, pen, selection.TopRight);
        context.DrawRectangle(brush, pen, selection.BottomLeft);
        context.DrawRectangle(brush, pen, selection.BottomRight);
        context.DrawRectangle(brush, pen, selection.Left);
        context.DrawRectangle(brush, pen, selection.Right);
        context.DrawRectangle(brush, pen, selection.Top);
        context.DrawRectangle(brush, pen, selection.Bottom);
    }
}
