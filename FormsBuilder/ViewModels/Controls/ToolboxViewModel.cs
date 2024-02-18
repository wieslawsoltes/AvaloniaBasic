using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using ReactiveUI;

namespace FormsBuilder;

public interface IToolboxViewModel
{
    void AttachToContainer(Control container);
    void DetachFromContainer(Control container);
}

public class ToolboxViewModel : ReactiveObject, IToolboxViewModel
{
    private readonly IDragAndDropEditorViewModel _dragAndDropEditorViewModel;

    public ToolboxViewModel(Control host, IOverlayService overlayService, IXamlEditor xamlEditor)
    {
        var visualRoot = host.GetVisualRoot() as Interactive;
 
        _dragAndDropEditorViewModel = new DragAndDropEditorViewModel(
            visualRoot, 
            overlayService, 
            xamlEditor,
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
        _dragAndDropEditorViewModel.OnPointerPressed(sender, e);
    }

    private void ContainerOnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        _dragAndDropEditorViewModel.OnPointerReleased(sender, e);
    }

    private void ContainerOnPointerCaptureLost(object? sender, PointerCaptureLostEventArgs e)
    {
        _dragAndDropEditorViewModel.OnPointerCaptureLost(sender, e);
    }

    private void ContainerOnHolding(object? sender, HoldingRoutedEventArgs e)
    {
        _dragAndDropEditorViewModel.OnHolding(sender, e);
    }

    private void ContainerOnPointerMoved(object? sender, PointerEventArgs e)
    {
        _dragAndDropEditorViewModel.OnPointerMoved(sender, e);
    }
}
