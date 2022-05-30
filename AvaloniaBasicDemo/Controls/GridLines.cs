using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace AvaloniaBasicDemo.Controls;

public sealed class GridLines : Control
{
    private readonly Pen _pen;
    private readonly Pen _penBold;
    private readonly int _cellWidth;
    private readonly int _cellHeight;
    private readonly int _boldSeparatorHorizontalSpacing;
    private readonly int _boldSeparatorVerticalSpacing;

    public GridLines()
    {
        _pen = new Pen(new SolidColorBrush(Color.FromArgb((byte)(255.0 * 0.1), 14, 94, 253))); 
        _penBold = new Pen(new SolidColorBrush(Color.FromArgb((byte)(255.0 * 0.3), 14, 94, 253)));
        _cellWidth = 10;
        _cellHeight = 10;
        _boldSeparatorHorizontalSpacing = 10;
        _boldSeparatorVerticalSpacing = 10;
    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);

        var width = Bounds.Width;
        var height = Bounds.Height;

        for(var i = 1; i < height / _cellHeight; i++)
        {
            var pen = i % _boldSeparatorVerticalSpacing == 0 ? _penBold : _pen;
            context.DrawLine(
                pen, 
                new Point(0 + 0.5, i * _cellHeight + 0.5), 
                new Point(width + 0.5, i * _cellHeight + 0.5));
        }

        for (var i = 1; i < width / _cellWidth; i++)
        {
            var pen = i % _boldSeparatorHorizontalSpacing == 0 ? _penBold : _pen;
            context.DrawLine(
                pen, 
                new Point(i * _cellWidth + 0.5, 0 + 0.5), 
                new Point(i * _cellWidth + 0.5, height + 0.5));
        }
    }
}
