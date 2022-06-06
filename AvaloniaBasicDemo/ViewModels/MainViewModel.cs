using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace AvaloniaBasicDemo.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty] private ToolboxViewModel _toolbox;
    [ObservableProperty] private TreeViewModel _tree;

    public MainViewModel()
    {
        _toolbox = new ToolboxViewModel();
        _tree = new TreeViewModel();
    }
}
