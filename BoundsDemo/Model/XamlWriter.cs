using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoundsDemo;

public class XamlWriter
{
    public static void WriteXaml(XamlItem xamlItem, bool isRoot, StringBuilder sb, int level)
    {
        sb.Append(new string(' ', level));
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
                    sb.Append(new string(' ', level));
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
}
