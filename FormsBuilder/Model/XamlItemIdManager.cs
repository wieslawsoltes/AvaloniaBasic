using System;

namespace FormsBuilder;

public interface IXamlItemIdManager
{
    string GetNewId();
}

public class XamlItemIdManager : IXamlItemIdManager
{
    public string GetNewId()
    {
        return Guid.NewGuid().ToString();
    }
}
