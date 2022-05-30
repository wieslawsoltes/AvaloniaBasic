using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Model;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class ImageViewModel : IDragItem
{
    public string? Name { get; init; }

    public string? Icon { get; init; }

    public Control CreatePreview()
    {
        var image = new Image();
        image.Width = 100d;
        image.Height = 100d;
        // TODO: image.Background = Brushes.Black;
        return image;
    }

    public Control CreateControl()
    {
        var image = new Image();
        image.Width = 100d;
        image.Height = 100d;
        // TODO: image.Background = Brushes.Blue;
        return image;
    }

    public void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not Image image)
        {
            return;
        }

        // TODO: image.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
