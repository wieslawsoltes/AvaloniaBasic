using Avalonia;

namespace AvaloniaBasicDemo.Behaviors;

public class DragSettings : AvaloniaObject
{
    public static readonly AttachedProperty<bool> IsDropAreaProperty = 
        AvaloniaProperty.RegisterAttached<IAvaloniaObject, bool>("IsDropArea", typeof(DragSettings));

    public static readonly AttachedProperty<bool> IsDragAreaProperty = 
        AvaloniaProperty.RegisterAttached<IAvaloniaObject, bool>("IsDragArea", typeof(DragSettings));

    public static readonly AttachedProperty<double> MinimumDragDeltaProperty = 
        AvaloniaProperty.RegisterAttached<IAvaloniaObject, double>("MinimumDragDelta", typeof(DragSettings), defaultValue: 3d, inherits: true);

    public static readonly AttachedProperty<bool> SnapToGridProperty = 
        AvaloniaProperty.RegisterAttached<IAvaloniaObject, bool>("SnapToGrid", typeof(DragSettings), defaultValue: false, inherits: true);

    public static readonly AttachedProperty<double> SnapXProperty = 
        AvaloniaProperty.RegisterAttached<IAvaloniaObject, double>("SnapX", typeof(DragSettings), defaultValue: 10d, inherits: true);

    public static readonly AttachedProperty<double> SnapYProperty = 
        AvaloniaProperty.RegisterAttached<IAvaloniaObject, double>("SnapY", typeof(DragSettings), defaultValue: 10d, inherits: true);

    public static readonly AttachedProperty<bool> EnableDragProperty = 
        AvaloniaProperty.RegisterAttached<IAvaloniaObject, bool>("EnableDrag", typeof(DragSettings));

    public static bool GetIsDropArea(IAvaloniaObject obj)
    {
        return obj.GetValue(IsDropAreaProperty);
    }

    public static void SetIsDropArea(IAvaloniaObject obj, bool value)
    {
        obj.SetValue(IsDropAreaProperty, value);
    }

    public static bool GetIsDragArea(IAvaloniaObject obj)
    {
        return obj.GetValue(IsDragAreaProperty);
    }

    public static void SetIsDragArea(IAvaloniaObject obj, bool value)
    {
        obj.SetValue(IsDragAreaProperty, value);
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

    public static void SetEnableDrag(IAvaloniaObject obj, bool value)
    {
        obj.SetValue(EnableDragProperty, value);
    }

    public static bool GetEnableDrag(IAvaloniaObject obj)
    {
        return obj.GetValue(EnableDragProperty);
    }
}
