using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using AvaloniaBasicDemo.Model;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class RectangleViewModel : IDragItem
{
    public string? Name { get; init; }

    public string? Icon { get; init; }

    public Control CreatePreview()
    {
        var rectangle = new Rectangle();
        rectangle.Width = 100d;
        rectangle.Height = 100d;
        rectangle.Fill = Brushes.Black;
        return rectangle;
    }

    public Control CreateControl()
    {
        var rectangle = new Rectangle();
        rectangle.Width = 100d;
        rectangle.Height = 100d;
        rectangle.Fill = Brushes.Blue;
        return rectangle;
    }

    public void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not Rectangle rectangle)
        {
            return;
        }

        rectangle.Fill = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
