using System;

namespace FormsBuilder;

public class XamlItemIdManager : IXamlItemIdManager
{
    public string GetNewId()
    {
        return Guid.NewGuid().ToString();
    }
}
