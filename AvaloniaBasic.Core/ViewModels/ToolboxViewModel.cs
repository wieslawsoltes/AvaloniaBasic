using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using AvaloniaBasic.Model;
using AvaloniaBasic.Utilities;
using AvaloniaBasic.ViewModels.Toolbox;
using AvaloniaBasic.ViewModels.Toolbox.Shapes;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaBasic.ViewModels;

public partial class ToolboxViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<IToolboxItem> _toolboxes;
    [ObservableProperty] private IToolboxItem? _selectedToolBoxItem;
    [ObservableProperty] private Canvas? _previewCanvas;
    [ObservableProperty] private Canvas? _dropAreaCanvas;

    public HierarchicalTreeDataGridSource<IToolboxItem> ToolboxSource { get; }

    public ToolboxViewModel()
    {
        _toolboxes = new ObservableCollection<IToolboxItem>
        {
            new ToolboxGroupViewModel
            {
                Name = "Layout",
                Items = new ObservableCollection<IDragItem>
                {
                    new BorderViewModel(),
                    new CanvasViewModel(),
                    new DecoratorViewModel(),
                    new DockPanelViewModel(),
                    new ExpanderViewModel(),
                    new GridViewModel(),
                    new GridSplitterViewModel(),
                    new LayoutTransformControlViewModel(),
                    new PanelViewModel(),
                    new RelativePanelViewModel(),
                    new ScrollBarViewModel(),
                    new ScrollViewerViewModel(),
                    new SplitViewViewModel(),
                    new StackPanelViewModel(),
                    new UniformGridViewModel(),
                    new WrapPanelViewModel(),
                }
            },
            new ToolboxGroupViewModel
            {
                Name = "Buttons",
                Items = new ObservableCollection<IDragItem>
                {
                    new ButtonViewModel(),
                    new ButtonSpinnerViewModel(),
                    new RepeatButtonViewModel(),
                    new RadioButtonViewModel(),
                    // SplitButton
                    new ToggleButtonViewModel(),
                    // ToggleSplitButton
                    new ToggleSwitchViewModel(),
                }
            },
            new ToolboxGroupViewModel
            {
                Name = "Data Display",
                Items = new ObservableCollection<IDragItem>
                {
                    new CarouselViewModel(),
                    new DataGridViewModel(),
                    new ItemsControlViewModel(),
                    new ItemsRepeaterViewModel(),
                    new ListBoxViewModel(),
                    new TabControlViewModel(),
                    new TabStripViewModel(),
                    new TreeDataGridViewModel(),
                    new TreeViewViewModel(),
                }
            },
            new ToolboxGroupViewModel
            {
                Name = "Text",
                Items = new ObservableCollection<IDragItem>
                {
                    new AccessTextViewModel(),
                    new AutoCompleteBoxViewModel(),
                    new MaskedTextBoxViewModel(),
                    new NumericUpDownViewModel(),
                    new TextBlockViewModel(),
                    new TextBoxViewModel(),
                    
                }
            },
            new ToolboxGroupViewModel
            {
                Name = "Value selectors",
                Items = new ObservableCollection<IDragItem>
                {
                    new CheckBoxViewModel(),
                    new ComboBoxViewModel(),
                    new SliderViewModel(),
                }
            },
            new ToolboxGroupViewModel
            {
                Name = "Content Display",
                Items = new ObservableCollection<IDragItem>
                {
                    new ContentControlViewModel(),
                    new LabelViewModel(),
                    new TransitioningContentControlViewModel(),
                    new ViewboxViewModel(),
                }
            },
            new ToolboxGroupViewModel
            {
                Name = "Images",
                Items = new ObservableCollection<IDragItem>
                {
                    // DrawingImage
                    new ImageViewModel(),
                }
            },
            new ToolboxGroupViewModel
            {
                Name = "Date and Time",
                Items = new ObservableCollection<IDragItem>
                {
                    new CalendarViewModel(),
                    new CalendarDatePickerViewModel(),
                    new DatePickerViewModel(),
                    new TimePickerViewModel(),
                }
            },
            new ToolboxGroupViewModel
            {
                Name = "Menus",
                Items = new ObservableCollection<IDragItem>
                {
                    new ContextMenuViewModel(),
                    // Flyout
                    new MenuViewModel(),
                    // MenuFlyout
                    new MenuItemViewModel(),
                    new SeparatorViewModel(),
                    // NativeMenu
                }
            },
            new ToolboxGroupViewModel
            {
                Name = "Shapes",
                Items = new ObservableCollection<IDragItem>
                {
                    new ArcViewModel(),
                    new EllipseViewModel(),
                    new LineViewModel(),
                    new PathViewModel(),
                    new PolygonViewModel(),
                    new PolylineViewModel(),
                    new RectangleViewModel(),
                }
            },
            new ToolboxGroupViewModel
            {
                Name = "Status Display",
                Items = new ObservableCollection<IDragItem>
                {
                    new ProgressBarViewModel(),
                }
            },
        };

        ToolboxSource = new HierarchicalTreeDataGridSource<IToolboxItem>(_toolboxes)
        {
            Columns =
            {
                new HierarchicalExpanderColumn<IToolboxItem>(
                    inner: new TemplateColumn<IToolboxItem>(
                        "Toolbox",
                        new FuncDataTemplate<IToolboxItem>((_, _) =>
                        {
                            return new Label
                            {
                                [!ContentControl.ContentProperty] = new Binding("Name")
                            };
                        }, true),
                        options: new ColumnOptions<IToolboxItem>
                        {
                            CanUserResizeColumn = false,
                            CanUserSortColumn = true,
                            CompareAscending = SortHelper.SortAscending<string?, IToolboxItem>(x => x.Name),
                            CompareDescending = SortHelper.SortDescending<string?, IToolboxItem>(x => x.Name)
                        },
                        width: new GridLength(1, GridUnitType.Star)), 
                    childSelector: x =>
                    {
                        if (x is ToolboxGroupViewModel toolboxGroupViewModel)
                        {
                            return toolboxGroupViewModel.Items;
                        }

                        return null;
                    },
                    hasChildrenSelector: x =>
                    {
                        if (x is ToolboxGroupViewModel toolboxGroupViewModel)
                        {
                            return toolboxGroupViewModel.Items?.Count > 0;
                        }

                        return false;
                    },
                    isExpandedSelector: x => x.IsExpanded)
            }
        };

        ToolboxSource.RowSelection!.SingleSelect = true;

        ToolboxSource.RowSelection.SelectionChanged += (_, args) =>
        {
            SelectedToolBoxItem = args.SelectedItems.FirstOrDefault();
        };
    }
}
