using Avalonia;
using Avalonia.Controls;
using ResizingControlDemo.Controls;

namespace ResizingControlDemo;

public partial class EditorView : UserControl
{
    public static readonly StyledProperty<Canvas> EditorCanvasProperty = 
        AvaloniaProperty.Register<EditorView, Canvas>(nameof(EditorCanvas));

    public static readonly StyledProperty<ResizingHostControl> ResizingHostControlProperty = 
        AvaloniaProperty.Register<EditorView, ResizingHostControl>(nameof(ResizingHostControl));

    [ResolveByName]
    public Canvas EditorCanvas
    {
        get => GetValue(EditorCanvasProperty);
        set => SetValue(EditorCanvasProperty, value);
    }

    [ResolveByName]
    public ResizingHostControl ResizingHostControl
    {
        get => GetValue(ResizingHostControlProperty);
        set => SetValue(ResizingHostControlProperty, value);
    }

    public EditorView()
    {
        InitializeComponent();
        
        SetCurrentValue(EditorCanvasProperty, PART_EditorCanvas);
        SetCurrentValue(ResizingHostControlProperty, PART_ResizingHostControl);
    }
}
