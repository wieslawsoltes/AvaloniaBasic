using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Controls;

namespace FormsBuilder;

public class Demos
{
    private readonly IXamlEditor _xamlEditor;
    private readonly IXamlItemIdManager _idManager;

    public Demos(IXamlEditor xamlEditor)
    {
        _xamlEditor = xamlEditor;
        _idManager = _xamlEditor.IdManager;
    }

    public ObservableCollection<XamlItem> DemoToolBox()
    {
        return new ObservableCollection<XamlItem>
        {
            //
            //
            //
            new(name: "TextBlock",
                properties: new()
                {
                    ["Text"] = "TextBlock"
                }, 
                id: _idManager.GetNewId(), 
                contentProperty: "Text", childrenProperty: null),
            new(name: "Label",
                properties: new()
                {
                    ["Content"] = "Label"
                }, 
                id: _idManager.GetNewId(), 
                contentProperty: "Content", childrenProperty: null),
            new(name: "TextBox",
                properties: new()
                {
                    ["Text"] = "TextBox"
                }, 
                id: _idManager.GetNewId(), 
                contentProperty: "Text", childrenProperty: null),
            //
            //
            //
            new(name: "Button",
                properties: new()
                {
                    ["Content"] = "Button"
                }, 
                id: _idManager.GetNewId(), 
                contentProperty: "Content", childrenProperty: null),
            new(name: "CheckBox",
                properties: new()
                {
                    ["Content"] = "CheckBox"
                }, 
                id: _idManager.GetNewId(), 
                contentProperty: "Content", childrenProperty: null),
            new(name: "RadioButton",
                properties: new()
                {
                    ["Content"] = "RadioButton"
                }, 
                id: _idManager.GetNewId(), 
                contentProperty: "Content", childrenProperty: null),
            //
            //
            //
            new(name: "Border",
                properties: new(),
                id: _idManager.GetNewId(),
                contentProperty: "Child", childrenProperty: "Child"),
            new(name: "Decorator",
                properties: new()
                {
                    ["Child"] = new XamlItem(
                        name: "Button",
                        properties: new()
                        {
                            ["Content"] = "Button"
                        }, 
                        id: _idManager.GetNewId(), 
                        contentProperty: "Content", childrenProperty: null)
                },
                id: _idManager.GetNewId(),
                contentProperty: "Child", childrenProperty: "Child"),
            //
            //
            //
            new(name: "Panel",
                properties: new(), 
                id: _idManager.GetNewId(), 
                contentProperty: "Children", childrenProperty: "Children"),
            new(name: "StackPanel",
                properties: new()
                {
                    ["Children"] = new XamlItems
                    {
                        new(name: "TextBlock",
                            properties: new()
                            {
                                ["Text"] = "TextBlock"
                            }, 
                            id: _idManager.GetNewId(), 
                            contentProperty: "Text", childrenProperty: null),
                        new(name: "TextBox",
                            properties: new()
                            {
                                ["Text"] = "TextBox"
                            },
                            id: _idManager.GetNewId(), 
                            contentProperty: "Text", childrenProperty: null),
                    }
                }, 
                id: _idManager.GetNewId(), 
                contentProperty: "Children", childrenProperty: "Children"),
            new(name: "DockPanel",
                properties: new(), 
                id: _idManager.GetNewId(), 
                contentProperty: "Children", childrenProperty: "Children"),
            new(name: "WrapPanel",
                properties: new(), 
                id: _idManager.GetNewId(), 
                contentProperty: "Children", childrenProperty: "Children"),
            new(name: "Grid",
                properties: new(), 
                id: _idManager.GetNewId(), 
                contentProperty: "Children", childrenProperty: "Children"),
            //
            //
            //
            new(name: "ItemsControl",
                properties: new(), 
                id: _idManager.GetNewId(), 
                contentProperty: "Items", childrenProperty: "Items"),
            new(name: "ListBox",
                properties: new()
                {
                    ["Items"] = new XamlItems
                    {
                        new(name: "ListBoxItem",
                            properties: new()
                            {
                                ["Content"] = "ListBoxItem 0"
                            }, id: _idManager.GetNewId()),
                        new(name: "ListBoxItem", 
                            properties: new()
                            {
                                ["Content"] = "ListBoxItem 1"
                            }, id: _idManager.GetNewId()),
                    }
                }, 
                id: _idManager.GetNewId(), 
                contentProperty: "Items", childrenProperty: "Items"),
            new(name: "ListBoxItem",
                properties: new(), 
                id: _idManager.GetNewId(), 
                contentProperty: "Content", childrenProperty: null),
            //
            //
            //
            new(name: "ComboBox",
                properties: new(), 
                id: _idManager.GetNewId(), 
                contentProperty: "ItemsSource", childrenProperty: "Items"),
            new(name: "ComboBoxItem",
                properties: new(), 
                id: _idManager.GetNewId(), 
                contentProperty: "Content", childrenProperty: null),
            //
            //
            //
            new(name: "TabControl",
                properties: new()
                {
                    ["Items"] = new XamlItems
                    {
                        new(name: "TabItem",
                            properties: new()
                            {
                                ["Content"] = "TabItem 0",
                                ["Header"] = "TabItem 0"
                            }, id: _idManager.GetNewId()),
                        new(name: "TabItem", 
                            properties: new()
                            {
                                ["Content"] = "TabItem 1",
                                ["Header"] = "TabItem 1"
                            }, id: _idManager.GetNewId()),
                    }
                }, 
                id: _idManager.GetNewId(), 
                contentProperty: "Items", childrenProperty: "Items"),
            new(name: "TabItem",
                properties: new()
                {
                    ["Content"] = "TabItem",
                    ["Header"] = "TabItem"
                },
                id: _idManager.GetNewId(), 
                contentProperty: "Content", childrenProperty: null),
            //
            //
            //
            new(name: "ProgressBar",
                properties: new(), 
                id: _idManager.GetNewId(), 
                contentProperty: null, childrenProperty: null),
            new(name: "Slider",
                properties: new(), 
                id: _idManager.GetNewId(), 
                contentProperty: null, childrenProperty: null),
            new(name: "DatePicker",
                properties: new(), 
                id: _idManager.GetNewId(), 
                contentProperty: null, childrenProperty: null),
            //
            //
            //
            new(name: "Rectangle",
                properties: new(), 
                id: _idManager.GetNewId(), 
                contentProperty: null, childrenProperty: null),
            new(name: "Ellipse",
                properties: new(), 
                id: _idManager.GetNewId(), 
                contentProperty: null, childrenProperty: null),
            new(name: "Line",
                properties: new(), 
                id: _idManager.GetNewId(), 
                contentProperty: null, childrenProperty: null),
            new(name: "Path",
                properties: new(), 
                id: _idManager.GetNewId(), 
                contentProperty: null, childrenProperty: null),
            //
            new(name: "Image",
                properties: new(), 
                id: _idManager.GetNewId(), 
                contentProperty: "Source", childrenProperty: null),
            new(name: "PathIcon",
                properties: new(), 
                id: _idManager.GetNewId(), 
                contentProperty: null, childrenProperty: null),
            //
            new(name: "ScrollViewer",
                properties: new(), 
                id: _idManager.GetNewId(), 
                contentProperty: "Content", childrenProperty: "Content"),
        };
    }

