using System;
using System.Collections.Generic;
using System.Linq;

namespace FormsBuilder;

public static class XamlItemFactory
{
    public static XamlItem Clone(XamlItem xamlItem, IXamlItemIdManager idManager, bool newId = true)
    {
        return new XamlItem(
            xamlItem.Name,
            CloneProperties(xamlItem.Properties, idManager, newId),
            newId && xamlItem.Id is not null ? idManager.GetNewId() : xamlItem.Id,
            xamlItem.ContentProperty, xamlItem.ChildrenProperty);
    }

    public static XamlProperties CloneProperties(XamlProperties properties, IXamlItemIdManager idManager, bool newId)
    {
        return properties.ToDictionary(
            x => x.Key,
            x => CloneValue(x.Value, idManager, newId));
    }

    public static XamlValue CloneValue(XamlValue value, IXamlItemIdManager idManager, bool newId)
    {
        switch (value)
        {
            case StringXamlValue stringXamlValue:
                return new StringXamlValue(stringXamlValue.Value);
            case XamlItemXamlValue xamlItemXamlValue:
                return new XamlItemXamlValue(Clone(xamlItemXamlValue.Value, idManager, newId));
            case XamlItemsXamlValue xamlItemsXamlValue:
                return new XamlItemsXamlValue(xamlItemsXamlValue.Value.Select(x => Clone(x, idManager, newId)).ToList());
            default:
                throw new NotSupportedException();
        }
    }
}
