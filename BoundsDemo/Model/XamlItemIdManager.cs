using System;

namespace BoundsDemo;

public class XamlItemIdManager
{
    public string GetNewId()
    {
        return Guid.NewGuid().ToString();
    }
}
