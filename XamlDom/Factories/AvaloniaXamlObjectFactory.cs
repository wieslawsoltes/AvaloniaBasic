using System.Text;

namespace XamlDom;

public class AvaloniaXamlObjectFactory : IXamlObjectFactory
{
    private readonly IXamlWriter _xamlWriter;
    private readonly IXamlLoader _xamlLoader;

    public AvaloniaXamlObjectFactory(IXamlWriter xamlWriter, IXamlLoader xamlLoader)
    {
        _xamlWriter = xamlWriter;
        _xamlLoader = xamlLoader;
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

        var obj = _xamlLoader.Load(xaml);

        return obj;
    }
}
