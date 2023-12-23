using System.Collections.Generic;
using System.Text;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BoundsDemo;

public class XamlItem
{
    public XamlItem(string name, Dictionary<string, string> properties)
    {
        Name = name;
        Properties = properties;
    }

    public string Name { get; }

    public Dictionary<string, string> Properties { get; }

    public Control? Create()
    {
        var sb = new StringBuilder();

        sb.Append('<');
        sb.Append(Name);
        sb.Append(' ');
        sb.Append("xmlns=\"https://github.com/avaloniaui\"");

        foreach (var property in Properties)
        {
            sb.Append(' ');
            sb.Append(property.Key);
            sb.Append('=');
            sb.Append('"');
            sb.Append(property.Value);
            sb.Append('"');
            sb.Append(' ');
        }

        sb.Append("/>");

        var xaml = sb.ToString();

        var obj = AvaloniaRuntimeXamlLoader.Load(xaml, null, null, null, designMode: false);

        return obj as Control;
    }
}
