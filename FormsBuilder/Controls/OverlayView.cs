using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.VisualTree;

namespace FormsBuilder;

public abstract class Renderer
{
    public abstract void Render(DrawingContext context);
}

public class SelectionRenderer : Renderer
{
    private readonly IXamlSelection _xamlSelection;

    public SelectionRenderer(IXamlSelection xamlSelection)
    {
        _xamlSelection = xamlSelection;
    }

    public override void Render(DrawingContext context)
    {
        var selected = _xamlSelection.Selected;
        var hovered = _xamlSelection.Hovered;

        if (selected.Count > 0)
        {
            foreach (var visual in selected)
            {
                RenderVisual(visual, context, new ImmutablePen(Colors.CornflowerBlue.ToUInt32()));
                RenderVisualThumbs(visual, context, new ImmutableSolidColorBrush(Colors.White), new ImmutablePen(Colors.CornflowerBlue.ToUInt32()));
            }

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

        if (_xamlSelection.DrawSelection)
        {
            var rect = RectHelper.GetSelectionRect(
                _xamlSelection.StartPoint,
                _xamlSelection.EndPoint);

            RenderSelection(
                rect, 
                context, 
                new ImmutableSolidColorBrush(Colors.CornflowerBlue, 0.3), 
                new ImmutablePen(Colors.CornflowerBlue.ToUInt32()));
        }

        // DrawHelp(context);
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
  
    private static void RenderSelection(Rect rect, DrawingContext context, IBrush? brush, IPen? pen)
    {
        context.DrawRectangle(brush, pen, rect);
    }

    /*
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
    */
}

public class OverlayView : Decorator
{
    private SelectionRenderer? _selectionRenderer;
    
    public OverlayView()
    {
        Child = new Canvas();
    }

    private MainViewViewModel? _mainViewModel;
    
    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);
        
        if (DataContext is MainViewViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            _mainViewModel.OverlayView = this;
            _selectionRenderer = new SelectionRenderer(_mainViewModel.XamlSelection);
        }
        else
        {
            if (_mainViewModel is not null)
            {
                _mainViewModel.OverlayView = null;
                _mainViewModel = null;
            }

            _selectionRenderer = null;
        }
    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);

        _selectionRenderer?.Render(context);
    }
}
