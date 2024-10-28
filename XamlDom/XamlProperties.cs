namespace XamlDom;

public class XamlProperties : System.Collections.Generic.Dictionary<string, XamlValue>
{
    public XamlProperties()
    {
    }

    public XamlProperties(System.Collections.Generic.Dictionary<string, XamlValue> properties)
    {
        foreach (var property in properties)
        {
            Add(property.Key, property.Value);
        }
    }

    public XamlProperties(System.Collections.Generic.IDictionary<string, XamlValue> properties)
    {
        foreach (var property in properties)
        {
            Add(property.Key, property.Value);
        }
    }
}
