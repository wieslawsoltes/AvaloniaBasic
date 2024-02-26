using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Threading;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace FormsBuilder;

public interface IToolboxXamlItemProvider
{
    ObservableCollection<XamlItem> ToolBoxItems { get; set; }

    public XamlItem? SelectedToolBoxItem { get; set; }
}

public class MainViewViewModel : ReactiveObject, IToolboxXamlItemProvider
{
    private readonly EditorCanvasView _editorCanvas;
    private readonly ApplicationService _applicationService;
    private readonly IXamlEditor _xamlEditor;
    private readonly IXamlSelection _xamlSelection;
    private readonly Demos _demos;

    public MainViewViewModel(EditorCanvasView editorCanvas)
    {
        _editorCanvas = editorCanvas;
        _applicationService = new ApplicationService();

        _xamlEditor = new XamlEditor();
        _xamlSelection = new XamlSelection(_xamlEditor, () => OverlayService?.Invalidate());

        _demos = new Demos(_xamlEditor);

        ToolBoxItems = _demos.DemoToolBox();
        SelectedToolBoxItem = ToolBoxItems.FirstOrDefault();

        NewCommand = ReactiveCommand.Create(New);
        OpenCommand = ReactiveCommand.CreateFromTask(OpenAsync);
        SaveCommand = ReactiveCommand.CreateFromTask(SaveAsync);
        CopyAsXamlCommand = ReactiveCommand.CreateFromTask(CopyAsXamlAsync);
        CopyAsSvgCommand = ReactiveCommand.CreateFromTask(CopyAsSvgAsync);
        PlayCommand = ReactiveCommand.Create(Play);
        StopCommand = ReactiveCommand.Create(Stop);

        MoveToolCommand = ReactiveCommand.Create(MoveTool);
        PaintToolCommand = ReactiveCommand.Create(PaintTool);
        RectangleToolCommand = ReactiveCommand.Create(RectangleTool);
        LineToolCommand = ReactiveCommand.Create(LineTool);
        EllipseToolCommand = ReactiveCommand.Create(EllipseTool);
    }

    public ICommand NewCommand { get; }

    public ICommand OpenCommand { get; }
    
    public ICommand SaveCommand { get; }

    public ICommand CopyAsXamlCommand { get; }

    public ICommand CopyAsSvgCommand { get; }

    public ICommand PlayCommand { get; }

    public ICommand StopCommand { get; }

    public ICommand MoveToolCommand { get; }

    public ICommand PaintToolCommand { get; }

    public ICommand RectangleToolCommand { get; }

    public ICommand LineToolCommand { get; }

    public ICommand EllipseToolCommand { get; }

    [Reactive] public bool IsPlaying { get; set; }

    public ObservableCollection<XamlItem> ToolBoxItems { get; set; }

    [Reactive] public XamlItem? SelectedToolBoxItem { get; set; }

    public IXamlEditor XamlEditor => _xamlEditor;
 
    public IXamlSelection XamlSelection => _xamlSelection;

    public IOverlayService? OverlayService { get; set; }

    private void New()
    {
        // TODO:
        // var control = _demos.DemoStackPanel();
        // var control = _demos.DemoDockPanel();
        var control = _demos.DemoCanvas();
        if (control is not null)
        {
            _xamlEditor.CanvasViewModel?.AddToRoot(control);
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
            await Dispatcher.UIThread.InvokeAsync(() => _xamlEditor.Reload(xamlItem));
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
        if (_xamlEditor.RootXamlItem is null)
        {
            return;
        }

        await JsonSerializer.SerializeAsync(
            stream, 
            _xamlEditor.RootXamlItem, 
            XamlItemJsonContext.s_instance.XamlItem);
    }

    private async Task CopyAsXamlAsync()
    {
        if (_xamlEditor.RootXamlItem is null)
        {
            return;
        }

        var xaml = await Task.Run(() =>
        {
            var settings = new XamlWriterSettings
            {
                Writer = new StringBuilder(),
                Namespace = "https://github.com/avaloniaui",
                WriteXmlns = false,
                WriteUid = false,
                Level = 0,
                WriteAttributesOnNewLine = false
            };

            var xamlWriter = new XamlWriter();
            xamlWriter.Write(_xamlEditor.RootXamlItem, settings);

            return settings.Writer.ToString();
        });

        await _applicationService.SetClipboardTextAsync(xaml);
    }

    private async Task CopyAsSvgAsync()
    {
        if (_xamlEditor.RootXamlItem is null)
        {
            return;
        }

        var control = _xamlEditor.LoadForExport(_xamlEditor.RootXamlItem);
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

    private void MoveTool()
    {
        XamlEditor.CanvasViewModel?.SetCurrentTool("Move");
    }

    private void PaintTool()
    {
        XamlEditor.CanvasViewModel?.SetCurrentTool("Paint");
    }

    private void RectangleTool()
    {
        XamlEditor.CanvasViewModel?.SetCurrentTool("Rectangle");
    }

    private void LineTool()
    {
        XamlEditor.CanvasViewModel?.SetCurrentTool("Line");
    }

    private void EllipseTool()
    {
        XamlEditor.CanvasViewModel?.SetCurrentTool("Ellipse");
    }
}
