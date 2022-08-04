using System.Collections.ObjectModel;
using AvaloniaBasicDemo.Model;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaBasicDemo.ViewModels;

public partial class ToolboxGroupViewModel : ObservableObject, IToolBoxItem
{
    [ObservableProperty] private string? _name;
    [ObservableProperty] private string? _icon;
    [ObservableProperty] private bool _isExpanded = true;
    [ObservableProperty] private ObservableCollection<IDragItem>? _items;
}
