using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace AvaloniaBasicDemo.Controls;

public sealed class GridLines : Control
{
    private readonly Pen _pen;
    private readonly Pen _penBold;

    public GridLines()
    {
        _pen = new Pen(new SolidColorBrush(Color.FromArgb((byte)(255.0 * 0.1), 14, 94, 253))); 
        _penBold = new Pen(new SolidColorBrush(Color.FromArgb((byte)(255.0 * 0.3), 14, 94, 253)));
    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);

        for(var i = 1; i < Bounds.Height / 10; i++)
        {
            context.DrawLine(
                i % 10 == 0 ? _penBold : _pen, 
                new Point(0 + 0.5, i * 10 + 0.5), 
                new Point(Bounds.Width + 0.5, i * 10 + 0.5));
        }

        for (var i = 1; i < Bounds.Width / 10; i++)
        {
            context.DrawLine(
                i % 10 == 0 ? _penBold : _pen, 
                new Point(i * 10 + 0.5, 0 + 0.5), 
                new Point(i * 10 + 0.5, Bounds.Height + 0.5));
        }
    }
}
