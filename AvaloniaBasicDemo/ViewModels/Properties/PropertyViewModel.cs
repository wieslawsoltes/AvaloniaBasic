using System;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace AvaloniaBasicDemo.ViewModels.Properties;

public abstract partial class PropertyViewModel : ObservableObject
{
    [ObservableProperty] private string? _name;
    [ObservableProperty] private object? _value;
    [ObservableProperty] private bool _isExpanded = true;

    public abstract Type GetValueType();

    public abstract bool IsReadOnly();
}
