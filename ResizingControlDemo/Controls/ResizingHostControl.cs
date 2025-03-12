using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.VisualTree;

namespace ResizingControlDemo.Controls;

public class ResizingHostControl : Border
{
    public static readonly AttachedProperty<ResizingAdornerControl?> SelectedResizingAdornerControlProperty =
        AvaloniaProperty.RegisterAttached<ResizingHostControl, Visual, ResizingAdornerControl?>("SelectedResizingAdornerControl", null, true);

    public static ResizingAdornerControl? GetSelectedResizingAdornerControl(Visual visual)
    {
        return visual.GetValue(SelectedResizingAdornerControlProperty);
    }

    public static void SetSelectedResizingAdornerControl(Visual visual, ResizingAdornerControl? selectedResizingAdornerControl)
    {
        visual.SetValue(SelectedResizingAdornerControlProperty, selectedResizingAdornerControl);
    }

    static ResizingHostControl()
    {
        BackgroundProperty.OverrideDefaultValue<ResizingHostControl>(Brushes.Transparent);
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);

        AddHandler(PointerPressedEvent, ResizingHostControl_OnPointerPressed, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
    }

    private void ResizingHostControl_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        var selectedResizingAdornerControl = GetValue(SelectedResizingAdornerControlProperty);
        if (selectedResizingAdornerControl is not null)
        {
            selectedResizingAdornerControl.SetCurrentValue(ResizingAdornerControl.IsResizingSelectedProperty, false);
        }

        SetCurrentValue(SelectedResizingAdornerControlProperty, null);
    }
}
