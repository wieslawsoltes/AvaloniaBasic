using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Threading;
using Avalonia.VisualTree;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace BoundsDemo;

public class MainViewViewModel : ReactiveObject
{
    private readonly EditorCanvasView _editorCanvas;
    private readonly ApplicationService _applicationService;
    private readonly Dictionary<Control, XamlItem> _controlsDictionary;
    private readonly XamlItemIdManager _idManager;

    public MainViewViewModel(EditorCanvasView editorCanvas)
    {
        _editorCanvas = editorCanvas;
        _applicationService = new ApplicationService();
        _controlsDictionary = new Dictionary<Control, XamlItem>();
        _idManager = new XamlItemIdManager();

        NewCommand = ReactiveCommand.Create(New);
        OpenCommand = ReactiveCommand.CreateFromTask(OpenAsync);
        SaveCommand = ReactiveCommand.CreateFromTask(SaveAsync);
        CopyAsXamlCommand = ReactiveCommand.CreateFromTask(CopyAsXamlAsync);
        CopyAsSvgCommand = ReactiveCommand.CreateFromTask(CopyAsSvgAsync);
        PlayCommand = ReactiveCommand.Create(Play);
        StopCommand = ReactiveCommand.Create(Stop);

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
            //
            //
            //
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
            new(name: "TabControl",
                id: _idManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>
                {
                    ["Items"] = (XamlValue) new List<XamlItem>
                    {
                        new(name: "TabItem", 
                            id: _idManager.GetNewId(),
                            properties: new Dictionary<string, XamlValue>
                            {
                                ["Content"] = (XamlValue) "TabItem 0",
                                ["Header"] = (XamlValue) "TabItem 0"
                            }),
                        new(name: "TabItem",
                            id: _idManager.GetNewId(), 
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
                id: _idManager.GetNewId(),
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

    public ICommand NewCommand { get; }

    public ICommand OpenCommand { get; }
    
    public ICommand SaveCommand { get; }

    public ICommand CopyAsXamlCommand { get; }

    public ICommand CopyAsSvgCommand { get; }

    public ICommand PlayCommand { get; }

    public ICommand StopCommand { get; }

    [Reactive] public bool IsPlaying { get; set; }

    public event EventHandler<EventArgs>? HoveredChanged;

    public event EventHandler<EventArgs>? SelectedChanged;

    public event EventHandler<EventArgs>? ControlAdded;

    public event EventHandler<EventArgs>? ControlRemoved;

    public event EventHandler<EventArgs>? PropertyValueChanged;

    public XamlItemIdManager IdManager => _idManager;
    
    public Visual? Hovered { get; set; }

    public HashSet<Visual> Selected { get; set; }

    public bool DrawSelection { get; set; }

    public Point StartPoint { get; set; }
    
    public Point EndPoint { get; set; }

    public XamlItem? RootXamlItem { get; set; }

    public List<XamlItem> ToolBoxItems { get; set; }

    public bool EnableEditing { get; set; }

    public CanvasViewModel? CanvasViewModel { get; set; }

    public void Hover(Visual? visual)
    {
        if (visual is null || !Selected.Contains(visual))
        {
            Hovered = visual;
            OnHoveredChanged(EventArgs.Empty);
        }
    }

    public void Select(IEnumerable<Visual>? visuals)
    {
        Hovered = null;
        OnHoveredChanged(EventArgs.Empty);
        Selected = visuals is null ? new HashSet<Visual>() : new HashSet<Visual>(visuals);
        OnSelectedChanged(EventArgs.Empty);
    }

    public void Selection(Point startPoint, Point endPoint)
    {
        // TODO:
        DrawSelection = true;
        StartPoint = startPoint;
        EndPoint = endPoint;
    }

    public void ClearSelection()
    {
        DrawSelection = false;
    }

    public Rect GetSelectionRect()
    {
        var topLeft = new Point(
            Math.Min(StartPoint.X, EndPoint.X),
            Math.Min(StartPoint.Y, EndPoint.Y));
        var bottomRight = new Point(
            Math.Max(StartPoint.X, EndPoint.X),
            Math.Max(StartPoint.Y, EndPoint.Y));
        return new Rect(topLeft, bottomRight);
    }

    private void AddControl(Control control, XamlItem xamlItem)
    {
        _controlsDictionary[control] = xamlItem;
        OnControlAdded();
    }

    private void RemoveControl(Control control)
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

    public void InsertXamlItem(Control target, Control control, XamlItem xamlItem)
    {
        if (!TryGetXamlItem(target, out var targetXamlItem))
        {
            RemoveControl(control);
            return;
        }

        if (targetXamlItem is null)
        {
            // TODO: Set xamlItem as root and build visual tree.
            return;
        }

        // TODO: Add xamlItem to targetXamlItem Children
        // TODO: Add xamlItem as targetXamlItem Content
        // TODO: After adding to Children or as Content build entire tree independently.

        if (targetXamlItem.ChildrenProperty is not null)
        {
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

        RemoveControl(control);
 
        /*
        if (target is Panel panel)
        {
            if (targetXamlItem.ChildrenProperty is not null)
            {
                panel.Children.Add(control);

                targetXamlItem.TryAddChild(xamlItem);
                AddControls(control, xamlItem);

                Debug(targetXamlItem);
            }
        }
        else if (target is ItemsControl itemsControl)
        {
            if (targetXamlItem.ChildrenProperty is not null)
            {
                itemsControl.Items.Add(control);

                targetXamlItem.TryAddChild(xamlItem);
                AddControls(control, xamlItem);

                Debug(targetXamlItem);
            }
        }
        else if (target is ContentControl contentControl)
        {
            if (targetXamlItem.ContentProperty is not null)
            {
                contentControl.Content = control;

                targetXamlItem.TrySetContent(new XamlItemXamlValue(xamlItem));
                AddControls(control, xamlItem);

                Debug(targetXamlItem);
            }
        }
        else
        {
            RemoveControl(control);
        }
        */
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

    private List<XamlItem>? _xamlItemsCopy;

    private void CreateSelectedCopy()
    {
        var selected = Selected.ToList();
        var xamlItems = new List<XamlItem>();

        foreach (var visual in selected)
        {
            if (visual is not Control control)
            {
                continue;
            }

            if (!TryGetXamlItem(control, out var xamlItem) || xamlItem is null)
            {
                continue;
            }

            var xamlItemCopy = XamlItemFactory.Clone(xamlItem, IdManager);

            xamlItems.Add(xamlItemCopy);
        }

        _xamlItemsCopy = xamlItems;
    }

    public void CopySelected()
    {
        CreateSelectedCopy();
    }

    public void CutSelected()
    {
        CreateSelectedCopy();
        RemoveSelected();
    }

    public void PasteSelected()
    {
        if (_xamlItemsCopy is null || _xamlItemsCopy.Count <= 0)
        {
            return;
        }

        if (Selected.Count > 1)
        {
            return;
        }

        var targetXamlItem = default(XamlItem);
        
        if (Selected.Count == 0)
        {
            targetXamlItem = RootXamlItem;
        }
        else if (Selected.Count == 1)
        {
            if (Selected.First() is not Control control)
            {
                return;
            }

            if (!TryGetXamlItem(control, out var xamlItem) || xamlItem is null)
            {
                return;
            }

            targetXamlItem = xamlItem;
        }

        if (targetXamlItem is null)
        {
            return;
        }

        foreach (var xamlItem in _xamlItemsCopy)
        {
            var xamlItemCopy = XamlItemFactory.Clone(xamlItem, IdManager);
            
            if (targetXamlItem.ChildrenProperty is not null)
            {
                if (targetXamlItem.TryAddChild(xamlItemCopy))
                {
                    Debug(targetXamlItem);
                }
            }
            else
            {
                // TODO: Add different hot-key for Content paste.
                if (targetXamlItem.ContentProperty is not null)
                {
                    if (targetXamlItem.TrySetContent(new XamlItemXamlValue(xamlItemCopy)))
                    {
                        Debug(targetXamlItem);
                    }
                }
            }

        }

        Reload(RootXamlItem);
    }

    public void RemoveSelected()
    {
        if (Selected.Count <= 0)
        {
            return;
        }

        var selected = Selected.ToList();

        var haveToReload = false;

        foreach (var visual in selected)
        {
            if (visual is not Control control)
            {
                continue;
            }

            if (!TryGetXamlItem(control, out var xamlItem) || xamlItem is null)
            {
                continue;
            }

            if (RemoveXamlItem(xamlItem))
            {
                haveToReload = true;
            }
        }

        if (haveToReload)
        {
            Reload(RootXamlItem);
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
        var settings = new XamlServiceSettings
        {
            Writer = new StringBuilder(),
            Namespace = "https://github.com/avaloniaui",
            WriteXmlns = true,
            WriteUid = false,
            Level = 0,
            WriteAttributesOnNewLine = false
        };

        XamlService.WriteXaml(xamlItem, settings);

        var xaml = settings.Writer.ToString();

        Console.Clear();
        Console.WriteLine(xaml);

        // var json = SerializeXamlItem(xamlItem);
        // Console.WriteLine(json);
        // var newXamlItem = DeserializeXamlItem(json);
    }

    public Control? DemoStackPanel()
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

        return LoadForDesign(xamlItem);
    }

    public Control? DemoPanelDockPanel()
    {
        var xamlItem = new XamlItem(name: "DockPanel",
            id: _idManager.GetNewId(),
            properties: new Dictionary<string, XamlValue>
            {
                ["Children"] = (XamlValue) new List<XamlItem>(),
                ["Background"] = (XamlValue) "White",
                ["Width"] = (XamlValue) "450",
                ["Height"] = (XamlValue) "500",
            },
            contentProperty: "Children", 
            childrenProperty: "Children");

        return LoadForDesign(xamlItem);
    }

    public void Reload(XamlItem rooXamlItem)
    {
        var control = LoadForDesign(rooXamlItem);
        if (control is not null)
        {
            CanvasViewModel?.AddRoot(control);
        }
    }

    private Control? LoadForDesign(XamlItem xamlItem)
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

    private Control? LoadForExport(XamlItem xamlItem)
    {
        return XamlItemControlFactory.CreateControl(xamlItem, isRoot: true, writeUid: false);
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

    private void New()
    {
        var control = DemoStackPanel();
        if (control is not null)
        {
            CanvasViewModel?.AddRoot(control);
        }
    }

    private async Task OpenAsync()
    {
        await _applicationService.OpenFileAsync(
            OpenCallbackAsync, 
            new List<string>(new[] { "Json", "All" }), 
            "Open");
    }

    private async Task OpenCallbackAsync(Stream stream)
    {
        var xamlItem = await JsonSerializer.DeserializeAsync(
            stream, 
            XamlItemJsonContext.s_instance.XamlItem);
        if (xamlItem is { })
        {
            await Dispatcher.UIThread.InvokeAsync(() => Reload(xamlItem));
        }
    }

    private async Task SaveAsync()
    {
        await _applicationService.SaveFileAsync(
            SaveCallbackAsync, 
            new List<string>(new[] { "Json", "All" }), 
            "Save", 
            "form", 
            "json");
    }
    
    private async Task SaveCallbackAsync(Stream stream)
    {
        if (RootXamlItem is null)
        {
            return;
        }

        await JsonSerializer.SerializeAsync(
            stream, 
            RootXamlItem, 
            XamlItemJsonContext.s_instance.XamlItem);
    }

    private async Task CopyAsXamlAsync()
    {
        if (RootXamlItem is null)
        {
            return;
        }

        var xaml = await Task.Run(() =>
        {
            var settings = new XamlServiceSettings
            {
                Writer = new StringBuilder(),
                Namespace = "https://github.com/avaloniaui",
                WriteXmlns = false,
                WriteUid = false,
                Level = 0,
                WriteAttributesOnNewLine = false
            };

            XamlService.WriteXaml(RootXamlItem, settings);

            return settings.Writer.ToString();
        });

        await _applicationService.SetClipboardTextAsync(xaml);
    }

    private async Task CopyAsSvgAsync()
    {
        if (RootXamlItem is null)
        {
            return;
        }

        var control = LoadForExport(RootXamlItem);
        if (control is null)
        {
            return;
        }

        var size = new Size(350, 500);
        using var stream = new MemoryStream();
        await RenderingService.RenderAsSvg(stream, size, control);
        var bytes = stream.ToArray();
        var svg = Encoding.UTF8.GetString(bytes);

        await _applicationService.SetClipboardTextAsync(svg);
    }

    private void Play()
    {
        IsPlaying = true;
        _editorCanvas.RootPanel.IsHitTestVisible = true;
    }

    private void Stop()
    {
        IsPlaying = false;
        _editorCanvas.RootPanel.IsHitTestVisible = false;
    }
}
