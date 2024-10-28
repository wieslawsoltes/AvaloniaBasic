using System.Text.Json.Serialization;

namespace XamlDom;

public partial class StringXamlValue : XamlValue
{
    [JsonConstructor]
    public StringXamlValue(string? value)
    {
        Value = value;
    }

    [JsonPropertyName("value")]
    public string? Value { get; set; }

    public static implicit operator string?(StringXamlValue value) 
        => value.Value;
}
