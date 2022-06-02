using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Behaviors;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class BorderViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var border = new Border();
        border.Width = 100d;
        border.Height = 100d;
        border.Background = Brushes.Black;
        return border;
    }

    public override Control CreateControl()
    {
        var border = new Border();
        border.Width = 100d;
        border.Height = 100d;
        border.Background = Brushes.LightGray;
        // TODO: Support setting Child
        DragSettings.SetIsDropArea(border, true);
        DragSettings.SetSnapToGrid(border, false);
        return border;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not Border border)
        {
            return;
        }

        border.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
