using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using AvaloniaBasicDemo.Behaviors;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class ToggleButtonViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var toggleButton = new ToggleButton();
        toggleButton.Content = "ToggleButton";
        toggleButton.Foreground = Brushes.Black;
        return toggleButton;
    }

    public override Control CreateControl()
    {
        var toggleButton = new ToggleButton();
        toggleButton.Content = "ToggleButton";
        //toggleButton.Foreground = Brushes.Blue;
        // TODO: Support setting Content
        DragSettings.SetIsDropArea(toggleButton, true);
        DragSettings.SetSnapToGrid(toggleButton, false);
        return toggleButton;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not ToggleButton toggleButton)
        {
            return;
        }

        toggleButton.Foreground = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
