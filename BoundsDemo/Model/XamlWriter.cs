using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoundsDemo;

public class XamlWriter
{
    public static void WriteXaml(XamlItem xamlItem, bool writeXmlns, StringBuilder sb, int level, bool writeAttributesOnNewLine = false)
    {
        sb.Append(new string(' ', level));
        sb.Append('<');
        sb.Append(xamlItem.Name);

        if (writeXmlns)
        {
            sb.Append(' ');
            sb.Append("xmlns=\"https://github.com/avaloniaui\"");
        }

        var hasObjectProperties = xamlItem.Properties.Any(x => x.Value is not string);

        WriteAttributeProperties(xamlItem, sb, level, writeAttributesOnNewLine);

        if (hasObjectProperties)
        {
            sb.Append('>');

            WriteObjectProperties(xamlItem, sb, level);

            sb.AppendLine();
            sb.Append(new string(' ', level));
            sb.Append("</");
            sb.Append(xamlItem.Name);
            sb.Append('>');
        }
        else
        {
            sb.Append("/>");
        }
    }

    private static void WriteAttributeProperties(XamlItem xamlItem, StringBuilder sb, int level, bool writeAttributesOnNewLine)
    {
        foreach (var property in xamlItem.Properties.Where(x => x.Value is string))
        {
            if (writeAttributesOnNewLine)
            {
                sb.AppendLine();
                sb.Append(new string(' ', level + 2));
            }
            else
            {
                sb.Append(' ');
            }

            sb.Append(property.Key);
            sb.Append('=');
            sb.Append('"');
            sb.Append(property.Value);
            sb.Append('"');
        }
    }

    private static void WritePropertyValue(StringBuilder sb, int level, object value)
    {
        switch (value)
        {
            case XamlItem childXamlItem:
            {
                sb.AppendLine();

                WriteXaml(childXamlItem, false, sb, level + 2);

                break;
            }
            case List<XamlItem> childXamlItems:
            {
                foreach (var childXamlItem in childXamlItems)
                {
                    sb.AppendLine();

                    WriteXaml(childXamlItem, false, sb, level + 2);
                }

                break;
            }
        }
    }

    private static void WriteObjectProperties(XamlItem xamlItem, StringBuilder sb, int level)
    {
        foreach (var property in xamlItem.Properties.Where(x => x.Value is not string))
        {
            var writeContentTag = xamlItem.ContentProperty is null || property.Key != xamlItem.ContentProperty;

            if (writeContentTag)
            {
                sb.AppendLine();
                sb.Append(new string(' ', level));
                sb.Append('<');
                sb.Append(xamlItem.Name);
                sb.Append('.');
                sb.Append(property.Key);
                sb.Append('>');
            }

            WritePropertyValue(sb, level, property.Value);

            if (writeContentTag)
            {
                sb.AppendLine();
                sb.Append(new string(' ', level));
                sb.Append("</");
                sb.Append(xamlItem.Name);
                sb.Append('.');
                sb.Append(property.Key);
                sb.Append('>');
            }
        }
    }
}
