using Avalonia.Controls;

namespace AvaloniaBasicDemo.Model;

public interface IDragItem : IToolBoxItem
{
    Control CreatePreview();

    Control CreateControl();

    void UpdatePreview(Control control, bool isPointerOver);
}
