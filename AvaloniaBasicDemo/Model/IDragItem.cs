using Avalonia.Controls;

namespace AvaloniaBasicDemo.Model;

public interface IDragItem
{
    string? Name { get; init; }

    string? Icon { get; init; }

    Control CreatePreview();

    Control CreateControl();

    void UpdatePreview(Control control, bool isPointerOver);
}
