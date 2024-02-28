namespace FormsBuilder;

public interface IXamlItemFactory
{
    XamlItem CloneItem(XamlItem xamlItem, bool newId);

    XamlProperties CloneProperties(XamlProperties properties, bool newId);

    XamlValue CloneValue(XamlValue value, bool newId);

    XamlItem CreateControl(
        string name,
        XamlProperties properties,
        string? contentProperty = null,
        string? childrenProperty = null);

    XamlItem CreateStyle(
        string name,
        XamlProperties properties,
        string? contentProperty = null,
        string? childrenProperty = null);

    XamlItem CreateSetter(
        string name,
        XamlProperties properties,
        string? contentProperty = null,
        string? childrenProperty = null);
}
