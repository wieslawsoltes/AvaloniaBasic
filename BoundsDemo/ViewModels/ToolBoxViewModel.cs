using System.Collections.Generic;

namespace BoundsDemo;

public class ToolBoxViewModel
{
    public ToolBoxViewModel()
    {
        ToolBoxItems = new List<XamlItem>
        {
            //
            //
            //
            new(name: "TextBlock", 
                properties: new Dictionary<string, object>
                {
                    ["Text"] = "TextBlock"
                }, 
                contentProperty: "Text", 
                childrenProperty: null),
            new(name: "Label", 
                properties: new Dictionary<string, object>
                {
                    ["Content"] = "Label"
                }, 
                contentProperty: "Content", 
                childrenProperty: null),
            new(name: "TextBox", 
                properties: new Dictionary<string, object>
                {
                    ["Text"] = "TextBox"
                }, 
                contentProperty: "Text", 
                childrenProperty: null),
            //
            //
            //
            new(name: "Button", 
                properties: new Dictionary<string, object>
                {
                    ["Content"] = "Button"
                }, 
                contentProperty: "Content", 
                childrenProperty: null),
            new(name: "CheckBox", 
                properties: new Dictionary<string, object>
                {
                    ["Content"] = "CheckBox"
                }, 
                contentProperty: "Content", 
                childrenProperty: null),
            new(name: "RadioButton", 
                properties: new Dictionary<string, object>
                {
                    ["Content"] = "RadioButton"
                }, 
                contentProperty: "Content", 
                childrenProperty: null),
            //
            //
            //
            new(name: "Border",
                properties: new Dictionary<string, object>(),
                contentProperty: "Child",
                childrenProperty: "Child"
            ),
            new(name: "Decorator",
                properties: new Dictionary<string, object>
                {
                    ["Child"] = new XamlItem(
                        name: "Button", 
                        properties: new Dictionary<string, object>
                        {
                            ["Content"] = "Button"
                        }, 
                        contentProperty: "Content", 
                        childrenProperty: null)
                },
                contentProperty: "Child",
                childrenProperty: "Child"),
            //
            //
            //
            new(name: "Panel", 
                properties: new Dictionary<string, object>(), 
                contentProperty: "Children", 
                childrenProperty: "Children"),
            new(name: "StackPanel",
                properties: new Dictionary<string, object>
                {
                    ["Children"] = new List<XamlItem>
                    {
                        new(name: "TextBlock", 
                            properties: new Dictionary<string, object>
                            {
                                ["Text"] = "TextBlock"
                            }, 
                            contentProperty: "Text", 
                            childrenProperty: null),
                        new(name: "TextBox", 
                            properties: new Dictionary<string, object>
                            {
                                ["Text"] = "TextBox"
                            },
                            contentProperty: "Text", 
                            childrenProperty: null),
                    }
                }, 
                contentProperty: "Children", 
                childrenProperty: "Children"),
            new(name: "DockPanel", 
                properties: new Dictionary<string, object>(), 
                contentProperty: "Children", 
                childrenProperty: "Children"),
            new(name: "WrapPanel", 
                properties: new Dictionary<string, object>(), 
                contentProperty: "Children", 
                childrenProperty: "Children"),
            new(name: "Grid", 
                properties: new Dictionary<string, object>(), 
                contentProperty: "Children", 
                childrenProperty: "Children"),
            //
            //
            //
            new(name: "ItemsControl", 
                properties: new Dictionary<string, object>(), 
                contentProperty: "Items", 
                childrenProperty: "Items"),
            new(name: "ListBox",
                properties: new Dictionary<string, object>
                {
                    ["Items"] = new List<XamlItem>
                    {
                        new(name: "ListBoxItem", 
                            properties: new Dictionary<string, object>
                            {
                                ["Content"] = "ListBoxItem 0"
                            }),
                        new(name: "ListBoxItem", 
                            properties: new Dictionary<string, object>
                            {
                                ["Content"] = "ListBoxItem 1"
                            }),
                    }
                }, 
                contentProperty: "Items", 
                childrenProperty: "Items"),
            new(name: "ListBoxItem", 
                properties: new Dictionary<string, object>(), 
                contentProperty: "Content", 
                childrenProperty: null),
            new(name: "ComboBox", 
                properties: new Dictionary<string, object>(), 
                contentProperty: "ItemsSource", 
                childrenProperty: "Items"),
            new(name: "ComboBoxItem", 
                properties: new Dictionary<string, object>(), 
                contentProperty: "Content", 
                childrenProperty: null),
            //
            //
            //
            new(name: "ProgressBar", 
                properties: new Dictionary<string, object>(), 
                contentProperty: null, 
                childrenProperty: null),
            new(name: "Slider", 
                properties: new Dictionary<string, object>(), 
                contentProperty: null, 
                childrenProperty: null),
            new(name: "DatePicker", 
                properties: new Dictionary<string, object>(), 
                contentProperty: null, 
                childrenProperty: null),
            //
            //
            //
            new(name: "Rectangle", 
                properties: new Dictionary<string, object>(), 
                contentProperty: null, 
                childrenProperty: null),
            new(name: "Ellipse", 
                properties: new Dictionary<string, object>(), 
                contentProperty: null, 
                childrenProperty: null),
            new(name: "Line", 
                properties: new Dictionary<string, object>(), 
                contentProperty: null, 
                childrenProperty: null),
            new(name: "Path", 
                properties: new Dictionary<string, object>(), 
                contentProperty: null, 
                childrenProperty: null),
            //
            new(name: "Image", 
                properties: new Dictionary<string, object>(), 
                contentProperty: "Source", 
                childrenProperty: null),
            new(name: "PathIcon", 
                properties: new Dictionary<string, object>(), 
                contentProperty: null, 
                childrenProperty: null),
            //
            new(name: "ScrollViewer", 
                properties: new Dictionary<string, object>(), 
                contentProperty: "Content", 
                childrenProperty: "Content"),
        };
    }

    public List<XamlItem> ToolBoxItems { get; set; }
}
