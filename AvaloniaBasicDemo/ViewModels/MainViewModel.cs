using AvaloniaBasicDemo.ViewModels.Settings;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace AvaloniaBasicDemo.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty] private ToolboxViewModel _toolbox;
    [ObservableProperty] private TreeViewModel _tree;
    [ObservableProperty] private DragSettingsViewModel _dragSettings;
    [ObservableProperty] private GridSettingsViewModel _gridSettings;

    public MainViewModel()
    {
        _toolbox = new ToolboxViewModel();
        _tree = new TreeViewModel();
        _dragSettings = new DragSettingsViewModel();
        _gridSettings = new GridSettingsViewModel();
    }
}
