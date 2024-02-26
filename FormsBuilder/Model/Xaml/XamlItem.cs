global using XamlProperties = System.Collections.Generic.Dictionary<string, FormsBuilder.XamlValue>;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace FormsBuilder;

public interface IXamlItem
{
    string Name { get; }
    string Id { get; }
    XamlProperties Properties { get; }
    string? ContentProperty { get; }
    string? ChildrenProperty { get; }
    IEnumerable<XamlItem> Children { get; }
    IEnumerable<XamlItem> GetChildren();
    IEnumerable<XamlItem> GetSelfAndChildren();
    bool TryAddChild(XamlItem childXamlItem);
    bool TryRemove(XamlItem childXamlItem);
    XamlValue? GetContent();
    bool TrySetContent(XamlValue contentXamlValue);
}

public class XamlItem : IXamlItem
{
    [JsonConstructor]
    public XamlItem(
        string name, 
        string id,
        XamlProperties properties,
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
    public XamlProperties Properties { get; }

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

    public IEnumerable<XamlItem> GetSelfAndChildren()
    {
        yield return this;

        foreach (var xamlItem in GetChildren())
        {
            yield return xamlItem;
        }
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

    public bool TryRemove(XamlItem childXamlItem)
    {
        if (ChildrenProperty is not null)
        {
            if (Properties.TryGetValue(ChildrenProperty, out var childrenValue))
            {
                if (childrenValue is XamlItemsXamlValue xamlItemsXamlValue)
                {
                    if (xamlItemsXamlValue.Value.Remove(childXamlItem))
                    {
                        return true;
                    }
                }
            }
        }

        if (ContentProperty is not null)
        {
            if (Properties.TryGetValue(ContentProperty, out var contentValue))
            {
                if (contentValue is XamlItemXamlValue xamlItemXamlValue)
                {
                    if (xamlItemXamlValue.Value == childXamlItem)
                    {
                        Properties[ContentProperty] = null;
                        return true;
                    }
                }
            }
        }

        return false;
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
