using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using Avalonia.Utilities;

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
            if (!_property.CanWrite && _editor.Current is { })
            {
                if (Value is { })
                {
                    if (Value.GetType() == _property.PropertyType)
                    {
                        _property.SetValue(_editor.Current, Value);
                    }
                    else
                    {
                        if (TypeUtilities.TryConvert(_property.PropertyType, Value, CultureInfo.InvariantCulture, out var result))
                        {
                            _property.SetValue(_editor.Current, result);
                        }
                    }
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
