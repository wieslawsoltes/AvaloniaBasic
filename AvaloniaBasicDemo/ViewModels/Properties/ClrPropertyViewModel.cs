using System;
using System.ComponentModel;
using System.Reflection;

namespace AvaloniaBasicDemo.ViewModels.Properties;

public partial class ClrPropertyViewModel : PropertyViewModel
{
    private readonly PropertyEditor _editor;
    private readonly PropertyInfo _property;
    
    public ClrPropertyViewModel(PropertyEditor editor, PropertyInfo property)
    {
        _editor = editor;
        _property = property;
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        if (e.PropertyName == nameof(Value))
        {
            if (_property.CanWrite)
            {
                if (_editor.Current is not null)
                {
                    _property.SetValue(_editor.Current, Value);
                }
            }
        }
    }

    public Type GetValueType()
    {
        return _property.PropertyType;
    }

    public bool IsReadOnly()
    {
        return !_property.CanWrite;
    }
}
