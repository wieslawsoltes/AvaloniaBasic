using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using XamlDom;

namespace FormsBuilder;

public class RectangleTool : DrawTool
{
    public RectangleTool(IToolContext context) : base(context)
    {
    }

    protected override XamlItem CreateXamlItem(IToolContext context)
    {
#if false
        var xamlItem = context.XamlFactory.CreateControl(
            name: "DockPanel",
            properties: new XamlProperties
            {
                ["Children"] = new XamlItems
                {
                    context.XamlFactory.CreateControl(
                        name: "TextBlock", 
                        properties: new XamlProperties
                        {
                            ["Text"] = "TextBlock",
                            ["DockPanel.Dock"] = "Top"
                        }, 
                        contentProperty: "Text", 
                        childrenProperty: null),
                    context.XamlFactory.CreateControl(
                        name: "TextBox", 
                        properties: new XamlProperties
                        {
                            ["Text"] = "TextBox",
                            ["VerticalAlignment"] = "Stretch",
                        },
                        contentProperty: "Text", 
                        childrenProperty: null),
                },
                ["Background"] = "LightGray",
            }, 
            contentProperty: "Children", 
            childrenProperty: "Children");
#else
        var xamlItem = context.XamlItemFactory.CreateControl(
            name: "Rectangle",
            properties: new XamlProperties
            {
                ["Fill"] = "#D9D9D9",
            },
            contentProperty: null, 
            childrenProperty: null);
#endif
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
