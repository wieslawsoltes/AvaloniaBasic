using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XamlDom;

public partial class XamlItemsXamlValue : XamlValue
{
    [JsonConstructor]
    public XamlItemsXamlValue(XamlItems? value)
    {
        Value = value;
    }

    [JsonPropertyName("value")]
    public XamlItems? Value { get; set; }

    public static implicit operator XamlItems(XamlItemsXamlValue value) => value.Value;
}
