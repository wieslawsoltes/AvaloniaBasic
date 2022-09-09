namespace AvaloniaBasic.Model;

public interface IDragItem : IToolboxItem
{
    object CreatePreview();

    object CreateControl();

    void UpdatePreview(object control, bool isPointerOver);

    bool IsDropArea();
}
