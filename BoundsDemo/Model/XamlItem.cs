using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace BoundsDemo;

public class XamlItem
{
    [JsonConstructor]
    public XamlItem(
        string name, 
        string id,
        Dictionary<string, XamlValue> properties,
        string? contentProperty = null,
        string? childrenProperty = null)
    {
        Name = name;
        Id = id;
        Properties = properties;
        ContentProperty = contentProperty;
        ChildrenProperty = childrenProperty;
    }

    [JsonPropertyName("name")]
    public string Name { get; }

    [JsonPropertyName("id")]
    public string Id { get; }

    [JsonPropertyName("properties")]
    public Dictionary<string, XamlValue> Properties { get; }

    [JsonPropertyName("contentProperty")]
    public string? ContentProperty { get; }

    [JsonPropertyName("childrenProperty")]
    public string? ChildrenProperty { get; }

    [JsonIgnore]
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
                case StringXamlValue:
                    return Enumerable.Empty<XamlItem>();
                case XamlItemXamlValue xamlItemXamlValue:
                    return Enumerable.Repeat(xamlItemXamlValue.Value, 1);
                case XamlItemsXamlValue xamlItemsXamlValue:
                    return xamlItemsXamlValue.Value;
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
                case StringXamlValue:
                    return Enumerable.Empty<XamlItem>();
                case XamlItemXamlValue xamlItemXamlValue:
                    return Enumerable.Repeat(xamlItemXamlValue.Value, 1);
                case XamlItemsXamlValue xamlItemsXamlValue:
                    return xamlItemsXamlValue.Value;
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
            if (childrenValue is XamlItemsXamlValue xamlItemsXamlValue)
            {
                xamlItemsXamlValue.Value.Add(childXamlItem);
            }
        }
        else
        {
            var childrenList = new List<XamlItem> {childXamlItem};
            Properties[ChildrenProperty] = new XamlItemsXamlValue(childrenList);
        }

        return true;
    }

    public XamlValue? GetContent()
    {
        if (ContentProperty is null)
        {
            return null;
        }

        return Properties[ContentProperty];
    }

    public bool TrySetContent(XamlValue contentXamlValue)
    {
        if (ContentProperty is null)
        {
            return false;
        }

        Properties[ContentProperty] = contentXamlValue;

        return true;
    }
}
