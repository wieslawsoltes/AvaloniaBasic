using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Behaviors;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class ButtonViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var button = new Button();
        button.Content = "Button";
        button.Foreground = Brushes.Black;
        return button;
    }

    public override Control CreateControl()
    {
        var button = new Button();
        button.Content = "Button";
        //button.Foreground = Brushes.Blue;
        // TODO: Support setting Content
        DragSettings.SetIsDropArea(button, true);
        DragSettings.SetSnapToGrid(button, false);
        return button;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not Button button)
        {
            return;
        }

        button.Foreground = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
