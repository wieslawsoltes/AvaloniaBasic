using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Behaviors;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class GridSplitterViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var gridSplitter = new GridSplitter();
        gridSplitter.Background = Brushes.Black;
        return gridSplitter;
    }

    public override Control CreateControl()
    {
        var gridSplitter = new GridSplitter();
        gridSplitter.Background = Brushes.LightGray;
        DragSettings.SetIsDropArea(gridSplitter, true);
        DragSettings.SetSnapToGrid(gridSplitter, false);
        return gridSplitter;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not GridSplitter gridSplitter)
        {
            return;
        }

        gridSplitter.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
