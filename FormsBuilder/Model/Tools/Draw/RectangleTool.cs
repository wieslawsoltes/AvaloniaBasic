using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace FormsBuilder;

public class RectangleTool : Tool
{
    private readonly HashSet<Visual> _ignored;
    private Point _startPoint;
    private Point _endPoint;
    private bool _captured;
    private Rect _rect;

    private Interactive? _visualRoot;
    private Control? _control;
    private XamlItem? _targetXamlItem;
    private XamlItem? _xamlItem;

    public RectangleTool(IToolContext context)
    {
        _ignored = new HashSet<Visual>(new Visual[] {context.OverlayService.Overlay});
    }

    private XamlItem CreateXamlItem(IToolContext context)
    {
#if false
        var xamlItem = new XamlItem(name: "DockPanel",
            id: context.XamlEditor.IdManager.GetNewId(),
            properties: new Dictionary<string, XamlValue>
            {
                ["Children"] = (XamlValue) new List<XamlItem>
                {
                    new(name: "TextBlock", 
                        id: context.XamlEditor.IdManager.GetNewId(),
                        properties: new Dictionary<string, XamlValue>
                        {
                            ["Text"] = (XamlValue) "TextBlock",
                            ["DockPanel.Dock"] = (XamlValue) "Top"
                        }, 
                        contentProperty: "Text", 
                        childrenProperty: null),
                    new(name: "TextBox", 
                        id: context.XamlEditor.IdManager.GetNewId(),
                        properties: new Dictionary<string, XamlValue>
                        {
                            ["Text"] = (XamlValue) "TextBox",
                            ["VerticalAlignment"] = (XamlValue) "Stretch",
                        },
                        contentProperty: "Text", 
                        childrenProperty: null),
                },
                ["Background"] = (XamlValue) "LightGray",
            }, 
            contentProperty: "Children", 
            childrenProperty: "Children");
#else

        var xamlItem = new XamlItem(name: "Rectangle",
            id: context.XamlEditor.IdManager.GetNewId(),
            properties: new Dictionary<string, XamlValue>
            {
                ["Fill"] = (XamlValue) "Blue",
            },
            contentProperty: null,
            childrenProperty: null);
#endif
        return xamlItem;
    }

    private void UpdateXamlItem(Point position, double width, double height, XamlItem xamlItem)
    {
        xamlItem.Properties["Canvas.Left"] = StringXamlValue.From(position.X);
        xamlItem.Properties["Canvas.Top"] = StringXamlValue.From(position.Y);
        xamlItem.Properties["Width"] = StringXamlValue.From(width);
        xamlItem.Properties["Height"] = StringXamlValue.From(height);
    }

    private void UpdateControl(Point position, double width, double height, Control control)
    {
        Canvas.SetLeft(control, position.X);
        Canvas.SetTop(control, position.Y);
        control.Width = width;
        control.Height = height;
    }

    private void Initialize(IToolContext context, object? sender, PointerPressedEventArgs e)
    {
        _visualRoot = (sender as Control)?.GetVisualRoot() as Interactive;
        if (_visualRoot is null)
        {
            return;
        }

        var target = context.XamlEditor.HitTest(_visualRoot, e.GetPosition(_visualRoot), _ignored);
        if (target is null)
        {
            _visualRoot = null;
            return;
        }

        var width = _rect.Width;
        var height = _rect.Height;
        var translatePoint = _visualRoot.TranslatePoint(_rect.TopLeft, target);
        var position = translatePoint.Value;

        width = SnapHelper.SnapValue(width, 12);
        height = SnapHelper.SnapValue(height, 12);
        position = SnapHelper.SnapPoint(position, 12, 12, true);

        _xamlItem = CreateXamlItem(context);

        UpdateXamlItem(position, width, height, _xamlItem);
        
        context.XamlEditor.TryGetXamlItem(target, out var targetXamlItem);
        _targetXamlItem = targetXamlItem;

        context.XamlEditor.InsertXamlItem(_targetXamlItem, _xamlItem, position);
  
        context.XamlEditor.TryGetControl(_xamlItem, out var control);
        _control = control;
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

        var width = _rect.Width;
        var height = _rect.Height;
        var translatePoint = _visualRoot.TranslatePoint(_rect.TopLeft, target);
        var position = translatePoint.Value;

        width = SnapHelper.SnapValue(width, 12);
        height = SnapHelper.SnapValue(height, 12);
        position = SnapHelper.SnapPoint(position, 12, 12, true);

        UpdateXamlItem(position, width, height, _xamlItem);
        UpdateControl(position, width, height, _control);

        // TODO:
        context.XamlEditor.Debug(_targetXamlItem);
    }

    private void Reset()
    {
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
        _rect = RectHelper.GetSelectionRect(_startPoint, _endPoint);

        Initialize(context, sender, e);

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
        _rect = RectHelper.GetSelectionRect(_startPoint, _endPoint);

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
        _rect = RectHelper.GetSelectionRect(_startPoint, _endPoint);

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
