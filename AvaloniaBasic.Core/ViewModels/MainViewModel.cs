using Avalonia.Controls;
using AvaloniaBasic.ViewModels.Settings;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaBasic.ViewModels;

[ObservableObject]
public partial class MainViewModel
{
    [ObservableProperty] private ToolboxViewModel _toolbox;
    [ObservableProperty] private TreeViewModel _tree;
    [ObservableProperty] private DragSettingsViewModel _dragSettings;
    [ObservableProperty] private GridSettingsViewModel _gridSettings;
    [ObservableProperty] private Canvas? _previewCanvas;
    [ObservableProperty] private Canvas? _dropAreaCanvas;

    public MainViewModel()
    {
        _toolbox = new ToolboxViewModel();
        _tree = new TreeViewModel();
        _dragSettings = new DragSettingsViewModel();
        _gridSettings = new GridSettingsViewModel();
    }
}
