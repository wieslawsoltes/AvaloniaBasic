using System;
using System.Linq;

namespace FormsBuilder;

public class XamlItemFactory(IXamlItemIdManager idManager) : IXamlItemFactory
{
    public XamlItem CloneItem(XamlItem xamlItem, bool newId)
    {
        return new XamlItem(
            xamlItem.Name,
            CloneProperties(xamlItem.Properties, newId),
            newId && xamlItem.Id is not null ? idManager.GetNewId() : xamlItem.Id,
            xamlItem.ContentProperty, xamlItem.ChildrenProperty);
    }

    public XamlProperties CloneProperties(XamlProperties properties, bool newId)
    {
        return new XamlProperties(
            properties.ToDictionary(
                x => x.Key,
                x => CloneValue(x.Value, newId)));
    }

    public XamlValue CloneValue(XamlValue value, bool newId)
    {
        switch (value)
        {
            case StringXamlValue stringXamlValue:
                return new StringXamlValue(stringXamlValue.Value);
            case XamlItemXamlValue xamlItemXamlValue:
                return new XamlItemXamlValue(CloneItem(xamlItemXamlValue.Value, newId));
            case XamlItemsXamlValue xamlItemsXamlValue:
                return new XamlItemsXamlValue(
                    new XamlItems(
                        xamlItemsXamlValue.Value.Select(x => CloneItem(x, newId)).ToList()));
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
