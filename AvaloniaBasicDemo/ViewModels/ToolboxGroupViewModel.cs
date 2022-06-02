using System.Collections.ObjectModel;
using AvaloniaBasicDemo.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace AvaloniaBasicDemo.ViewModels;

public partial class ToolboxGroupViewModel : ObservableObject, IToolBoxItem
{
    [ObservableProperty] private string? _name;
    [ObservableProperty] private string? _icon;
    [ObservableProperty] private bool _isExpanded = true;

    public ObservableCollection<IDragItem>? Items { get; init; }
}
