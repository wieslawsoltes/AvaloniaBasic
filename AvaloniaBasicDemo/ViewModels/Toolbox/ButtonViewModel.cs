using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Model;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class ButtonViewModel : IDragItem
{
    public string? Name { get; init; }

    public string? Icon { get; init; }

    public Control CreatePreview()
    {
        var button = new Button();
        button.Content = "Button";
        button.Foreground = Brushes.Black;
        return button;
    }

    public Control CreateControl()
    {
        var button = new Button();
        button.Content = "Button";
        //button.Foreground = Brushes.Blue;
        return button;
    }

    public void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not Button button)
        {
            return;
        }

        button.Foreground = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
