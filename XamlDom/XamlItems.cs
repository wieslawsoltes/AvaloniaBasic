namespace XamlDom;

public class XamlItems : System.Collections.Generic.List<XamlItem>
{
    public XamlItems()
    {
    }

    public XamlItems(System.Collections.Generic.List<XamlItem> items)
    {
        AddRange(items);
    }
    
    public XamlItems(System.Collections.Generic.IEnumerable<XamlItem> items)
    {
        AddRange(items);
    }
}
