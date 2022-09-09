using Avalonia.Controls;
using AvaloniaBasic.ViewModels.Properties;

namespace AvaloniaBasic.Model;

public interface IPropertyEditorFactory
{
    Control? CreateEditor(PropertyViewModel propertyViewModel);
}
