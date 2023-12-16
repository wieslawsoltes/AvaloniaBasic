using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
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

    private void HitTest(Interactive interactive, Point point)
    {
        var root = interactive.GetVisualRoot();
        if (root is null)
        {
            return;
        }

        var descendants = interactive.GetVisualDescendants();
        //var descendants = interactive.GetLogicalDescendants();

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
            Console.WriteLine($"[{point}] {visual} {transformedBounds}");
            OverlayControl.Result = visual;
            OverlayControl.InvalidateVisual();
            //AdornerLayer.GetAdornerLayer(this).InvalidateVisual();
            //var adornerLayer = this.FindDescendantOfType<VisualLayerManager>(true).AdornerLayer;
            //adornerLayer.InvalidateVisual();
        }
        else
        {
            OverlayControl.Result = null;
            OverlayControl.InvalidateVisual();
            //AdornerLayer.GetAdornerLayer(this).InvalidateVisual();
            //var adornerLayer = this.FindDescendantOfType<VisualLayerManager>(true).AdornerLayer;
            //adornerLayer.InvalidateVisual();
        }

        // Console.WriteLine($"[{point}]");
        // foreach (var visual in visuals)
        // {
        //     Console.WriteLine($"  {visual}");
        // }
    }
}

public class Overlay : Control
{
    public Visual? Result;

    public Overlay()
    {
        
    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);

        if (Result is not null)
        {
            var transformedBounds = Result.GetTransformedBounds();
            if (transformedBounds is not null)
            {
                // Console.WriteLine($"{transformedBounds}");
                var pen = new ImmutablePen(Colors.Red.ToUInt32(), 1);
                using var _ = context.PushTransform(transformedBounds.Value.Transform);
                context.DrawRectangle(null, pen, transformedBounds.Value.Bounds);
            }
        }
    }
}
