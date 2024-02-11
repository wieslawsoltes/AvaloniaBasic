using System.Collections.Generic;
using Avalonia.Controls;

namespace FormsBuilder;

public class Demos
{
    private readonly XamlEditorViewModel _xamlEditorViewModel;

    public Demos(XamlEditorViewModel xamlEditorViewModel)
    {
        _xamlEditorViewModel = xamlEditorViewModel;
    }

    public List<XamlItem> DemoToolBox()
    {
        return new List<XamlItem>
        {
            //
            //
            //
            new(name: "TextBlock", 
                id: _xamlEditorViewModel.IdManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>
                {
                    ["Text"] = (XamlValue) "TextBlock"
                }, 
                contentProperty: "Text", 
                childrenProperty: null),
            new(name: "Label", 
                id: _xamlEditorViewModel.IdManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>
                {
                    ["Content"] = (XamlValue) "Label"
                }, 
                contentProperty: "Content", 
                childrenProperty: null),
            new(name: "TextBox", 
                id: _xamlEditorViewModel.IdManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>
                {
                    ["Text"] = (XamlValue) "TextBox"
                }, 
                contentProperty: "Text", 
                childrenProperty: null),
            //
            //
            //
            new(name: "Button", 
                id: _xamlEditorViewModel.IdManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>
                {
                    ["Content"] = (XamlValue) "Button"
                }, 
                contentProperty: "Content", 
                childrenProperty: null),
            new(name: "CheckBox", 
                id: _xamlEditorViewModel.IdManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>
                {
                    ["Content"] = (XamlValue) "CheckBox"
                }, 
                contentProperty: "Content", 
                childrenProperty: null),
            new(name: "RadioButton", 
                id: _xamlEditorViewModel.IdManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>
                {
                    ["Content"] = (XamlValue) "RadioButton"
                }, 
                contentProperty: "Content", 
                childrenProperty: null),
            //
            //
            //
            new(name: "Border",
                id: _xamlEditorViewModel.IdManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(),
                contentProperty: "Child",
                childrenProperty: "Child"
            ),
            new(name: "Decorator",
                id: _xamlEditorViewModel.IdManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>
                {
                    ["Child"] = (XamlValue) new XamlItem(
                        name: "Button", 
                        id: _xamlEditorViewModel.IdManager.GetNewId(),
                        properties: new Dictionary<string, XamlValue>
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
                id: _xamlEditorViewModel.IdManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(), 
                contentProperty: "Children", 
                childrenProperty: "Children"),
            new(name: "StackPanel",
                id: _xamlEditorViewModel.IdManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>
                {
                    ["Children"] = (XamlValue) new List<XamlItem>
                    {
                        new(name: "TextBlock", 
                            id: _xamlEditorViewModel.IdManager.GetNewId(),
                            properties: new Dictionary<string, XamlValue>
                            {
                                ["Text"] = (XamlValue) "TextBlock"
                            }, 
                            contentProperty: "Text", 
                            childrenProperty: null),
                        new(name: "TextBox", 
                            id: _xamlEditorViewModel.IdManager.GetNewId(),
                            properties: new Dictionary<string, XamlValue>
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
                id: _xamlEditorViewModel.IdManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(), 
                contentProperty: "Children", 
                childrenProperty: "Children"),
            new(name: "WrapPanel", 
                id: _xamlEditorViewModel.IdManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(), 
                contentProperty: "Children", 
                childrenProperty: "Children"),
            new(name: "Grid", 
                id: _xamlEditorViewModel.IdManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(), 
                contentProperty: "Children", 
                childrenProperty: "Children"),
            //
            //
            //
            new(name: "ItemsControl", 
                id: _xamlEditorViewModel.IdManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(), 
                contentProperty: "Items", 
                childrenProperty: "Items"),
            new(name: "ListBox",
                id: _xamlEditorViewModel.IdManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>
                {
                    ["Items"] = (XamlValue) new List<XamlItem>
                    {
                        new(name: "ListBoxItem", 
                            id: _xamlEditorViewModel.IdManager.GetNewId(),
                            properties: new Dictionary<string, XamlValue>
                            {
                                ["Content"] = (XamlValue) "ListBoxItem 0"
                            }),
                        new(name: "ListBoxItem",
                            id: _xamlEditorViewModel.IdManager.GetNewId(), 
                            properties: new Dictionary<string, XamlValue>
                            {
                                ["Content"] = (XamlValue) "ListBoxItem 1"
                            }),
                    }
                }, 
                contentProperty: "Items", 
                childrenProperty: "Items"),
            new(name: "ListBoxItem", 
                id: _xamlEditorViewModel.IdManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(), 
                contentProperty: "Content", 
                childrenProperty: null),
            //
            //
            //
            new(name: "ComboBox", 
                id: _xamlEditorViewModel.IdManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(), 
                contentProperty: "ItemsSource", 
                childrenProperty: "Items"),
            new(name: "ComboBoxItem", 
                id: _xamlEditorViewModel.IdManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(), 
                contentProperty: "Content", 
                childrenProperty: null),
            //
            //
            //
            new(name: "TabControl",
                id: _xamlEditorViewModel.IdManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>
                {
                    ["Items"] = (XamlValue) new List<XamlItem>
                    {
                        new(name: "TabItem", 
                            id: _xamlEditorViewModel.IdManager.GetNewId(),
                            properties: new Dictionary<string, XamlValue>
                            {
                                ["Content"] = (XamlValue) "TabItem 0",
                                ["Header"] = (XamlValue) "TabItem 0"
                            }),
                        new(name: "TabItem",
                            id: _xamlEditorViewModel.IdManager.GetNewId(), 
                            properties: new Dictionary<string, XamlValue>
                            {
                                ["Content"] = (XamlValue) "TabItem 1",
                                ["Header"] = (XamlValue) "TabItem 1"
                            }),
                    }
                }, 
                contentProperty: "Items", 
                childrenProperty: "Items"),
            new(name: "TabItem", 
                id: _xamlEditorViewModel.IdManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>
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
                id: _xamlEditorViewModel.IdManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(), 
                contentProperty: null, 
                childrenProperty: null),
            new(name: "Slider", 
                id: _xamlEditorViewModel.IdManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(), 
                contentProperty: null, 
                childrenProperty: null),
            new(name: "DatePicker", 
                id: _xamlEditorViewModel.IdManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(), 
                contentProperty: null, 
                childrenProperty: null),
            //
            //
            //
            new(name: "Rectangle", 
                id: _xamlEditorViewModel.IdManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(), 
                contentProperty: null, 
                childrenProperty: null),
            new(name: "Ellipse", 
                id: _xamlEditorViewModel.IdManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(), 
                contentProperty: null, 
                childrenProperty: null),
            new(name: "Line", 
                id: _xamlEditorViewModel.IdManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(), 
                contentProperty: null, 
                childrenProperty: null),
            new(name: "Path", 
                id: _xamlEditorViewModel.IdManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(), 
                contentProperty: null, 
                childrenProperty: null),
            //
            new(name: "Image", 
                id: _xamlEditorViewModel.IdManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(), 
                contentProperty: "Source", 
                childrenProperty: null),
            new(name: "PathIcon", 
                id: _xamlEditorViewModel.IdManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(), 
                contentProperty: null, 
                childrenProperty: null),
            //
            new(name: "ScrollViewer", 
                id: _xamlEditorViewModel.IdManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(), 
                contentProperty: "Content", 
                childrenProperty: "Content"),
        };
    }

    public Control? DemoStackPanel()
    {
        var xamlItem = new XamlItem(name: "StackPanel",
            id: _xamlEditorViewModel.IdManager.GetNewId(),
            properties: new Dictionary<string, XamlValue>
            {
                ["Children"] = (XamlValue) new List<XamlItem>(),
                ["Background"] = (XamlValue) "White",
                ["Width"] = (XamlValue) "350",
                ["Height"] = (XamlValue) "500",
            },
            contentProperty: "Children", 
            childrenProperty: "Children");

        return _xamlEditorViewModel.LoadForDesign(xamlItem);
    }

    public Control? DemoDockPanel()
    {
        var xamlItem = new XamlItem(name: "DockPanel",
            id: _xamlEditorViewModel.IdManager.GetNewId(),
            properties: new Dictionary<string, XamlValue>
            {
                ["Children"] = (XamlValue) new List<XamlItem>(),
                ["Background"] = (XamlValue) "White",
                ["Width"] = (XamlValue) "450",
                ["Height"] = (XamlValue) "500",
            },
            contentProperty: "Children", 
            childrenProperty: "Children");

        return _xamlEditorViewModel.LoadForDesign(xamlItem);
    }

    public Control? DemoCanvas()
    {
        var xamlItem = new XamlItem(name: "Canvas",
            id: _xamlEditorViewModel.IdManager.GetNewId(),
            properties: new Dictionary<string, XamlValue>
            {
                ["Children"] = (XamlValue) new List<XamlItem>(),
                ["Background"] = (XamlValue) "White",
                ["Width"] = (XamlValue) "350",
                ["Height"] = (XamlValue) "500",
            },
            contentProperty: "Children", 
            childrenProperty: "Children");

        return _xamlEditorViewModel.LoadForDesign(xamlItem);
    }
}
