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
