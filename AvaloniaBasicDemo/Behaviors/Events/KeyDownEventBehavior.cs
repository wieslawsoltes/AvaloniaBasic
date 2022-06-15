using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Xaml.Interactivity;

namespace Avalonia.Xaml.Interactions.Events;

public abstract class KeyDownEventBehavior<T> : Behavior<T> where T : Control
{
    public static readonly StyledProperty<RoutingStrategies> RoutingStrategiesProperty = 
        AvaloniaProperty.Register<KeyDownEventBehavior<T>, RoutingStrategies>(
            nameof(RoutingStrategies),
            RoutingStrategies.Tunnel | RoutingStrategies.Bubble);

    public RoutingStrategies RoutingStrategies
    {
        get => GetValue(RoutingStrategiesProperty);
        set => SetValue(RoutingStrategiesProperty, value);
    }

    protected override void OnAttachedToVisualTree()
    {
        AssociatedObject?.AddHandler(InputElement.KeyDownEvent, KeyDown, RoutingStrategies);
    }

    protected override void OnDetachedFromVisualTree()
    {
        AssociatedObject?.RemoveHandler(InputElement.KeyDownEvent, KeyDown);
    }

    private void KeyDown(object? sender, KeyEventArgs e)
    {
        OnKeyDown(sender, e);
    }

    protected virtual void OnKeyDown(object? sender, KeyEventArgs e)
    {
    }
}
