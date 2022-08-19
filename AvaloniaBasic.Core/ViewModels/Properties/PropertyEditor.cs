using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaBasic.ViewModels.Properties;

[ObservableObject]
public partial class PropertyEditor
{
    [ObservableProperty] private AvaloniaObject? _current;

    [ObservableProperty] private bool _isUpdating;
}
