using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace AvaloniaBasicDemo.ViewModels.Properties;

public abstract partial class PropertyViewModel : ObservableObject
{
    private readonly PropertyEditor _editor;
    [ObservableProperty] private string? _name;
    [ObservableProperty] private object? _value;
    [ObservableProperty] private bool _isExpanded = true;

    protected PropertyViewModel(PropertyEditor editor)
    {
        _editor = editor;
    }
}
