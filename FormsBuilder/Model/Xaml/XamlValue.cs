using System.Collections.Generic;
using System.Globalization;
using System.Text.Json.Serialization;
using Avalonia;

namespace FormsBuilder;

[JsonPolymorphic]
[JsonDerivedType(typeof(StringXamlValue), typeDiscriminator: "xamlString")]
[JsonDerivedType(typeof(XamlItemXamlValue), typeDiscriminator: "xamlItem")]
[JsonDerivedType(typeof(XamlItemsXamlValue), typeDiscriminator: "xamlItems")]
public abstract partial class XamlValue
{
    public static implicit operator XamlValue(string value)
    {
        return new StringXamlValue(value);
    }

    public static implicit operator XamlValue(float value)
    {
        return value.ToString(CultureInfo.InvariantCulture);
    }

    public static implicit operator XamlValue(double value)
    {
        return value.ToString(CultureInfo.InvariantCulture);
    }

    public static implicit operator XamlValue(decimal value)
    {
        return value.ToString(CultureInfo.InvariantCulture);
    }

    public static implicit operator XamlValue(Point value)
    {
        return string.Concat(
            value.X.ToString(CultureInfo.InvariantCulture), 
            ',', 
            value.Y.ToString(CultureInfo.InvariantCulture));
    }

    public static implicit operator XamlValue(XamlItem value)
    {
        return new XamlItemXamlValue(value);
    }

    public static implicit operator XamlValue(XamlItems value)
    {
        return new XamlItemsXamlValue(value);
    }
}
