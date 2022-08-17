using Avalonia.Controls;
using AvaloniaBasic.Model;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaBasic.ViewModels;

public abstract partial class DragItemViewModel : ObservableObject, IDragItem
{
    [ObservableProperty] private string? _name;
    [ObservableProperty] private string? _group;
    [ObservableProperty] private string? _icon;
    [ObservableProperty] private bool _isExpanded;

    public abstract Control CreatePreview();

    public abstract Control CreateControl();

    public abstract void UpdatePreview(Control control, bool isPointerOver);

    public abstract bool IsDropArea();
}
