using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using AvaloniaBasicDemo.Behaviors;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class ScrollBarViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var scrollBar = new ScrollBar();
        scrollBar.Width = 30d;
        scrollBar.Height = 100d;
        scrollBar.Background = Brushes.Black;
        return scrollBar;
    }

    public override Control CreateControl()
    {
        var scrollBar = new ScrollBar();
        scrollBar.Width = 30d;
        scrollBar.Height = 100d;
        scrollBar.Background = Brushes.LightGray;
        DragSettings.SetIsDropArea(scrollBar, true);
        DragSettings.SetSnapToGrid(scrollBar, false);
        return scrollBar;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not ScrollBar scrollBar)
        {
            return;
        }

        scrollBar.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
