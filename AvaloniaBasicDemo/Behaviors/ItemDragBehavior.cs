using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using Avalonia.Xaml.Interactivity;
using AvaloniaBasicDemo.Model;
using AvaloniaBasicDemo.Utilities;

namespace AvaloniaBasicDemo.Behaviors;

public class ItemDragBehavior : Behavior<ListBoxItem>
{
    public static readonly StyledProperty<Canvas?> PreviewCanvasProperty = 
        AvaloniaProperty.Register<ItemDragBehavior, Canvas?>(nameof(PreviewCanvas));

    private Point _start;
    private bool _isDragging;
    private Control? _previewControl;
    private IPanel? _dropArea;

    public Canvas? PreviewCanvas
    {
        get => GetValue(PreviewCanvasProperty);
        set => SetValue(PreviewCanvasProperty, value);
    }

    protected override void OnAttachedToVisualTree()
    {
        if (AssociatedObject is { })
        {
            AssociatedObject.AddHandler(InputElement.PointerPressedEvent, PointerPressed, RoutingStrategies.Tunnel);
            AssociatedObject.AddHandler(InputElement.PointerReleasedEvent, PointerReleased, RoutingStrategies.Tunnel);
            AssociatedObject.AddHandler(InputElement.PointerMovedEvent, PointerMoved, RoutingStrategies.Tunnel);
        }
    }

    protected override void OnDetachedFromVisualTree()
    {
        if (AssociatedObject is { })
        {
            AssociatedObject.RemoveHandler(InputElement.PointerPressedEvent, PointerPressed);
            AssociatedObject.RemoveHandler(InputElement.PointerReleasedEvent, PointerReleased);
            AssociatedObject.RemoveHandler(InputElement.PointerMovedEvent, PointerMoved);
        }
    }

    private void PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (PreviewCanvas is null)
        {
            return;
        }

        var point = e.GetPosition(PreviewCanvas);

        _start = point;
        e.Pointer.Capture(AssociatedObject);
    }

    private void PointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (PreviewCanvas is null)
        {
            return;
        }

        if (_previewControl is { })
        {
            RemovePreview();
        }

        if (_dropArea is { })
        {
            var point = e.GetPosition(PreviewCanvas);

            AddControl(point);

            _dropArea = null;
        }

        _isDragging = false;
        e.Pointer.Capture(null);
    }

    private void PointerMoved(object? sender, PointerEventArgs e)
    {
        if (PreviewCanvas is null)
        {
            return;
        }

        if (AssociatedObject is null)
        {
            return;
        }

        if (e.Pointer.Captured == null)
        {
            return;
        }

        var point = e.GetPosition(PreviewCanvas);

        _dropArea = AssociatedObject
            .GetVisualRoot()
            .GetVisualsAt(point)
            .OfType<IPanel>()
            .FirstOrDefault(DragSettings.GetIsDropArea);

        if (!_isDragging)
        {
            var minimumDragDelta = DragSettings.GetMinimumDragDelta(AssociatedObject);
            var delta = _start - point;
            if (Math.Abs(delta.X) > minimumDragDelta || Math.Abs(delta.Y) > minimumDragDelta)
            {
                _isDragging = true;

                AddPreview(point);
            }
        }
        else
        {
            MovePreview(point);
        }

        UpdatePreview();
    }

    private Point SnapPoint(Point point, bool isPreview)
    {
        if (AssociatedObject is null)
        {
            return point;
        }

        if (_dropArea is { })
        {
            var translatePoint = _dropArea.TranslatePoint(point, PreviewCanvas);
            if (translatePoint is { })
            {
                point = translatePoint.Value;
            }
        }

        var snapToGrid = DragSettings.GetSnapToGrid(AssociatedObject) && _dropArea is { };
        var snapX = DragSettings.GetSnapX(AssociatedObject);
        var snapY = DragSettings.GetSnapY(AssociatedObject);
        var snappedPoint = Snap.SnapPoint(point, snapX, snapY, snapToGrid);

        if (_dropArea is { })
        {
            if (isPreview)
            {
                var translatePoint = PreviewCanvas.TranslatePoint(snappedPoint, _dropArea);
                if (translatePoint is { })
                {
                    snappedPoint = translatePoint.Value;
                }
            }
            else
            {
                var translatePointBack = PreviewCanvas.TranslatePoint(snappedPoint, _dropArea);
                if (translatePointBack is { })
                {
                    var root = AssociatedObject.GetVisualRoot();
                    var translatePoint = root.TranslatePoint(translatePointBack.Value, _dropArea);
                    if (translatePoint is { })
                    {
                        snappedPoint = translatePoint.Value;
                    }
                }
            }
        }

        return snappedPoint;
    }
    
    private void AddPreview(Point point)
    {
        if (PreviewCanvas is null)
        {
            return;
        }

        if (AssociatedObject?.DataContext is IDragItem item)
        {
            point = SnapPoint(point, true);

            _previewControl = item.CreatePreview();

            Canvas.SetLeft(_previewControl, point.X);
            Canvas.SetTop(_previewControl, point.Y);

            PreviewCanvas.Children.Add(_previewControl);
        }
    }

    private void MovePreview(Point point)
    {
        if (AssociatedObject is null)
        {
            return;
        }
        
        if (_previewControl is null)
        {
            return;
        }

        point = SnapPoint(point, true);

        Canvas.SetLeft(_previewControl, point.X);
        Canvas.SetTop(_previewControl, point.Y);
    }

    private void UpdatePreview()
    {
        if (AssociatedObject is null)
        {
            return;
        }

        if (_previewControl is null)
        {
            return;
        }

        if (AssociatedObject?.DataContext is IDragItem item)
        {
            var isPointerOver = _dropArea is { };

            item.UpdatePreview(_previewControl, isPointerOver);
        }
    }

    private void RemovePreview()
    {
        if (AssociatedObject is null)
        {
            return;
        }

        if (PreviewCanvas is null)
        {
            return;
        }

        if (_previewControl is null)
        {
            return;
        }

        PreviewCanvas.Children.Remove(_previewControl);

        _previewControl = null;
    }

    private void AddControl(Point point)
    {
        if (_dropArea is null)
        {
            return;
        }

        if (AssociatedObject?.DataContext is IDragItem item)
        {
            var control = item.CreateControl();

            point = SnapPoint(point, false);

            Canvas.SetLeft(control, point.X);
            Canvas.SetTop(control, point.Y);

            _dropArea.Children.Add(control);
        }
    }
}
