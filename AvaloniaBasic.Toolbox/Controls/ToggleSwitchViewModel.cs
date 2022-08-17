using Avalonia.Controls;
using Avalonia.Media;

namespace AvaloniaBasic.ViewModels.Toolbox;

public class ToggleSwitchViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var toggleSwitch = new ToggleSwitch();
        toggleSwitch.OnContent = "On";
        toggleSwitch.OffContent = "Off";
        toggleSwitch.Foreground = Brushes.Black;
        return toggleSwitch;
    }

    public override Control CreateControl()
    {
        var toggleSwitch = new ToggleSwitch();
        toggleSwitch.OnContent = "On";
        toggleSwitch.OffContent = "Off";
        //toggleSwitch.Foreground = Brushes.Blue;
        // TODO: Support setting Content
        return toggleSwitch;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        toggleSwitch.Foreground = isPointerOver ? Brushes.Green : Brushes.Red;
    }

    public override bool IsDropArea() => true;
}
