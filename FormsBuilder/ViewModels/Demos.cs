using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Controls;

namespace FormsBuilder;

public class Demos
{
    private readonly IXamlEditor _xamlEditor;

    public Demos(IXamlEditor xamlEditor)
    {
        _xamlEditor = xamlEditor;
    }

    public ObservableCollection<XamlItem> DemoToolBox()
    {
        return new ObservableCollection<XamlItem>
        {
            //
            //
            //
            new(name: "TextBlock", 
                id: _xamlEditor.IdManager.GetNewId(),
                properties: new()
                {
                    ["Text"] = (XamlValue) "TextBlock"
                }, 
                contentProperty: "Text", 
                childrenProperty: null),
            new(name: "Label", 
                id: _xamlEditor.IdManager.GetNewId(),
                properties: new()
                {
                    ["Content"] = (XamlValue) "Label"
                }, 
                contentProperty: "Content", 
                childrenProperty: null),
            new(name: "TextBox", 
                id: _xamlEditor.IdManager.GetNewId(),
                properties: new()
                {
                    ["Text"] = (XamlValue) "TextBox"
                }, 
                contentProperty: "Text", 
                childrenProperty: null),
            //
            //
            //
            new(name: "Button", 
                id: _xamlEditor.IdManager.GetNewId(),
                properties: new()
                {
                    ["Content"] = (XamlValue) "Button"
                }, 
                contentProperty: "Content", 
                childrenProperty: null),
            new(name: "CheckBox", 
                id: _xamlEditor.IdManager.GetNewId(),
                properties: new()
                {
                    ["Content"] = (XamlValue) "CheckBox"
                }, 
                contentProperty: "Content", 
                childrenProperty: null),
            new(name: "RadioButton", 
                id: _xamlEditor.IdManager.GetNewId(),
                properties: new()
                {
                    ["Content"] = (XamlValue) "RadioButton"
                }, 
                contentProperty: "Content", 
                childrenProperty: null),
            //
            //
            //
            new(name: "Border",
                id: _xamlEditor.IdManager.GetNewId(),
                properties: new(),
                contentProperty: "Child",
                childrenProperty: "Child"
            ),
            new(name: "Decorator",
                id: _xamlEditor.IdManager.GetNewId(),
                properties: new()
                {
                    ["Child"] = (XamlValue) new XamlItem(
                        name: "Button", 
                        id: _xamlEditor.IdManager.GetNewId(),
                        properties: new()
                        {
                            ["Content"] = (XamlValue) "Button"
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
                id: _xamlEditor.IdManager.GetNewId(),
                properties: new(), 
                contentProperty: "Children", 
                childrenProperty: "Children"),
            new(name: "StackPanel",
                id: _xamlEditor.IdManager.GetNewId(),
                properties: new()
                {
                    ["Children"] = (XamlValue) new List<XamlItem>
                    {
                        new(name: "TextBlock", 
                            id: _xamlEditor.IdManager.GetNewId(),
                            properties: new()
                            {
                                ["Text"] = (XamlValue) "TextBlock"
                            }, 
                            contentProperty: "Text", 
                            childrenProperty: null),
                        new(name: "TextBox", 
                            id: _xamlEditor.IdManager.GetNewId(),
                            properties: new()
                            {
                                ["Text"] = (XamlValue) "TextBox"
                            },
                            contentProperty: "Text", 
                            childrenProperty: null),
                    }
                }, 
                contentProperty: "Children", 
                childrenProperty: "Children"),
            new(name: "DockPanel", 
                id: _xamlEditor.IdManager.GetNewId(),
                properties: new(), 
                contentProperty: "Children", 
                childrenProperty: "Children"),
            new(name: "WrapPanel", 
                id: _xamlEditor.IdManager.GetNewId(),
                properties: new(), 
                contentProperty: "Children", 
                childrenProperty: "Children"),
            new(name: "Grid", 
                id: _xamlEditor.IdManager.GetNewId(),
                properties: new(), 
                contentProperty: "Children", 
                childrenProperty: "Children"),
            //
            //
            //
            new(name: "ItemsControl", 
                id: _xamlEditor.IdManager.GetNewId(),
                properties: new(), 
                contentProperty: "Items", 
                childrenProperty: "Items"),
            new(name: "ListBox",
                id: _xamlEditor.IdManager.GetNewId(),
                properties: new()
                {
                    ["Items"] = (XamlValue) new List<XamlItem>
                    {
                        new(name: "ListBoxItem", 
                            id: _xamlEditor.IdManager.GetNewId(),
                            properties: new()
                            {
                                ["Content"] = (XamlValue) "ListBoxItem 0"
                            }),
                        new(name: "ListBoxItem",
                            id: _xamlEditor.IdManager.GetNewId(), 
                            properties: new()
                            {
                                ["Content"] = (XamlValue) "ListBoxItem 1"
                            }),
                    }
                }, 
                contentProperty: "Items", 
                childrenProperty: "Items"),
            new(name: "ListBoxItem", 
                id: _xamlEditor.IdManager.GetNewId(),
                properties: new(), 
                contentProperty: "Content", 
                childrenProperty: null),
            //
            //
            //
            new(name: "ComboBox", 
                id: _xamlEditor.IdManager.GetNewId(),
                properties: new(), 
                contentProperty: "ItemsSource", 
                childrenProperty: "Items"),
            new(name: "ComboBoxItem", 
                id: _xamlEditor.IdManager.GetNewId(),
                properties: new(), 
                contentProperty: "Content", 
                childrenProperty: null),
            //
            //
            //
            new(name: "TabControl",
                id: _xamlEditor.IdManager.GetNewId(),
                properties: new()
                {
                    ["Items"] = (XamlValue) new List<XamlItem>
                    {
                        new(name: "TabItem", 
                            id: _xamlEditor.IdManager.GetNewId(),
                            properties: new()
                            {
                                ["Content"] = (XamlValue) "TabItem 0",
                                ["Header"] = (XamlValue) "TabItem 0"
                            }),
                        new(name: "TabItem",
                            id: _xamlEditor.IdManager.GetNewId(), 
                            properties: new()
                            {
                                ["Content"] = (XamlValue) "TabItem 1",
                                ["Header"] = (XamlValue) "TabItem 1"
                            }),
                    }
                }, 
                contentProperty: "Items", 
                childrenProperty: "Items"),
            new(name: "TabItem", 
                id: _xamlEditor.IdManager.GetNewId(),
                properties: new()
                {
                    ["Content"] = (XamlValue) "TabItem",
                    ["Header"] = (XamlValue) "TabItem"
                },
                contentProperty: "Content", 
                childrenProperty: null),
            //
            //
            //
            new(name: "ProgressBar", 
                id: _xamlEditor.IdManager.GetNewId(),
                properties: new(), 
                contentProperty: null, 
                childrenProperty: null),
            new(name: "Slider", 
                id: _xamlEditor.IdManager.GetNewId(),
                properties: new(), 
                contentProperty: null, 
                childrenProperty: null),
            new(name: "DatePicker", 
                id: _xamlEditor.IdManager.GetNewId(),
                properties: new(), 
                contentProperty: null, 
                childrenProperty: null),
            //
            //
            //
            new(name: "Rectangle", 
                id: _xamlEditor.IdManager.GetNewId(),
                properties: new(), 
                contentProperty: null, 
                childrenProperty: null),
            new(name: "Ellipse", 
                id: _xamlEditor.IdManager.GetNewId(),
                properties: new(), 
                contentProperty: null, 
                childrenProperty: null),
            new(name: "Line", 
                id: _xamlEditor.IdManager.GetNewId(),
                properties: new(), 
                contentProperty: null, 
                childrenProperty: null),
            new(name: "Path", 
                id: _xamlEditor.IdManager.GetNewId(),
                properties: new(), 
                contentProperty: null, 
                childrenProperty: null),
            //
            new(name: "Image", 
                id: _xamlEditor.IdManager.GetNewId(),
                properties: new(), 
                contentProperty: "Source", 
                childrenProperty: null),
            new(name: "PathIcon", 
                id: _xamlEditor.IdManager.GetNewId(),
                properties: new(), 
                contentProperty: null, 
                childrenProperty: null),
            //
            new(name: "ScrollViewer", 
                id: _xamlEditor.IdManager.GetNewId(),
                properties: new(), 
                contentProperty: "Content", 
                childrenProperty: "Content"),
        };
    }

    public Control? DemoStackPanel()
    {
        var xamlItem = new XamlItem(name: "StackPanel",
            id: _xamlEditor.IdManager.GetNewId(),
            properties: new()
            {
                ["Children"] = (XamlValue) new List<XamlItem>(),
                ["Background"] = (XamlValue) "White",
                ["Width"] = (XamlValue) "336",
                ["Height"] = (XamlValue) "480",
            },
            contentProperty: "Children", 
            childrenProperty: "Children");

        return _xamlEditor.LoadForDesign(xamlItem);
    }

    public Control? DemoDockPanel()
    {
        var xamlItem = new XamlItem(name: "DockPanel",
            id: _xamlEditor.IdManager.GetNewId(),
            properties: new()
            {
                ["Children"] = (XamlValue) new List<XamlItem>(),
                ["Background"] = (XamlValue) "White",
                ["Width"] = (XamlValue) "336",
                ["Height"] = (XamlValue) "480",
            },
            contentProperty: "Children", 
            childrenProperty: "Children");

        return _xamlEditor.LoadForDesign(xamlItem);
    }

    public Control? DemoCanvas()
    {
        var xamlItem = new XamlItem(name: "Canvas",
            id: _xamlEditor.IdManager.GetNewId(),
            properties: new()
            {
                ["Children"] = (XamlValue) new List<XamlItem>(),
                ["Background"] = (XamlValue) "White",
                ["Width"] = (XamlValue) "336",
                ["Height"] = (XamlValue) "480",
            },
            contentProperty: "Children", 
            childrenProperty: "Children");

        return _xamlEditor.LoadForDesign(xamlItem);
    }
}
