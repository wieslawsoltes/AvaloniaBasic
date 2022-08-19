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
}
