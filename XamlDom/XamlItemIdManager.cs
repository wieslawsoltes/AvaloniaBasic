using System;

namespace XamlDom;

public class XamlItemIdManager : IXamlItemIdManager
{
    public string GetNewId()
    {
        return Guid.NewGuid().ToString();
    }
}
