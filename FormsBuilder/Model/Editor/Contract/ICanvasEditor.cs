using System.Collections.Generic;
using Avalonia.Controls;

namespace FormsBuilder;

public interface ICanvasEditor<T> where T : class
{
    bool ReverseOrder { get; set; }
    void AttachHost(T host, Panel rootPanel, GridLinesControl gridLinesControl);
    void DetachHost();
    void AddToRoot(T control);
    void SetCurrentTool(string type);
    IReadOnlyList<Tool> Tools { get; }
}
