using Avalonia;

namespace AvaloniaBasicDemo.Behaviors;

public class DragSettings : AvaloniaObject
{
    public static readonly AttachedProperty<bool> IsDropAreaProperty = 
        AvaloniaProperty.RegisterAttached<IAvaloniaObject, bool>("IsDropArea", typeof(ToolboxDragBehavior));

    public static readonly AttachedProperty<double> MinimumDragDeltaProperty = 
        AvaloniaProperty.RegisterAttached<IAvaloniaObject, double>("MinimumDragDelta", typeof(ToolboxDragBehavior), defaultValue: 3d, inherits: true);

    public static readonly AttachedProperty<bool> SnapToGridProperty = 
        AvaloniaProperty.RegisterAttached<IAvaloniaObject, bool>("SnapToGrid", typeof(ToolboxDragBehavior), defaultValue: false, inherits: true);

    public static readonly AttachedProperty<double> SnapXProperty = 
        AvaloniaProperty.RegisterAttached<IAvaloniaObject, double>("SnapX", typeof(ToolboxDragBehavior), defaultValue: 10d, inherits: true);

    public static readonly AttachedProperty<double> SnapYProperty = 
        AvaloniaProperty.RegisterAttached<IAvaloniaObject, double>("SnapY", typeof(ToolboxDragBehavior), defaultValue: 10d, inherits: true);

    public static bool GetIsDropArea(IAvaloniaObject obj)
    {
        return obj.GetValue(IsDropAreaProperty);
    }

    public static void SetIsDropArea(IAvaloniaObject obj, bool value)
    {
        obj.SetValue(IsDropAreaProperty, value);
    }

    public static double GetMinimumDragDelta(IAvaloniaObject obj)
    {
        return obj.GetValue(MinimumDragDeltaProperty);
    }

    public static void SetMinimumDragDelta(IAvaloniaObject obj, double value)
    {
        obj.SetValue(MinimumDragDeltaProperty, value);
    }

    public static bool GetSnapToGrid(IAvaloniaObject obj)
    {
        return obj.GetValue(SnapToGridProperty);
    }

    public static void SetSnapToGrid(IAvaloniaObject obj, bool value)
    {
        obj.SetValue(SnapToGridProperty, value);
    }

    public static double GetSnapX(IAvaloniaObject obj)
    {
        return obj.GetValue(SnapXProperty);
    }

    public static void SetSnapX(IAvaloniaObject obj, double value)
    {
        obj.SetValue(SnapXProperty, value);
    }
    
    public static double GetSnapY(IAvaloniaObject obj)
    {
        return obj.GetValue(SnapYProperty);
    }

    public static void SetSnapY(IAvaloniaObject obj, double value)
    {
        obj.SetValue(SnapYProperty, value);
    }
}
