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
                ["Stroke"] = (XamlValue) "Blue",
                ["StrokeThickness"] = (XamlValue) "2.0",
            },
            contentProperty: null,
            childrenProperty: null);

        return xamlItem;
    }

    protected override void UpdateXamlItem(Point startPosition, Point endPosition, XamlItem xamlItem)
    {
        xamlItem.Properties["StartPoint"] = StringXamlValue.From(startPosition);
        xamlItem.Properties["EndPoint"] = StringXamlValue.From(endPosition);
    }

    protected override void UpdateControl(Point startPosition, Point endPosition, Control control)
    {
        if (control is Line line)
        {
            line.StartPoint = startPosition;
            line.EndPoint = endPosition;
        }
    }
}
