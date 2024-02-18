using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FormsBuilder;

[JsonSerializable(typeof(XamlItem))]
[JsonSerializable(typeof(XamlItem[]))]
[JsonSerializable(typeof(List<XamlItem>))]
[JsonSerializable(typeof(Dictionary<string, XamlValue>))]
[JsonSerializable(typeof(XamlValue))]
[JsonSerializable(typeof(StringXamlValue))]
[JsonSerializable(typeof(XamlItemXamlValue))]
[JsonSerializable(typeof(XamlItemsXamlValue))]
public partial class XamlItemJsonContext : JsonSerializerContext
{
    public static readonly XamlItemJsonContext s_instance = new(
        new JsonSerializerOptions
        {
            WriteIndented = true,
            // TODO:
            // ReferenceHandler = ReferenceHandler.Preserve,
            IncludeFields = false,
            IgnoreReadOnlyFields = true,
            IgnoreReadOnlyProperties = false,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
        });
}
