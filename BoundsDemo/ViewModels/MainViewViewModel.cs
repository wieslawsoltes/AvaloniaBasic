using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Threading;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace BoundsDemo;

public class MainViewViewModel : ReactiveObject
{
    private readonly EditorCanvasView _editorCanvas;
    private readonly ApplicationService _applicationService;
    private readonly XamlEditorViewModel _xamlEditorViewModel;
    private readonly XamlSelectionViewModel _xamlSelectionViewModel;

    public MainViewViewModel(EditorCanvasView editorCanvas)
    {
        _editorCanvas = editorCanvas;
        _applicationService = new ApplicationService();

        _xamlEditorViewModel = new XamlEditorViewModel();
        _xamlSelectionViewModel = new XamlSelectionViewModel(_xamlEditorViewModel);

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

    public ICommand NewCommand { get; }

    public ICommand OpenCommand { get; }
    
    public ICommand SaveCommand { get; }

    public ICommand CopyAsXamlCommand { get; }

    public ICommand CopyAsSvgCommand { get; }

    public ICommand PlayCommand { get; }

    public ICommand StopCommand { get; }

    [Reactive] public bool IsPlaying { get; set; }

    public List<XamlItem> ToolBoxItems { get; set; }

    public XamlEditorViewModel XamlEditorViewModel => _xamlEditorViewModel;
 
    public XamlSelectionViewModel XamlSelectionViewModel => _xamlSelectionViewModel;

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

    private void New()
    {
        // var control = DemoStackPanel();
        // var control = DemoDockPanel();
        var control = DemoCanvas();
        if (control is not null)
        {
            _xamlEditorViewModel.CanvasViewModel?.AddRoot(control);
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
            await Dispatcher.UIThread.InvokeAsync(() => _xamlEditorViewModel.Reload(xamlItem));
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
        if (_xamlEditorViewModel.RootXamlItem is null)
        {
            return;
        }

        await JsonSerializer.SerializeAsync(
            stream, 
            _xamlEditorViewModel.RootXamlItem, 
            XamlItemJsonContext.s_instance.XamlItem);
    }

    private async Task CopyAsXamlAsync()
    {
        if (_xamlEditorViewModel.RootXamlItem is null)
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

            XamlService.WriteXaml(_xamlEditorViewModel.RootXamlItem, settings);

            return settings.Writer.ToString();
        });

        await _applicationService.SetClipboardTextAsync(xaml);
    }

    private async Task CopyAsSvgAsync()
    {
        if (_xamlEditorViewModel.RootXamlItem is null)
        {
            return;
        }

        var control = _xamlEditorViewModel.LoadForExport(_xamlEditorViewModel.RootXamlItem);
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
