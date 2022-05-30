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
            new CheckBoxViewModel { Name = "CheckBox" },
            new ComboBoxViewModel { Name = "ComboBox" },
            new DataGridViewModel { Name = "DataGrid" },
            new GridViewModel { Name = "Grid" },
            new ImageViewModel { Name = "Image" },
            new LabelViewModel { Name = "Label" },
            new RadioButtonViewModel { Name = "RadioButton" },
            new RectangleViewModel { Name = "Rectangle" },
            new StackPanelViewModel { Name = "StackPanel" },
            new TabControlViewModel { Name = "TabControl" },
            new TextBlockViewModel { Name = "TextBlock" },
            new TextBoxViewModel { Name = "TextBox" },
        };
    }
}
