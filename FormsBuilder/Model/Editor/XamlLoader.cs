using Avalonia.Markup.Xaml;

namespace FormsBuilder;

public class XamlLoader : IXamlLoader
{
    public object? Load(string xaml)
    {
        return AvaloniaRuntimeXamlLoader.Load(
            xaml: xaml, 
            localAssembly: null, 
            rootInstance: null, 
            uri: null, 
            designMode: false);
    }
}
