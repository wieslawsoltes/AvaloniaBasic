using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Threading;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace FormsBuilder;

public class MainViewViewModel : ReactiveObject
{
    private readonly EditorCanvasView _editorCanvas;
    private readonly ApplicationService _applicationService;
    private readonly IXamlEditorViewModel _xamlEditorViewModel;
    private readonly IXamlSelectionViewModel _xamlSelectionViewModel;
    private readonly Demos _demos;

    public MainViewViewModel(EditorCanvasView editorCanvas)
    {
        _editorCanvas = editorCanvas;
        _applicationService = new ApplicationService();

        _xamlEditorViewModel = new XamlEditorViewModel();
        _xamlSelectionViewModel = new XamlSelectionViewModel(_xamlEditorViewModel);

        _demos = new Demos(_xamlEditorViewModel);

        ToolBoxItems = _demos.DemoToolBox();

        NewCommand = ReactiveCommand.Create(New);
        OpenCommand = ReactiveCommand.CreateFromTask(OpenAsync);
        SaveCommand = ReactiveCommand.CreateFromTask(SaveAsync);
        CopyAsXamlCommand = ReactiveCommand.CreateFromTask(CopyAsXamlAsync);
        CopyAsSvgCommand = ReactiveCommand.CreateFromTask(CopyAsSvgAsync);
        PlayCommand = ReactiveCommand.Create(Play);
        StopCommand = ReactiveCommand.Create(Stop);
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

    public IXamlEditorViewModel XamlEditorViewModel => _xamlEditorViewModel;
 
    public IXamlSelectionViewModel XamlSelectionViewModel => _xamlSelectionViewModel;

    public Demos Demos => _demos;

    private void New()
    {
        // TODO:
        // var control = _demos.DemoStackPanel();
        // var control = _demos.DemoDockPanel();
        var control = _demos.DemoCanvas();
        if (control is not null)
        {
            _xamlEditorViewModel.CanvasViewModel?.AddToRoot(control);
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
