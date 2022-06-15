using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Xaml.Interactivity;

namespace Avalonia.Xaml.Interactions.Events;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class DoubleTappedBehavior<T> : Behavior<T> where T : Control
{
    /// <summary>
    /// 
    /// </summary>
    public static readonly StyledProperty<RoutingStrategies> RoutingStrategiesProperty = 
        AvaloniaProperty.Register<DoubleTappedBehavior<T>, RoutingStrategies>(
            nameof(RoutingStrategies),
            RoutingStrategies.Bubble);

    /// <summary>
    /// 
    /// </summary>
    public RoutingStrategies RoutingStrategies
    {
        get => GetValue(RoutingStrategiesProperty);
        set => SetValue(RoutingStrategiesProperty, value);
    }

    /// <summary>
    /// 
    /// </summary>
    protected override void OnAttachedToVisualTree()
    {
        AssociatedObject?.AddHandler(Gestures.DoubleTappedEvent, DoubleTapped, RoutingStrategies);
    }

    /// <summary>
    /// 
    /// </summary>
    protected override void OnDetachedFromVisualTree()
    {
        AssociatedObject?.RemoveHandler(Gestures.DoubleTappedEvent, DoubleTapped);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DoubleTapped(object? sender, RoutedEventArgs e)
    {
        OnDoubleTapped(sender, e);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void OnDoubleTapped(object? sender, RoutedEventArgs e)
    {
    }
}
