using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.LogicalTree;
using Avalonia.Threading;
using AvaloniaBasicDemo.Model;
using AvaloniaBasicDemo.Utilities;
using AvaloniaBasicDemo.ViewModels.Properties;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace AvaloniaBasicDemo.ViewModels;

public partial class TreeViewModel : ObservableObject
{
    private readonly Dictionary<Type, TypeProperties> _typePropertiesCache = new();
    private readonly Dictionary<IAvaloniaObject, ObservableCollection<PropertyViewModel>> _propertiesCache = new();

    private readonly PropertyEditor _editor = new ();

    [ObservableProperty] private ObservableCollection<LogicalViewModel> _logicalTree;
    [ObservableProperty] private ObservableCollection<PropertyViewModel> _properties;
    [ObservableProperty] private LogicalViewModel? _selectedLogical;

    public HierarchicalTreeDataGridSource<LogicalViewModel> LogicalTreeSource { get; }

    public HierarchicalTreeDataGridSource<PropertyViewModel> PropertiesSource { get; }

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

        LogicalTreeSource.RowSelection!.SingleSelect = true;

        LogicalTreeSource.RowSelection.SelectionChanged += (_, args) =>
        {
            SelectedLogical = args.SelectedItems.FirstOrDefault();

            UpdateProperties();
        };

        _properties = new ObservableCollection<PropertyViewModel>();

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
                            CanUserSortColumn = true,
                            CompareAscending = SortHelper.SortAscending<string?, PropertyViewModel>(x => x.Name),
                            CompareDescending = SortHelper.SortDescending<string?, PropertyViewModel>(x => x.Name)
                        },
                        width: new GridLength(1, GridUnitType.Star)), 
                    childSelector: x =>
                    {
                        if (x is GroupPropertyViewModel g)
                        {
                            return g.Children;
                        }

                        return null;
                    },
                    hasChildrenSelector: x =>
                    {
                        if (x is GroupPropertyViewModel g)
                        {
                            return g.Children?.Count > 0;
                        }

                        return false;
                    },
                    isExpandedSelector: x => x.IsExpanded),
                new TemplateColumn<PropertyViewModel>(
                    "Value",
                    new FuncDataTemplate<PropertyViewModel>((p, _) =>
                    {
                        switch (p)
                        {
                            case GroupPropertyViewModel groupPropertyViewModel:
                            {
                                // TODO:

                                break;
                            }
                            case AvaloniaPropertyViewModel avaloniaPropertyViewModel:
                            {
                                var isReadOnly = avaloniaPropertyViewModel.IsReadOnly();
                                var type = avaloniaPropertyViewModel.GetValueType();
                                if (type == typeof(bool) || type == typeof(bool?))
                                {
                                    return new CheckBox
                                    {
                                        [!ToggleButton.IsCheckedProperty] = new Binding("Value"),
                                        HorizontalAlignment = HorizontalAlignment.Stretch,
                                        HorizontalContentAlignment = HorizontalAlignment.Left,
                                        IsEnabled = !isReadOnly
                                    };
                                }
                                else if (type == typeof(string))
                                {
                                    return new TextBox
                                    {
                                        [!TextBox.TextProperty] = new Binding("Value"),
                                        HorizontalAlignment = HorizontalAlignment.Stretch,
                                        IsReadOnly = isReadOnly
                                    };   
                                }
                                // TODO:

                                break;
                            }
                            case ClrPropertyViewModel clrPropertyViewModel:
                            {
                                // TODO:

                                break;
                            }
                        }

                        return new TextBlock()
                        {
                            [!TextBlock.TextProperty] = new Binding("Value"),
                            HorizontalAlignment = HorizontalAlignment.Stretch,
                            VerticalAlignment = VerticalAlignment.Center
                        };
                    }, false),
                    options: new ColumnOptions<PropertyViewModel>
                    {
                        CanUserResizeColumn = true,
                        CanUserSortColumn = false
                    },
                    width: new GridLength(1, GridUnitType.Star))
            }
        };
    }

    private void UpdateProperties()
    {
        if (SelectedLogical?.Logical is not IAvaloniaObject logical)
        {
            _editor.Current = null;
            return;
        }

        _editor.Current = logical;

        if (_propertiesCache.TryGetValue(logical, out var cachedProperties))
        {
            _properties.Clear();

            foreach (var property in cachedProperties)
            {
                _properties.Add(property);
            }

            return;
        }

        var type = logical.GetType();
        if (!_typePropertiesCache.TryGetValue(type, out var typeProperties))
        {
            typeProperties = new TypeProperties(type);
            _typePropertiesCache[type] = typeProperties;
        }

        // Properties

        var avaloniaProps = new GroupPropertyViewModel(_editor)
        {
            Name = "Properties",
            Children = new ObservableCollection<PropertyViewModel>()
        };

        foreach (var avaloniaProperty in typeProperties.Properties)
        {
            var value = logical.GetValue(avaloniaProperty);
            var property = new AvaloniaPropertyViewModel(_editor, avaloniaProperty)
            {
                Name = avaloniaProperty.Name,
                Value = value
            };
            avaloniaProps.Children.Add(property);
        }

        // Attached Properties

        var avaloniaAttachedProps = new GroupPropertyViewModel(_editor)
        {
            Name = "Attached Properties",
            Children = new ObservableCollection<PropertyViewModel>()
        };

        foreach (var avaloniaAttachedProperty in typeProperties.AttachedProperties)
        {
            var value = logical.GetValue(avaloniaAttachedProperty);
            var property = new AvaloniaPropertyViewModel(_editor, avaloniaAttachedProperty)
            {
                Name = $"{avaloniaAttachedProperty.OwnerType.Name}.{avaloniaAttachedProperty.Name}",
                Value = value
            };
            avaloniaAttachedProps.Children.Add(property);
        } 

        // CLR Properties

        var clrProps = new GroupPropertyViewModel(_editor)
        {
            Name = "CLR Properties",
            Children = new ObservableCollection<PropertyViewModel>()
        };

        foreach (var clrProperty in typeProperties.ClrProperties)
        {
            try
            {
                var value = clrProperty.GetValue(logical);
                var property = new ClrPropertyViewModel(_editor, clrProperty)
                {
                    Name = clrProperty.Name,
                    Value = value
                };
                clrProps.Children.Add(property);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        // Property Groups

        var properties = new ObservableCollection<PropertyViewModel>
        {
            avaloniaProps, 
            avaloniaAttachedProps, 
            clrProps
        };

        _propertiesCache[logical] = properties;

        _properties.Clear();

        foreach (var property in properties)
        {
            _properties.Add(property);
        }
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
