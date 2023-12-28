using Avalonia;

namespace BoundsDemo;

public class VisualSelection
{
    public VisualSelection(Rect bounds, double thickness)
    {
        TopLeft = CreateCornerRect(bounds.TopLeft, thickness);
        TopRight = CreateCornerRect(bounds.TopRight, thickness);
        BottomLeft = CreateCornerRect(bounds.BottomLeft, thickness);
        BottomRight = CreateCornerRect(bounds.BottomRight, thickness);
        Left = CreateEdgeRect(bounds.Left, bounds.Center.Y, thickness);
        Right = CreateEdgeRect(bounds.Right, bounds.Center.Y, thickness);
        Top = CreateEdgeRect(bounds.Center.X, bounds.Top, thickness);
        Bottom = CreateEdgeRect(bounds.Center.X, bounds.Bottom, thickness);
    }

    public Rect TopLeft { get; }
    public Rect TopRight { get; }
    public Rect BottomLeft { get; }
    public Rect BottomRight { get; }
    public Rect Left { get; }
    public Rect Right { get; }
    public Rect Top { get; }
    public Rect Bottom { get; }

    private Rect CreateCornerRect(Point cornerPoint, double thickness)
    {
        return new Rect(cornerPoint, cornerPoint).Inflate(thickness);
    }

    private Rect CreateEdgeRect(double x, double y, double thickness)
    {
        return new Rect(new Point(x, y), new Point(x, y)).Inflate(thickness);
    }
}
