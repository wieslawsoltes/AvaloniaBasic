using Avalonia;
using Avalonia.Controls;
using AvaloniaBasic.ViewModels;

namespace AvaloniaBasicDemo.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();

        AttachedToVisualTree += OnAttachedToVisualTree;
    }

    private void OnAttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
    {
        if (DataContext is MainViewModel mainViewModel)
        {
            mainViewModel.PreviewCanvas = PreviewCanvas;
            mainViewModel.Toolbox.PreviewCanvas = PreviewCanvas;
        } 
    }
}
