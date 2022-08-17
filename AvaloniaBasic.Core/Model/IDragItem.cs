using Avalonia.Controls;

namespace AvaloniaBasic.Model;

public interface IDragItem : IToolBoxItem
{
    Control CreatePreview();

    Control CreateControl();

    void UpdatePreview(Control control, bool isPointerOver);
}
