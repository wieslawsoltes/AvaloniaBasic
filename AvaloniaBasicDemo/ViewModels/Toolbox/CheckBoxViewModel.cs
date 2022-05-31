using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Model;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class CheckBoxViewModel : IDragItem
{
    public string? Name { get; init; }

    public string? Icon { get; init; }

    public Control CreatePreview()
    {
        var checkBox = new CheckBox();
        checkBox.Content = "CheckBox";
        checkBox.Foreground = Brushes.Black;
        return checkBox;
    }

    public Control CreateControl()
    {
        var checkBox = new CheckBox();
        checkBox.Content = "CheckBox";
        //checkBox.Foreground = Brushes.Blue;
        return checkBox;
    }

    public void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not CheckBox checkBox)
        {
            return;
        }

        checkBox.Foreground = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
