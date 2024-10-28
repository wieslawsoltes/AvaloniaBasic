using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.VisualTree;
using XamlDom;

namespace FormsBuilder;

public class AvaloniaControlMap : IControlMap<Control>
{
    private static string? GetUid(Control x)
    {
        return XamlItemProperties.GetUid(x);
    }

    private static IEnumerable<Control> GetSelfAndVisualDescendants(Control control)
    {
        return control
            .GetSelfAndVisualDescendants()
            .Where(x => x is Control)
            .Cast<Control>();
    }

    public Dictionary<string, XamlItem> CreateMap(XamlItem xamlItem)
    {
        return xamlItem
            .GetSelfAndChildren()
            .ToDictionary(x => x.Id, x => x);
    }

    public Dictionary<string, Control> CreateMap(Control control)
    {
        return GetSelfAndVisualDescendants(control)
            .Select(x => new 
            {
                Uid = GetUid(x), 
                Control = x
            })
            .Where(x => x.Uid is not null)
            .ToDictionary(x => x.Uid, x => x.Control);
    }
}
