using System;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Layout;
using AvaloniaBasic.Model;

namespace AvaloniaBasic.Services;

public class PropertyEditorFactory : IPropertyEditorFactory
{
    public object? CreateEditor(IProperty property)
    {
        var isReadOnly = property.IsReadOnly();
        var type = property.GetValueType();

        if (type == typeof(bool) || type == typeof(bool?))
        {
            return new CheckBox
            {
                [!ToggleButton.IsCheckedProperty] = new Binding("Value"),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                IsEnabled = !isReadOnly
            };
        }

        if (type == typeof(string) 
            || type == typeof(decimal) || type == typeof(decimal?) 
            || type == typeof(double) || type == typeof(double?) 
            || type == typeof(float) || type == typeof(float?) 
            || type == typeof(long) || type == typeof(long?) 
            || type == typeof(int) || type == typeof(int?) 
            || type == typeof(short) || type == typeof(short?) 
            || type == typeof(byte)|| type == typeof(byte?))
        {
            return new TextBox
            {
                [!TextBox.TextProperty] = new Binding("Value"),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                IsReadOnly = isReadOnly
            };   
        }

        if (type.IsEnum)
        {
            var values = Enum.GetValues(type);

            return new ComboBox
            {
                Items = values,
                [!!SelectingItemsControl.SelectedItemProperty] = new Binding("Value"),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                IsEnabled = !isReadOnly
            };
        }

        return null;
    }
}
