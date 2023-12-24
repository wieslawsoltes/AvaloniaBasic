using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

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

    public IEnumerable<XamlItem> Children
    {
        get
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
    }

    public Control? Create(bool isRoot = true)
    {
        var sb = new StringBuilder();

        WriteXaml(this, isRoot, sb);

        var xaml = sb.ToString();
        
        Console.WriteLine($"[XAML {Name}]");
        Console.WriteLine(xaml);

        var obj = AvaloniaRuntimeXamlLoader.Load(xaml, null, null, null, designMode: false);

        return obj as Control;
    }

    public XamlItem Clone()
    {
        return new XamlItem(
            Name, 
            Properties.ToDictionary(
                x => x.Key, 
                x => Clone(x)),
            ContentProperty,
            ChildrenProperty);
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

    public static void WriteXaml(XamlItem xamlItem, bool isRoot, StringBuilder sb)
    {
        sb.Append('<');
        sb.Append(xamlItem.Name);

        if (isRoot)
        {
            sb.Append(' ');
            sb.Append("xmlns=\"https://github.com/avaloniaui\"");
        }

        var isComplex = xamlItem.Properties.Any(x => x.Value is not string);

        foreach (var property in xamlItem.Properties.Where(x => x.Value is string))
        {
            sb.Append(' ');
            sb.Append(property.Key);
            sb.Append('=');
            sb.Append('"');
            sb.Append(property.Value);
            sb.Append('"');
            sb.Append(' ');
        }

        if (isComplex)
        {
            sb.Append('>');

            foreach (var property in xamlItem.Properties.Where(x => x.Value is not string))
            {
                var writeContentTag = xamlItem.ContentProperty is null || property.Key != xamlItem.ContentProperty;

                if (writeContentTag)
                {
                    sb.AppendLine();
                    sb.Append('<');
                    sb.Append(xamlItem.Name);
                    sb.Append('.');
                    sb.Append(property.Key);
                    sb.Append('>');
                }

                switch (property.Value)
                {
                    case XamlItem childXamlItem:
                    {
                        sb.AppendLine();
                        WriteXaml(childXamlItem, false, sb);
                        break;
                    }
                    case List<XamlItem> childXamlItems:
                    {
                        foreach (var childXamlItem in childXamlItems)
                        {
                            sb.AppendLine();
                            WriteXaml(childXamlItem, false, sb);
                        }

                        break;
                    }
                }

                if (writeContentTag)
                {
                    sb.AppendLine();
                    sb.Append("</");
                    sb.Append(xamlItem.Name);
                    sb.Append('.');
                    sb.Append(property.Key);
                    sb.Append('>');
                }
            }

            sb.AppendLine();
            sb.Append("</");
            sb.Append(xamlItem.Name);
            sb.Append('>');
        }
        else
        {
            sb.Append("/>");
        }
    }
}
