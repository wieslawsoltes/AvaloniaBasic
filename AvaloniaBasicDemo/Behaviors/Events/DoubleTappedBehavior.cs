using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Xaml.Interactivity;

namespace Avalonia.Xaml.Interactions.Events;

public abstract class DoubleTappedBehavior<T> : Behavior<T> where T : Control
{
    public static readonly StyledProperty<RoutingStrategies> RoutingStrategiesProperty = 
        AvaloniaProperty.Register<DoubleTappedBehavior<T>, RoutingStrategies>(nameof(RoutingStrategies), RoutingStrategies.Tunnel | RoutingStrategies.Bubble);

    public RoutingStrategies RoutingStrategies
    {
        get => GetValue(RoutingStrategiesProperty);
        set => SetValue(RoutingStrategiesProperty, value);
    }

    protected override void OnAttachedToVisualTree()
    {
        AssociatedObject?.AddHandler(Gestures.DoubleTappedEvent, DoubleTapped, RoutingStrategies);
    }

    protected override void OnDetachedFromVisualTree()
    {
        AssociatedObject?.RemoveHandler(Gestures.DoubleTappedEvent, DoubleTapped);
    }

    private void DoubleTapped(object? sender, RoutedEventArgs e)
    {
        OnDoubleTapped(sender, e);
    }

    protected virtual void OnDoubleTapped(object? sender, RoutedEventArgs e)
    {
    }
}
