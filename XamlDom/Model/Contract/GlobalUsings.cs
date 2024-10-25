namespace FormsBuilder;

// global using XamlProperties = System.Collections.Generic.Dictionary<string, FormsBuilder.XamlValue>;

// global using XamlItems = System.Collections.Generic.List<FormsBuilder.XamlItem>;

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

public class XamlItems : System.Collections.Generic.List<FormsBuilder.XamlItem>
{
    public XamlItems()
    {
    }

    public XamlItems(System.Collections.Generic.List<FormsBuilder.XamlItem> items)
    {
        AddRange(items);
    }
    
    public XamlItems(System.Collections.Generic.IEnumerable<FormsBuilder.XamlItem> items)
    {
        AddRange(items);
    }
}
