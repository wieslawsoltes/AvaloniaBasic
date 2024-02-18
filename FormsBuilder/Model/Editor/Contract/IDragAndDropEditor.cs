using Avalonia.Input;

namespace FormsBuilder;

public interface IDragAndDropEditor
{
    void OnPointerPressed(object? sender, PointerPressedEventArgs e);
    void OnPointerReleased(object? sender, PointerReleasedEventArgs e);
    void OnPointerCaptureLost(object? sender, PointerCaptureLostEventArgs e);
    void OnHolding(object? sender, HoldingRoutedEventArgs e);
    void OnPointerMoved(object? sender, PointerEventArgs e);
}
