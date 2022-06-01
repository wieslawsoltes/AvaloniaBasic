using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Behaviors;
using AvaloniaBasicDemo.Model;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class GridSplitterViewModel : IDragItem
{
    public string? Name { get; init; }

    public string? Icon { get; init; }

    public Control CreatePreview()
    {
        var gridSplitter = new GridSplitter();
        gridSplitter.Background = Brushes.Black;
        return gridSplitter;
    }

    public Control CreateControl()
    {
        var gridSplitter = new GridSplitter();
        gridSplitter.Background = Brushes.LightGray;
        DragSettings.SetIsDropArea(gridSplitter, true);
        DragSettings.SetSnapToGrid(gridSplitter, false);
        return gridSplitter;
    }

    public void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not GridSplitter gridSplitter)
        {
            return;
        }

        gridSplitter.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
