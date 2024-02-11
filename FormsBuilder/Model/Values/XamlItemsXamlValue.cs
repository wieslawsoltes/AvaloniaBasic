using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FormsBuilder;

public partial class XamlItemsXamlValue : XamlValue
{
    [JsonConstructor]
    public XamlItemsXamlValue(List<XamlItem>? value)
    {
        Value = value;
    }

    [JsonPropertyName("value")]
    public List<XamlItem>? Value { get; set; }

    public static implicit operator List<XamlItem>(XamlItemsXamlValue value) => value.Value;
}
