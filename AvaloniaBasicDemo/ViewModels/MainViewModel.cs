using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using AvaloniaBasicDemo.Model;
using AvaloniaBasicDemo.ViewModels.Toolbox;
using AvaloniaBasicDemo.ViewModels.Toolbox.Shapes;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace AvaloniaBasicDemo.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<IToolBoxItem> _toolboxes;

    public HierarchicalTreeDataGridSource<IToolBoxItem> Source { get; }

    public MainViewModel()
    {
        _toolboxes = new ObservableCollection<IToolBoxItem>
        {
            new ToolboxGroupViewModel
            {
                Name = "Layout",
                Items = new ObservableCollection<IDragItem>
                {
                    new BorderViewModel { Name = "Border" },
                    new CanvasViewModel { Name = "Canvas" },
                    new DecoratorViewModel { Name = "Decorator" },
                    new DockPanelViewModel { Name = "DockPanel" },
                    new ExpanderViewModel { Name = "Expander" },
                    new GridViewModel { Name = "Grid" },
                    new GridSplitterViewModel { Name = "GridSplitter" },
                    new LayoutTransformControlViewModel { Name = "LayoutTransformControl" },
                    new PanelViewModel { Name = "Panel" },
                    new RelativePanelViewModel { Name = "RelativePanel" },
                    new ScrollBarViewModel { Name = "ScrollBar" },
                    new ScrollViewerViewModel { Name = "ScrollViewer" },
                    new SplitViewViewModel { Name = "SplitView" },
                    new StackPanelViewModel { Name = "StackPanel" },
                    new UniformGridViewModel { Name = "UniformGrid" },
                    new WrapPanelViewModel { Name = "WrapPanel" },
                }
            },
            new ToolboxGroupViewModel
            {
                Name = "Buttons",
                Items = new ObservableCollection<IDragItem>
                {
                    new ButtonViewModel { Name = "Button" },
                    new ButtonSpinnerViewModel { Name = "ButtonSpinner" },
                    new RepeatButtonViewModel { Name = "RepeatButton" },
                    new RadioButtonViewModel { Name = "RadioButton" },
                    // SplitButton
                    new ToggleButtonViewModel { Name = "ToggleButton" },
                    // ToggleSplitButton
                    new ToggleSwitchViewModel { Name = "ToggleSwitch" },
                }
            },
            new ToolboxGroupViewModel
            {
                Name = "Data Display",
                Items = new ObservableCollection<IDragItem>
                {
                    new CarouselViewModel { Name = "Carousel" },
                    new DataGridViewModel { Name = "DataGrid" },
                    new ItemsControlViewModel { Name = "ItemsControl" },
                    new ItemsRepeaterViewModel { Name = "ItemsRepeater" },
                    new ListBoxViewModel { Name = "ListBox" },
                    new TabControlViewModel { Name = "TabControl" },
                    new TabStripViewModel { Name = "TabStrip" },
                    new TreeDataGridViewModel { Name = "TreeDataGrid" },
                    new TreeViewViewModel { Name = "TreeView" },
                }
            },
            new ToolboxGroupViewModel
            {
                Name = "Text",
                Items = new ObservableCollection<IDragItem>
                {
                    new AccessTextViewModel { Name = "AccessText" },
                    new AutoCompleteBoxViewModel { Name = "AutoCompleteBox" },
                    new MaskedTextBoxViewModel { Name = "MaskedTextBoxViewModel" },
                    new NumericUpDownViewModel { Name = "NumericUpDown" },
                    new TextBlockViewModel { Name = "TextBlock" },
                    new TextBoxViewModel { Name = "TextBox" },
                    
                }
            },
            new ToolboxGroupViewModel
            {
                Name = "Value selectors",
                Items = new ObservableCollection<IDragItem>
                {
                    new CheckBoxViewModel { Name = "CheckBox" },
                    new ComboBoxViewModel { Name = "ComboBox" },
                    new SliderViewModel { Name = "Slider" },
                }
            },
            new ToolboxGroupViewModel
            {
                Name = "Content Display",
                Items = new ObservableCollection<IDragItem>
                {
                    new ContentControlViewModel { Name = "ContentControl" },
                    new LabelViewModel { Name = "Label" },
                    new TransitioningContentControlViewModel { Name = "TransitioningContentControl" },
                    new ViewboxViewModel { Name = "Viewbox" },
                }
            },
            new ToolboxGroupViewModel
            {
                Name = "Images",
                Items = new ObservableCollection<IDragItem>
                {
                    // DrawingImage
                    new ImageViewModel { Name = "Image" },
                }
            },
            new ToolboxGroupViewModel
            {
                Name = "Date and Time",
                Items = new ObservableCollection<IDragItem>
                {
                    new CalendarViewModel { Name = "Calendar" },
                    new CalendarDatePickerViewModel { Name = "CalendarDatePicker" },
                    new DatePickerViewModel { Name = "DatePicker" },
                    new TimePickerViewModel { Name = "TimePicker" },
                }
            },
            new ToolboxGroupViewModel
            {
                Name = "Menus",
                Items = new ObservableCollection<IDragItem>
                {
                    new ContextMenuViewModel { Name = "ContextMenu" },
                    // Flyout
                    new MenuViewModel { Name = "Menu" },
                    // MenuFlyout
                    new MenuItemViewModel { Name = "MenuItem" },
                    new SeparatorViewModel { Name = "Separator" },
                    // NativeMenu
                }
            },
            new ToolboxGroupViewModel
            {
                Name = "Shapes",
                Items = new ObservableCollection<IDragItem>
                {
                    new ArcViewModel { Name = "Arc" },
                    new EllipseViewModel { Name = "Ellipse" },
                    new LineViewModel { Name = "Line" },
                    new PathViewModel { Name = "Path" },
                    new PolygonViewModel { Name = "Polygon" },
                    new PolylineViewModel { Name = "Polyline" },
                    new RectangleViewModel { Name = "Rectangle" },
                }
            },
            new ToolboxGroupViewModel
            {
                Name = "Status Display",
                Items = new ObservableCollection<IDragItem>
                {
                    new ProgressBarViewModel { Name = "ProgressBar" },
                }
            },
        };

        Source = new HierarchicalTreeDataGridSource<IToolBoxItem>(_toolboxes)
		{
			Columns =
			{
				new HierarchicalExpanderColumn<IToolBoxItem>(
                    inner: new TemplateColumn<IToolBoxItem>(
					    "Toolbox",
					    new FuncDataTemplate<IToolBoxItem>((_, _) =>
                        {
                            return new Label
                            {
                                [!ContentControl.ContentProperty] = new Binding("Name")
                            };
                        }, true),
					    options: new ColumnOptions<IToolBoxItem>
					    {
						    CanUserResizeColumn = false,
						    CanUserSortColumn = false,
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
    }
}
