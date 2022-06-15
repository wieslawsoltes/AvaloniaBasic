using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Xaml.Interactivity;

namespace Avalonia.Xaml.Interactions.Events;

public abstract class TextInputEventBehavior<T> : Behavior<T> where T : Control
{
    public static readonly StyledProperty<RoutingStrategies> RoutingStrategiesProperty = 
        AvaloniaProperty.Register<TextInputEventBehavior<T>, RoutingStrategies>(
            nameof(RoutingStrategies),
            RoutingStrategies.Tunnel | RoutingStrategies.Bubble);

    public RoutingStrategies RoutingStrategies
    {
        get => GetValue(RoutingStrategiesProperty);
        set => SetValue(RoutingStrategiesProperty, value);
    }

    protected override void OnAttachedToVisualTree()
    {
        AssociatedObject?.AddHandler(InputElement.TextInputEvent, TextInput, RoutingStrategies);
    }

    protected override void OnDetachedFromVisualTree()
    {
        AssociatedObject?.RemoveHandler(InputElement.TextInputEvent, TextInput);
    }

    private void TextInput(object? sender, TextInputEventArgs e)
    {
        OnTextInput(sender, e);
    }

    protected virtual void OnTextInput(object? sender, TextInputEventArgs e)
    {
    }
}
