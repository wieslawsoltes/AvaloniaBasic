using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Behaviors;
using AvaloniaBasicDemo.Model;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class GridViewModel : IDragItem
{
    public string? Name { get; init; }

    public string? Icon { get; init; }

    public Control CreatePreview()
    {
        var grid = new Grid();
        grid.Width = 100d;
        grid.Height = 100d;
        grid.Background = Brushes.Black;
        return grid;
    }

    public Control CreateControl()
    {
        var grid = new Grid();
        grid.Width = 100d;
        grid.Height = 100d;
        grid.Background = Brushes.LightGray;
        DragSettings.SetIsDropArea(grid, true);
        DragSettings.SetSnapToGrid(grid, false);
        return grid;
    }

    public void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not Grid grid)
        {
            return;
        }

        grid.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
