using Avalonia;
using Avalonia.Controls;

namespace FormsBuilder;

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
            mainViewModel.XamlEditor.CanvasViewModel = new CanvasViewModel(
                OverlayView,
                mainViewModel.XamlEditor,
                mainViewModel.XamlSelection);
            mainViewModel.XamlEditor.CanvasViewModel.AttachHost(this, RootPanel, GridLines);

            // TODO:
            mainViewModel.NewCommand.Execute(null);
        }
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);

        if (DataContext is MainViewViewModel mainViewModel)
        {
            mainViewModel.XamlEditor.CanvasViewModel?.DetachHost();
            mainViewModel.XamlEditor.CanvasViewModel = null;
        }
    }
}
