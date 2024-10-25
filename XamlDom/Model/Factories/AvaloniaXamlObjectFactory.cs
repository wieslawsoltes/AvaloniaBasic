using System.Text;
using Avalonia.Markup.Xaml;

namespace FormsBuilder;

public class AvaloniaXamlObjectFactory : IXamlObjectFactory
{
    private readonly IXamlWriter _xamlWriter;

    public AvaloniaXamlObjectFactory(IXamlWriter xamlWriter)
    {
        _xamlWriter = xamlWriter;
    }
    
    public object? CreateControl(XamlItem xamlItem, bool isRoot = true, bool writeUid = false)
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

        _xamlWriter.Write(xamlItem, settings);

        var xaml = settings.Writer.ToString();

#if DEBUG
        // Console.Clear();
        // Console.WriteLine(xaml);
#endif

        var obj = AvaloniaRuntimeXamlLoader.Load(xaml, null, null, null, designMode: false);

        return obj;
    }
}
