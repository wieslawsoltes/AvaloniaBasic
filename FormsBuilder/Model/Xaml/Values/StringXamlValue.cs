using System.Globalization;
using System.Text.Json.Serialization;

namespace FormsBuilder;

public partial class StringXamlValue : XamlValue
{
    [JsonConstructor]
    public StringXamlValue(string? value)
    {
        Value = value;
    }

    [JsonPropertyName("value")]
    public string? Value { get; set; }

    public static implicit operator string(StringXamlValue value) 
        => value.Value;

    public static StringXamlValue From(float value) 
        => new (value.ToString(CultureInfo.InvariantCulture));

    public static StringXamlValue From(double value) 
        => new (value.ToString(CultureInfo.InvariantCulture));

    public static StringXamlValue From(decimal value) 
        => new (value.ToString(CultureInfo.InvariantCulture));
}
