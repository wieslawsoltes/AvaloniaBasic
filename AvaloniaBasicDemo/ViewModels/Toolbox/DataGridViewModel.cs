using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Model;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class DataGridViewModel : IDragItem
{
    public string? Name { get; init; }

    public string? Icon { get; init; }

    public Control CreatePreview()
    {
        var dataGrid = new DataGrid();
        dataGrid.AutoGenerateColumns = false;
        dataGrid.Columns.Add(new DataGridTextColumn { Header = "Item 1", Width = DataGridLength.Auto });
        dataGrid.Columns.Add(new DataGridTextColumn { Header = "Item 2", Width = DataGridLength.Auto });
        dataGrid.Columns.Add(new DataGridTextColumn { Header = "Item 3", Width = DataGridLength.Auto });
        dataGrid.Width = 300d;
        dataGrid.Height = 100d;
        dataGrid.Background = Brushes.Black;
        return dataGrid;
    }

    public Control CreateControl()
    {
        var dataGrid = new DataGrid();
        dataGrid.AutoGenerateColumns = false;
        dataGrid.Columns.Add(new DataGridTextColumn { Header = "Item 1", Width = DataGridLength.Auto });
        dataGrid.Columns.Add(new DataGridTextColumn { Header = "Item 2", Width = DataGridLength.Auto });
        dataGrid.Columns.Add(new DataGridTextColumn { Header = "Item 3", Width = DataGridLength.Auto });
        dataGrid.Width = 300d;
        dataGrid.Height = 100d;
        dataGrid.Background = Brushes.LightGray;
        return dataGrid;
    }

    public void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not DataGrid dataGrid)
        {
            return;
        }

        dataGrid.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
