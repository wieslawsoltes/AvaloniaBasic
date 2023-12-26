using System;
using System.Collections.Generic;
using System.Linq;

namespace BoundsDemo;

public class XamlItem
{
    public XamlItem(
        string name, 
        Dictionary<string, object> properties,
        string? contentProperty = null,
        string? childrenProperty = null)
    {
        Name = name;
        Properties = properties;
        ContentProperty = contentProperty;
        ChildrenProperty = childrenProperty;
    }

    public string Name { get; }

    public Dictionary<string, object> Properties { get; }

    public string? ContentProperty { get; }

    public string? ChildrenProperty { get; }

    public IEnumerable<XamlItem> Children => GetChildren();

    public XamlItem Clone()
    {
        return new XamlItem(
            Name, 
            Properties.ToDictionary(x => x.Key, x => Clone(x)),
            ContentProperty,
            ChildrenProperty);
    }

    public IEnumerable<XamlItem> GetChildren()
    {
        if (ChildrenProperty is null)
        {
            return Enumerable.Empty<XamlItem>();
        }

        Properties.TryGetValue(ChildrenProperty, out var value);

        switch (value)
        {
            case null:
                return Enumerable.Empty<XamlItem>();
            case XamlItem xamlItem:
                return Enumerable.Repeat(xamlItem, 1);
            case List<XamlItem> xamlItems:
                return xamlItems;
            default:
                throw new NotSupportedException();
        }
    }

    private static object Clone(KeyValuePair<string, object> kvp)
    {
        switch (kvp.Value)
        {
            case string str:
                return str;
            case XamlItem xamlItem:
                return xamlItem.Clone();
            case List<XamlItem> xamlItems:
                return xamlItems.Select(y => y.Clone()).ToList();
            default:
                throw new NotSupportedException();
        }
    }
}
