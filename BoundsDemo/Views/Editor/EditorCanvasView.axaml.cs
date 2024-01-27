using Avalonia;
using Avalonia.Controls;

namespace BoundsDemo;

public partial class EditorCanvasView : UserControl
{
    public static readonly StyledProperty<OverlayView> OverlayViewProperty = 
        AvaloniaProperty.Register<EditorCanvasView, OverlayView>(nameof(OverlayView));

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
            mainViewModel.CanvasViewModel = new CanvasViewModel(OverlayView);
            mainViewModel.CanvasViewModel.AttachHost(this, RootPanel);

            // var control = mainViewModel.DemoStackPanel();
            // var control = mainViewModel.DemoDockPanel();
            var control = mainViewModel.DemoCanvas();
            if (control is not null)
            {
                mainViewModel.CanvasViewModel.AddRoot(control);
            }
        }
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);

        if (DataContext is MainViewViewModel mainViewModel)
        {
            mainViewModel.CanvasViewModel?.DetachHost();
            mainViewModel.CanvasViewModel = null;
        }
    }
}
