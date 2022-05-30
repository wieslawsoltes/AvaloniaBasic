using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Model;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class BorderViewModel : IDragItem
{
    public string? Name { get; init; }

    public string? Icon { get; init; }

    public Control CreatePreview()
    {
        var border = new Border();
        border.Width = 100d;
        border.Height = 100d;
        border.Background = Brushes.Black;
        return border;
    }

    public Control CreateControl()
    {
        var border = new Border();
        border.Width = 100d;
        border.Height = 100d;
        border.Background = Brushes.Blue;
        return border;
    }

    public void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not Border border)
        {
            return;
        }

        border.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
