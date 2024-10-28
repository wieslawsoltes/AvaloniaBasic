using System;
using System.Collections.Generic;
using Avalonia;
using XamlDom;

namespace FormsBuilder;

public interface IXamlSelection
{
    event EventHandler<EventArgs>? HoveredChanged;
    event EventHandler<EventArgs>? SelectedChanged;
    event EventHandler<EventArgs>? SelectedMoved;
    HitTestMode HitTestMode  { get; set; }
    Visual? Hovered { get; }
    HashSet<Visual> Selected { get; }
    bool DrawSelection { get; set; }
    Point StartPoint { get; set; }
    Point EndPoint { get; set; }
    void CopySelected();
    void CutSelected();
    void PasteSelected();
    void RemoveSelected();
    void BeginMoveSelection();
    void EndMoveSelection();
    void MoveSelection(Point delta);
    void HoverItem(XamlItem xamlItem);
    void SelectItems(IEnumerable<XamlItem> xamlItems);
    void Hover(Visual? visual);
    void Select(IEnumerable<Visual>? visuals);
    void Selection(Point startPoint, Point endPoint);
    void ClearSelection();
    void InvalidateOverlay();
}
