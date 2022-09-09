using System.Collections.Generic;
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

    public abstract object CreatePreview();

    public abstract object CreateControl();

    public abstract void UpdatePreview(object control, bool isPointerOver);

    public abstract bool IsDropArea();
}
