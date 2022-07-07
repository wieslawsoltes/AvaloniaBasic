using System.Collections.ObjectModel;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace AvaloniaBasicDemo.ViewModels.Properties;

public partial class GroupPropertyViewModel : PropertyViewModel
{
    [ObservableProperty] private ObservableCollection<PropertyViewModel>? _children;
}
