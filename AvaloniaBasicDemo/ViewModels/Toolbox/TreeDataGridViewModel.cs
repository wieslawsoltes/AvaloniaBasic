using Avalonia.Controls;
using Avalonia.Media;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class TreeDataGridViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var treeDataGrid = new TreeDataGrid();
        treeDataGrid.Width = 300d;
        treeDataGrid.Height = 100d;
        treeDataGrid.Background = Brushes.Black;
        return treeDataGrid;
    }

    public override Control CreateControl()
    {
        var treeDataGrid = new TreeDataGrid();
        treeDataGrid.Width = 300d;
        treeDataGrid.Height = 100d;
        treeDataGrid.Background = Brushes.LightGray;
        return treeDataGrid;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not TreeDataGrid treeDataGrid)
        {
            return;
        }

        treeDataGrid.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
