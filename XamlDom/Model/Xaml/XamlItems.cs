namespace FormsBuilder;

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
