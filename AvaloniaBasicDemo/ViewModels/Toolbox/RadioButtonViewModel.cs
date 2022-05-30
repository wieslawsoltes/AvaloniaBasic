using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Model;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class RadioButtonViewModel : IDragItem
{
    public string? Name { get; init; }

    public string? Icon { get; init; }

    public Control CreatePreview()
    {
        var radioButton = new RadioButton();
        radioButton.Content = "RadioButton";
        radioButton.Foreground = Brushes.Black;
        return radioButton;
    }

    public Control CreateControl()
    {
        var radioButton = new RadioButton();
        radioButton.Content = "RadioButton";
        radioButton.Foreground = Brushes.Blue;
        return radioButton;
    }

    public void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not RadioButton radioButton)
        {
            return;
        }

        radioButton.Foreground = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
