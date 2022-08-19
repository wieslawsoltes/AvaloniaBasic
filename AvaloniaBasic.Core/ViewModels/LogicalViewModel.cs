using System.Collections.ObjectModel;
using Avalonia.LogicalTree;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaBasic.ViewModels;

[ObservableObject]
public partial class LogicalViewModel
{
    [ObservableProperty] private string? _name;
    [ObservableProperty] private bool _isExpanded;
    [ObservableProperty] private ILogical? _logical;
    [ObservableProperty] private ObservableCollection<LogicalViewModel>? _children;
}
