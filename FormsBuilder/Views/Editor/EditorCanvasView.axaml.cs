using Avalonia;
using Avalonia.Controls;

namespace FormsBuilder;

public partial class EditorCanvasView : UserControl
{
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
                mainViewModel.OverlayService,
                mainViewModel.XamlEditor,
                mainViewModel.XamlSelection);
            mainViewModel.XamlEditor.CanvasViewModel.AttachHost(this, RootPanel, GridLinesControl);

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
