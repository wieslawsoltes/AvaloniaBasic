namespace AvaloniaBasic.Model;

public interface IPropertyEditorFactory
{
    object? CreateEditor(IProperty property);
}
