using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasic.Behaviors;

namespace AvaloniaBasic.ViewModels.Toolbox;

public class TreeViewViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var treeView = new TreeView();
        treeView.Width = 100d;
        treeView.Height = 100d;
        treeView.Background = Brushes.Black;
        return treeView;
    }

    public override Control CreateControl()
    {
        var treeView = new TreeView();
        treeView.Width = 100d;
        treeView.Height = 100d;
        treeView.Background = Brushes.LightGray;
        DragSettings.SetIsDropArea(treeView, true);
        DragSettings.SetSnapToGrid(treeView, false);
        return treeView;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not TreeView treeView)
        {
            return;
        }

        treeView.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
