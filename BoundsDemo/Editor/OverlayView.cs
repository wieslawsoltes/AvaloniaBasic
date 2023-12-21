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

public class VisualSelection
{
    public VisualSelection(Rect bounds, double thickness)
    {
        TopLeft = new Rect(
            bounds.TopLeft, 
            bounds.TopLeft).Inflate(thickness);
        TopRight = new Rect(
            bounds.TopRight,
            bounds.TopRight).Inflate(thickness);
        BottomLeft = new Rect(
            bounds.BottomLeft, 
            bounds.BottomLeft).Inflate(thickness);
        BottomRight = new Rect(
            bounds.BottomRight,
            bounds.BottomRight).Inflate(thickness);
        Left = new Rect(
            new Point(bounds.Left, bounds.Center.Y), 
            new Point(bounds.Left, bounds.Center.Y)).Inflate(thickness);
       Right = new Rect(
            new Point(bounds.Right, bounds.Center.Y), 
            new Point(bounds.Right, bounds.Center.Y)).Inflate(thickness);
       Top = new Rect(
            new Point(bounds.Center.X, bounds.Top), 
            new Point(bounds.Center.X, bounds.Top)).Inflate(thickness);
        Bottom = new Rect(
            new Point(bounds.Center.X, bounds.Bottom), 
            new Point(bounds.Center.X, bounds.Bottom)).Inflate(thickness);
    }

    public Rect TopLeft { get; set; }

    public Rect TopRight { get; set; }

    public Rect BottomLeft { get; set; }

    public Rect BottomRight { get; set; }

    public Rect Left { get; set; }

    public Rect Right { get; set; }

    public Rect Top { get; set; }

    public Rect Bottom { get; set; }
}

public class OverlayView : Control
{
    public static readonly AttachedProperty<bool> EnableHitTestProperty =
        AvaloniaProperty.RegisterAttached<OverlayView, Visual, bool>("EnableHitTest", false, true);

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
        if (visual is null || visual != Selected)
        {
            Hovered = visual;
            InvalidateVisual();
            OnHoveredChanged(EventArgs.Empty);
        }
    }

    public void Select(Visual? visual)
    {
        Hovered = null;
        OnHoveredChanged(EventArgs.Empty);
        Selected = visual;
        InvalidateVisual();
        OnSelectedChanged(EventArgs.Empty);
    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);

        if (Selected is not null)
        {
            RenderVisual(Selected, context, new ImmutablePen(Colors.CornflowerBlue.ToUInt32()));
            RenderVisualThumbs(Selected, context, new ImmutableSolidColorBrush(Colors.White), new ImmutablePen(Colors.CornflowerBlue.ToUInt32()));

            if (Hovered is null)
            {
                DrawName(context, Selected.GetType().Name);
            }
        }

        if (Hovered is not null)
        {
            RenderVisual(Hovered, context, new ImmutablePen(Colors.CornflowerBlue.ToUInt32()));
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
            Brushes.CornflowerBlue);
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
            Brushes.CornflowerBlue);
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

    private static void RenderVisualThumbs(Visual visual, DrawingContext context, IBrush? brush, IPen? pen)
    {
        var transformedBounds = visual.GetTransformedBounds();
        if (transformedBounds is not null)
        {
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
}
