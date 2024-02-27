using System.Collections.ObjectModel;
using Avalonia.Controls;

namespace FormsBuilder;

public class Demos
{
    private readonly IXamlEditor _xamlEditor;
    private readonly IXamlFactory _xamlFactory;

    public Demos(IXamlEditor xamlEditor, IXamlFactory xamlFactory)
    {
        _xamlEditor = xamlEditor;
        _xamlFactory = xamlFactory;
    }

    public ObservableCollection<XamlItem> DemoToolBox()
    {
        return new ObservableCollection<XamlItem>
        {
            //
            //
            //
            _xamlFactory.CreateControl(
                name: "TextBlock",
                properties: new XamlProperties
                {
                    ["Text"] = "TextBlock"
                },
                contentProperty: "Text", 
                childrenProperty: null),
            _xamlFactory.CreateControl(
                name: "Label",
                properties: new XamlProperties
                {
                    ["Content"] = "Label"
                },
                contentProperty: "Content", 
                childrenProperty: null),
            _xamlFactory.CreateControl(
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
            _xamlFactory.CreateControl(
                name: "Button",
                properties: new XamlProperties
                {
                    ["Content"] = "Button"
                }, 
                contentProperty: "Content", 
                childrenProperty: null),
            _xamlFactory.CreateControl(
                name: "CheckBox",
                properties: new XamlProperties
                {
                    ["Content"] = "CheckBox"
                }, 
                contentProperty: "Content", 
                childrenProperty: null),
            _xamlFactory.CreateControl(
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
            _xamlFactory.CreateControl(
                name: "Border",
                properties: new XamlProperties(),
                contentProperty: "Child", 
                childrenProperty: "Child"),
            _xamlFactory.CreateControl(
                name: "Decorator",
                properties: new XamlProperties
                {
                    ["Child"] = _xamlFactory.CreateControl(
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
            _xamlFactory.CreateControl(
                name: "Panel",
                properties: new XamlProperties(), 
                contentProperty: "Children", 
                childrenProperty: "Children"),
            _xamlFactory.CreateControl(
                name: "StackPanel",
                properties: new XamlProperties
                {
                    ["Children"] = new XamlItems
                    {
                        _xamlFactory.CreateControl(
                            name: "TextBlock",
                            properties: new XamlProperties
                            {
                                ["Text"] = "TextBlock"
                            }, 
                            contentProperty: "Text", 
                            childrenProperty: null),
                        _xamlFactory.CreateControl(
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
            _xamlFactory.CreateControl(
                name: "DockPanel",
                properties: new XamlProperties(), 
                contentProperty: "Children", 
                childrenProperty: "Children"),
            _xamlFactory.CreateControl(
                name: "WrapPanel",
                properties: new XamlProperties(), 
                contentProperty: "Children", 
                childrenProperty: "Children"),
            _xamlFactory.CreateControl(
                name: "Grid",
                properties: new XamlProperties(), 
                contentProperty: "Children", 
                childrenProperty: "Children"),
            //
            //
            //
            _xamlFactory.CreateControl(
                name: "ItemsControl",
                properties: new XamlProperties(), 
                contentProperty: "Items", 
                childrenProperty: "Items"),
            _xamlFactory.CreateControl(
                name: "ListBox",
                properties: new XamlProperties
                {
                    ["Items"] = new XamlItems
                    {
                        _xamlFactory.CreateControl(
                            name: "ListBoxItem",
                            properties: new XamlProperties
                            {
                                ["Content"] = "ListBoxItem 0"
                            }),
                        _xamlFactory.CreateControl(
                            name: "ListBoxItem", 
                            properties: new XamlProperties
                            {
                                ["Content"] = "ListBoxItem 1"
                            }),
                    }
                },
                contentProperty: "Items", 
                childrenProperty: "Items"),
            _xamlFactory.CreateControl(
                name: "ListBoxItem",
                properties: new XamlProperties(),
                contentProperty: "Content", 
                childrenProperty: null),
            //
            //
            //
            _xamlFactory.CreateControl(
                name: "ComboBox",
                properties: new XamlProperties(), 
                contentProperty: "ItemsSource", 
                childrenProperty: "Items"),
            _xamlFactory.CreateControl(
                name: "ComboBoxItem",
                properties: new XamlProperties(), 
                contentProperty: "Content", 
                childrenProperty: null),
            //
            //
            //
            _xamlFactory.CreateControl(
                name: "TabControl",
                properties: new XamlProperties
                {
                    ["Items"] = new XamlItems
                    {
                        _xamlFactory.CreateControl(
                            name: "TabItem",
                            properties: new XamlProperties
                            {
                                ["Content"] = "TabItem 0",
                                ["Header"] = "TabItem 0"
                            }),
                        _xamlFactory.CreateControl(
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
            _xamlFactory.CreateControl(
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
            _xamlFactory.CreateControl(
                name: "ProgressBar",
                properties: new XamlProperties(), 
                contentProperty: null, 
                childrenProperty: null),
            _xamlFactory.CreateControl(
                name: "Slider",
                properties: new XamlProperties(),
                contentProperty: null, 
                childrenProperty: null),
            _xamlFactory.CreateControl(
                name: "DatePicker",
                properties: new XamlProperties(), 
                contentProperty: null, 
                childrenProperty: null),
            //
            //
            //
            _xamlFactory.CreateControl(
                name: "Rectangle",
                properties: new XamlProperties(), 
                contentProperty: null, 
                childrenProperty: null),
            _xamlFactory.CreateControl(
                name: "Ellipse",
                properties: new XamlProperties(), 
                contentProperty: null, 
                childrenProperty: null),
            _xamlFactory.CreateControl(
                name: "Line",
                properties: new XamlProperties(), 
                contentProperty: null, 
                childrenProperty: null),
            _xamlFactory.CreateControl(
                name: "Path",
                properties: new XamlProperties(), 
                contentProperty: null, 
                childrenProperty: null),
            //
            _xamlFactory.CreateControl(
                name: "Image",
                properties: new XamlProperties(), 
                contentProperty: "Source", 
                childrenProperty: null),
            _xamlFactory.CreateControl(
                name: "PathIcon",
                properties: new XamlProperties(), 
                contentProperty: null, 
                childrenProperty: null),
            //
            _xamlFactory.CreateControl(
                name: "ScrollViewer",
                properties: new XamlProperties(), 
                contentProperty: "Content", 
                childrenProperty: "Content"),
        };
    }

    public Control? DemoStackPanel()
    {
        var xamlItem = _xamlFactory.CreateControl(
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
        var xamlItem = _xamlFactory.CreateControl(
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
        var xamlItemStyleRectangle = _xamlFactory.CreateStyle(
            name: "Style",
            properties: new XamlProperties
            {
                ["Selector"] = "Rectangle",
                ["Children"] = new XamlItems
                {
                    _xamlFactory.CreateSetter(
                        name: "Setter",
                        properties: new XamlProperties
                        {
                            ["Property"] = "Stroke",
                            ["Value"] = "Red"
                        }),
                    _xamlFactory.CreateSetter(
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

        var xamlItem = _xamlFactory.CreateControl(
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
