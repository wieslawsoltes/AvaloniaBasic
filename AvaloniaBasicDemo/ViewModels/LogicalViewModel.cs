using System.Collections.ObjectModel;
using Avalonia.LogicalTree;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaBasicDemo.ViewModels;

public partial class LogicalViewModel : ObservableObject
{
    [ObservableProperty] private string? _name;
    [ObservableProperty] private bool _isExpanded;
    [ObservableProperty] private ILogical? _logical;
    [ObservableProperty] private ObservableCollection<LogicalViewModel>? _children;
}
