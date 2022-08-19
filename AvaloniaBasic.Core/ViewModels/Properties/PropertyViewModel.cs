using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaBasic.ViewModels.Properties;

[ObservableObject]
public abstract partial class PropertyViewModel
{
    [ObservableProperty] private string? _name;
    [ObservableProperty] private object? _value;
    [ObservableProperty] private bool _isExpanded = true;

    public abstract Type GetValueType();

    public abstract bool IsReadOnly();
}
