using System.Linq;

namespace FormsBuilder;

public interface IXamlWriter
{
    void Write(XamlItem xamlItem, XamlWriterSettings settings);
    void WriteRootNamespace(XamlWriterSettings settings);
    void WriteNamespaces(XamlWriterSettings settings);
    void WriteUidAttribute(XamlItem xamlItem, XamlWriterSettings settings);
    void WriteAttributeProperties(XamlItem xamlItem, XamlWriterSettings settings);
    void WritePropertyValue(XamlValue value, XamlWriterSettings settings);
    void WriteObjectProperties(XamlItem xamlItem, XamlWriterSettings settings);
}

public class XamlWriter : IXamlWriter
{
    public void Write(XamlItem xamlItem, XamlWriterSettings settings)
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

            WriteObjectProperties(xamlItem, settings with { Level = settings.Level + 2 });

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

    public void WriteRootNamespace(XamlWriterSettings settings)
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

    public void WriteNamespaces(XamlWriterSettings settings)
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
        settings.Writer.Append("xmlns:formsBuilder=\"clr-namespace:FormsBuilder\"");
    }

    public void WriteUidAttribute(XamlItem xamlItem, XamlWriterSettings settings)
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
        settings.Writer.Append("formsBuilder:XamlItemProperties.Uid");
        settings.Writer.Append('=');
        settings.Writer.Append('"');
        settings.Writer.Append(xamlItem.Id);
        settings.Writer.Append('"');
    }

    public void WriteAttributeProperties(XamlItem xamlItem, XamlWriterSettings settings)
    {
        if (settings.WriteUid && xamlItem.Id is not null)
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

    public void WritePropertyValue(XamlValue value, XamlWriterSettings settings)
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

                Write(xamlItemXamlValue.Value, settings with { WriteXmlns = false, Level = settings.Level });

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

                    Write(xamlItem, settings with { WriteXmlns = false, Level = settings.Level });
                }

                break;
            }
        }
    }

    public void WriteObjectProperties(XamlItem xamlItem, XamlWriterSettings settings)
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

            WritePropertyValue(
                property.Value, 
                settings with { Level = writeContentTag ? settings.Level + 2 : settings.Level });

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
