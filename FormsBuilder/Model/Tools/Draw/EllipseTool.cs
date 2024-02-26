using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;

namespace FormsBuilder;

public class EllipseTool : DrawTool
{
    public EllipseTool(IToolContext context) : base(context)
    {
    }

    protected override XamlItem CreateXamlItem(IToolContext context)
    {
        var xamlItem = new XamlItem(name: "Ellipse",
            id: context.XamlEditor.IdManager.GetNewId(),
            properties: new()
            {
                ["Fill"] = "#D9D9D9",
            },
            contentProperty: null,
            childrenProperty: null);

        return xamlItem;
    }

    protected override void UpdateXamlItem(Point startPosition, Point endPosition, XamlItem xamlItem)
    {
        var rect = RectHelper.GetSelectionRect(startPosition, endPosition);
        xamlItem.Properties["Canvas.Left"] = rect.TopLeft.X;
        xamlItem.Properties["Canvas.Top"] = rect.TopLeft.Y;
        xamlItem.Properties["Width"] = rect.Width;
        xamlItem.Properties["Height"] = rect.Height;
    }

    protected override void UpdateControl(Point startPosition, Point endPosition, Control control)
    {
        var rect = RectHelper.GetSelectionRect(startPosition, endPosition);
        Canvas.SetLeft(control, rect.TopLeft.X);
        Canvas.SetTop(control, rect.TopLeft.Y);
        control.Width = rect.Width;
        control.Height = rect.Height;
    }
}
