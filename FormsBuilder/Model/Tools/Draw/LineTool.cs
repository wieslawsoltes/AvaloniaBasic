using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;

namespace FormsBuilder;

public class LineTool : DrawTool
{
    public LineTool(IToolContext context) : base(context)
    {
    }

    protected override XamlItem CreateXamlItem(IToolContext context)
    {
        var xamlItem = context.XamlFactory.CreateControl(
            name: "Line",
            properties: new XamlProperties
            {
                ["Stroke"] = "#000000",
                ["StrokeThickness"] = "1.0",
            },
            contentProperty: null, 
            childrenProperty: null);

        return xamlItem;
    }

    protected override void UpdateXamlItem(Point startPosition, Point endPosition, XamlItem xamlItem)
    {
        var startPoint = startPosition;
        var endPoint = endPosition - startPosition;
        xamlItem.Properties["Canvas.Left"] = startPoint.X;
        xamlItem.Properties["Canvas.Top"] = startPoint.Y;
        xamlItem.Properties["StartPoint"] = new Point();
        xamlItem.Properties["EndPoint"] = endPoint;
    }

    protected override void UpdateControl(Point startPosition, Point endPosition, Control control)
    {
        if (control is Line line)
        {
            var startPoint = startPosition;
            var endPoint = endPosition - startPosition;
            Canvas.SetLeft(control, startPoint.X);
            Canvas.SetTop(control, startPoint.Y);
            line.StartPoint = new Point();
            line.EndPoint = endPoint;
        }
    }
}
