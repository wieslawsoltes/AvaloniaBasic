using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Behaviors;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class SeparatorViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var separator = new Separator();
        separator.Width = 100d;
        separator.Height = 1d;
        separator.Background = Brushes.Black;
        return separator;
    }

    public override Control CreateControl()
    {
        var separator = new Separator();
        separator.Width = 100d;
        separator.Height = 1d;
        separator.Background = Brushes.Gray;
        DragSettings.SetIsDropArea(separator, true);
        DragSettings.SetSnapToGrid(separator, false);
        return separator;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not Separator separator)
        {
            return;
        }

        separator.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
