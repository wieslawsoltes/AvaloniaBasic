using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using AvaloniaBasicDemo.Model;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class EllipseViewModel : IDragItem
{
    public string? Name { get; init; }

    public string? Icon { get; init; }

    public Control CreatePreview()
    {
        var ellipse = new Ellipse();
        ellipse.Width = 100d;
        ellipse.Height = 100d;
        ellipse.Fill = Brushes.Black;
        return ellipse;
    }

    public Control CreateControl()
    {
        var ellipse = new Ellipse();
        ellipse.Width = 100d;
        ellipse.Height = 100d;
        ellipse.Fill = Brushes.Gray;
        return ellipse;
    }

    public void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not Ellipse ellipse)
        {
            return;
        }

        ellipse.Fill = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
