using System.Collections.Generic;
using AvaloniaBasic.Model;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaBasic.ViewModels;

[ObservableObject]
public abstract partial class ToolboxItemViewModel : IToolboxItem
{
    [ObservableProperty] private string? _name;
    [ObservableProperty] private string? _group;
    [ObservableProperty] private string? _icon;
    [ObservableProperty] private bool _isExpanded;

    public virtual bool HasChildren => false;

    public virtual IEnumerable<IToolboxItem>? GetChildren() => null;
}
