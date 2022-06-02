using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Behaviors;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class ItemsRepeaterViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var itemsRepeater = new ItemsRepeater();
        itemsRepeater.Width = 100d;
        itemsRepeater.Height = 100d;
        itemsRepeater.Background = Brushes.Black;
        return itemsRepeater;
    }

    public override Control CreateControl()
    {
        var itemsRepeater = new ItemsRepeater();
        itemsRepeater.Width = 100d;
        itemsRepeater.Height = 100d;
        itemsRepeater.Background = Brushes.LightGray;
        DragSettings.SetIsDropArea(itemsRepeater, true);
        DragSettings.SetSnapToGrid(itemsRepeater, false);
        return itemsRepeater;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not ItemsRepeater itemsRepeater)
        {
            return;
        }

        itemsRepeater.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
