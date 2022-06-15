using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Xaml.Interactivity;

namespace Avalonia.Xaml.Interactions.Events;

public abstract class PointerEnterEventBehavior<T> : Behavior<T> where T : Control
{
    public static readonly StyledProperty<RoutingStrategies> RoutingStrategiesProperty = 
        AvaloniaProperty.Register<PointerEnterEventBehavior<T>, RoutingStrategies>(
            nameof(RoutingStrategies),
            RoutingStrategies.Direct);

    public RoutingStrategies RoutingStrategies
    {
        get => GetValue(RoutingStrategiesProperty);
        set => SetValue(RoutingStrategiesProperty, value);
    }

    protected override void OnAttachedToVisualTree()
    {
        AssociatedObject?.AddHandler(InputElement.PointerEnterEvent, PointerEnter, RoutingStrategies);
    }

    protected override void OnDetachedFromVisualTree()
    {
        AssociatedObject?.RemoveHandler(InputElement.PointerEnterEvent, PointerEnter);
    }

    private void PointerEnter(object? sender, PointerEventArgs e)
    {
        OnPointerEnter(sender, e);
    }

    protected virtual void OnPointerEnter(object? sender, PointerEventArgs e)
    {
    }
}
