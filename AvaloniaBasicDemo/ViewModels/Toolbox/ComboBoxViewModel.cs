using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Model;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class ComboBoxViewModel : IDragItem
{
    public string? Name { get; init; }

    public string? Icon { get; init; }

    public Control CreatePreview()
    {
        var comboBox = new ComboBox();
        comboBox.Items = new[] { "Item 1", "Item 2", "Item 3" };
        comboBox.SelectedIndex = 0;
        comboBox.Foreground = Brushes.Black;
        return comboBox;
    }

    public Control CreateControl()
    {
        var comboBox = new ComboBox();
        comboBox.Items = new[] { "Item 1", "Item 2", "Item 3" };
        comboBox.SelectedIndex = 0;
        comboBox.Foreground = Brushes.Blue;
        return comboBox;
    }

    public void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not ComboBox comboBox)
        {
            return;
        }

        comboBox.Foreground = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
