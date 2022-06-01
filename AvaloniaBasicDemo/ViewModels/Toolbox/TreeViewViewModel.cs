using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Behaviors;
using AvaloniaBasicDemo.Model;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class TreeViewViewModel : IDragItem
{
    public string? Name { get; init; }

    public string? Icon { get; init; }

    public Control CreatePreview()
    {
        var treeView = new TreeView();
        treeView.Width = 100d;
        treeView.Height = 100d;
        treeView.Background = Brushes.Black;
        return treeView;
    }

    public Control CreateControl()
    {
        var treeView = new TreeView();
        treeView.Width = 100d;
        treeView.Height = 100d;
        treeView.Background = Brushes.LightGray;
        DragSettings.SetIsDropArea(treeView, true);
        DragSettings.SetSnapToGrid(treeView, false);
        return treeView;
    }

    public void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not TreeView treeView)
        {
            return;
        }

        treeView.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
