using System;
using System.Linq;

namespace FormsBuilder;

public interface IXamlFactory
{
    XamlItem Clone(XamlItem xamlItem, bool newId = true);

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
    public XamlItem Clone(XamlItem xamlItem, bool newId = true)
    {
        return new XamlItem(
            xamlItem.Name,
            CloneProperties(xamlItem.Properties, newId),
            newId && xamlItem.Id is not null ? idManager.GetNewId() : xamlItem.Id,
            xamlItem.ContentProperty, xamlItem.ChildrenProperty);
    }

    private XamlProperties CloneProperties(XamlProperties properties, bool newId)
    {
        return properties.ToDictionary(
            x => x.Key,
            x => CloneValue(x.Value, newId));
    }

    private XamlValue CloneValue(XamlValue value, bool newId)
    {
        switch (value)
        {
            case StringXamlValue stringXamlValue:
                return new StringXamlValue(stringXamlValue.Value);
            case XamlItemXamlValue xamlItemXamlValue:
                return new XamlItemXamlValue(Clone(xamlItemXamlValue.Value, newId));
            case XamlItemsXamlValue xamlItemsXamlValue:
                return new XamlItemsXamlValue(xamlItemsXamlValue.Value.Select(x => Clone(x, newId)).ToList());
            default:
                throw new NotSupportedException();
        }
    }

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
