using System;

namespace AvaloniaBasic.Model;

public interface IProperty : ITreeItem<IProperty>
{
    string? Name { get; set; }

    object? Value { get; set; }

    bool IsExpanded { get; set; }

    Type GetValueType();

    bool IsReadOnly();
}