    public Control? DemoStackPanel()
    {
        var xamlItem = new XamlItem(name: "StackPanel",
            properties: new()
            {
                ["Children"] = new XamlItems(),
                ["Background"] = "White",
                ["Width"] = "336",
                ["Height"] = "480",
            },
            id: _idManager.GetNewId(), 
            contentProperty: "Children", childrenProperty: "Children");

        return _xamlEditor.LoadForDesign(xamlItem);
    }

    public Control? DemoDockPanel()
    {
        var xamlItem = new XamlItem(name: "DockPanel",
            properties: new()
            {
                ["Children"] = new XamlItems(),
                ["Background"] = "White",
                ["Width"] = "336",
                ["Height"] = "480",
            },
            id: _idManager.GetNewId(), 
            contentProperty: "Children", childrenProperty: "Children");

        return _xamlEditor.LoadForDesign(xamlItem);
    }

    // TODO:
    public ObservableCollection<XamlItem> DemoComponents()
    {
        return new ObservableCollection<XamlItem>
        {
            // TODO:
        };
    }

    // TODO:
    public ObservableCollection<XamlItem> DemoStyles()
    {
        return new ObservableCollection<XamlItem>
        {
            // TODO:
        };
    }

    public Control? DemoCanvas()
    {
        var xamlItemStyleRectangle = new XamlItem(
            "Style",
            new()
            {
                ["Selector"] = "Rectangle",
                ["Children"] = new XamlItems
                {
                    new(name: "Setter",
                        properties: new()
                        {
                            ["Property"] = "Stroke",
                            ["Value"] = "Red"
                        }),
                    new(name: "Setter", 
                        properties: new()
                        {
                            ["Property"] = "StrokeThickness",
                            ["Value"] = "2"
                        }),
                }
            },
            _idManager.GetNewId(), 
            contentProperty: "Children", 
            childrenProperty: "Children");

        var xamlItem = new XamlItem(name: "Canvas",
            properties: new()
            {
                ["Styles"] = new XamlItems
                {
                    xamlItemStyleRectangle
                },
                ["Background"] = "White",
                ["Width"] = "336",
                ["Height"] = "480",
                ["Children"] = new XamlItems(),
            },
            id: _idManager.GetNewId(), 
            contentProperty: "Children", 
            childrenProperty: "Children");

        return _xamlEditor.LoadForDesign(xamlItem);
    }
}
