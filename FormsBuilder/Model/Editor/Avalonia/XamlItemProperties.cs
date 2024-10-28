using Avalonia;

namespace FormsBuilder;

public class XamlItemProperties : AvaloniaObject
{
    public static readonly AttachedProperty<string?> UidProperty =
        AvaloniaProperty.RegisterAttached<XamlItemProperties, AvaloniaObject, string?>("Uid");

    public static string? GetUid(AvaloniaObject element)
    {
        return element.GetValue(UidProperty);
    }

    public static void SetUid(AvaloniaObject element, string? value)
    {
        element.SetValue(UidProperty, value);
    }
}
