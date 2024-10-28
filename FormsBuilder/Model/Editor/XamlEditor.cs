using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using XamlDom;

namespace FormsBuilder;

public class XamlEditor<T> : ReactiveObject, IXamlEditor<T> where T : class
{
    private readonly Dictionary<T, XamlItem> _controlsDictionary;
    private readonly IXamlObjectFactory _xamlObjectFactory;
    private readonly IControlMap<T> _controlMap;

    public XamlEditor(IXamlObjectFactory xamlObjectFactory, IControlMap<T> controlMap)
    {
        _controlsDictionary = new Dictionary<T, XamlItem>();
        _xamlObjectFactory = xamlObjectFactory;
        _controlMap = controlMap;
    }

    public event EventHandler<EventArgs>? PropertyValueChanged;

    public event EventHandler<EventArgs>? ControlAdded;

    public event EventHandler<EventArgs>? ControlRemoved;

    [Reactive]
    public XamlItem? RootXamlItem { get; private set; }

    [Reactive]
    public bool EnableEditing { get; set; }

    [Reactive]
    public ICanvasEditor<T>? CanvasViewModel { get; set; }

    public void AddControl(T control, XamlItem xamlItem)
    {
        _controlsDictionary[control] = xamlItem;
        OnControlAdded();
    }

    public void RemoveControl(T control)
    {
        _controlsDictionary.Remove(control);
        OnControlRemoved();
    }

    private void CleanControls()
    {
        _controlsDictionary.Clear();
    }
    
    private void AddControls(T control, XamlItem xamlItem)
    {
        var xamlItemsMap = _controlMap.CreateMap(xamlItem);

        var controlsMap = _controlMap.CreateMap(control);

        foreach (var kvpXamlItem in xamlItemsMap)
        {
            if (controlsMap.TryGetValue(kvpXamlItem.Key, out var controlValue))
            {
                AddControl(controlValue, kvpXamlItem.Value);
            }
        }
    }

    public void InsertXamlItem(XamlItem targetXamlItem, XamlItem xamlItem, double x, double y, bool enableCallback)
    {
        // TODO: Add xamlItem to targetXamlItem Children
        // TODO: Add xamlItem as targetXamlItem Content
        // TODO: After adding to Children or as Content build entire tree independently.

        if (targetXamlItem.ChildrenProperty is not null)
        {
            if (enableCallback)
            {
                InsertCallback(xamlItem, x, y, targetXamlItem);
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
            }
        }
    }

    private void InsertCallback(XamlItem xamlItem, double x, double y, XamlItem targetXamlItem)
    {
        // TODO: Add callback service for XamlItem to position inserted item in target.
        if (targetXamlItem.Name == "Canvas")
        {
            xamlItem.Properties["Canvas.Left"] = x;
            xamlItem.Properties["Canvas.Top"] = y;
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

    public bool TryGetXamlItem(T control, out XamlItem? xamlItem)
    {
        return _controlsDictionary.TryGetValue(control, out xamlItem);
    }

    public bool TryGetControl(XamlItem xamlItem, out T? control)
    {
        control = _controlsDictionary.FirstOrDefault(x => x.Value == xamlItem).Key;
        return control is not null;
    }

    public void UpdatePropertyValue(T control, string propertyName, XamlValue propertyValue)
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
   
    public T? LoadForDesign(XamlItem xamlItem)
    {
        var control = _xamlObjectFactory.CreateControl(xamlItem, isRoot: true, writeUid: true) as T;

        if (control is null)
        {
            return default;
        }

        CleanControls();

        RootXamlItem = xamlItem;

        AddControls(control, xamlItem);

        // AddControl(control, xamlItem);

        return control;
    }

    public T? LoadForExport(XamlItem xamlItem)
    {
        return _xamlObjectFactory.CreateControl(xamlItem, isRoot: true, writeUid: false) as T;
    }

    public void Reload(XamlItem rooXamlItem)
    {
        var control = LoadForDesign(rooXamlItem);
        if (control is not null)
        {
            CanvasViewModel?.AddToRoot(control);
        }
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

        // TOOD: if (!Design.IsDesignMode)
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
