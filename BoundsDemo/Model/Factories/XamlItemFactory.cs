using System;
using System.Collections.Generic;
using System.Linq;

namespace BoundsDemo;

public static class XamlItemFactory
{
    public static XamlItem Clone(XamlItem xamlItem, XamlItemIdManager idManager, bool newId = true)
    {
        return new XamlItem(
            xamlItem.Name, 
            newId ? idManager.GetNewId() : xamlItem.Id,
            CloneProperties(xamlItem.Properties, idManager, newId),
            xamlItem.ContentProperty,
            xamlItem.ChildrenProperty);
    }

    public static Dictionary<string, XamlValue> CloneProperties(Dictionary<string, XamlValue> properties, XamlItemIdManager idManager, bool newId)
    {
        return properties.ToDictionary(
            x => x.Key,
            x => CloneValue(x.Value, idManager, newId));
    }

    public static XamlValue CloneValue(XamlValue value, XamlItemIdManager idManager, bool newId)
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
