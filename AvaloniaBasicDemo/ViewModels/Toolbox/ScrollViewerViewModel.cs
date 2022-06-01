using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Behaviors;
using AvaloniaBasicDemo.Model;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class ScrollViewerViewModel : IDragItem
{
    public string? Name { get; init; }

    public string? Icon { get; init; }

    public Control CreatePreview()
    {
        var scrollViewer = new ScrollViewer();
        scrollViewer.Width = 100d;
        scrollViewer.Height = 100d;
        scrollViewer.Background = Brushes.Black;
        return scrollViewer;
    }

    public Control CreateControl()
    {
        var scrollViewer = new ScrollViewer();
        scrollViewer.Width = 100d;
        scrollViewer.Height = 100d;
        scrollViewer.Background = Brushes.LightGray;
        DragSettings.SetIsDropArea(scrollViewer, true);
        DragSettings.SetSnapToGrid(scrollViewer, false);
        return scrollViewer;
    }

    public void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not ScrollViewer scrollViewer)
        {
            return;
        }

        scrollViewer.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
