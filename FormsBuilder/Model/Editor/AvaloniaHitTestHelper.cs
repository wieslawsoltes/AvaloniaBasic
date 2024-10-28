using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.LogicalTree;
using Avalonia.VisualTree;

namespace FormsBuilder;

public class AvaloniaHitTestHelper
{
    public static Control? HitTest(IEnumerable<Visual> descendants, Point position, HashSet<Visual> ignored, Predicate<Control> predicate)
    {
        var visuals = descendants
            .OfType<Control>()
            .Where(visual =>
            {
                if (!predicate(visual))
                {
                    return false;
                }

                if (ignored.Contains(visual))
                {
                    return false;
                }

                var transformedBounds = visual.GetTransformedBounds();
                return transformedBounds is not null
                       && transformedBounds.Value.Contains(position);

            });

        return visuals.Reverse().FirstOrDefault();
    }

    public static Control? HitTest(ILogical root, Point position, HashSet<Visual> ignored, Predicate<Control> predicate)
    {
        var descendants = root.GetLogicalDescendants().Cast<Visual>();

        return HitTest(descendants, position, ignored, predicate);
    }
}
