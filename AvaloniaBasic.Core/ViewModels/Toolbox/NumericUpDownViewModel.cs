using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasic.Behaviors;

namespace AvaloniaBasic.ViewModels.Toolbox;

public class NumericUpDownViewModel : DragItemViewModel
{
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
        DragSettings.SetIsDropArea(numericUpDown, true);
        DragSettings.SetSnapToGrid(numericUpDown, false);
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
}
