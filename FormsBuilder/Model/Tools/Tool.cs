using Avalonia.Input;

namespace FormsBuilder;

public abstract class Tool
{
    public virtual void OnPointerPressed(IToolContext context, object? sender, PointerPressedEventArgs e)
    {
    }

    public virtual void OnPointerReleased(IToolContext context, object? sender, PointerReleasedEventArgs e)
    {
    }

    public virtual void OnPointerMoved(IToolContext context, object? sender, PointerEventArgs e)
    {
    }

    public virtual void OnPointerExited(IToolContext context, object? sender, PointerEventArgs e)
    {
    }
    
    public virtual void OnPointerCaptureLost(IToolContext context, object? sender, PointerCaptureLostEventArgs e)
    {
    }
}
