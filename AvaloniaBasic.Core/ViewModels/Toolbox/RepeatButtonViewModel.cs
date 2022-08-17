using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasic.Behaviors;

namespace AvaloniaBasic.ViewModels.Toolbox;

public class RepeatButtonViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var repeatButton = new RepeatButton();
        repeatButton.Content = "RepeatButton";
        repeatButton.Foreground = Brushes.Black;
        return repeatButton;
    }

    public override Control CreateControl()
    {
        var repeatButton = new RepeatButton();
        repeatButton.Content = "RepeatButton";
        //repeatButton.Foreground = Brushes.Blue;
        // TODO: Support setting Content
        DragSettings.SetIsDropArea(repeatButton, true);
        DragSettings.SetSnapToGrid(repeatButton, false);
        return repeatButton;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not RepeatButton repeatButton)
        {
            return;
        }

        repeatButton.Foreground = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
