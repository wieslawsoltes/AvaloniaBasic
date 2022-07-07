using System;
using Avalonia;

namespace AvaloniaBasicDemo.ViewModels.Properties;

public partial class AvaloniaPropertyViewModel : PropertyViewModel
{
    private readonly AvaloniaProperty _property;
    
    public AvaloniaPropertyViewModel(PropertyEditor editor, AvaloniaProperty property) 
        : base(editor)
    {
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
