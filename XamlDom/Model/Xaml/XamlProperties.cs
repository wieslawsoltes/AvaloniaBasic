namespace FormsBuilder;

public class XamlProperties : System.Collections.Generic.Dictionary<string, FormsBuilder.XamlValue>
{
    public XamlProperties()
    {
    }

    public XamlProperties(System.Collections.Generic.Dictionary<string, FormsBuilder.XamlValue> properties)
    {
        foreach (var property in properties)
        {
            Add(property.Key, property.Value);
        }
    }

    public XamlProperties(System.Collections.Generic.IDictionary<string, FormsBuilder.XamlValue> properties)
    {
        foreach (var property in properties)
        {
            Add(property.Key, property.Value);
        }
    }
}
