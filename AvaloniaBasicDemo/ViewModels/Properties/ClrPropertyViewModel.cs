using System;
using System.Reflection;

namespace AvaloniaBasicDemo.ViewModels.Properties;

public partial class ClrPropertyViewModel : PropertyViewModel
{
    private readonly PropertyInfo _property;
    
    public ClrPropertyViewModel(PropertyEditor editor, PropertyInfo property) 
        : base(editor)
    {
        _property = property;
    }

    public Type GetValueType()
    {
        return _property.PropertyType;
    }
}
