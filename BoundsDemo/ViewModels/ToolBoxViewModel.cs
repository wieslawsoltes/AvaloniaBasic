using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Avalonia.Controls;

namespace BoundsDemo;

public class ToolBoxViewModel
{
    private readonly Dictionary<Control, XamlItem> _controlsDictionary;

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

        _controlsDictionary = new Dictionary<Control, XamlItem>();
    }

    public event EventHandler<EventArgs>? ControlAdded;

    public event EventHandler<EventArgs>? ControlRemoved;

    public XamlItem? RootXamlItem { get; set; }

    public List<XamlItem> ToolBoxItems { get; set; }

    public bool EnableEditing { get; set; }

    public void AddControl(Control control, XamlItem xamlItem)
    {
        _controlsDictionary[control] = xamlItem;
        OnControlAdded();
    }

    public void RemoveControl(Control control)
    {
        _controlsDictionary.Remove(control);
        OnControlRemoved();
    }

    public bool TryGetXamlItem(Control control, out XamlItem? xamlItem)
    {
        return _controlsDictionary.TryGetValue(control, out xamlItem);
    }

    public bool TryGetControl(XamlItem xamlItem, out Control? control)
    {
        control = _controlsDictionary.FirstOrDefault(x => x.Value == xamlItem).Key;
        return control is not null;
    }

    public void UpdatePropertyValue(Control control, string propertyName, string propertyValue)
    {
        if (!EnableEditing)
        {
            return;
        }

        if (TryGetXamlItem(control, out var xamlItem))
        {
            xamlItem.Properties[propertyName] = propertyValue;

#if DEBUG
            // TODO:
            // Debug(xamlItem);
            Debug(RootXamlItem);
#endif
        }
    }

    public void Debug(XamlItem xamlItem)
    {
        var sb = new StringBuilder();

        XamlWriter.WriteXaml(xamlItem, writeXmlns: true, sb, level: 0);

        var xaml = sb.ToString();

        Console.Clear();
        Console.WriteLine(xaml);
    }

    public Control Demo()
    {
        var xamlItem = new XamlItem(name: "StackPanel",
            properties: new Dictionary<string, object>
            {
                ["Children"] = new List<XamlItem>(),
                ["Background"] = "White",
                ["Width"] = "350",
                ["Height"] = "500",
            },
            contentProperty: "Children", 
            childrenProperty: "Children");

        var control = XamlItemControlFactory.CreateControl(xamlItem);

        RootXamlItem = xamlItem;
            
        AddControl(control, xamlItem);

        return control;
    }

    protected virtual void OnControlAdded()
    {
        ControlAdded?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void OnControlRemoved()
    {
        ControlRemoved?.Invoke(this, EventArgs.Empty);
    }
}
