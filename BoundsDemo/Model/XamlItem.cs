using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BoundsDemo;

public class XamlItem
{
    public XamlItem(string name, Dictionary<string, object> properties)
    {
        Name = name;
        Properties = properties;
    }

    public string Name { get; }

    public Dictionary<string, object> Properties { get; }

    public Control? Create(bool isRoot = true)
    {
        var sb = new StringBuilder();

        Write(this, isRoot, sb);

        var xaml = sb.ToString();

        var obj = AvaloniaRuntimeXamlLoader.Load(xaml, null, null, null, designMode: false);

        return obj as Control;
    }

    public XamlItem Clone()
    {
        return new XamlItem(
            Name, 
            Properties.ToDictionary(
                x => x.Key, 
                x => Clone(x)));
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

    private static void Write(XamlItem xamlItem, bool isRoot, StringBuilder sb)
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
            foreach (var property in xamlItem.Properties.Where(x => x.Value is not string))
            {
                sb.Append('<');
                sb.Append(xamlItem.Name);
                sb.Append('.');
                sb.Append(property.Key);
                sb.Append('>');

                switch (property.Value)
                {
                    case XamlItem childXamlItem:
                    {
                        Write(childXamlItem, false, sb);
                        break;
                    }
                    case List<XamlItem> childXamlItems:
                    {
                        foreach (var childXamlItem in childXamlItems)
                        {
                            Write(childXamlItem, false, sb);
                        }

                        break;
                    }
                }

                sb.Append("</");
                sb.Append(xamlItem.Name);
                sb.Append('.');
                sb.Append(property.Key);
                sb.Append('>');
            }

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
