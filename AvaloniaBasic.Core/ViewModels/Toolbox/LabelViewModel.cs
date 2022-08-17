using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasic.Behaviors;

namespace AvaloniaBasic.ViewModels.Toolbox;

public class LabelViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var label = new Label();
        label.Content = "Label";
        label.Foreground = Brushes.Black;
        return label;
    }

    public override Control CreateControl()
    {
        var label = new Label();
        label.Content = "Label";
        //label.Foreground = Brushes.Blue;
        // TODO: Support setting Content
        DragSettings.SetIsDropArea(label, true);
        DragSettings.SetSnapToGrid(label, false);
        return label;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not Label label)
        {
            return;
        }

        label.Foreground = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
