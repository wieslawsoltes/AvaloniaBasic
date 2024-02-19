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
    private Rect _selectionRect;

    public RectangleTool(IToolContext context)
    {
        _ignored = new HashSet<Visual>(new Visual[] {context.OverlayService.Overlay});
    }

    public override void OnPointerPressed(IToolContext context, object? sender, PointerPressedEventArgs e)
    {
        var point = e.GetPosition(null);

        _startPoint = point;
        _endPoint = _startPoint;
        _selectionRect = RectHelper.GetSelectionRect(_startPoint, _endPoint);

        e.Pointer.Capture(context.Host);
        _captured = true;
 
        context.XamlSelection.Hover(null);
        context.XamlSelection.Select(null);
        context.XamlSelection.ClearSelection();

        context.Host.Focus();
    }

    public override void OnPointerReleased(IToolContext context, object? sender, PointerReleasedEventArgs e)
    {
        if (e.Pointer.Captured is null || !_captured)
        {
            return;
        }
        
        e.Pointer.Capture(null);
        _captured = false;

        _endPoint = e.GetPosition(null);
        _selectionRect = RectHelper.GetSelectionRect(_startPoint, _endPoint);
  
        context.XamlSelection.Selection(_startPoint, _endPoint);
        context.XamlSelection.ClearSelection();

        {
            var visualRoot = (sender as Control).GetVisualRoot() as Interactive;

            var target = context.XamlEditor.HitTest(visualRoot, e.GetPosition(visualRoot), _ignored);
            if (target is null)
            {
                return;
            }

            // TODO:
            // var position = _selectionRect.TopLeft;
            // var position = e.GetPosition(target);

            var width = _selectionRect.Width;
            var height = _selectionRect.Height;
            var position = visualRoot.TranslatePoint(_selectionRect.TopLeft, target).Value;

            var rectangleXamlItem = new XamlItem(name: "Rectangle",
                id: context.XamlEditor.IdManager.GetNewId(),
                properties: new Dictionary<string, XamlValue>
                {
                    ["Fill"] = (XamlValue) "Blue",
                },
                contentProperty: null,
                childrenProperty: null);

            rectangleXamlItem.Properties["Canvas.Left"] = StringXamlValue.From(position.X);
            rectangleXamlItem.Properties["Canvas.Top"] = StringXamlValue.From(position.Y);
            rectangleXamlItem.Properties["Width"] = StringXamlValue.From(width);
            rectangleXamlItem.Properties["Height"] = StringXamlValue.From(height);

            context.XamlEditor.InsertXamlItem(target, rectangleXamlItem, position);
        }
    }

    public override void OnPointerMoved(IToolContext context, object? sender, PointerEventArgs e)
    {
        if (e.Pointer.Captured is null || !_captured)
        {
            return;
        }

        _endPoint = e.GetPosition(null);
        _selectionRect = RectHelper.GetSelectionRect(_startPoint, _endPoint);

        context.XamlSelection.Selection(_startPoint, _endPoint);
    }

    public override void OnPointerExited(IToolContext context, object? sender, PointerEventArgs e)
    {
        _captured = false;
        e.Pointer.Capture(null);

        context.XamlSelection.Hover(null);
        context.XamlSelection.ClearSelection();
    }

    public override void OnPointerCaptureLost(IToolContext context, object? sender, PointerCaptureLostEventArgs e)
    {
        _captured = false;
        e.Pointer.Capture(null);

        context.XamlSelection.Hover(null);
        context.XamlSelection.ClearSelection();
    }
}
