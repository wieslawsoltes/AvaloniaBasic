using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia;
using Avalonia.Controls;

namespace FormsBuilder;

public class XamlSelection : IXamlSelection
{
    private readonly IXamlEditor _xamlEditor;
    private readonly Action _invalidateOverlay;
    private readonly Dictionary<Control, Point> _positions = new();
    private XamlItems? _xamlItemsCopy;

    public XamlSelection(IXamlEditor xamlEditor, Action invalidateOverlay)
    {
        _xamlEditor = xamlEditor;
        _invalidateOverlay = invalidateOverlay;
    }

    public event EventHandler<EventArgs>? HoveredChanged;

    public event EventHandler<EventArgs>? SelectedChanged;

    public event EventHandler<EventArgs>? SelectedMoved;

    public HitTestMode HitTestMode { get; set; } = HitTestMode.Logical;

    public Visual? Hovered { get; private set; }

    public HashSet<Visual> Selected { get; private set; }

    public bool DrawSelection { get; set; }

    public Point StartPoint { get; set; }
    
    public Point EndPoint { get; set; }

    private void CreateSelectedCopy()
    {
        var selected = Selected.ToList();
        var xamlItems = new XamlItems();

        foreach (var visual in selected)
        {
            if (visual is not Control control)
            {
                continue;
            }

            if (!_xamlEditor.TryGetXamlItem(control, out var xamlItem) || xamlItem is null)
            {
                continue;
            }

            // var xamlItemCopy = XamlItemFactory.Clone(xamlItem, _xamlEditor.IdManager);
            // xamlItems.Add(xamlItemCopy);
            xamlItems.Add(xamlItem);
        }

        _xamlItemsCopy = xamlItems;
    }

    public void CopySelected()
    {
        CreateSelectedCopy();
    }

    public void CutSelected()
    {
        CreateSelectedCopy();
        RemoveSelected();
    }

    public void PasteSelected()
    {
        if (_xamlItemsCopy is null || _xamlItemsCopy.Count <= 0)
        {
            Hover(null);
            Select(null);
            return;
        }

        var targetXamlItem = default(XamlItem);
        var isTargetXamlItemRoot = false;
        
        if (Selected.Count is 0 or > 1)
        {
            targetXamlItem = _xamlEditor.RootXamlItem;
        }
        else if (Selected.Count is 1)
        {
            var selected = Selected.First();

            if (selected is not Control control)
            {
                Hover(null);
                Select(null);
                return;
            }

            if (!_xamlEditor.TryGetXamlItem(control, out var selectedXamlItem) || selectedXamlItem is null)
            {
                Hover(null);
                Select(null);
                return;
            }

            var canSetContentOrChildren = selectedXamlItem.ChildrenProperty is not null
                                          || selectedXamlItem.ContentProperty is not null;

            var firstXamlItemToCopy = _xamlItemsCopy.First();
            var copyItemIsSameAsSelected = firstXamlItemToCopy == selectedXamlItem;

            targetXamlItem = canSetContentOrChildren && !copyItemIsSameAsSelected
                ? selectedXamlItem
                : _xamlEditor.RootXamlItem;
        }

        if (targetXamlItem == _xamlEditor.RootXamlItem)
        {
            isTargetXamlItemRoot = true;
        }

        if (targetXamlItem is null)
        {
            Hover(null);
            Select(null);
            return;
        }

        var newXamlItems = new XamlItems();

        foreach (var xamlItem in _xamlItemsCopy)
        {
            if (targetXamlItem == xamlItem)
            {
                break;
            }

            var xamlItemCopy = XamlItemFactory.Clone(xamlItem, _xamlEditor.IdManager);
            
            if (targetXamlItem.ChildrenProperty is not null)
            {
                if (targetXamlItem.TryAddChild(xamlItemCopy))
                {
                    // TODO:
                    // newXamlItems.Add(xamlItemCopy);

                    if (isTargetXamlItemRoot)
                    {
                        newXamlItems.Add(xamlItemCopy);
                    }

                    _xamlEditor.Debug(targetXamlItem);
                }
            }
            else
            {
                // TODO: Add different hot-key for Content paste.
                if (targetXamlItem.ContentProperty is not null)
                {
                    if (targetXamlItem.TrySetContent(new XamlItemXamlValue(xamlItemCopy)))
                    {
                        // TODO:
                        // newXamlItems.Add(xamlItemCopy);

                        _xamlEditor.Debug(targetXamlItem);
                    }
                }
            }
        }

        _xamlEditor.Reload(_xamlEditor.RootXamlItem);

        Hover(null);
        SelectItems(newXamlItems);
    }

