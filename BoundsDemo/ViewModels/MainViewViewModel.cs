using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using Avalonia;
using Avalonia.Controls;
using Avalonia.VisualTree;

namespace BoundsDemo;

public class MainViewViewModel
{
    private readonly EditorCanvasView _editorCanvas;
    private readonly Dictionary<Control, XamlItem> _controlsDictionary;
    private readonly XamlItemIdManager _idManager;

    public MainViewViewModel()
    public MainViewViewModel(EditorCanvasView editorCanvas)
    {
        _editorCanvas = editorCanvas;
        _controlsDictionary = new Dictionary<Control, XamlItem>();
        _idManager = new XamlItemIdManager();

        ToolBoxItems = new List<XamlItem>
        {
            //
            //
            //
            new(name: "TextBlock", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>
                {
                    ["Text"] = (XamlValue) "TextBlock"
                }, 
                contentProperty: "Text", 
                childrenProperty: null),
            new(name: "Label", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>
                {
                    ["Content"] = (XamlValue) "Label"
                }, 
                contentProperty: "Content", 
                childrenProperty: null),
            new(name: "TextBox", 
                id: _idManager.GetNewId(),
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
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>
                {
                    ["Content"] = (XamlValue) "Button"
                }, 
                contentProperty: "Content", 
                childrenProperty: null),
            new(name: "CheckBox", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>
                {
                    ["Content"] = (XamlValue) "CheckBox"
                }, 
                contentProperty: "Content", 
                childrenProperty: null),
            new(name: "RadioButton", 
                id: _idManager.GetNewId(),
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
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(),
                contentProperty: "Child",
                childrenProperty: "Child"
            ),
            new(name: "Decorator",
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>
                {
                    ["Child"] = (XamlValue) new XamlItem(
                        name: "Button", 
                        id: _idManager.GetNewId(),
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
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(), 
                contentProperty: "Children", 
                childrenProperty: "Children"),
            new(name: "StackPanel",
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>
                {
                    ["Children"] = (XamlValue) new List<XamlItem>
                    {
                        new(name: "TextBlock", 
                            id: _idManager.GetNewId(),
                            properties: new Dictionary<string, XamlValue>
                            {
                                ["Text"] = (XamlValue) "TextBlock"
                            }, 
                            contentProperty: "Text", 
                            childrenProperty: null),
                        new(name: "TextBox", 
                            id: _idManager.GetNewId(),
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
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(), 
                contentProperty: "Children", 
                childrenProperty: "Children"),
            new(name: "WrapPanel", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(), 
                contentProperty: "Children", 
                childrenProperty: "Children"),
            new(name: "Grid", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(), 
                contentProperty: "Children", 
                childrenProperty: "Children"),
            //
            //
            //
            new(name: "ItemsControl", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(), 
                contentProperty: "Items", 
                childrenProperty: "Items"),
            new(name: "ListBox",
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>
                {
                    ["Items"] = (XamlValue) new List<XamlItem>
                    {
                        new(name: "ListBoxItem", 
                            id: _idManager.GetNewId(),
                            properties: new Dictionary<string, XamlValue>
                            {
                                ["Content"] = (XamlValue) "ListBoxItem 0"
                            }),
                        new(name: "ListBoxItem",
                            id: _idManager.GetNewId(), 
                            properties: new Dictionary<string, XamlValue>
                            {
                                ["Content"] = (XamlValue) "ListBoxItem 1"
                            }),
                    }
                }, 
                contentProperty: "Items", 
                childrenProperty: "Items"),
            new(name: "ListBoxItem", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(), 
                contentProperty: "Content", 
                childrenProperty: null),
            new(name: "ComboBox", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(), 
                contentProperty: "ItemsSource", 
                childrenProperty: "Items"),
            new(name: "ComboBoxItem", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(), 
                contentProperty: "Content", 
                childrenProperty: null),
            //
            //
            //
            new(name: "ProgressBar", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(), 
                contentProperty: null, 
                childrenProperty: null),
            new(name: "Slider", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(), 
                contentProperty: null, 
                childrenProperty: null),
            new(name: "DatePicker", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(), 
                contentProperty: null, 
                childrenProperty: null),
            //
            //
            //
            new(name: "Rectangle", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(), 
                contentProperty: null, 
                childrenProperty: null),
            new(name: "Ellipse", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(), 
                contentProperty: null, 
                childrenProperty: null),
            new(name: "Line", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(), 
                contentProperty: null, 
                childrenProperty: null),
            new(name: "Path", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(), 
                contentProperty: null, 
                childrenProperty: null),
            //
            new(name: "Image", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(), 
                contentProperty: "Source", 
                childrenProperty: null),
            new(name: "PathIcon", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(), 
                contentProperty: null, 
                childrenProperty: null),
            //
            new(name: "ScrollViewer", 
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>(), 
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

    public void AddControls(Control control, XamlItem xamlItem)
    {
        var xamlItemsMap = xamlItem
            .GetSelfAndChildren()
            .ToDictionary(x => x.Id, x => x);

        var controlsMap = control
            .GetSelfAndVisualDescendants()
            .Where(x => x is Control)
            .Cast<Control>()
            .Select(x => new 
            {
                Uid = XamlItemProperties.GetUid(x), 
                Control = x
            })
            .Where(x => x.Uid is not null)
            .ToDictionary(x => x.Uid, x => x.Control);

        foreach (var kvpXamlItem in xamlItemsMap)
        {
            if (controlsMap.TryGetValue(kvpXamlItem.Key, out var controlValue))
            {
                AddControl(controlValue, kvpXamlItem.Value);
            }
        }
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
            xamlItem.Properties[propertyName] = (XamlValue) propertyValue;
            OnPropertyValueChanged();
#if DEBUG
            // TODO:
            // Debug(xamlItem);
            Debug(RootXamlItem);
#endif
        }
    }

    private XamlItem? DeserializeXamlItem(string json)
    {
        return JsonSerializer.Deserialize(
            json, 
            XamlItemJsonContext.s_instance.XamlItem);
    }

    private string? SerializeXamlItem(XamlItem xamlItem)
    {
        return JsonSerializer.Serialize(
            xamlItem, 
            XamlItemJsonContext.s_instance.XamlItem);
    }

    public void Debug(XamlItem xamlItem)
    {
        var sb = new StringBuilder();

        XamlWriter.WriteXaml(xamlItem, writeXmlns: true, writeUid: false, sb, level: 0);

        var xaml = sb.ToString();

        Console.Clear();
        Console.WriteLine(xaml);

        
        var json = SerializeXamlItem(xamlItem);
        Console.WriteLine(json);

        var newXamlItem = DeserializeXamlItem(json);
        
    }

    public Control Demo()
    {
        var xamlItem = new XamlItem(name: "StackPanel",
            id: _idManager.GetNewId(),
            properties: new Dictionary<string, XamlValue>
            {
                ["Children"] = (XamlValue) new List<XamlItem>(),
                ["Background"] = (XamlValue) "White",
                ["Width"] = (XamlValue) "350",
                ["Height"] = (XamlValue) "500",
            },
            contentProperty: "Children", 
            childrenProperty: "Children");

        var control = XamlItemControlFactory.CreateControl(xamlItem, isRoot: true, writeUid: true);

        if (control is not null)
        {
            RootXamlItem = xamlItem;

            AddControls(control, xamlItem);

            // AddControl(control, xamlItem);
        }
        
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
