using System.Text;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace FormsBuilder;

public static class XamlItemControlFactory
{
    public static Control? CreateControl(XamlItem xamlItem, bool isRoot = true, bool writeUid = false)
    {
        var settings = new XamlWriterSettings
        {
            Writer = new StringBuilder(),
            Namespace = "https://github.com/avaloniaui",
            WriteXmlns = isRoot,
            WriteUid = writeUid,
            Level = 0,
            WriteAttributesOnNewLine = false
        };

        var xamlWriter = new XamlWriter();
        xamlWriter.Write(xamlItem, settings);

        var xaml = settings.Writer.ToString();

#if DEBUG
        // Console.Clear();
        // Console.WriteLine(xaml);
#endif

        var obj = AvaloniaRuntimeXamlLoader.Load(xaml, null, null, null, designMode: false);

        return obj as Control;
    }
}
