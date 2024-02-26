using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using Avalonia;
using Avalonia.Controls;
using Avalonia.LogicalTree;
using Avalonia.VisualTree;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace FormsBuilder;

public class XamlEditor : ReactiveObject, IXamlEditor
{
    private readonly Dictionary<Control, XamlItem> _controlsDictionary;
    private readonly IXamlItemIdManager _idManager;

    public XamlEditor()
    {
        _controlsDictionary = new Dictionary<Control, XamlItem>();
        _idManager = new XamlItemIdManager();
    }

    public event EventHandler<EventArgs>? PropertyValueChanged;

    public event EventHandler<EventArgs>? ControlAdded;

    public event EventHandler<EventArgs>? ControlRemoved;

    [Reactive]
    public XamlItem? RootXamlItem { get; private set; }

    [Reactive]
    public bool EnableEditing { get; set; }

    [Reactive]
    public ICanvasEditor? CanvasViewModel { get; set; }

    public IXamlItemIdManager IdManager => _idManager;

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

    private void CleanControls()
    {
        _controlsDictionary.Clear();
    }
    
    private void AddControls(Control control, XamlItem xamlItem)
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

    public void InsertXamlItem(XamlItem targetXamlItem, XamlItem xamlItem, Point position, bool enableCallback)
    {
        // TODO: Add xamlItem to targetXamlItem Children
        // TODO: Add xamlItem as targetXamlItem Content
        // TODO: After adding to Children or as Content build entire tree independently.

        if (targetXamlItem.ChildrenProperty is not null)
        {
            if (enableCallback)
            {
                InsertCallback(xamlItem, position, targetXamlItem);
            }

            if (targetXamlItem.TryAddChild(xamlItem))
            {
                Reload(RootXamlItem);
                Debug(targetXamlItem);
                return;
            }
        }

        if (targetXamlItem.ContentProperty is not null)
        {
            if (targetXamlItem.TrySetContent(new XamlItemXamlValue(xamlItem)))
            {
                Reload(RootXamlItem);
                Debug(targetXamlItem);
                return;
            }
        }
    }

    private void InsertCallback(XamlItem xamlItem, Point position, XamlItem targetXamlItem)
    {
        // TODO: Add callback service for XamlItem to position inserted item in target.
        if (targetXamlItem.Name == "Canvas")
        {
            xamlItem.Properties["Canvas.Left"] = position.X;
            xamlItem.Properties["Canvas.Top"] = position.Y;
        }
    }

    public bool RemoveXamlItem(XamlItem xamlItem)
    {
        var rooXamlItem = RootXamlItem;
        if (rooXamlItem is null)
        {
            return false;
        }

        if (rooXamlItem == xamlItem)
        {
            // TODO: Remove root.
            return false;
        }

        return rooXamlItem.TryRemove(xamlItem);
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

    public void UpdatePropertyValue(Control control, string propertyName, XamlValue propertyValue)
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
   
    public Control? LoadForDesign(XamlItem xamlItem)
    {
        var control = XamlItemControlFactory.CreateControl(xamlItem, isRoot: true, writeUid: true);

        if (control is null)
        {
            return null;
        }

        CleanControls();

        RootXamlItem = xamlItem;

        AddControls(control, xamlItem);

        // AddControl(control, xamlItem);

        return control;
    }

    public Control? LoadForExport(XamlItem xamlItem)
    {
        return XamlItemControlFactory.CreateControl(xamlItem, isRoot: true, writeUid: false);
    }

    public void Reload(XamlItem rooXamlItem)
    {
        var control = LoadForDesign(rooXamlItem);
        if (control is not null)
        {
            CanvasViewModel?.AddToRoot(control);
        }
    }

    public Control? HitTest(IEnumerable<Visual> descendants, Point position, HashSet<Visual> ignored)
    {
        var visuals = descendants
            .OfType<Control>()
            .Where(visual =>
            {
                if (!Contains(visual))
                {
                    return false;
                }

                if (ignored.Contains(visual))
                {
                    return false;
                }

                var transformedBounds = visual.GetTransformedBounds();
                return transformedBounds is not null
                       && transformedBounds.Value.Contains(position);

            });

        return visuals.Reverse().FirstOrDefault();

        bool Contains(Control visual)
        {
            return TryGetXamlItem(visual, out _);
        }
    }

    public Control? HitTest(ILogical root, Point position, HashSet<Visual> ignored)
    {
        var descendants = root.GetLogicalDescendants().Cast<Visual>();

        return HitTest(descendants, position, ignored);
    }

    public void Debug(XamlItem xamlItem)
    {
#if DEBUG
        var settings = new XamlWriterSettings
        {
            Writer = new StringBuilder(),
            Namespace = "https://github.com/avaloniaui",
            WriteXmlns = true,
            WriteUid = false,
            Level = 0,
            WriteAttributesOnNewLine = false
        };

        var xamlWriter = new XamlWriter();
        xamlWriter.Write(xamlItem, settings);

        var xaml = settings.Writer.ToString();

        if (!Design.IsDesignMode)
        {
            Console.Clear();
            Console.WriteLine(xaml);
        }

        // var json = SerializeXamlItem(xamlItem);
        // Console.WriteLine(json);
        // var newXamlItem = DeserializeXamlItem(json);
#endif
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
}
