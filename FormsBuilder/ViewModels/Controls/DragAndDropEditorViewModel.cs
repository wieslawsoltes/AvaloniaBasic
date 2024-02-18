using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace FormsBuilder;

public interface IDragAndDropEditorViewModel
{
    void OnPointerPressed(object? sender, PointerPressedEventArgs e);
    void OnPointerReleased(object? sender, PointerReleasedEventArgs e);
    void OnPointerCaptureLost(object? sender, PointerCaptureLostEventArgs e);
    void OnHolding(object? sender, HoldingRoutedEventArgs e);
    void OnPointerMoved(object? sender, PointerEventArgs e);
}

public class DragAndDropEditorViewModel : IDragAndDropEditorViewModel
{
    private readonly Control _host;
    private readonly IOverlayService _overlayService;
    private readonly IXamlEditor _xamlEditor;
    private bool _captured;
    private Point _start;
    private Control? _control;
    private HashSet<Visual> _ignored;
    private XamlItem? _xamlItem;
    
    public DragAndDropEditorViewModel(
        Control host, 
        IOverlayService overlayService, 
        IXamlEditor xamlEditor)
    {
        _host = host;
        _overlayService = overlayService;
        _xamlEditor = xamlEditor;
        _ignored = new HashSet<Visual>();
    }

    public void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (e.Pointer.Type == PointerType.Mouse)
        {
            _captured = true;
            _start = e.GetPosition(e.Source as Control);
        }
    }

    public void OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (_control is not null)
        {
            RemovePreview();
            Drop(e, _ignored);
        }

        _captured = false;
        _control = null;
        _xamlItem = null;
    }

    public void OnPointerCaptureLost(object? sender, PointerCaptureLostEventArgs e)
    {
        _captured = false;
        _control = null;
        _xamlItem = null;
    }

    public void OnHolding(object? sender, HoldingRoutedEventArgs e)
    {
        if (e.PointerType != PointerType.Mouse)
        {
            _captured = true;
            _start = e.Position;
        }
    }

    public void OnPointerMoved(object? sender, PointerEventArgs e)
    {
        if (!_captured)
        {
            return;
        }

        if (e.Pointer.Type != PointerType.Mouse)
        {
            e.PreventGestureRecognition();
        }

        var position = e.GetPosition(e.Source as Control);
        var delta = _start - position;

        if (Math.Abs(delta.X) > 3 || Math.Abs(delta.Y) > 3)
        {
            if (_control is null)
            {
                e.PreventGestureRecognition();

                CreatePreview(sender);
                AddPreview();
            }
        }

        if (_control is not null)
        {
            e.PreventGestureRecognition();
            
            MovePreview(e, position);
        }
    }

    private XamlItem? GetXamlItemFromListBoxItem(object? sender)
    {
        if (sender is ListBoxItem listBoxItem && listBoxItem.Content is XamlItem toolBoxItem)
        {
            return toolBoxItem;
        }

        return null;
    }
    
    private void CreatePreview(object? sender)
    {
        try
        {
            var toolBoxItem = GetXamlItemFromListBoxItem(sender);
            if (toolBoxItem is not null)
            {
                _xamlItem = XamlItemFactory.Clone(toolBoxItem, _xamlEditor.IdManager);
                _control = XamlItemControlFactory.CreateControl(_xamlItem, isRoot: true, writeUid: true);
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }

    private void AddPreview()
    {
        if (_control is not null)
        {
            _overlayService.Canvas.Children.Add(_control);

            _ignored = new HashSet<Visual>(new Visual[] {_overlayService.Overlay, _control});
        }
    }

    private void MovePreview(PointerEventArgs e, Point position)
    {
        if (_control is not null && e.Source is Control source)
        {
            var location = source.TranslatePoint(position, _overlayService.Canvas);
            if (location is not null)
            {
                Canvas.SetLeft(_control, location.Value.X);
                Canvas.SetTop(_control, location.Value.Y);
            }
        }
    }

    private void RemovePreview()
    {
        if (_control is not null)
        {
            _overlayService.Canvas.Children.Remove(_control);
        }
    }

    private void Drop(PointerEventArgs e, HashSet<Visual> ignored)
    {
        if (_control is null || _xamlItem is null)
        {
            return;
        }

        if (_host.GetVisualRoot() is not Interactive root)
        {
            return;
        }

        var target = _xamlEditor.HitTest(root, e.GetPosition(root), ignored);
        if (target is null)
        {
            return;
        }

        var position = e.GetPosition(target);

        position = SnapHelper.SnapPoint(position, 6, 6, true);

        _xamlEditor.RemoveControl(_control);

        _xamlEditor.InsertXamlItem(target, _xamlItem, position);
    }
}
