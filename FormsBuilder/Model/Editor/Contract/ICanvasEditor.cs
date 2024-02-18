using Avalonia.Controls;

namespace FormsBuilder;

public interface ICanvasEditor
{
    bool ReverseOrder { get; set; }
    void AttachHost(Control host, Panel rootPanel, GridLinesControl gridLinesControl);
    void DetachHost();
    void AddToRoot(Control control);
}
