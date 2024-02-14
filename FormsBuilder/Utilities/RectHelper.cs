using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.VisualTree;

namespace FormsBuilder;

public static class RectHelper
{
    public static Rect GetSelectionRect(Point startPoint, Point endPoint)
    {
        var topLeft = new Point(
            Math.Min(startPoint.X, endPoint.X),
            Math.Min(startPoint.Y, endPoint.Y));
        var bottomRight = new Point(
            Math.Max(startPoint.X, endPoint.X),
            Math.Max(startPoint.Y, endPoint.Y));
        return new Rect(topLeft, bottomRight);
    }

    public static Rect GetSelectionRectUnion(IEnumerable<Visual> visuals)
    {
        var selectionRect = new Rect();

        foreach (var visual in visuals)
        {
            var transformedBounds = visual.GetTransformedBounds();
            if (transformedBounds is not null)
            {
                var transformedRect = transformedBounds.Value.Bounds.TransformToAABB(transformedBounds.Value.Transform);
                selectionRect = selectionRect.Union(transformedRect);
            }
        }

        return selectionRect;
    }
}
