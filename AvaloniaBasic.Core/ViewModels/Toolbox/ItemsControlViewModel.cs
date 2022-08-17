using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasic.Behaviors;

namespace AvaloniaBasic.ViewModels.Toolbox;

public class ItemsControlViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var itemsControl = new ItemsControl();
        itemsControl.Width = 100d;
        itemsControl.Height = 100d;
        itemsControl.Background = Brushes.Black;
        return itemsControl;
    }

    public override Control CreateControl()
    {
        var itemsControl = new ItemsControl();
        itemsControl.Width = 100d;
        itemsControl.Height = 100d;
        itemsControl.Background = Brushes.LightGray;
        DragSettings.SetIsDropArea(itemsControl, true);
        DragSettings.SetSnapToGrid(itemsControl, false);
        return itemsControl;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not ItemsControl itemsControl)
        {
            return;
        }

        itemsControl.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
