using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Behaviors;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class ButtonSpinnerViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var buttonSpinner = new ButtonSpinner();
        buttonSpinner.Content = "Button Spinner";
        buttonSpinner.Foreground = Brushes.Black;
        return buttonSpinner;
    }

    public override Control CreateControl()
    {
        var buttonSpinner = new ButtonSpinner();
        buttonSpinner.Content = "Button Spinner";
        //buttonSpinner.Foreground = Brushes.Blue;
        // TODO: Support setting Content
        DragSettings.SetIsDropArea(buttonSpinner, true);
        DragSettings.SetSnapToGrid(buttonSpinner, false);
        return buttonSpinner;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not ButtonSpinner buttonSpinner)
        {
            return;
        }

        buttonSpinner.Foreground = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
