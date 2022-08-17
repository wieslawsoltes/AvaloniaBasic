using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Avalonia;

namespace AvaloniaBasic.Model;

internal class TypeProperties
{
    public List<AvaloniaProperty> Properties { get; }

    public List<AvaloniaProperty> AttachedProperties { get; }

    public List<PropertyInfo> ClrProperties { get; }

    public TypeProperties(Type type)
    {
        Properties = AvaloniaPropertyRegistry.Instance
            .GetRegistered(type)
            .OrderBy(x => x.Name)
            .ToList();

        AttachedProperties = AvaloniaPropertyRegistry.Instance
            .GetRegisteredAttached(type)
            .OrderBy(x => x.OwnerType.Name).ThenBy(x => x.Name)
            .ToList();

        ClrProperties = type
            .GetProperties()
            .OrderBy(x => x.Name)
            .ToList();  
    }
}
