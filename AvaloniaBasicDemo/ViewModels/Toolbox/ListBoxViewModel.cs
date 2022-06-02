using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Behaviors;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class ListBoxViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var listBox = new ListBox();
        listBox.Width = 100d;
        listBox.Height = 100d;
        listBox.Background = Brushes.Black;
        return listBox;
    }

    public override Control CreateControl()
    {
        var listBox = new ListBox();
        listBox.Width = 100d;
        listBox.Height = 100d;
        listBox.Background = Brushes.LightGray;
        DragSettings.SetIsDropArea(listBox, true);
        DragSettings.SetSnapToGrid(listBox, false);
        return listBox;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not ListBox listBox)
        {
            return;
        }

        listBox.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
