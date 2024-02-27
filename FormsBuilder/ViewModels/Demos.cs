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
            new XamlItem(
                name: "TextBlock",
                properties: new XamlProperties
                {
                    ["Text"] = "TextBlock"
                }, 
                id: _idManager.GetNewId(), 
                contentProperty: "Text", 
                childrenProperty: null),
            new XamlItem(
                name: "Label",
                properties: new XamlProperties
                {
                    ["Content"] = "Label"
                }, 
                id: _idManager.GetNewId(), 
                contentProperty: "Content", 
                childrenProperty: null),
            new XamlItem(
                name: "TextBox",
                properties: new XamlProperties
                {
                    ["Text"] = "TextBox"
                }, 
                id: _idManager.GetNewId(), 
                contentProperty: "Text", 
                childrenProperty: null),
            //
            //
            //
            new XamlItem(
                name: "Button",
                properties: new XamlProperties
                {
                    ["Content"] = "Button"
                }, 
                id: _idManager.GetNewId(), 
                contentProperty: "Content", 
                childrenProperty: null),
            new XamlItem(
                name: "CheckBox",
                properties: new XamlProperties
                {
                    ["Content"] = "CheckBox"
                }, 
                id: _idManager.GetNewId(), 
                contentProperty: "Content", 
                childrenProperty: null),
            new XamlItem(
                name: "RadioButton",
                properties: new XamlProperties
                {
                    ["Content"] = "RadioButton"
                }, 
                id: _idManager.GetNewId(), 
                contentProperty: "Content", 
                childrenProperty: null),
            //
            //
            //
            new XamlItem(
                name: "Border",
                properties: new XamlProperties(),
                id: _idManager.GetNewId(),
                contentProperty: "Child", 
                childrenProperty: "Child"),
            new XamlItem(
                name: "Decorator",
                properties: new XamlProperties
                {
                    ["Child"] = new XamlItem(
                        name: "Button",
                        properties: new XamlProperties
                        {
                            ["Content"] = "Button"
                        }, 
                        id: _idManager.GetNewId(), 
                        contentProperty: "Content", 
                        childrenProperty: null)
                },
                id: _idManager.GetNewId(),
                contentProperty: "Child", 
                childrenProperty: "Child"),
            //
            //
            //
            new XamlItem(
                name: "Panel",
                properties: new XamlProperties(), 
                id: _idManager.GetNewId(), 
                contentProperty: "Children", 
                childrenProperty: "Children"),
            new XamlItem(
                name: "StackPanel",
                properties: new XamlProperties
                {
                    ["Children"] = new XamlItems
                    {
                        new XamlItem(
                            name: "TextBlock",
                            properties: new XamlProperties
                            {
                                ["Text"] = "TextBlock"
                            }, 
                            id: _idManager.GetNewId(), 
                            contentProperty: "Text", 
                            childrenProperty: null),
                        new XamlItem(
                            name: "TextBox",
                            properties: new XamlProperties
                            {
                                ["Text"] = "TextBox"
                            },
                            id: _idManager.GetNewId(), 
                            contentProperty: "Text", 
                            childrenProperty: null),
                    }
                }, 
                id: _idManager.GetNewId(), 
                contentProperty: "Children", 
                childrenProperty: "Children"),
            new XamlItem(
                name: "DockPanel",
                properties: new XamlProperties(), 
                id: _idManager.GetNewId(), 
                contentProperty: "Children", 
                childrenProperty: "Children"),
            new XamlItem(
                name: "WrapPanel",
                properties: new XamlProperties(), 
                id: _idManager.GetNewId(), 
                contentProperty: "Children", 
                childrenProperty: "Children"),
            new XamlItem(
                name: "Grid",
                properties: new XamlProperties(), 
                id: _idManager.GetNewId(), 
                contentProperty: "Children", 
                childrenProperty: "Children"),
            //
            //
            //
            new XamlItem(
                name: "ItemsControl",
                properties: new XamlProperties(), 
                id: _idManager.GetNewId(), 
                contentProperty: "Items", 
                childrenProperty: "Items"),
            new XamlItem(
                name: "ListBox",
                properties: new XamlProperties
                {
                    ["Items"] = new XamlItems
                    {
                        new XamlItem(
                            name: "ListBoxItem",
                            properties: new XamlProperties
                            {
                                ["Content"] = "ListBoxItem 0"
                            }, 
                            id: _idManager.GetNewId()),
                        new XamlItem(
                            name: "ListBoxItem", 
                            properties: new XamlProperties
                            {
                                ["Content"] = "ListBoxItem 1"
                            }, 
                            id: _idManager.GetNewId()),
                    }
                }, 
                id: _idManager.GetNewId(), 
                contentProperty: "Items", 
                childrenProperty: "Items"),
            new XamlItem(
                name: "ListBoxItem",
                properties: new XamlProperties(), 
                id: _idManager.GetNewId(), 
                contentProperty: "Content", 
                childrenProperty: null),
            //
            //
            //
            new XamlItem(
                name: "ComboBox",
                properties: new XamlProperties(), 
                id: _idManager.GetNewId(), 
                contentProperty: "ItemsSource", 
                childrenProperty: "Items"),
            new XamlItem(
                name: "ComboBoxItem",
                properties: new XamlProperties(), 
                id: _idManager.GetNewId(), 
                contentProperty: "Content", 
                childrenProperty: null),
            //
            //
            //
            new XamlItem(
                name: "TabControl",
                properties: new XamlProperties
                {
                    ["Items"] = new XamlItems
                    {
                        new XamlItem(
                            name: "TabItem",
                            properties: new XamlProperties
                            {
                                ["Content"] = "TabItem 0",
                                ["Header"] = "TabItem 0"
                            }, 
                            id: _idManager.GetNewId()),
                        new XamlItem(
                            name: "TabItem", 
                            properties: new XamlProperties
                            {
                                ["Content"] = "TabItem 1",
                                ["Header"] = "TabItem 1"
                            }, 
                            id: _idManager.GetNewId()),
                    }
                }, 
                id: _idManager.GetNewId(), 
                contentProperty: "Items", 
                childrenProperty: "Items"),
            new XamlItem(
                name: "TabItem",
                properties: new XamlProperties
                {
                    ["Content"] = "TabItem",
                    ["Header"] = "TabItem"
                },
                id: _idManager.GetNewId(), 
                contentProperty: "Content", 
                childrenProperty: null),
            //
            //
            //
            new XamlItem(
                name: "ProgressBar",
                properties: new XamlProperties(), 
                id: _idManager.GetNewId(), 
                contentProperty: null, 
                childrenProperty: null),
            new XamlItem(
                name: "Slider",
                properties: new XamlProperties(), 
                id: _idManager.GetNewId(), 
                contentProperty: null, 
                childrenProperty: null),
            new XamlItem(
                name: "DatePicker",
                properties: new XamlProperties(), 
                id: _idManager.GetNewId(), 
                contentProperty: null, 
                childrenProperty: null),
            //
            //
            //
            new XamlItem(
                name: "Rectangle",
                properties: new XamlProperties(), 
                id: _idManager.GetNewId(), 
                contentProperty: null, 
                childrenProperty: null),
            new XamlItem(
                name: "Ellipse",
                properties: new XamlProperties(), 
                id: _idManager.GetNewId(), 
                contentProperty: null, 
                childrenProperty: null),
            new XamlItem(
                name: "Line",
                properties: new XamlProperties(), 
                id: _idManager.GetNewId(), 
                contentProperty: null, 
                childrenProperty: null),
            new XamlItem(
                name: "Path",
                properties: new XamlProperties(), 
                id: _idManager.GetNewId(), 
                contentProperty: null, 
                childrenProperty: null),
            //
            new XamlItem(
                name: "Image",
                properties: new XamlProperties(), 
                id: _idManager.GetNewId(), 
                contentProperty: "Source", 
                childrenProperty: null),
            new XamlItem(
                name: "PathIcon",
                properties: new XamlProperties(), 
                id: _idManager.GetNewId(), 
                contentProperty: null, 
                childrenProperty: null),
            //
            new XamlItem(
                name: "ScrollViewer",
                properties: new XamlProperties(), 
                id: _idManager.GetNewId(), 
                contentProperty: "Content", 
                childrenProperty: "Content"),
        };
    }

    public Control? DemoStackPanel()
    {
        var xamlItem = new XamlItem(
            name: "StackPanel",
            properties: new XamlProperties
            {
                ["Children"] = new XamlItems(),
                ["Background"] = "White",
                ["Width"] = "336",
                ["Height"] = "480",
            },
            id: _idManager.GetNewId(), 
            contentProperty: "Children", 
            childrenProperty: "Children");

        return _xamlEditor.LoadForDesign(xamlItem);
    }

    public Control? DemoDockPanel()
    {
        var xamlItem = new XamlItem(
            name: "DockPanel",
            properties: new XamlProperties
            {
                ["Children"] = new XamlItems(),
                ["Background"] = "White",
                ["Width"] = "336",
                ["Height"] = "480",
            },
            id: _idManager.GetNewId(), 
            contentProperty: "Children", 
            childrenProperty: "Children");

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
            name: "Style",
            properties: new XamlProperties
            {
                ["Selector"] = "Rectangle",
                ["Children"] = new XamlItems
                {
                    new XamlItem(
                        name: "Setter",
                        properties: new XamlProperties
                        {
                            ["Property"] = "Stroke",
                            ["Value"] = "Red"
                        }),
                    new XamlItem(
                        name: "Setter", 
                        properties: new XamlProperties
                        {
                            ["Property"] = "StrokeThickness",
                            ["Value"] = "2"
                        }),
                }
            },
            _idManager.GetNewId(), 
            contentProperty: "Children", 
            childrenProperty: "Children");

        var xamlItem = new XamlItem(
            name: "Canvas",
            properties: new XamlProperties
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
