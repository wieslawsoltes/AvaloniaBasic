using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace ResizingControlDemo.Controls;

public class ToolBoxItem : ListBoxItem
{
    private string? _typeName;
    private bool _isPressed;
    private Control? _control;
    private Point _start;

    protected override Type StyleKeyOverride => typeof(ListBoxItem);

    public ToolBoxItem()
    {
        AddHandler(PointerPressedEvent, ListBoxItemOnPointerPressed, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        AddHandler(PointerReleasedEvent, ListBoxItemOnPointerReleased, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        AddHandler(PointerMovedEvent, ListBoxItemOnPointerMoved, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
    }

    private void ListBoxItemOnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (sender is Control { DataContext: string typeName })
        {
            if (Parent is ToolBox { EditorCanvas: { } editorCanvas })
            {
                var adornerLayer = AdornerLayer.GetAdornerLayer(editorCanvas);
                _start = e.GetPosition(adornerLayer);

                _typeName = typeName;
                _isPressed = true;
            }
        }
    }
    
    private void ListBoxItemOnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (!_isPressed || _typeName is null)
        {
            return;
        }

        if (Parent is ToolBox { EditorCanvas: { } editorCanvas })
        {
            if (_control is not null)
            {
                var adornerLayer = AdornerLayer.GetAdornerLayer(editorCanvas);
                adornerLayer.Children.Remove(_control);

                var control = ControlFactory.CreateControl(_typeName);
                if (control is not null)
                {
                    var canvasPoint = e.GetPosition(editorCanvas);

                    AdornerLayer.SetAdorner(control, new ResizingAdornerControl
                    {
                        EditorCanvas = editorCanvas
                    });

                    DropIntoCanvas(editorCanvas, canvasPoint, control);
                }
            }
        }

        _control = null;
        _typeName = null;
        _isPressed = true;
    }

    public static void InsertToCanvas(Canvas canvas, Control control, Point point)
    {
        canvas.Children.Add(control);
                        
        var left = point.X;
        var top = point.Y;
#if true
        left = ResizingAdornerControl.Snap(left, 8.0);
        top = ResizingAdornerControl.Snap(top, 8.0);
#endif    
        Canvas.SetLeft(control, left);
        Canvas.SetTop(control, top);
    }

    public static void DropIntoCanvas(Canvas editorCanvas, Point canvasPoint, Control control)
    {
        var isAddedToChild = false;
                    
        foreach (var child in editorCanvas.Children)
        {
            var childPoint = editorCanvas.TranslatePoint(canvasPoint, child).Value;

            // Panel.Children
            if (child is Panel panel 
                && panel.GetTransformedBounds().Value.Bounds.Contains(childPoint))
            {
                if (panel is Canvas canvas)
                {
                    InsertToCanvas(canvas, control, childPoint);
                    isAddedToChild = true;
                    break;
                }

                if (control is not Shape)
                {
                    control.Width = double.NaN;
                    control.Height = double.NaN;
                }

                panel.Children.Add(control);
                isAddedToChild = true;

                break;
            }

            // ContentControl.Content
            if (child is ContentControl contentControl 
                && child.GetTransformedBounds().Value.Bounds.Contains(childPoint))
            {
                if (control is not Shape)
                {
                    control.Width = double.NaN;
                    control.Height = double.NaN;
                }
                
                contentControl.Content = control;
                isAddedToChild = true;
                break;
            }
            
            // Decorator.Child
            if (child is Decorator decorator 
                && child.GetTransformedBounds().Value.Bounds.Contains(childPoint))
            {
                if (control is not Shape)
                {
                    control.Width = double.NaN;
                    control.Height = double.NaN;
                }
                
                decorator.Child = control;
                isAddedToChild = true;
                break;
            }
        }

        if (!isAddedToChild)
        {
            // TODO: Remove
            // if (control is Canvas canvas)
            // {
            //     canvas.Classes.Add("ResizingAdorner");
            // }

            InsertToCanvas(editorCanvas, control, canvasPoint);
        }
    }
    
    private void ListBoxItemOnPointerMoved(object? sender, PointerEventArgs e)
    {
        if (!_isPressed || _typeName is null)
        {
            return;
        }

        if (Parent is ToolBox { EditorCanvas: { } editorCanvas })
        {
            var adornerLayer = AdornerLayer.GetAdornerLayer(editorCanvas);

            if (_control is null)
            {
                var move = e.GetPosition(adornerLayer);
                var delta = _start - move;
                if (Math.Abs(delta.X) < 3 && Math.Abs(delta.Y) < 3)
                {
                    return;
                }
            }

            var canvasPoint = e.GetPosition(editorCanvas);

            if (_control is null)
            {
                _control = ControlFactory.CreateControl(_typeName);
                adornerLayer.Children.Add(_control);
            }

            var left = canvasPoint.X;
            var top = canvasPoint.Y;
#if true
            left = ResizingAdornerControl.Snap(left, 8.0);
            top = ResizingAdornerControl.Snap(top, 8.0);
#endif
            var adornerPoint = editorCanvas.TranslatePoint(new Point(left, top), adornerLayer).Value;
   
            Canvas.SetLeft(_control, adornerPoint.X);
            Canvas.SetTop(_control, adornerPoint.Y);
        }
    }
}
