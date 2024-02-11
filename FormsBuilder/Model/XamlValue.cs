using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FormsBuilder;

[JsonPolymorphic]
[JsonDerivedType(typeof(StringXamlValue), typeDiscriminator: "string")]
[JsonDerivedType(typeof(XamlItemXamlValue), typeDiscriminator: "xamlItem")]
[JsonDerivedType(typeof(XamlItemsXamlValue), typeDiscriminator: "xamlItems")]
public abstract partial class XamlValue
{
    public static explicit operator XamlValue(string value) => new StringXamlValue(value);

    public static explicit operator XamlValue(XamlItem value) => new XamlItemXamlValue(value);

    public static explicit operator XamlValue(List<XamlItem> value) => new XamlItemsXamlValue(value);
}
