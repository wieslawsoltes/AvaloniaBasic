using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Behaviors;
using AvaloniaBasicDemo.Model;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class CanvasViewModel : IDragItem
{
    public string? Name { get; init; }

    public string? Icon { get; init; }

    public Control CreatePreview()
    {
        var canvas = new Canvas();
        canvas.Width = 200d;
        canvas.Height = 200d;
        canvas.Background = Brushes.Black;
        return canvas;
    }

    public Control CreateControl()
    {
        var canvas = new Canvas();
        canvas.Width = 200d;
        canvas.Height = 200d;
        canvas.Background = Brushes.LightGray;
        DragSettings.SetIsDropArea(canvas, true);
        DragSettings.SetSnapToGrid(canvas, false);
        return canvas;
    }

    public void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not Canvas canvas)
        {
            return;
        }

        canvas.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
