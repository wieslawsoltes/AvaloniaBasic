using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace ResizingControlDemo.Controls;

public class GridControl : Control
{
    private const double GridSize = 8;

    public override void Render(DrawingContext context)
    {
        base.Render(context);
        
        for (var x = 0.0; x <= Bounds.Width; x += GridSize)
        {
            for (var y = 0.0; y <= Bounds.Height; y += GridSize)
            {
                context.DrawEllipse(Brushes.Gray, null, new Point(x, y), 1, 1);
            }
        }
    }
}
