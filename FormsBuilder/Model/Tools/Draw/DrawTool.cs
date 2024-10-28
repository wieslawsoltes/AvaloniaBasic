using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using XamlDom;

namespace FormsBuilder;

public abstract class DrawTool : Tool
{
    private HashSet<Visual> _ignored;
    private Point _startPoint;
    private Point _endPoint;
    private bool _captured;

    private Interactive? _visualRoot;
    private Control? _control;
    private XamlItem? _targetXamlItem;
    private XamlItem? _xamlItem;
    private Point _startPosition;
    private Point _endPosition;

    public DrawTool(IToolContext context)
    {
        _ignored = new HashSet<Visual>(new Visual[] {context.OverlayService.Overlay});
    }

    protected abstract XamlItem CreateXamlItem(IToolContext context);

    protected abstract void UpdateXamlItem(Point startPosition, Point endPosition, XamlItem xamlItem);

    protected abstract void UpdateControl(Point startPosition, Point endPosition,Control control);

    private void Pressed(IToolContext context, object? sender, PointerPressedEventArgs e)
    {
        _visualRoot = (sender as Control)?.GetVisualRoot() as Interactive;
        if (_visualRoot is null)
        {
            return;
        }

        var target = AvaloniaHitTestHelper.HitTest(
            _visualRoot, 
            e.GetPosition(_visualRoot), 
            _ignored,
            x => context.XamlEditor.TryGetXamlItem(x, out _));
        if (target is null)
        {
            _visualRoot = null;
            return;
        }

        var translatePoint = _visualRoot.TranslatePoint(_startPoint, target);
        _startPosition = translatePoint.Value;

        _startPosition = SnapHelper.SnapPoint(_startPosition, 12, 12, true);

        _xamlItem = CreateXamlItem(context);

        UpdateXamlItem(_startPosition, _startPosition, _xamlItem);

        context.XamlEditor.TryGetXamlItem(target, out var targetXamlItem);
        _targetXamlItem = targetXamlItem;

        context.XamlEditor.InsertXamlItem(_targetXamlItem, _xamlItem, _startPosition.X, _startPosition.Y, enableCallback: false);
  
        context.XamlEditor.TryGetControl(_xamlItem, out var control);
        _control = control;

        _ignored.Add(_control);
    }

    private void Move(IToolContext context)
    {
        if (_visualRoot is null || _targetXamlItem is null || _control is null || _xamlItem is null)
        {
            return;
        }

        context.XamlEditor.TryGetControl(_targetXamlItem, out var target);
        if (target is null)
        {
            return;
        }

        var translatePoint = _visualRoot.TranslatePoint(_endPoint, target); 
        _endPosition = translatePoint.Value;

        _endPosition = SnapHelper.SnapPoint(_endPosition, 12, 12, true);

        UpdateXamlItem(_startPosition, _endPosition, _xamlItem);
        UpdateControl(_startPosition, _endPosition, _control);

        // TODO:
        context.XamlEditor.Debug(_targetXamlItem);
    }

    private void Reset()
    {
        if (_control is not null)
        {
            _ignored.Remove(_control);
        }

        _visualRoot = null;
        _targetXamlItem = null;
        _control = null;
        _xamlItem = null;
    }

    public override void OnPointerPressed(IToolContext context, object? sender, PointerPressedEventArgs e)
    {
        var point = e.GetPosition(null);

        _startPoint = point;
        _endPoint = _startPoint;

        Pressed(context, sender, e);

        e.Pointer.Capture(context.Host);
        _captured = true;
    }

    public override void OnPointerReleased(IToolContext context, object? sender, PointerReleasedEventArgs e)
    {
        if (e.Pointer.Captured is null || !_captured)
        {
            return;
        }
        
        e.Pointer.Capture(null);
        _captured = false;

        var point = e.GetPosition(null);

        _endPoint = point;

        Move(context);
        Reset();
    }

    public override void OnPointerMoved(IToolContext context, object? sender, PointerEventArgs e)
    {
        if (e.Pointer.Captured is null || !_captured)
        {
            return;
        }

        var point = e.GetPosition(null);

        _endPoint = point;

        Move(context);
    }

    public override void OnPointerExited(IToolContext context, object? sender, PointerEventArgs e)
    {
        _captured = false;
        e.Pointer.Capture(null);

        Reset();
    }

    public override void OnPointerCaptureLost(IToolContext context, object? sender, PointerCaptureLostEventArgs e)
    {
        _captured = false;
        e.Pointer.Capture(null);

        Reset();
    }
}
