using System.Diagnostics;
using System.Linq;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Rendering.Composition;
using Avalonia.VisualTree;
using AvaloniaBasic.Behaviors;

namespace AvaloniaBasic.Editors;

internal static class ControlEditor
{
    public static Control? FindDragArea(Interactive? canvas, Point point)
    {
        if (canvas is null)
        {
            return null;
        }

        var root = canvas.GetVisualRoot();
        if (root is null)
        {
            return null;
        }

        
        /*
        var compositionVisual = ElementComposition.GetElementVisual(canvas);
        if (compositionVisual is null)
        {
            return null;
        }
        compositionVisual.TransformMatrix.
        */
        
        
        // var visuals = canvas
        //     .GetVisualDescendants()
        //     .Where(x => x.TransformedBounds is not null && x.TransformedBounds.Value.Contains(point))
        //     .Where(x => x.Bounds.Contains(point))
        //     .Reverse();
        var visuals = root.Renderer.HitTest(point, canvas, x => true);

        var dragAreas = visuals.OfType<Control>().Where(DragSettings.GetIsDragArea).ToList();

        Debug.WriteLine($"dragAreas:"); foreach (var x in dragAreas) Debug.WriteLine($"  {x}");

        var dragArea = dragAreas.FirstOrDefault();
        return dragArea;
    }

    public static Control? FindDropArea(Interactive? canvas, Point point)
    {
        if (canvas is null)
        {
            return null;
        }

        var root = canvas.GetVisualRoot();
        if (root is null)
        {
            return null;
        }

        // var visuals = (root as TopLevel)
        //     .GetVisualDescendants()
        //     .Where(x => x.TransformedBounds is not null && x.TransformedBounds.Value.Contains(point))
        //     .Reverse();
        var visuals = root.Renderer.HitTest(point, root, x => true);

        var dropAreas = visuals.OfType<Control>().Where(DragSettings.GetIsDropArea).ToList();

        // Debug.WriteLine($"dropAreas:"); foreach (var x in dropAreas) Debug.WriteLine($"  {x}");

        var dropArea = dropAreas.FirstOrDefault(DragSettings.GetIsDropArea);
        return dropArea;
    }

    public static void AddControl(Control control, Control target, Point point)
    {
        DragSettings.SetIsDragArea(control, true);

        // TODO: Properly position in panel.

        if (target is Canvas)
        {
            Canvas.SetLeft(control, point.X);
            Canvas.SetTop(control, point.Y);
        }

        if (target is IPanel and not Canvas)
        {
            // TODO: control.Margin = new Thickness(point.X, point.Y, 0d, 0d);
        }

        // TODO: Use ContentAttribute to set content if applicable.

        if (target is IPanel panel)
        {
            panel.Children.Add(control);
        }
        else if (target is IContentControl contentControl)
        {
            contentControl.Content = control;
        }
        else if (target is Decorator decorator)
        {
            decorator.Child = control;
        }
        else if (target is ItemsControl itemsControl)
        {
            if (itemsControl.Items is AvaloniaList<object> items)
            {
                items.Add(control);
            }
        }
    }
}
