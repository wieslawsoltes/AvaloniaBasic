using Avalonia.Controls;
using Avalonia.Media;

namespace AvaloniaBasic.ViewModels.Toolbox;

public class NumericUpDownViewModel : ToolboxItemViewModel
{
    public NumericUpDownViewModel()
    {
        Name = "NumericUpDown";
        Group = "Text";
    }

    public override Control CreatePreview()
    {
        var numericUpDown = new NumericUpDown();
        numericUpDown.Minimum = 0;
        numericUpDown.Maximum = 100;
        numericUpDown.Value = 50;
        numericUpDown.Background = Brushes.Black;
        return numericUpDown;
    }

    public override Control CreateControl()
    {
        var numericUpDown = new NumericUpDown();
        numericUpDown.Minimum = 0;
        numericUpDown.Maximum = 100;
        numericUpDown.Value = 50;
        return numericUpDown;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not NumericUpDown numericUpDown)
        {
            return;
        }

        numericUpDown.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }

    public override bool IsDropArea() => true;
}
