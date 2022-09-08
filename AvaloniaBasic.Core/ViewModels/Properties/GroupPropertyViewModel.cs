using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaBasic.ViewModels.Properties;

public partial class GroupPropertyViewModel : PropertyViewModel
{
    [ObservableProperty] private ObservableCollection<PropertyViewModel>? _children;

    public override bool HasChildren => _children?.Count > 0;

    public override IEnumerable<PropertyViewModel>? GetChildren() => _children;

    public override Type GetValueType()
    {
        throw new NotImplementedException();
    }

    public override bool IsReadOnly()
    {
        throw new NotImplementedException();
    }
}
