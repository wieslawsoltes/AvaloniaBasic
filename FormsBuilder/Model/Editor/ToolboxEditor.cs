using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace FormsBuilder;

public class ToolboxEditor : IToolboxEditor
{
    private readonly IDragAndDropEditor _dragAndDropEditor;

    public ToolboxEditor(
        Control host, 
        IOverlayService overlayService, 
        IAvaloniaFactory avaloniaFactory,
        IXamlEditor xamlEditor,
        IXamlFactory xamlFactory)
    {
        var visualRoot = host.GetVisualRoot() as Interactive;
 
        _dragAndDropEditor = new DragAndDropEditor(
            visualRoot, 
            overlayService, 
            avaloniaFactory,
            xamlEditor,
            xamlFactory,
            GetXamlItem);
    }

    private XamlItem? GetXamlItem(object? sender)
    {
        return sender is ListBoxItem {Content: XamlItem toolBoxItem} 
            ? toolBoxItem 
            : null;
    }

    public void AttachToContainer(Control container)
    {
        container.AddHandler(InputElement.PointerPressedEvent, ContainerOnPointerPressed, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        container.AddHandler(InputElement.PointerReleasedEvent, ContainerOnPointerReleased, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        container.AddHandler(InputElement.PointerMovedEvent, ContainerOnPointerMoved, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        container.AddHandler(InputElement.PointerCaptureLostEvent, ContainerOnPointerCaptureLost, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        container.AddHandler(Gestures.HoldingEvent, ContainerOnHolding);
    }

    public void DetachFromContainer(Control container)
    {
        container.RemoveHandler(InputElement.PointerPressedEvent, ContainerOnPointerPressed);
        container.RemoveHandler(InputElement.PointerReleasedEvent, ContainerOnPointerReleased);
        container.RemoveHandler(InputElement.PointerMovedEvent, ContainerOnPointerMoved);
        container.RemoveHandler(InputElement.PointerCaptureLostEvent, ContainerOnPointerCaptureLost);
        container.RemoveHandler(Gestures.HoldingEvent, ContainerOnHolding);
    }

    private void ContainerOnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        _dragAndDropEditor.OnPointerPressed(sender, e);
    }

    private void ContainerOnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        _dragAndDropEditor.OnPointerReleased(sender, e);
    }

    private void ContainerOnPointerCaptureLost(object? sender, PointerCaptureLostEventArgs e)
    {
        _dragAndDropEditor.OnPointerCaptureLost(sender, e);
    }

    private void ContainerOnHolding(object? sender, HoldingRoutedEventArgs e)
    {
        _dragAndDropEditor.OnHolding(sender, e);
    }

    private void ContainerOnPointerMoved(object? sender, PointerEventArgs e)
    {
        _dragAndDropEditor.OnPointerMoved(sender, e);
    }
}
