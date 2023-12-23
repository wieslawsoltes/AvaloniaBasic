using System.Collections.Generic;

namespace BoundsDemo;

public class ToolBoxItem
{
    public ToolBoxItem(string name, Dictionary<string, object> properties)
    {
        Name = name;
        Properties = properties;
    }

    public string Name { get; }

    public Dictionary<string, object> Properties { get; }
}
