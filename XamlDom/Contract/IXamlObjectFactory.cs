namespace XamlDom;

public interface IXamlObjectFactory
{
    object? CreateControl(XamlItem xamlItem, bool isRoot = true, bool writeUid = false);
}
