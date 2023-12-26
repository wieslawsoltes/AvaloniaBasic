using System.Text;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BoundsDemo;

public static class XamlItemControlFactory
{
    public static Control? CreateControl(XamlItem xamlItem, bool isRoot = true)
    {
        var sb = new StringBuilder();

        XamlWriter.WriteXaml(xamlItem, isRoot, sb, level: 0);

        var xaml = sb.ToString();

#if DEBU
        Console.Clear();
        Console.WriteLine(xaml);
#endif

        var obj = AvaloniaRuntimeXamlLoader.Load(xaml, null, null, null, designMode: false);

        return obj as Control;
    }
}
