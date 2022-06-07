using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.LogicalTree;
using Avalonia.Threading;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace AvaloniaBasicDemo.ViewModels;

public partial class TreeViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<LogicalViewModel> _logicalTree;
    [ObservableProperty] private ObservableCollection<PropertyViewModel> _properties;
    [ObservableProperty] private LogicalViewModel? _selectedLogical;

    public HierarchicalTreeDataGridSource<LogicalViewModel> LogicalTreeSource { get; }

    public HierarchicalTreeDataGridSource<PropertyViewModel> PropertiesSource { get; }

    public TreeViewModel()
    {
        _logicalTree = new ObservableCollection<LogicalViewModel>();

        _properties = new ObservableCollection<PropertyViewModel>();

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

        PropertiesSource = new HierarchicalTreeDataGridSource<PropertyViewModel>(_properties)
        {
            Columns =
            {
                new HierarchicalExpanderColumn<PropertyViewModel>(
                    inner: new TemplateColumn<PropertyViewModel>(
                        "Property",
                        new FuncDataTemplate<PropertyViewModel>((_, _) =>
                        {
                            return new Label
                            {
                                [!ContentControl.ContentProperty] = new Binding("Name"),
                                HorizontalAlignment = HorizontalAlignment.Left,
                                HorizontalContentAlignment = HorizontalAlignment.Left
                            };
                        }, true),
                        options: new ColumnOptions<PropertyViewModel>
                        {
                            CanUserResizeColumn = true,
                            CanUserSortColumn = false
                        },
                        width: new GridLength(1, GridUnitType.Star)), 
                    childSelector: x => x.Children,
                    hasChildrenSelector: x => x.Children?.Count > 0,
                    isExpandedSelector: x => x.IsExpanded),
                new TemplateColumn<PropertyViewModel>(
                    "Value",
                    new FuncDataTemplate<PropertyViewModel>((_, _) =>
                    {
                        return new Label
                        {
                            [!ContentControl.ContentProperty] = new Binding("Value"),
                            HorizontalAlignment = HorizontalAlignment.Left,
                            HorizontalContentAlignment = HorizontalAlignment.Left
                        };
                    }, true),
                    options: new ColumnOptions<PropertyViewModel>
                    {
                        CanUserResizeColumn = true,
                        CanUserSortColumn = false,
                    },
                    width: new GridLength(1, GridUnitType.Star))
            }
        };

        LogicalTreeSource.RowSelection!.SingleSelect = true;

        LogicalTreeSource.RowSelection.SelectionChanged += (_, args) =>
        {
            SelectedLogical = args.SelectedItems.FirstOrDefault();

            UpdateProperties();
        };
    }

    private void UpdateProperties()
    {
        _properties.Clear();

        if (SelectedLogical?.Logical is not IAvaloniaObject logical)
        {
            return;
        }

        var type = logical.GetType();

        var avaloniaProperties = AvaloniaPropertyRegistry.Instance.GetRegistered(type);
        var avaloniaAttachedProperties = AvaloniaPropertyRegistry.Instance.GetRegisteredAttached(type);
        var clrProperties = type.GetProperties();

        var avaloniaProps = new PropertyViewModel
        {
            Name = "Properties",
            Children = new ObservableCollection<PropertyViewModel>()
        };

        foreach (var avaloniaProperty in avaloniaProperties)
        {
            var value = logical.GetValue(avaloniaProperty);
            var property = new PropertyViewModel
            {
                Name = avaloniaProperty.Name,
                Value = value?.ToString()
            };
            avaloniaProps.Children.Add(property);
        }

        var avaloniaAttachedProps = new PropertyViewModel
        {
            Name = "Attached Properties",
            Children = new ObservableCollection<PropertyViewModel>()
        };

        foreach (var avaloniaAttachedProperty in avaloniaAttachedProperties)
        {
            var value = logical.GetValue(avaloniaAttachedProperty);
            var property = new PropertyViewModel
            {
                Name = avaloniaAttachedProperty.Name,
                Value = value?.ToString()
            };
            avaloniaAttachedProps.Children.Add(property);
        } 

        var clrProps = new PropertyViewModel
        {
            Name = "CLR Properties",
            Children = new ObservableCollection<PropertyViewModel>()
        };

        foreach (var clrProperty in clrProperties)
        {
            try
            {
                var value = clrProperty.GetValue(logical);
                var property = new PropertyViewModel
                {
                    Name = clrProperty.Name,
                    Value = value?.ToString()
                };
                clrProps.Children.Add(property);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        _properties.Add(avaloniaProps);
        _properties.Add(avaloniaAttachedProps);
        _properties.Add(clrProps);
    }

    private void AddToLogicalTree(ILogical root, ObservableCollection<LogicalViewModel> tree)
    {
        var logicalViewModel = new LogicalViewModel()
        {
            Name = root.GetType().Name,
            Logical = root,
            IsExpanded = true
        };
        tree.Add(logicalViewModel);

        var logicalDescendants = root.GetLogicalChildren();
        foreach (var logical in logicalDescendants)
        {
            if (logicalViewModel.Children is null)
            {
                logicalViewModel.Children = new ObservableCollection<LogicalViewModel>();
            }

            AddToLogicalTree(logical, logicalViewModel.Children);
        }
    }

    public void UpdateLogicalTree(ILogical root)
    {
        Dispatcher.UIThread.Post(
            () =>
            {
                _logicalTree.Clear();

                AddToLogicalTree(root, _logicalTree);
            }, 
            DispatcherPriority.Layout);
    }
}
