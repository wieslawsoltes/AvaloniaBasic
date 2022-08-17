using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasic.Behaviors;

namespace AvaloniaBasic.ViewModels.Toolbox;

public class ContextMenuViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var contextMenu = new ContextMenu();
        contextMenu.Width = 200d;
        contextMenu.Height = 100d;
        contextMenu.Background = Brushes.Black;
        return contextMenu;
    }

    public override Control CreateControl()
    {
        var contextMenu = new ContextMenu();
        contextMenu.Width = 200d;
        contextMenu.Height = 100d;
        contextMenu.Background = Brushes.LightGray;
        DragSettings.SetIsDropArea(contextMenu, true);
        DragSettings.SetSnapToGrid(contextMenu, false);
        return contextMenu;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not ContextMenu contextMenu)
        {
            return;
        }

        contextMenu.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
