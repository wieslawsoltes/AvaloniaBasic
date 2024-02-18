using Avalonia;
using Avalonia.Controls;

namespace FormsBuilder;

public interface IOverlayService
{
    Visual? Overlay { get; }
    Canvas Canvas { get; }
    void Invalidate();
}
