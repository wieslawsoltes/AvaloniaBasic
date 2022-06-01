using System.Collections.ObjectModel;
using AvaloniaBasicDemo.Model;
using AvaloniaBasicDemo.ViewModels.Toolbox;

namespace AvaloniaBasicDemo.ViewModels;

public class MainViewModel
{
    public ObservableCollection<IDragItem> Items { get; set; }

    public MainViewModel()
    {
        Items = new ObservableCollection<IDragItem>
        {
            new BorderViewModel { Name = "Border" },
            new ButtonViewModel { Name = "Button" },
            new CalendarViewModel { Name = "Calendar" },
            new CanvasViewModel { Name = "Canvas" },
            new CheckBoxViewModel { Name = "CheckBox" },
            new ComboBoxViewModel { Name = "ComboBox" },
            new ContentControlViewModel { Name = "ContentControl" },
            new DataGridViewModel { Name = "DataGrid" },
            new DatePickerViewModel { Name = "DatePicker" },
            new DockPanelViewModel { Name = "DockPanel" },
            new EllipseViewModel { Name = "Ellipse" },
            new ExpanderViewModel { Name = "Expander" },
            new GridViewModel { Name = "Grid" },
            new GridSplitterViewModel { Name = "GridSplitter" },
            new ImageViewModel { Name = "Image" },
            new LabelViewModel { Name = "Label" },
            new ListBoxViewModel { Name = "ListBox" },
            new MenuViewModel { Name = "Menu" },
            new ProgressBarViewModel { Name = "ProgressBar" },
            new RadioButtonViewModel { Name = "RadioButton" },
            new RectangleViewModel { Name = "Rectangle" },
            new ScrollBarViewModel { Name = "ScrollBar" },
            new ScrollViewerViewModel { Name = "ScrollViewer" },
            new SeparatorViewModel { Name = "Separator" },
            new SliderViewModel { Name = "Slider" },
            new StackPanelViewModel { Name = "StackPanel" },
            new TabControlViewModel { Name = "TabControl" },
            new TextBlockViewModel { Name = "TextBlock" },
            new TextBoxViewModel { Name = "TextBox" },
            new TreeViewViewModel { Name = "TreeView" },
            new ViewboxViewModel { Name = "Viewbox" },
            new WrapPanelViewModel { Name = "WrapPanel" },
        };
    }
}
