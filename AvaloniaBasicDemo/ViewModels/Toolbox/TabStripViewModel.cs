using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using AvaloniaBasicDemo.Behaviors;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class TabStripViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var tabStrip = new TabStrip();
        tabStrip.Items = new[] { "Item 1", "Item 2", "Item 3" };
        tabStrip.SelectedIndex = 0;
        tabStrip.Foreground = Brushes.Black;
        return tabStrip;
    }

    public override Control CreateControl()
    {
        var tabStrip = new TabStrip();
        tabStrip.Items = new[] { "Item 1", "Item 2", "Item 3" };
        tabStrip.SelectedIndex = 0;
        //tabStrip.Foreground = Brushes.Blue;
        DragSettings.SetIsDropArea(tabStrip, true);
        DragSettings.SetSnapToGrid(tabStrip, false);
        return tabStrip;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not TabStrip tabStrip)
        {
            return;
        }

        tabStrip.Foreground = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
