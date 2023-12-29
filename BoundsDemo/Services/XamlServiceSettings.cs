using System.Text;

namespace BoundsDemo;

public readonly record struct XamlServiceSettings(
    StringBuilder Writer,
    string Namespace,
    bool WriteXmlns, 
    bool WriteUid, 
    int Level, 
    bool WriteAttributesOnNewLine);
