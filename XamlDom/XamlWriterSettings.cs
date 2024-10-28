using System.Text;

namespace XamlDom;

public readonly record struct XamlWriterSettings(
    StringBuilder Writer,
    string Namespace,
    bool WriteXmlns, 
    bool WriteUid, 
    int Level, 
    bool WriteAttributesOnNewLine);
