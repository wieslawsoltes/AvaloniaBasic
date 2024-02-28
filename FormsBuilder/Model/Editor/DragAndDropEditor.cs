using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace FormsBuilder;

public class DragAndDropEditor : IDragAndDropEditor
{
    private readonly Interactive _visualRoot;
    private readonly IOverlayService _overlayService;
    private readonly IXamlObjectFactory _xamlObjectFactory;
    private readonly IXamlEditor _xamlEditor;
    private readonly IXamlItemFactory _xamlItemFactory;
    private readonly Func<object?, XamlItem?> _getXamlItem;
    private bool _captured;
    private Point _start;
    private Control? _control;
    private HashSet<Visual> _ignored;
    private XamlItem? _xamlItem;
    
    public DragAndDropEditor(
        Interactive visualRoot, 
        IOverlayService overlayService,  
        IXamlObjectFactory xamlObjectFactory,
        IXamlEditor xamlEditor,
        IXamlItemFactory xamlItemFactory,
        Func<object?, XamlItem?> getXamlItem)
    {
        _visualRoot = visualRoot;
        _overlayService = overlayService;
        _xamlObjectFactory = xamlObjectFactory;
        _xamlEditor = xamlEditor;
        _xamlItemFactory = xamlItemFactory;
        _getXamlItem = getXamlItem;
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
            Insert(e, _ignored);
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

    private void CreatePreview(object? sender)
    {
        try
        {
            var toolBoxItem = _getXamlItem(sender);
            if (toolBoxItem is not null)
            {
                _xamlItem = _xamlItemFactory.CloneItem(toolBoxItem, true);
                _control = _xamlObjectFactory.CreateControl(_xamlItem, isRoot: true, writeUid: true) as Control;
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

    private void Insert(PointerEventArgs e, HashSet<Visual> ignored)
    {
        if (_control is null || _xamlItem is null)
        {
            return;
        }

        _xamlEditor.RemoveControl(_control);

        var target = _xamlEditor.HitTest(_visualRoot, e.GetPosition(_visualRoot), ignored);
        if (target is null)
        {
            return;
        }

        var position = e.GetPosition(target);

        position = SnapHelper.SnapPoint(position, 6, 6, true);
        
        if (!_xamlEditor.TryGetXamlItem(target, out var targetXamlItem))
        {
            // TODO: Set xamlItem as root and build visual tree.
            return;
        }

        if (targetXamlItem is null)
        {
            return;
        }

        _xamlEditor.InsertXamlItem(targetXamlItem, _xamlItem, position, enableCallback: true);
    }
}
