using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Behaviors;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class CheckBoxViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var checkBox = new CheckBox();
        checkBox.Content = "CheckBox";
        checkBox.Foreground = Brushes.Black;
        return checkBox;
    }

    public override Control CreateControl()
    {
        var checkBox = new CheckBox();
        checkBox.Content = "CheckBox";
        //checkBox.Foreground = Brushes.Blue;
        // TODO: Support setting Content
        DragSettings.SetIsDropArea(checkBox, true);
        DragSettings.SetSnapToGrid(checkBox, false);
        return checkBox;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not CheckBox checkBox)
        {
            return;
        }

        checkBox.Foreground = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
