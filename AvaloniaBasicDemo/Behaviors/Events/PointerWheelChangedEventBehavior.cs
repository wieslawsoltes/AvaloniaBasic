using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Xaml.Interactivity;

namespace Avalonia.Xaml.Interactions.Events;

public abstract class PointerWheelChangedEventBehavior<T> : Behavior<T> where T : Control
{
    public static readonly StyledProperty<RoutingStrategies> RoutingStrategiesProperty = 
        AvaloniaProperty.Register<PointerWheelChangedEventBehavior<T>, RoutingStrategies>(
            nameof(RoutingStrategies),
            RoutingStrategies.Tunnel | RoutingStrategies.Bubble);

    public RoutingStrategies RoutingStrategies
    {
        get => GetValue(RoutingStrategiesProperty);
        set => SetValue(RoutingStrategiesProperty, value);
    }

    protected override void OnAttachedToVisualTree()
    {
        AssociatedObject?.AddHandler(InputElement.PointerWheelChangedEvent, PointerWheelChanged, RoutingStrategies);
    }

    protected override void OnDetachedFromVisualTree()
    {
        AssociatedObject?.RemoveHandler(InputElement.PointerWheelChangedEvent, PointerWheelChanged);
    }

    private void PointerWheelChanged(object? sender, PointerWheelEventArgs e)
    {
        OnPointerWheelChanged(sender, e);
    }

    protected virtual void OnPointerWheelChanged(object? sender, PointerWheelEventArgs e)
    {
    }
}
