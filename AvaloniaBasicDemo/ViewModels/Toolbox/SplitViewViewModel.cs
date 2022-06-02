using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Behaviors;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class SplitViewViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var splitView = new SplitView();
        splitView.Width = 100d;
        splitView.Height = 100d;
        splitView.Background = Brushes.Black;
        return splitView;
    }

    public override Control CreateControl()
    {
        var splitView = new SplitView();
        splitView.Width = 100d;
        splitView.Height = 100d;
        splitView.Background = Brushes.LightGray;
        DragSettings.SetIsDropArea(splitView, true);
        DragSettings.SetSnapToGrid(splitView, false);
        return splitView;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not SplitView splitView)
        {
            return;
        }

        splitView.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
