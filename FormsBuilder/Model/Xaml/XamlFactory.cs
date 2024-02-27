namespace FormsBuilder;

public interface IXamlFactory
{
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

public class XamlFactory(IXamlItemIdManager idManager) : IXamlFactory
{
    public XamlItem CreateControl(
        string name,
        XamlProperties properties,
        string? contentProperty = null,
        string? childrenProperty = null)
    {
        return new XamlItem(
            name: name,
            properties: properties,
            id: idManager.GetNewId(),
            contentProperty: contentProperty, 
            childrenProperty: childrenProperty);
    }

    public XamlItem CreateStyle(
        string name,
        XamlProperties properties,
        string? contentProperty = null,
        string? childrenProperty = null)
    {
        return new XamlItem(
            name: name,
            properties: properties,
            id: null,
            contentProperty: contentProperty, 
            childrenProperty: childrenProperty);
    }

    public XamlItem CreateSetter(
        string name,
        XamlProperties properties,
        string? contentProperty = null,
        string? childrenProperty = null)
    {
        return new XamlItem(
            name: name,
            properties: properties,
            id: null,
            contentProperty: contentProperty, 
            childrenProperty: childrenProperty);
    }
}
