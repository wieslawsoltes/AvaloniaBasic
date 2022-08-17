using Avalonia.Controls;

namespace AvaloniaBasic.Model;

public interface IDragItem : IToolboxItem
{
    Control CreatePreview();

    Control CreateControl();

    void UpdatePreview(Control control, bool isPointerOver);

    bool IsDropArea();
}
