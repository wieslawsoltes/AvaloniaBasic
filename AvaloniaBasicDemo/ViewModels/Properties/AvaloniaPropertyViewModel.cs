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

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        if (e.PropertyName == nameof(Value))
        {
            if (!_property.IsReadOnly)
            {
                _editor.Current?.SetValue(_property, Value);
            }
        }
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
