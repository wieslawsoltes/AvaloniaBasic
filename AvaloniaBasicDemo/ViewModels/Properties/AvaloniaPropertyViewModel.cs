using System;
using System.ComponentModel;
using Avalonia;

namespace AvaloniaBasicDemo.ViewModels.Properties;

public partial class AvaloniaPropertyViewModel : PropertyViewModel
{
    private readonly PropertyEditor _editor;
    private readonly AvaloniaProperty _property;
    
    public AvaloniaPropertyViewModel(PropertyEditor editor, AvaloniaProperty property)
    {
        _editor = editor;
        _property = property;
    }

    public Type GetValueType()
    {
        return _property.PropertyType;
    }

    public bool IsReadOnly()
    {
        return _property.IsReadOnly;
    }
}
