using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Behaviors;
using AvaloniaBasicDemo.Model;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class TabControlViewModel : IDragItem
{
    public string? Name { get; init; }

    public string? Icon { get; init; }

    public Control CreatePreview()
    {
        var tabControl = new TabControl();
        tabControl.Items = new[] { "Item 1", "Item 2", "Item 3" };
        tabControl.SelectedIndex = 0;
        tabControl.Foreground = Brushes.Black;
        return tabControl;
    }

    public Control CreateControl()
    {
        var tabControl = new TabControl();
        tabControl.Items = new[] { "Item 1", "Item 2", "Item 3" };
        tabControl.SelectedIndex = 0;
        //tabControl.Foreground = Brushes.Blue;
        DragSettings.SetIsDropArea(tabControl, true);
        DragSettings.SetSnapToGrid(tabControl, false);
        return tabControl;
    }

    public void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not TabControl tabControl)
        {
            return;
        }

        tabControl.Foreground = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
