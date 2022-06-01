using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Behaviors;
using AvaloniaBasicDemo.Model;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class MenuViewModel : IDragItem
{
    public string? Name { get; init; }

    public string? Icon { get; init; }

    public Control CreatePreview()
    {
        var menu = new Menu();
        menu.Width = 100d;
        menu.Height = 100d;
        menu.Background = Brushes.Black;
        return menu;
    }

    public Control CreateControl()
    {
        var menu = new Menu();
        menu.Width = 100d;
        menu.Height = 100d;
        menu.Background = Brushes.LightGray;
        DragSettings.SetIsDropArea(menu, true);
        DragSettings.SetSnapToGrid(menu, false);
        return menu;
    }

    public void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not Menu menu)
        {
            return;
        }

        menu.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
