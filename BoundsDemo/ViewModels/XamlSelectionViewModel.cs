using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using ReactiveUI;

namespace BoundsDemo;

public interface IXamlSelectionViewModel
{
    event EventHandler<EventArgs>? HoveredChanged;
    event EventHandler<EventArgs>? SelectedChanged;
    Visual? Hovered { get; set; }
    HashSet<Visual> Selected { get; set; }
    bool DrawSelection { get; set; }
    Point StartPoint { get; set; }
    Point EndPoint { get; set; }
    IObservable<IReactivePropertyChangedEventArgs<IReactiveObject>> Changing { get; }
    IObservable<IReactivePropertyChangedEventArgs<IReactiveObject>> Changed { get; }
    IObservable<Exception> ThrownExceptions { get; }
    void CopySelected();
    void CutSelected();
    void PasteSelected();
    void RemoveSelected();
    void Hover(Visual? visual);
    void Select(IEnumerable<Visual>? visuals);
    void Selection(Point startPoint, Point endPoint);
    void ClearSelection();
    Rect GetSelectionRect();
    IDisposable SuppressChangeNotifications();
    bool AreChangeNotificationsEnabled();
    IDisposable DelayChangeNotifications();
    event PropertyChangingEventHandler? PropertyChanging;
    event PropertyChangedEventHandler? PropertyChanged;
}

public class XamlSelectionViewModel : ReactiveObject, IXamlSelectionViewModel
{
    private readonly XamlEditorViewModel _xamlEditorViewModel;
    private List<XamlItem>? _xamlItemsCopy;

    public XamlSelectionViewModel(XamlEditorViewModel xamlEditorViewModel)
    {
        _xamlEditorViewModel = xamlEditorViewModel;
    }

    public event EventHandler<EventArgs>? HoveredChanged;

    public event EventHandler<EventArgs>? SelectedChanged;

    public Visual? Hovered { get; set; }

    public HashSet<Visual> Selected { get; set; }

    public bool DrawSelection { get; set; }

    public Point StartPoint { get; set; }
    
    public Point EndPoint { get; set; }

    private void CreateSelectedCopy()
    {
        var selected = Selected.ToList();
        var xamlItems = new List<XamlItem>();

        foreach (var visual in selected)
        {
            if (visual is not Control control)
            {
                continue;
            }

            if (!_xamlEditorViewModel.TryGetXamlItem(control, out var xamlItem) || xamlItem is null)
            {
                continue;
            }

            var xamlItemCopy = XamlItemFactory.Clone(xamlItem, _xamlEditorViewModel.IdManager);

            xamlItems.Add(xamlItemCopy);
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
            return;
        }

        if (Selected.Count > 1)
        {
            return;
        }

        var targetXamlItem = default(XamlItem);
        
        if (Selected.Count == 0)
        {
            targetXamlItem = _xamlEditorViewModel.RootXamlItem;
        }
        else if (Selected.Count == 1)
        {
            if (Selected.First() is not Control control)
            {
                return;
            }

            if (!_xamlEditorViewModel.TryGetXamlItem(control, out var xamlItem) || xamlItem is null)
            {
                return;
            }

            targetXamlItem = xamlItem;
        }

        if (targetXamlItem is null)
        {
            return;
        }

        foreach (var xamlItem in _xamlItemsCopy)
        {
            var xamlItemCopy = XamlItemFactory.Clone(xamlItem, _xamlEditorViewModel.IdManager);
            
            if (targetXamlItem.ChildrenProperty is not null)
            {
                if (targetXamlItem.TryAddChild(xamlItemCopy))
                {
                    _xamlEditorViewModel.Debug(targetXamlItem);
                }
            }
            else
            {
                // TODO: Add different hot-key for Content paste.
                if (targetXamlItem.ContentProperty is not null)
                {
                    if (targetXamlItem.TrySetContent(new XamlItemXamlValue(xamlItemCopy)))
                    {
                        _xamlEditorViewModel.Debug(targetXamlItem);
                    }
                }
            }

        }

        _xamlEditorViewModel.Reload(_xamlEditorViewModel.RootXamlItem);
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

            if (!_xamlEditorViewModel.TryGetXamlItem(control, out var xamlItem) || xamlItem is null)
            {
                continue;
            }

            if (_xamlEditorViewModel.RemoveXamlItem(xamlItem))
            {
                haveToReload = true;
            }
        }

        if (haveToReload)
        {
            _xamlEditorViewModel.Reload(_xamlEditorViewModel.RootXamlItem);
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

    public void Hover(Visual? visual)
    {
        if (visual is null || !Selected.Contains(visual))
        {
            Hovered = visual;
            OnHoveredChanged(EventArgs.Empty);
        }
    }

    public void Select(IEnumerable<Visual>? visuals)
    {
        Hovered = null;
        OnHoveredChanged(EventArgs.Empty);
        Selected = visuals is null ? new HashSet<Visual>() : new HashSet<Visual>(visuals);
        OnSelectedChanged(EventArgs.Empty);
    }

    public void Selection(Point startPoint, Point endPoint)
    {
        // TODO:
        DrawSelection = true;
        StartPoint = startPoint;
        EndPoint = endPoint;
    }

    public void ClearSelection()
    {
        DrawSelection = false;
    }

    public Rect GetSelectionRect()
    {
        var topLeft = new Point(
            Math.Min(StartPoint.X, EndPoint.X),
            Math.Min(StartPoint.Y, EndPoint.Y));
        var bottomRight = new Point(
            Math.Max(StartPoint.X, EndPoint.X),
            Math.Max(StartPoint.Y, EndPoint.Y));
        return new Rect(topLeft, bottomRight);
    }
}
