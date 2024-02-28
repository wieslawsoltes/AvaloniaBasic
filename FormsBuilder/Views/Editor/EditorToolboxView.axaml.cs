using Avalonia;
using Avalonia.Controls;

namespace FormsBuilder;

public partial class EditorToolboxView : UserControl
{
    private IToolboxEditor? _toolboxViewModel;

    public EditorToolboxView()
    {
        InitializeComponent();
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);

        if (DataContext is MainViewViewModel mainViewModel)
        {
            _toolboxViewModel = new ToolboxEditor(
                this, 
                mainViewModel.OverlayService,  
                mainViewModel.XamlObjectFactory, 
                mainViewModel.XamlEditor, 
                mainViewModel.XamlItemFactory);

            ToolboxListBox.ContainerPrepared += ToolboxListBoxOnContainerPrepared;
            ToolboxListBox.ContainerClearing += ToolboxListBoxOnContainerClearing;
        }
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);

        _toolboxViewModel = null;

        ToolboxListBox.ContainerPrepared -= ToolboxListBoxOnContainerPrepared;
        ToolboxListBox.ContainerClearing -= ToolboxListBoxOnContainerClearing;
    }

    private void ToolboxListBoxOnContainerPrepared(object? sender, ContainerPreparedEventArgs e)
    {
        _toolboxViewModel?.AttachToContainer(e.Container);
    }

    private void ToolboxListBoxOnContainerClearing(object? sender, ContainerClearingEventArgs e)
    {
        _toolboxViewModel?.DetachFromContainer(e.Container);
    }
}
