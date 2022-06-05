using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace AvaloniaBasicDemo.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty] private ToolboxViewModel _toolbox;

    public MainViewModel()
    {
        _toolbox = new ToolboxViewModel();
    }
}
