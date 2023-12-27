using System.Text.Json.Serialization;

namespace BoundsDemo;

public partial class XamlItemXamlValue : XamlValue
{
    [JsonConstructor]
    public XamlItemXamlValue(XamlItem? value)
    {
        Value = value;
    }

    [JsonPropertyName("value")]
    public XamlItem? Value { get; set; }

    public static implicit operator XamlItem(XamlItemXamlValue value) => value.Value;
}
