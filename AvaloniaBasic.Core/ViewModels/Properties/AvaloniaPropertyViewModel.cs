using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using Avalonia;
using Avalonia.Utilities;

namespace AvaloniaBasic.ViewModels.Properties;

public partial class AvaloniaPropertyViewModel : PropertyViewModel
{
    private readonly PropertyEditor _editor;
    private readonly AvaloniaProperty _property;
    
    public AvaloniaPropertyViewModel(PropertyEditor editor, AvaloniaProperty property)
    {
        _editor = editor;
        _property = property;
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        if (e.PropertyName == nameof(Value))
        {
            if (!_property.IsReadOnly && _editor.Current is { })
            {
                if (Value is { })
                {
                    if (Value.GetType() == _property.PropertyType)
                    {
                        try
                        {
                            _editor.Current?.SetValue(_property, Value);
                        }
                        catch (Exception exception)
                        {
                            Debug.WriteLine(exception);
                        }
                    }
                    else
                    {
                        try
                        {
                            if (TypeUtilities.TryConvert(_property.PropertyType, Value, CultureInfo.InvariantCulture, out var result))
                            {
                                _editor.Current?.SetValue(_property, result);
                            }
                        }
                        catch (Exception exception)
                        {
                            Debug.WriteLine(exception);
                        }
                    }
                }
            }
        }
    }

    public override Type GetValueType()
    {
        return _property.PropertyType;
    }

    public override bool IsReadOnly()
    {
        return _property.IsReadOnly;
    }
}
