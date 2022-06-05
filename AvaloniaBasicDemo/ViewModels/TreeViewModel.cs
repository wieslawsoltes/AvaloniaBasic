using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.LogicalTree;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace AvaloniaBasicDemo.ViewModels;

public partial class TreeViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<LogicalViewModel> _logicalTree;

    public HierarchicalTreeDataGridSource<LogicalViewModel> LogicalTreeSource { get; }

    public TreeViewModel()
    {
        _logicalTree = new ObservableCollection<LogicalViewModel>();

        LogicalTreeSource = new HierarchicalTreeDataGridSource<LogicalViewModel>(_logicalTree)
        {
            Columns =
            {
                new HierarchicalExpanderColumn<LogicalViewModel>(
                    inner: new TemplateColumn<LogicalViewModel>(
                        "Logical",
                        new FuncDataTemplate<LogicalViewModel>((_, _) =>
                        {
                            return new Label
                            {
                                [!ContentControl.ContentProperty] = new Binding("Name")
                            };
                        }, true),
                        options: new ColumnOptions<LogicalViewModel>
                        {
                            CanUserResizeColumn = false,
                            CanUserSortColumn = false,
                        },
                        width: new GridLength(1, GridUnitType.Star)), 
                    childSelector: x => x.Children,
                    hasChildrenSelector: x => x.Children?.Count > 0,
                    isExpandedSelector: x => x.IsExpanded)
            }
        };
    }

    private void AddToLogicalTree(ILogical root, ObservableCollection<LogicalViewModel> tree, int level)
    {
        var logicalViewModel = new LogicalViewModel()
        {
            Name = root.GetType().Name,
            Logical = root,
            IsExpanded = level <= 1
        };
        tree.Add(logicalViewModel);

        var logicalDescendants = root.GetLogicalChildren();
        foreach (var logical in logicalDescendants)
        {
            if (logicalViewModel.Children is null)
            {
                logicalViewModel.Children = new ObservableCollection<LogicalViewModel>();
            }

            AddToLogicalTree(logical, logicalViewModel.Children, level += 1);
        }
    }

    public void UpdateLogicalTree(ILogical root)
    {
        _logicalTree.Clear();

        AddToLogicalTree(root, _logicalTree, 0);
    }
}
