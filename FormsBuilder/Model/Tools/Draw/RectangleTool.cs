using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;

namespace FormsBuilder;

public class RectangleTool : DrawTool
{
    public RectangleTool(IToolContext context) : base(context)
    {
    }

    protected override XamlItem CreateXamlItem(IToolContext context)
    {
#if false
        var xamlItem = new XamlItem(name: "DockPanel",
            id: context.XamlEditor.IdManager.GetNewId(),
            properties: new()
            {
                ["Children"] = (XamlValue) new List<XamlItem>
                {
                    new(name: "TextBlock", 
                        id: context.XamlEditor.IdManager.GetNewId(),
                        properties: new()
                        {
                            ["Text"] = (XamlValue) "TextBlock",
                            ["DockPanel.Dock"] = (XamlValue) "Top"
                        }, 
                        contentProperty: "Text", 
                        childrenProperty: null),
                    new(name: "TextBox", 
                        id: context.XamlEditor.IdManager.GetNewId(),
                        properties: new()
                        {
                            ["Text"] = (XamlValue) "TextBox",
                            ["VerticalAlignment"] = (XamlValue) "Stretch",
                        },
                        contentProperty: "Text", 
                        childrenProperty: null),
                },
                ["Background"] = (XamlValue) "LightGray",
            }, 
            contentProperty: "Children", 
            childrenProperty: "Children");
#else
        var xamlItem = new XamlItem(name: "Rectangle",
            id: context.XamlEditor.IdManager.GetNewId(),
            properties: new()
            {
                ["Fill"] = (XamlValue) "#D9D9D9",
            },
            contentProperty: null,
            childrenProperty: null);
#endif
        return xamlItem;
    }

    protected override void UpdateXamlItem(Point startPosition, Point endPosition, XamlItem xamlItem)
    {
        var rect = RectHelper.GetSelectionRect(startPosition, endPosition);
        xamlItem.Properties["Canvas.Left"] = StringXamlValue.From(rect.TopLeft.X);
        xamlItem.Properties["Canvas.Top"] = StringXamlValue.From(rect.TopLeft.Y);
        xamlItem.Properties["Width"] = StringXamlValue.From(rect.Width);
        xamlItem.Properties["Height"] = StringXamlValue.From(rect.Height);
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
