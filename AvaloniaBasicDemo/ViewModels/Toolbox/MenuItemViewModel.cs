using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Behaviors;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class MenuItemViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var menuItem = new MenuItem();
        menuItem.Header = "MenuItem";
        menuItem.Background = Brushes.Black;
        return menuItem;
    }

    public override Control CreateControl()
    {
        var menuItem = new MenuItem();
        menuItem.Header = "MenuItem";
        menuItem.Background = Brushes.LightGray;
        DragSettings.SetIsDropArea(menuItem, true);
        DragSettings.SetSnapToGrid(menuItem, false);
        return menuItem;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not MenuItem menuItem)
        {
            return;
        }

        menuItem.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
