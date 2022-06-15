using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Input.TextInput;
using Avalonia.Interactivity;
using Avalonia.Xaml.Interactivity;

namespace Avalonia.Xaml.Interactions.Events;

public abstract class TextInputOptionsQueryEventBehavior<T> : Behavior<T> where T : Control
{
    public static readonly StyledProperty<RoutingStrategies> RoutingStrategiesProperty = 
        AvaloniaProperty.Register<TextInputOptionsQueryEventBehavior<T>, RoutingStrategies>(
            nameof(RoutingStrategies),
            RoutingStrategies.Tunnel | RoutingStrategies.Bubble);

    public RoutingStrategies RoutingStrategies
    {
        get => GetValue(RoutingStrategiesProperty);
        set => SetValue(RoutingStrategiesProperty, value);
    }

    protected override void OnAttachedToVisualTree()
    {
        AssociatedObject?.AddHandler(InputElement.TextInputOptionsQueryEvent, TextInputOptionsQuery, RoutingStrategies);
    }

    protected override void OnDetachedFromVisualTree()
    {
        AssociatedObject?.RemoveHandler(InputElement.TextInputOptionsQueryEvent, TextInputOptionsQuery);
    }

    private void TextInputOptionsQuery(object? sender, TextInputOptionsQueryEventArgs e)
    {
        OnTextInputOptionsQuery(sender, e);
    }

    protected virtual void OnTextInputOptionsQuery(object? sender, TextInputOptionsQueryEventArgs e)
    {
    }
}
