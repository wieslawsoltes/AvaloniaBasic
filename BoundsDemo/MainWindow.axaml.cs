using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.VisualTree;

namespace BoundsDemo;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        Focus();
    }

    protected override void OnPointerMoved(PointerEventArgs e)
    {
        base.OnPointerMoved(e);

        HitTest(this, e.GetPosition(this));
    }

    protected override void OnPointerExited(PointerEventArgs e)
    {
        base.OnPointerExited(e);
        
        OverlayControl.Result = null;
        OverlayControl.InvalidateVisual();
    }

    protected override void OnPointerCaptureLost(PointerCaptureLostEventArgs e)
    {
        base.OnPointerCaptureLost(e);
        
        OverlayControl.Result = null;
        OverlayControl.InvalidateVisual();
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);

        if (e.Key == Key.V)
        {
            OverlayControl.HitTestMode = HitTestMode.Visual;
            OverlayControl.Result = null;
            OverlayControl.InvalidateVisual();
        }
        if (e.Key == Key.L)
        {
            OverlayControl.HitTestMode = HitTestMode.Logical;
            OverlayControl.Result = null;
            OverlayControl.InvalidateVisual();
        }
    }

    private void HitTest(Interactive interactive, Point point)
    {
        var root = interactive.GetVisualRoot();
        if (root is null)
        {
            return;
        }

        var descendants = new List<Visual>();

        if (OverlayControl.HitTestMode == HitTestMode.Visual)
        {
            descendants.AddRange(interactive.GetVisualDescendants());
        }

        if (OverlayControl.HitTestMode == HitTestMode.Logical)
        {
            descendants.AddRange(interactive.GetLogicalDescendants().Cast<Visual>());
        }

        var visuals = descendants
            .Where(x =>
            {
                if (x is Visual v && !Equals(x, OverlayControl))
                {
                    var transformedBounds = v.GetTransformedBounds();
                    return transformedBounds is not null
                           && transformedBounds.Value.Contains(point);
                }

                return false;
            })
            .Reverse();

        var result = visuals.FirstOrDefault();
        if (result is Visual visual)
        {
            var transformedBounds = visual.GetTransformedBounds();
            OverlayControl.Result = visual;
            OverlayControl.InvalidateVisual();
            // AdornerLayer.GetAdornerLayer(this).InvalidateVisual();
            // var adornerLayer = this.FindDescendantOfType<VisualLayerManager>(true).AdornerLayer;
            // adornerLayer.InvalidateVisual();
            // Console.WriteLine($"[{point}] {visual} {transformedBounds}");
        }
        else
        {
            OverlayControl.Result = null;
            OverlayControl.InvalidateVisual();
            // AdornerLayer.GetAdornerLayer(this).InvalidateVisual();
            // var adornerLayer = this.FindDescendantOfType<VisualLayerManager>(true).AdornerLayer;
            // adornerLayer.InvalidateVisual();
        }

        // Console.WriteLine($"[{point}]");
        // foreach (var visual in visuals)
        // {
        //     Console.WriteLine($"  {visual}");
        // }
    }
}

public enum HitTestMode
{
    Logical,
    Visual,
}

public class Overlay : Control
{
    public Visual? Result;

    public HitTestMode HitTestMode = HitTestMode.Logical;

    public override void Render(DrawingContext context)
    {
        base.Render(context);

        if (Result is not null)
        {
            var transformedBounds = Result.GetTransformedBounds();
            if (transformedBounds is not null)
            {
                var pen = new ImmutablePen(Colors.Red.ToUInt32(), 1);
                using var _ = context.PushTransform(transformedBounds.Value.Transform);
                context.DrawRectangle(null, pen, transformedBounds.Value.Bounds);
            }
            
            var formattedText = new FormattedText(Result.GetType().Name, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, Typeface.Default, 12, Brushes.Red);
            context.DrawText(formattedText, new Point(5, 5));
        }
    
        var formattedTextMode = new FormattedText($"[V] [L] Mode: {HitTestMode}", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, Typeface.Default, 12, Brushes.Blue);
        context.DrawText(formattedTextMode, new Point(5, Bounds.Height - 20));
    }
}
