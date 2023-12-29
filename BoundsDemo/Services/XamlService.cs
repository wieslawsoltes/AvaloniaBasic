using System.Linq;

namespace BoundsDemo;

public class XamlService
{
    public static void WriteXaml(XamlItem xamlItem, XamlServiceSettings settings)
    {
        settings.Writer.Append(new string(' ', settings.Level));
        settings.Writer.Append('<');
        settings.Writer.Append(xamlItem.Name);

        WriteRootNamespace(settings);
        WriteNamespaces(settings);

        var hasObjectProperties = xamlItem.Properties.Any(x => x.Value is not StringXamlValue);

        WriteAttributeProperties(xamlItem, settings);

        if (hasObjectProperties)
        {
            settings.Writer.Append('>');

            WriteObjectProperties(xamlItem, settings);

            settings.Writer.AppendLine();
            settings.Writer.Append(new string(' ', settings.Level));
            settings.Writer.Append("</");
            settings.Writer.Append(xamlItem.Name);
            settings.Writer.Append('>');
        }
        else
        {
            settings.Writer.Append("/>");
        }
    }

    private static void WriteRootNamespace(XamlServiceSettings settings)
    {
        if (!settings.WriteXmlns)
        {
            return;
        }

        if (settings.WriteAttributesOnNewLine)
        {
            settings.Writer.AppendLine();
            settings.Writer.Append(new string(' ', settings.Level + 2));
        }
        else
        {
            settings.Writer.Append(' ');
        }

        settings.Writer.Append("xmlns=\"");
        settings.Writer.Append(settings.Namespace);
        settings.Writer.Append('"');
    }

    private static void WriteNamespaces(XamlServiceSettings settings)
    {
        if (!settings.WriteXmlns || !settings.WriteUid)
        {
            return;
        }

        if (settings.WriteAttributesOnNewLine)
        {
            settings.Writer.AppendLine();
            settings.Writer.Append(new string(' ', settings.Level + 2));
        }
        else
        {
            settings.Writer.Append(' ');
        }

        // TODO:
        // settings.Writer.Append("xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"");
        settings.Writer.Append("xmlns:boundsDemo=\"clr-namespace:BoundsDemo\"");
    }

    private static void WriteUidAttribute(XamlItem xamlItem, XamlServiceSettings settings)
    {
        if (settings.WriteAttributesOnNewLine)
        {
            settings.Writer.AppendLine();
            settings.Writer.Append(new string(' ', settings.Level + 2));
        }
        else
        {
            settings.Writer.Append(' ');
        }

        // TODO: Use x:Uid instead of Tag
        // settings.Writer.Append("x:Uid");
        // settings.Writer.Append("Tag");
        settings.Writer.Append("boundsDemo:XamlItemProperties.Uid");
        settings.Writer.Append('=');
        settings.Writer.Append('"');
        settings.Writer.Append(xamlItem.Id);
        settings.Writer.Append('"');
    }

    private static void WriteAttributeProperties(XamlItem xamlItem, XamlServiceSettings settings)
    {
        if (settings.WriteUid)
        {
            WriteUidAttribute(xamlItem, settings);
        }

        foreach (var property in xamlItem.Properties.Where(x => x.Value is StringXamlValue))
        {
            if (property.Value is not StringXamlValue stringXamlValue)
            {
                continue;
            }
            
            if (settings.WriteAttributesOnNewLine)
            {
                settings.Writer.AppendLine();
                settings.Writer.Append(new string(' ', settings.Level + 2));
            }
            else
            {
                settings.Writer.Append(' ');
            }

            settings.Writer.Append(property.Key);
            settings.Writer.Append('=');
            settings.Writer.Append('"');
            settings.Writer.Append(stringXamlValue.Value);
            settings.Writer.Append('"');
        }
    }

    private static void WritePropertyValue(XamlValue value, XamlServiceSettings settings)
    {
        switch (value)
        {
            case XamlItemXamlValue xamlItemXamlValue:
            {
                if (xamlItemXamlValue.Value is null)
                {
                    break;
                }

                settings.Writer.AppendLine();

                WriteXaml(xamlItemXamlValue.Value, settings with { WriteXmlns = false, Level = settings.Level + 2 });

                break;
            }
            case XamlItemsXamlValue xamlItemsXamlValue:
            {
                if (xamlItemsXamlValue.Value is null)
                {
                    break;
                }

                foreach (var xamlItem in xamlItemsXamlValue.Value)
                {
                    settings.Writer.AppendLine();

                    WriteXaml(xamlItem, settings with { WriteXmlns = false, Level = settings.Level + 2 });
                }

                break;
            }
        }
    }

    private static void WriteObjectProperties(XamlItem xamlItem, XamlServiceSettings settings)
    {
        var properties = xamlItem.Properties.Where(x => x.Value is not StringXamlValue);
 
        foreach (var property in properties)
        {
            var writeContentTag = xamlItem.ContentProperty is null || property.Key != xamlItem.ContentProperty;

            if (writeContentTag)
            {
                settings.Writer.AppendLine();
                settings.Writer.Append(new string(' ', settings.Level));
                settings.Writer.Append('<');
                settings.Writer.Append(xamlItem.Name);
                settings.Writer.Append('.');
                settings.Writer.Append(property.Key);
                settings.Writer.Append('>');
            }

            WritePropertyValue(property.Value, settings);

            if (writeContentTag)
            {
                settings.Writer.AppendLine();
                settings.Writer.Append(new string(' ', settings.Level));
                settings.Writer.Append("</");
                settings.Writer.Append(xamlItem.Name);
                settings.Writer.Append('.');
                settings.Writer.Append(property.Key);
                settings.Writer.Append('>');
            }
        }
    }
}
