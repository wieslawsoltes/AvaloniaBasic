using System.Text;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace FormsBuilder;

public interface IAvaloniaFactory
{
    Control? CreateControl(XamlItem xamlItem, bool isRoot = true, bool writeUid = false);
}

public class AvaloniaFactory : IAvaloniaFactory
{
    private readonly IXamlWriter _xamlWriter;

    public AvaloniaFactory(IXamlWriter xamlWriter)
    {
        _xamlWriter = xamlWriter;
    }
    
    public Control? CreateControl(XamlItem xamlItem, bool isRoot = true, bool writeUid = false)
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

        return obj as Control;
    }
}
