using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasic.Behaviors;

namespace AvaloniaBasic.ViewModels.Toolbox;

public class CanvasViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var canvas = new Canvas();
        canvas.Width = 200d;
        canvas.Height = 200d;
        canvas.Background = Brushes.Black;
        return canvas;
    }

    public override Control CreateControl()
    {
        var canvas = new Canvas();
        canvas.Width = 200d;
        canvas.Height = 200d;
        canvas.Background = Brushes.LightGray;
        DragSettings.SetIsDropArea(canvas, true);
        DragSettings.SetSnapToGrid(canvas, false);
        return canvas;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not Canvas canvas)
        {
            return;
        }

        canvas.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