    public void RemoveSelected()
    {
        if (Selected.Count <= 0)
        {
            return;
        }

        var selected = Selected.ToList();

        var haveToReload = false;

        foreach (var visual in selected)
        {
            if (visual is not Control control)
            {
                continue;
            }

            if (!_xamlEditor.TryGetXamlItem(control, out var xamlItem) || xamlItem is null)
            {
                continue;
            }

            if (_xamlEditor.RemoveXamlItem(xamlItem))
            {
                haveToReload = true;
            }
        }

        if (haveToReload)
        {
            _xamlEditor.Reload(_xamlEditor.RootXamlItem);
        }
    }

    protected virtual void OnHoveredChanged(EventArgs e)
    {
        HoveredChanged?.Invoke(this, e);
    }

    protected virtual void OnSelectedChanged(EventArgs e)
    {
        SelectedChanged?.Invoke(this, e);
    }

    protected virtual void OnSelectedMoved(EventArgs e)
    {
        SelectedMoved?.Invoke(this, e);
    }

    public void HoverItem(XamlItem xamlItem)
    {
        if (_xamlEditor.TryGetControl(xamlItem, out var control) && control is not null)
        {
            Hover(control);
        }
    }

    public void SelectItems(IEnumerable<XamlItem> xamlItems)
    {
        var newSelectedVisuals = new List<Visual>();

        foreach (var newXamlItem in xamlItems)
        {
            if (_xamlEditor.TryGetControl(newXamlItem, out var control) && control is not null)
            {
                newSelectedVisuals.Add(control);
            }
        }

        Select(newSelectedVisuals.Count > 0 ? newSelectedVisuals : null);
    }

    public void Hover(Visual? visual)
    {
        if (visual is not null && Selected.Contains(visual))
        {
            return;
        }

        Hovered = visual;
        OnHoveredChanged(EventArgs.Empty);
        InvalidateOverlay();
    }

    public void Select(IEnumerable<Visual>? visuals)
    {
        Hovered = null;
        OnHoveredChanged(EventArgs.Empty);
        Selected = visuals is null ? new HashSet<Visual>() : new HashSet<Visual>(visuals);
        OnSelectedChanged(EventArgs.Empty);
        InvalidateOverlay();
    }

    public void Selection(Point startPoint, Point endPoint)
    {
        // TODO:
        DrawSelection = true;
        StartPoint = startPoint;
        EndPoint = endPoint;
        InvalidateOverlay();
    }

    public void BeginMoveSelection()
    {
        if (Selected.Count <= 0)
        {
            return;
        }
        
        // TODO: Move Selected
 
        var selected = Selected.ToList();

        foreach (var visual in selected)
        {
            if (visual is not Control control)
            {
                continue;
            }

            if (!_xamlEditor.TryGetXamlItem(control, out var xamlItem) || xamlItem is null)
            {
                continue;
            }

            // TODO: Canvas
            if (control.Parent is Canvas)
            {
                var left = Canvas.GetLeft(control);
                var top = Canvas.GetTop(control);

                if (double.IsNaN(left))
                {
                    left = 0.0;
                }

                if (double.IsNaN(top))
                {
                    top = 0.0;
                }

                _positions[control] = new Point(left, top);
            }

            // TODO: Add support for other panels.
        }
    }

    public void EndMoveSelection()
    {
        _positions.Clear();
    }

    public void MoveSelection(Point delta)
    {
        if (Selected.Count <= 0)
        {
            return;
        }
        
        // TODO: Move Selected
 
        var selected = Selected.ToList();

        foreach (var visual in selected)
        {
            if (visual is not Control control)
            {
                continue;
            }

            if (!_xamlEditor.TryGetXamlItem(control, out var xamlItem) || xamlItem is null)
            {
                continue;
            }

            // TODO: Canvas
            if (control.Parent is Canvas)
            {
                // var left = Canvas.GetLeft(control) + delta.X;
                // var top = Canvas.GetTop(control) + delta.Y;
                var position = _positions[control];
                var left = position.X + delta.X;
                var top = position.Y + delta.Y;

                left = SnapHelper.SnapValue(left, 6);
                top = SnapHelper.SnapValue(top, 6);

                Canvas.SetLeft(control, left);
                Canvas.SetTop(control, top);

                _xamlEditor.UpdatePropertyValue(control, "Canvas.Left", left);
                _xamlEditor.UpdatePropertyValue(control, "Canvas.Top", top);
            }

            // TODO: Add support for other panels.
        }

        OnSelectedMoved(EventArgs.Empty);
        InvalidateOverlay();
    }

    public void ClearSelection()
    {
        DrawSelection = false;
        InvalidateOverlay();
    }

    public void InvalidateOverlay()
    {
        _invalidateOverlay.Invoke();
    }
}
