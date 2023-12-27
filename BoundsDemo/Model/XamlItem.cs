using System;
using System.Collections.Generic;
using System.Linq;

namespace BoundsDemo;

public class XamlItem
{
    public XamlItem(
        string name, 
        string id,
        Dictionary<string, object> properties,
        string? contentProperty = null,
        string? childrenProperty = null)
    {
        Name = name;
        Id = id;
        Properties = properties;
        ContentProperty = contentProperty;
        ChildrenProperty = childrenProperty;
    }

    public string Name { get; }

    public string Id { get; }

    public Dictionary<string, object> Properties { get; }

    public string? ContentProperty { get; }

    public string? ChildrenProperty { get; }

    public IEnumerable<XamlItem> Children => GetChildren();

    public IEnumerable<XamlItem> GetChildren()
    {
        if (ChildrenProperty is null && ContentProperty is null)
        {
            return Enumerable.Empty<XamlItem>();
        }

        if (ChildrenProperty is not null)
        {
            Properties.TryGetValue(ChildrenProperty, out var children);

            switch (children)
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

        if (ContentProperty is not null)
        {
            Properties.TryGetValue(ContentProperty, out var content);

            switch (content)
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

        return Enumerable.Empty<XamlItem>();
    }

    public bool TryAddChild(XamlItem childXamlItem)
    {
        if (ChildrenProperty is null)
        {
            return false;
        }

        if (Properties.TryGetValue(ChildrenProperty, out var childrenValue))
        {
            if (childrenValue is List<XamlItem> children)
            {
                children.Add(childXamlItem);
            }
        }
        else
        {
            var childrenList = new List<XamlItem> {childXamlItem};
            Properties[ChildrenProperty] = childrenList;
        }

        return true;
    }

    public object? GetContent()
    {
        if (ContentProperty is null)
        {
            return null;
        }

        return Properties[ContentProperty];
    }

    public bool TrySetContent(XamlItem contentXamlItem)
    {
        if (ContentProperty is null)
        {
            return false;
        }

        Properties[ContentProperty] = contentXamlItem;

        return true;
    }

    public XamlItem Clone(XamlItemIdManager idManager, bool newId = true)
    {
        return new XamlItem(
            Name, 
            newId ? idManager.GetNewId() : Id,
            Properties.ToDictionary(x => x.Key, x => CloneValue(x.Value, idManager, newId)),
            ContentProperty,
            ChildrenProperty);
    }

    private static object CloneValue(object value, XamlItemIdManager idManager, bool newId)
    {
        switch (value)
        {
            case string str:
                return str;
            case XamlItem xamlItem:
                return xamlItem.Clone(idManager, newId);
            case List<XamlItem> xamlItems:
                return xamlItems.Select(y => y.Clone(idManager, newId)).ToList();
            default:
                throw new NotSupportedException();
        }
    }
}
