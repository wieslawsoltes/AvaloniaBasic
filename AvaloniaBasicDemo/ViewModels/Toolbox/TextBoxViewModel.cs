using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Model;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class TextBoxViewModel : IDragItem
{
    public string? Name { get; init; }

    public string? Icon { get; init; }

    public Control CreatePreview()
    {
        var textBox = new TextBox();
        textBox.Text = "Text";
        textBox.Foreground = Brushes.Black;
        return textBox;
    }

    public Control CreateControl()
    {
        var textBox = new TextBox();
        textBox.Text = "Text";
        //textBox.Foreground = Brushes.Blue;
        return textBox;
    }

    public void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not TextBox textBox)
        {
            return;
        }

        textBox.Foreground = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
