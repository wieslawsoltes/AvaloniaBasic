using System.Collections.Generic;
using Avalonia.Controls;
using AvaloniaBasic.Model;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaBasic.ViewModels;

[ObservableObject]
public abstract partial class ToolboxItemViewModel : IDragItem
{
    [ObservableProperty] private string? _name;
    [ObservableProperty] private string? _group;
    [ObservableProperty] private string? _icon;
    [ObservableProperty] private bool _isExpanded;

    public bool HasChildren => false;

    public IEnumerable<IToolboxItem>? GetChildren() => null;

    public abstract Control CreatePreview();

    public abstract Control CreateControl();

    public abstract void UpdatePreview(Control control, bool isPointerOver);

    public abstract bool IsDropArea();
}
