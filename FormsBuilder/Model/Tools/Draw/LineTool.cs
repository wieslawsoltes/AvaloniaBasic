using System;
using System.Collections.Generic;
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
        var xamlItem = new XamlItem(name: "Line",
            id: context.XamlEditor.IdManager.GetNewId(),
            properties: new Dictionary<string, XamlValue>
            {
                ["Stroke"] = (XamlValue) "#000000",
                ["StrokeThickness"] = (XamlValue) "1.0",
            },
            contentProperty: null,
            childrenProperty: null);

        return xamlItem;
    }

    protected override void UpdateXamlItem(Point startPosition, Point endPosition, XamlItem xamlItem)
    {
        var startPoint = startPosition;
        var endPoint = endPosition - startPosition;
        xamlItem.Properties["Canvas.Left"] = StringXamlValue.From(startPoint.X);
        xamlItem.Properties["Canvas.Top"] = StringXamlValue.From(startPoint.Y);
        xamlItem.Properties["StartPoint"] = StringXamlValue.From(new Point());
        xamlItem.Properties["EndPoint"] = StringXamlValue.From(endPoint);
        // xamlItem.Properties["StartPoint"] = StringXamlValue.From(startPosition);
        // xamlItem.Properties["EndPoint"] = StringXamlValue.From(endPosition);
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
            // line.StartPoint = startPosition;
            // line.EndPoint = endPosition;
        }
    }
}
