using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Behaviors;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class MenuViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var menu = new Menu();
        menu.Width = 100d;
        menu.Height = 100d;
        menu.Background = Brushes.Black;
        return menu;
    }

    public override Control CreateControl()
    {
        var menu = new Menu();
        menu.Width = 100d;
        menu.Height = 100d;
        menu.Background = Brushes.LightGray;
        DragSettings.SetIsDropArea(menu, true);
        DragSettings.SetSnapToGrid(menu, false);
        return menu;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not Menu menu)
        {
            return;
        }

        menu.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
