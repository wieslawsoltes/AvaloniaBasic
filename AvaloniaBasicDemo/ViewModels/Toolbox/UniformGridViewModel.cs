using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using AvaloniaBasicDemo.Behaviors;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class UniformGridViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var uniformGrid = new UniformGrid();
        uniformGrid.Width = 100d;
        uniformGrid.Height = 100d;
        uniformGrid.Background = Brushes.Black;
        return uniformGrid;
    }

    public override Control CreateControl()
    {
        var uniformGrid = new UniformGrid();
        uniformGrid.Width = 100d;
        uniformGrid.Height = 100d;
        uniformGrid.Background = Brushes.LightGray;
        DragSettings.SetIsDropArea(uniformGrid, true);
        DragSettings.SetSnapToGrid(uniformGrid, false);
        return uniformGrid;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not UniformGrid uniformGrid)
        {
            return;
        }

        uniformGrid.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
