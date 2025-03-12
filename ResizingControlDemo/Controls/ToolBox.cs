using System;
using Avalonia;
using Avalonia.Controls;

namespace ResizingControlDemo.Controls;

public class ToolBox : ListBox
{
    public static readonly StyledProperty<Canvas> EditorCanvasProperty = 
        AvaloniaProperty.Register<ToolBox, Canvas>(nameof(EditorCanvas));

    [ResolveByName]
    public Canvas EditorCanvas
    {
        get => GetValue(EditorCanvasProperty);
        set => SetValue(EditorCanvasProperty, value);
    }

    protected override Type StyleKeyOverride => typeof(ListBox);

    protected override Control CreateContainerForItemOverride(object? item, int index, object? recycleKey)
    {
        return new ToolBoxItem();
    }
}
