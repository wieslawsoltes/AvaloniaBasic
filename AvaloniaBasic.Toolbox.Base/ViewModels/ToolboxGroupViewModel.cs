using System.Collections.Generic;
using System.Collections.ObjectModel;
using AvaloniaBasic.Model;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaBasic.ViewModels;

[ObservableObject]
public partial class ToolboxGroupViewModel : IToolboxItem
{
    [ObservableProperty] private string? _name;
    [ObservableProperty] private string? _group;
    [ObservableProperty] private string? _icon;
    [ObservableProperty] private bool _isExpanded = true;
    [ObservableProperty] private ObservableCollection<IDragItem>? _items;

    public bool HasChildren => _items?.Count > 0;

    public IEnumerable<IToolboxItem>? GetChildren() => _items;
}
