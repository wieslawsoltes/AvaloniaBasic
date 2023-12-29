using Avalonia;
using Avalonia.Controls;

namespace BoundsDemo;

public partial class EditorCanvasView : UserControl
{
    public static readonly StyledProperty<OverlayView> OverlayViewProperty = 
        AvaloniaProperty.Register<EditorCanvasView, OverlayView>(nameof(OverlayView));

    internal CanvasViewModel? _canvasViewModel;

    public OverlayView OverlayView
    {
        get => GetValue(OverlayViewProperty);
        set => SetValue(OverlayViewProperty, value);
    }

    public EditorCanvasView()
    {
        InitializeComponent();
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);

        Focusable = true;

        if (DataContext is MainViewViewModel mainViewModel)
        {
            _canvasViewModel = new CanvasViewModel(OverlayView);
            _canvasViewModel.AttachHost(this, RootPanel);

            // var control = mainViewModel.DemoStackPanel();
            var control = mainViewModel.DemoPanelDockPanel();
            if (control is not null)
            {
                _canvasViewModel.AddRoot(control);
            }
        }
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);

        _canvasViewModel?.DetachHost();
        _canvasViewModel = null;
    }
}
