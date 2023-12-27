using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Avalonia;
using Avalonia.Controls;

namespace BoundsDemo;

public class ToolBoxViewModel
{
    private readonly Dictionary<Control, XamlItem> _controlsDictionary;
    private readonly XamlItemIdManager _idManager;

    public ToolBoxViewModel()
    {
        _controlsDictionary = new Dictionary<Control, XamlItem>();
        _idManager = new XamlItemIdManager();

        ToolBoxItems = new List<XamlItem>
        {
            //
            //
            //
            new(name: "TextBlock", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, object>
                {
                    ["Text"] = "TextBlock"
                }, 
                contentProperty: "Text", 
                childrenProperty: null),
            new(name: "Label", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, object>
                {
                    ["Content"] = "Label"
                }, 
                contentProperty: "Content", 
                childrenProperty: null),
            new(name: "TextBox", 
                id: _idManager.GetNewId(),
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
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, object>
                {
                    ["Content"] = "Button"
                }, 
                contentProperty: "Content", 
                childrenProperty: null),
            new(name: "CheckBox", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, object>
                {
                    ["Content"] = "CheckBox"
                }, 
                contentProperty: "Content", 
                childrenProperty: null),
            new(name: "RadioButton", 
                id: _idManager.GetNewId(),
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
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, object>(),
                contentProperty: "Child",
                childrenProperty: "Child"
            ),
            new(name: "Decorator",
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, object>
                {
                    ["Child"] = new XamlItem(
                        name: "Button", 
                        id: _idManager.GetNewId(),
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
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, object>(), 
                contentProperty: "Children", 
                childrenProperty: "Children"),
            new(name: "StackPanel",
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, object>
                {
                    ["Children"] = new List<XamlItem>
                    {
                        new(name: "TextBlock", 
                            id: _idManager.GetNewId(),
                            properties: new Dictionary<string, object>
                            {
                                ["Text"] = "TextBlock"
                            }, 
                            contentProperty: "Text", 
                            childrenProperty: null),
                        new(name: "TextBox", 
                            id: _idManager.GetNewId(),
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
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, object>(), 
                contentProperty: "Children", 
                childrenProperty: "Children"),
            new(name: "WrapPanel", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, object>(), 
                contentProperty: "Children", 
                childrenProperty: "Children"),
            new(name: "Grid", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, object>(), 
                contentProperty: "Children", 
                childrenProperty: "Children"),
            //
            //
            //
            new(name: "ItemsControl", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, object>(), 
                contentProperty: "Items", 
                childrenProperty: "Items"),
            new(name: "ListBox",
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, object>
                {
                    ["Items"] = new List<XamlItem>
                    {
                        new(name: "ListBoxItem", 
                            id: _idManager.GetNewId(),
                            properties: new Dictionary<string, object>
                            {
                                ["Content"] = "ListBoxItem 0"
                            }),
                        new(name: "ListBoxItem",
                            id: _idManager.GetNewId(), 
                            properties: new Dictionary<string, object>
                            {
                                ["Content"] = "ListBoxItem 1"
                            }),
                    }
                }, 
                contentProperty: "Items", 
                childrenProperty: "Items"),
            new(name: "ListBoxItem", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, object>(), 
                contentProperty: "Content", 
                childrenProperty: null),
            new(name: "ComboBox", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, object>(), 
                contentProperty: "ItemsSource", 
                childrenProperty: "Items"),
            new(name: "ComboBoxItem", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, object>(), 
                contentProperty: "Content", 
                childrenProperty: null),
            //
            //
            //
            new(name: "ProgressBar", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, object>(), 
                contentProperty: null, 
                childrenProperty: null),
            new(name: "Slider", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, object>(), 
                contentProperty: null, 
                childrenProperty: null),
            new(name: "DatePicker", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, object>(), 
                contentProperty: null, 
                childrenProperty: null),
            //
            //
            //
            new(name: "Rectangle", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, object>(), 
                contentProperty: null, 
                childrenProperty: null),
            new(name: "Ellipse", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, object>(), 
                contentProperty: null, 
                childrenProperty: null),
            new(name: "Line", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, object>(), 
                contentProperty: null, 
                childrenProperty: null),
            new(name: "Path", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, object>(), 
                contentProperty: null, 
                childrenProperty: null),
            //
            new(name: "Image", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, object>(), 
                contentProperty: "Source", 
                childrenProperty: null),
            new(name: "PathIcon", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, object>(), 
                contentProperty: null, 
                childrenProperty: null),
            //
            new(name: "ScrollViewer", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, object>(), 
                contentProperty: "Content", 
                childrenProperty: "Content"),
        };
    }

    public event EventHandler<EventArgs>? HoveredChanged;

    public event EventHandler<EventArgs>? SelectedChanged;

    public event EventHandler<EventArgs>? ControlAdded;

    public event EventHandler<EventArgs>? ControlRemoved;

    public event EventHandler<EventArgs>? PropertyValueChanged;

    public XamlItemIdManager IdManager => _idManager;
    
    public Visual? Hovered { get; set; }

    public Visual? Selected { get; set; }

    public XamlItem? RootXamlItem { get; set; }

    public List<XamlItem> ToolBoxItems { get; set; }

    public bool EnableEditing { get; set; }

    public void Hover(Visual? visual)
    {
        if (visual is null || visual != Selected)
        {
            Hovered = visual;
            OnHoveredChanged(EventArgs.Empty);
        }
    }

    public void Select(Visual? visual)
    {
        Hovered = null;
        OnHoveredChanged(EventArgs.Empty);
        Selected = visual;
        OnSelectedChanged(EventArgs.Empty);
    }

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
            OnPropertyValueChanged();
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

        XamlWriter.WriteXaml(xamlItem, writeXmlns: true, writeUid: false, sb, level: 0);

        var xaml = sb.ToString();

        Console.Clear();
        Console.WriteLine(xaml);
    }

    public Control Demo()
    {
        var xamlItem = new XamlItem(name: "StackPanel",
            id: _idManager.GetNewId(),
            properties: new Dictionary<string, object>
            {
                ["Children"] = new List<XamlItem>(),
                ["Background"] = "White",
                ["Width"] = "350",
                ["Height"] = "500",
            },
            contentProperty: "Children", 
            childrenProperty: "Children");

        var control = XamlItemControlFactory.CreateControl(xamlItem, isRoot: true, writeUid: true);

        RootXamlItem = xamlItem;
            
        AddControl(control, xamlItem);

        return control;
    }

    protected virtual void OnHoveredChanged(EventArgs e)
    {
        HoveredChanged?.Invoke(this, e);
    }

    protected virtual void OnSelectedChanged(EventArgs e)
    {
        SelectedChanged?.Invoke(this, e);
    }

    protected virtual void OnControlAdded()
    {
        ControlAdded?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void OnControlRemoved()
    {
        ControlRemoved?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void OnPropertyValueChanged()
    {
        PropertyValueChanged?.Invoke(this, EventArgs.Empty);
    }
}
