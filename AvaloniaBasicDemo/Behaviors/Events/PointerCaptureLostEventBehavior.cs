using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Xaml.Interactivity;

namespace Avalonia.Xaml.Interactions.Events;

public abstract class PointerCaptureLostEventBehavior<T> : Behavior<T> where T : Control
{
    public static readonly StyledProperty<RoutingStrategies> RoutingStrategiesProperty = 
        AvaloniaProperty.Register<PointerCaptureLostEventBehavior<T>, RoutingStrategies>(
            nameof(RoutingStrategies),
            RoutingStrategies.Direct);

    public RoutingStrategies RoutingStrategies
    {
        get => GetValue(RoutingStrategiesProperty);
        set => SetValue(RoutingStrategiesProperty, value);
    }

    protected override void OnAttachedToVisualTree()
    {
        AssociatedObject?.AddHandler(InputElement.PointerCaptureLostEvent, PointerCaptureLost, RoutingStrategies);
    }

    protected override void OnDetachedFromVisualTree()
    {
        AssociatedObject?.RemoveHandler(InputElement.PointerCaptureLostEvent, PointerCaptureLost);
    }

    private void PointerCaptureLost(object? sender, PointerCaptureLostEventArgs e)
    {
        OnPointerCaptureLost(sender, e);
    }

    protected virtual void OnPointerCaptureLost(object? sender, PointerCaptureLostEventArgs e)
    {
    }
}
