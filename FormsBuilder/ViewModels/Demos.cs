using System.Collections.ObjectModel;
using Avalonia.Controls;

namespace FormsBuilder;

public class Demos
{
    private readonly IXamlEditor _xamlEditor;
    private readonly IXamlItemFactory _xamlItemFactory;

    public Demos(IXamlEditor xamlEditor, IXamlItemFactory xamlItemFactory)
    {
        _xamlEditor = xamlEditor;
        _xamlItemFactory = xamlItemFactory;
    }

    public ObservableCollection<XamlItem> DemoToolBox()
    {
        return new ObservableCollection<XamlItem>
        {
            //
            //
            //
            _xamlItemFactory.CreateControl(
                name: "TextBlock",
                properties: new XamlProperties
                {
                    ["Text"] = "TextBlock"
                },
                contentProperty: "Text", 
                childrenProperty: null),
            _xamlItemFactory.CreateControl(
                name: "Label",
                properties: new XamlProperties
                {
                    ["Content"] = "Label"
                },
                contentProperty: "Content", 
                childrenProperty: null),
            _xamlItemFactory.CreateControl(
                name: "TextBox",
                properties: new XamlProperties
                {
                    ["Text"] = "TextBox"
                },
                contentProperty: "Text", 
                childrenProperty: null),
            //
            //
            //
            _xamlItemFactory.CreateControl(
                name: "Button",
                properties: new XamlProperties
                {
                    ["Content"] = "Button"
                }, 
                contentProperty: "Content", 
                childrenProperty: null),
            _xamlItemFactory.CreateControl(
                name: "CheckBox",
                properties: new XamlProperties
                {
                    ["Content"] = "CheckBox"
                }, 
                contentProperty: "Content", 
                childrenProperty: null),
            _xamlItemFactory.CreateControl(
                name: "RadioButton",
                properties: new XamlProperties
                {
                    ["Content"] = "RadioButton"
                }, 
                contentProperty: "Content", 
                childrenProperty: null),
            //
            //
            //
            _xamlItemFactory.CreateControl(
                name: "Border",
                properties: new XamlProperties(),
                contentProperty: "Child", 
                childrenProperty: "Child"),
            _xamlItemFactory.CreateControl(
                name: "Decorator",
                properties: new XamlProperties
                {
                    ["Child"] = _xamlItemFactory.CreateControl(
                        name: "Button",
                        properties: new XamlProperties
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
            _xamlItemFactory.CreateControl(
                name: "Panel",
                properties: new XamlProperties(), 
                contentProperty: "Children", 
                childrenProperty: "Children"),
            _xamlItemFactory.CreateControl(
                name: "StackPanel",
                properties: new XamlProperties
                {
                    ["Children"] = new XamlItems
                    {
                        _xamlItemFactory.CreateControl(
                            name: "TextBlock",
                            properties: new XamlProperties
                            {
                                ["Text"] = "TextBlock"
                            }, 
                            contentProperty: "Text", 
                            childrenProperty: null),
                        _xamlItemFactory.CreateControl(
                            name: "TextBox",
                            properties: new XamlProperties
                            {
                                ["Text"] = "TextBox"
                            },
                            contentProperty: "Text", 
                            childrenProperty: null),
                    }
                }, 
                contentProperty: "Children", 
                childrenProperty: "Children"),
            _xamlItemFactory.CreateControl(
                name: "DockPanel",
                properties: new XamlProperties(), 
                contentProperty: "Children", 
                childrenProperty: "Children"),
            _xamlItemFactory.CreateControl(
                name: "WrapPanel",
                properties: new XamlProperties(), 
                contentProperty: "Children", 
                childrenProperty: "Children"),
            _xamlItemFactory.CreateControl(
                name: "Grid",
                properties: new XamlProperties(), 
                contentProperty: "Children", 
                childrenProperty: "Children"),
            //
            //
            //
            _xamlItemFactory.CreateControl(
                name: "ItemsControl",
                properties: new XamlProperties(), 
                contentProperty: "Items", 
                childrenProperty: "Items"),
            _xamlItemFactory.CreateControl(
                name: "ListBox",
                properties: new XamlProperties
                {
                    ["Items"] = new XamlItems
                    {
                        _xamlItemFactory.CreateControl(
                            name: "ListBoxItem",
                            properties: new XamlProperties
                            {
                                ["Content"] = "ListBoxItem 0"
                            }),
                        _xamlItemFactory.CreateControl(
                            name: "ListBoxItem", 
                            properties: new XamlProperties
                            {
                                ["Content"] = "ListBoxItem 1"
                            }),
                    }
                },
                contentProperty: "Items", 
                childrenProperty: "Items"),
            _xamlItemFactory.CreateControl(
                name: "ListBoxItem",
                properties: new XamlProperties(),
                contentProperty: "Content", 
                childrenProperty: null),
            //
            //
            //
            _xamlItemFactory.CreateControl(
                name: "ComboBox",
                properties: new XamlProperties(), 
                contentProperty: "ItemsSource", 
                childrenProperty: "Items"),
            _xamlItemFactory.CreateControl(
                name: "ComboBoxItem",
                properties: new XamlProperties(), 
                contentProperty: "Content", 
                childrenProperty: null),
            //
            //
            //
            _xamlItemFactory.CreateControl(
                name: "TabControl",
                properties: new XamlProperties
                {
                    ["Items"] = new XamlItems
                    {
                        _xamlItemFactory.CreateControl(
                            name: "TabItem",
                            properties: new XamlProperties
                            {
                                ["Content"] = "TabItem 0",
                                ["Header"] = "TabItem 0"
                            }),
                        _xamlItemFactory.CreateControl(
                            name: "TabItem", 
                            properties: new XamlProperties
                            {
                                ["Content"] = "TabItem 1",
                                ["Header"] = "TabItem 1"
                            }),
                    }
                }, 
                contentProperty: "Items", 
                childrenProperty: "Items"),
            _xamlItemFactory.CreateControl(
                name: "TabItem",
                properties: new XamlProperties
                {
                    ["Content"] = "TabItem",
                    ["Header"] = "TabItem"
                },
                contentProperty: "Content", 
                childrenProperty: null),
            //
            //
            //
            _xamlItemFactory.CreateControl(
                name: "ProgressBar",
                properties: new XamlProperties(), 
                contentProperty: null, 
                childrenProperty: null),
            _xamlItemFactory.CreateControl(
                name: "Slider",
                properties: new XamlProperties(),
                contentProperty: null, 
                childrenProperty: null),
            _xamlItemFactory.CreateControl(
                name: "DatePicker",
                properties: new XamlProperties(), 
                contentProperty: null, 
                childrenProperty: null),
            //
            //
            //
            _xamlItemFactory.CreateControl(
                name: "Rectangle",
                properties: new XamlProperties(), 
                contentProperty: null, 
                childrenProperty: null),
            _xamlItemFactory.CreateControl(
                name: "Ellipse",
                properties: new XamlProperties(), 
                contentProperty: null, 
                childrenProperty: null),
            _xamlItemFactory.CreateControl(
                name: "Line",
                properties: new XamlProperties(), 
                contentProperty: null, 
                childrenProperty: null),
            _xamlItemFactory.CreateControl(
                name: "Path",
                properties: new XamlProperties(), 
                contentProperty: null, 
                childrenProperty: null),
            //
            _xamlItemFactory.CreateControl(
                name: "Image",
                properties: new XamlProperties(), 
                contentProperty: "Source", 
                childrenProperty: null),
            _xamlItemFactory.CreateControl(
                name: "PathIcon",
                properties: new XamlProperties(), 
                contentProperty: null, 
                childrenProperty: null),
            //
            _xamlItemFactory.CreateControl(
                name: "ScrollViewer",
                properties: new XamlProperties(), 
                contentProperty: "Content", 
                childrenProperty: "Content"),
        };
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

    public Control? DemoStackPanel()
    {
        var xamlItem = _xamlItemFactory.CreateControl(
            name: "StackPanel",
            properties: new XamlProperties
            {
                ["Children"] = new XamlItems(),
                ["Background"] = "White",
                ["Width"] = "336",
                ["Height"] = "480",
            },
            contentProperty: "Children", 
            childrenProperty: "Children");

        return _xamlEditor.LoadForDesign(xamlItem);
    }

    public Control? DemoDockPanel()
    {
        var xamlItem = _xamlItemFactory.CreateControl(
            name: "DockPanel",
            properties: new XamlProperties
            {
                ["Children"] = new XamlItems(),
                ["Background"] = "White",
                ["Width"] = "336",
                ["Height"] = "480",
            }, 
            contentProperty: "Children", 
            childrenProperty: "Children");

        return _xamlEditor.LoadForDesign(xamlItem);
    }

    public Control? DemoCanvas()
    {
        var xamlItemStyleRectangle = _xamlItemFactory.CreateStyle(
            name: "Style",
            properties: new XamlProperties
            {
                ["Selector"] = "Rectangle",
                ["Children"] = new XamlItems
                {
                    _xamlItemFactory.CreateSetter(
                        name: "Setter",
                        properties: new XamlProperties
                        {
                            ["Property"] = "Stroke",
                            ["Value"] = "Red"
                        }),
                    _xamlItemFactory.CreateSetter(
                        name: "Setter", 
                        properties: new XamlProperties
                        {
                            ["Property"] = "StrokeThickness",
                            ["Value"] = "2"
                        }),
                }
            }, 
            contentProperty: "Children", 
            childrenProperty: "Children");

        var xamlItem = _xamlItemFactory.CreateControl(
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
            contentProperty: "Children", 
            childrenProperty: "Children");

        return _xamlEditor.LoadForDesign(xamlItem);
    }
}
