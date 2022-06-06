using System.Collections.ObjectModel;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace AvaloniaBasicDemo.ViewModels;

public partial class PropertyViewModel : ObservableObject
{
    [ObservableProperty] private string? _name;
    [ObservableProperty] private object? _value;
    [ObservableProperty] private bool _isExpanded = true;
    [ObservableProperty] private ObservableCollection<PropertyViewModel>? _children;
}
