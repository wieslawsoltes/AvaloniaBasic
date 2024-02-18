using Avalonia.Controls;

namespace FormsBuilder;

public interface IToolboxEditor
{
    void AttachToContainer(Control container);
    void DetachFromContainer(Control container);
}
