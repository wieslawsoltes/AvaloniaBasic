using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.LogicalTree;
using AvaloniaBasic.Model;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaBasic.ViewModels;

[ObservableObject]
public partial class LogicalViewModel : ITreeItem<LogicalViewModel>
{
    [ObservableProperty] private string? _name;
    [ObservableProperty] private bool _isExpanded;
    [ObservableProperty] private ILogical? _logical;
    [ObservableProperty] private ObservableCollection<LogicalViewModel>? _children;

    public bool HasChildren => _children?.Count > 0;

    public IEnumerable<LogicalViewModel>? GetChildren() => _children;
}
