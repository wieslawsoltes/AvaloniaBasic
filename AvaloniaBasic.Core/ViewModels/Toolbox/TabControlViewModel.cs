using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasic.Behaviors;

namespace AvaloniaBasic.ViewModels.Toolbox;

public class TabControlViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var tabControl = new TabControl();
        tabControl.Items = new[] { "Item 1", "Item 2", "Item 3" };
        tabControl.SelectedIndex = 0;
        tabControl.Foreground = Brushes.Black;
        return tabControl;
    }

    public override Control CreateControl()
    {
        var tabControl = new TabControl();
        tabControl.Items = new[] { "Item 1", "Item 2", "Item 3" };
        tabControl.SelectedIndex = 0;
        //tabControl.Foreground = Brushes.Blue;
        DragSettings.SetIsDropArea(tabControl, true);
        DragSettings.SetSnapToGrid(tabControl, false);
        return tabControl;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not TabControl tabControl)
        {
            return;
        }

        tabControl.Foreground = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
