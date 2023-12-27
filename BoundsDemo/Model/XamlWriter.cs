using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoundsDemo;

public class XamlWriter
{
    public static void WriteXaml(XamlItem xamlItem, bool writeXmlns, bool writeUid, StringBuilder sb, int level, bool writeAttributesOnNewLine = false)
    {
        sb.Append(new string(' ', level));
        sb.Append('<');
        sb.Append(xamlItem.Name);

        if (writeXmlns)
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

            sb.Append("xmlns=\"https://github.com/avaloniaui\"");
        }

        if (writeUid)
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

            sb.Append("xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"");
        }

        var hasObjectProperties = xamlItem.Properties.Any(x => x.Value is not StringXamlValue);

        WriteAttributeProperties(xamlItem, sb, level, writeUid, writeAttributesOnNewLine);

        if (hasObjectProperties)
        {
            sb.Append('>');

            WriteObjectProperties(xamlItem, sb, level, writeUid);

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

    private static void WriteUidAttribute(XamlItem xamlItem, StringBuilder sb, int level, bool writeAttributesOnNewLine)
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

        // TODO: Use x:Uid instead of Tag
        // sb.Append("x:Uid");
        sb.Append("Tag");
        sb.Append('=');
        sb.Append('"');
        sb.Append(xamlItem.Id);
        sb.Append('"');
    }

    private static void WriteAttributeProperties(XamlItem xamlItem, StringBuilder sb, int level, bool writeUid, bool writeAttributesOnNewLine)
    {
        if (writeUid)
        {
            WriteUidAttribute(xamlItem, sb, level, writeAttributesOnNewLine);
        }

        foreach (var property in xamlItem.Properties.Where(x => x.Value is StringXamlValue))
        {
            if (property.Value is not StringXamlValue stringXamlValue)
            {
                continue;
            }
            
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
            sb.Append(stringXamlValue.Value);
            sb.Append('"');
        }
    }

    private static void WritePropertyValue(StringBuilder sb, int level, XamlValue value, bool writeUid)
    {
        switch (value)
        {
            case XamlItemXamlValue xamlItemXamlValue:
            {
                sb.AppendLine();

                WriteXaml(xamlItemXamlValue.Value, false, writeUid, sb, level + 2);

                break;
            }
            case XamlItemsXamlValue xamlItemsXamlValue:
            {
                foreach (var xamlItem in xamlItemsXamlValue.Value)
                {
                    sb.AppendLine();

                    WriteXaml(xamlItem, false, writeUid, sb, level + 2);
                }

                break;
            }
        }
    }

    private static void WriteObjectProperties(XamlItem xamlItem, StringBuilder sb, int level, bool writeUid)
    {
        var properties = xamlItem.Properties.Where(x => x.Value is not StringXamlValue);
 
        foreach (var property in properties)
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

            WritePropertyValue(sb, level, property.Value, writeUid);

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
