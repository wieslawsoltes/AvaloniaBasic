using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Model;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class LabelViewModel : IDragItem
{
    public string? Name { get; init; }

    public string? Icon { get; init; }

    public Control CreatePreview()
    {
        var label = new Label();
        label.Content = "Label";
        label.Foreground = Brushes.Black;
        return label;
    }

    public Control CreateControl()
    {
        var label = new Label();
        label.Content = "Label";
        //label.Foreground = Brushes.Blue;
        return label;
    }

    public void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not Label label)
        {
            return;
        }

        label.Foreground = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
