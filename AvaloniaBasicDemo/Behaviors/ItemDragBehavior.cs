using System;
using System.Diagnostics;
using System.Linq;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using Avalonia.Xaml.Interactivity;
using AvaloniaBasicDemo.Model;
using AvaloniaBasicDemo.Utilities;

namespace AvaloniaBasicDemo.Behaviors;

public class ItemDragBehavior : Behavior<Control>
{
    public static readonly StyledProperty<Canvas?> PreviewCanvasProperty = 
        AvaloniaProperty.Register<ItemDragBehavior, Canvas?>(nameof(PreviewCanvas));

    public static readonly StyledProperty<Canvas?> DropAreaCanvasProperty = 
        AvaloniaProperty.Register<ItemDragBehavior, Canvas?>(nameof(DropAreaCanvas));

    private Point _start;
    private bool _started;
    private bool _isDragging;
    private Control? _previewControl;
    private IControl? _dropArea;

    public Canvas? PreviewCanvas
    {
        get => GetValue(PreviewCanvasProperty);
        set => SetValue(PreviewCanvasProperty, value);
    }

    public Canvas? DropAreaCanvas
    {
        get => GetValue(DropAreaCanvasProperty);
        set => SetValue(DropAreaCanvasProperty, value);
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
        if (AssociatedObject?.DataContext is not IDragItem)
        {
            return;
        }

        if (PreviewCanvas is null)
        {
            return;
        }

        _start = e.GetPosition(PreviewCanvas);
        _started = true;
    }

    private void PointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (PreviewCanvas is null || AssociatedObject is null)
        {
            return;
        }

        _started = false;

        if (!_isDragging)
        {
            return;
        }
        
        RemovePreview();

        if (_dropArea is { })
        {
            var point = e.GetPosition(_dropArea);

            AddControl(point);

            _dropArea = null;
        }

        _isDragging = false;
        e.Pointer.Capture(null);
    }

    private void PointerMoved(object? sender, PointerEventArgs e)
    {
        if (PreviewCanvas is null || AssociatedObject is null)
        {
            return;
        }

        if (!_started)
        {
            return;
        }
        
        if (!_isDragging)
        {
            var point = e.GetPosition(PreviewCanvas);
            var minimumDragDelta = DragSettings.GetMinimumDragDelta(AssociatedObject);
            var delta = _start - point;

            if (Math.Abs(delta.X) > minimumDragDelta || Math.Abs(delta.Y) > minimumDragDelta)
            {
                FindDropArea(point);
 
                if (_dropArea is { })
                {
                    point = e.GetPosition(_dropArea);
                }

                AddPreview(point);
                UpdatePreview();

                _isDragging = true;
                e.Pointer.Capture(AssociatedObject);
            }
        }
        else
        {
            var point = e.GetPosition(PreviewCanvas);

            FindDropArea(point);

            if (_dropArea is { })
            {
                point = e.GetPosition(_dropArea);
            }

            MovePreview(point);
            UpdatePreview();
        }
    }

    private void FindDropArea(Point point)
    {
        if (AssociatedObject is null)
        {
            _dropArea = null;
            return;
        }

        var root = AssociatedObject.GetVisualRoot();

        // TODO: var visuals = root.GetVisualsAt(point);
        var visuals = (root as TopLevel)
            .GetVisualDescendants()
            .Where(x => x.TransformedBounds is not null && x.TransformedBounds.Value.Contains(point))
            .Reverse();

        var dropAreas = visuals.OfType<IControl>().Where(DragSettings.GetIsDropArea).ToList();

        _dropArea = dropAreas.FirstOrDefault(DragSettings.GetIsDropArea);

        // Debug.WriteLine($"dropAreas: {dropAreas.Count}, _dropArea: {_dropArea}, point: {point}");
    }

    private Point SnapPoint(Point point, bool isPreview)
    {
        if (AssociatedObject is null)
        {
            return point;
        }

        IAvaloniaObject snapObject = _dropArea ?? AssociatedObject;

        var snapToGrid = DragSettings.GetSnapToGrid(snapObject) && _dropArea is { };
        var snapX = DragSettings.GetSnapX(snapObject);
        var snapY = DragSettings.GetSnapY(snapObject);
        var snappedPoint = Snap.SnapPoint(point, snapX, snapY, snapToGrid);

        if (_dropArea is { } && isPreview)
        {
            var translatePoint = _dropArea.TranslatePoint(snappedPoint, PreviewCanvas);
            if (translatePoint is { })
            {
                snappedPoint = translatePoint.Value;
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
            point = SnapPoint(point, _dropArea is null);

            _previewControl = item.CreatePreview();

            Canvas.SetLeft(_previewControl, point.X);
            Canvas.SetTop(_previewControl, point.Y);

            PreviewCanvas.Children.Add(_previewControl);
        }
    }

    private void MovePreview(Point point)
    {
        if (AssociatedObject is null || _previewControl is null)
        {
            return;
        }

        point = SnapPoint(point, true);

        Canvas.SetLeft(_previewControl, point.X);
        Canvas.SetTop(_previewControl, point.Y);
    }

    private void UpdatePreview()
    {
        if (AssociatedObject is null || _previewControl is null)
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
        if (AssociatedObject is null || PreviewCanvas is null || _previewControl is null)
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

            // TODO: Properly position in panel.

            // Debug.WriteLine($"_dropArea: {_dropArea}, point: {point}");

            if (_dropArea is Canvas)
            {
                Canvas.SetLeft(control, point.X);
                Canvas.SetTop(control, point.Y);
            }

            if (_dropArea is IPanel and not Canvas)
            {
                // TODO: control.Margin = new Thickness(point.X, point.Y, 0d, 0d);
            }

            // TODO: Use ContentAttribute to set content if applicable.

            if (_dropArea is IPanel panel)
            {
                panel.Children.Add(control);
            }
            else if (_dropArea is IContentControl contentControl)
            {
                contentControl.Content = control;
            }
            else if (_dropArea is Decorator decorator)
            {
                decorator.Child = control;
            }
            else if (_dropArea is ItemsControl itemsControl)
            {
                if (itemsControl.Items is AvaloniaList<object> items)
                {
                    items.Add(control);
                }
            } 
        }
    }
}
