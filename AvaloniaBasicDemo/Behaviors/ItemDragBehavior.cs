using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using Avalonia.Xaml.Interactivity;
using AvaloniaBasicDemo.Model;

namespace AvaloniaBasicDemo.Behaviors;

public class ItemDragBehavior : Behavior<ListBoxItem>
{
    public static readonly StyledProperty<Canvas?> PreviewCanvasProperty = 
        AvaloniaProperty.Register<ItemDragBehavior, Canvas?>(nameof(PreviewCanvas));

    public static readonly StyledProperty<double> MinimumDragDeltaProperty = 
        AvaloniaProperty.Register<ItemDragBehavior, double>(nameof(MinimumDragDelta), 5d);

    public static readonly AttachedProperty<bool> IsDropAreaProperty = 
        AvaloniaProperty.RegisterAttached<IAvaloniaObject, bool>("IsDropArea", typeof(ItemDragBehavior));

    private Point _start;
    private bool _isDragging;
    private Control? _previewControl;
    private IPanel? _dropArea;

    public Canvas? PreviewCanvas
    {
        get => GetValue(PreviewCanvasProperty);
        set => SetValue(PreviewCanvasProperty, value);
    }

    public double MinimumDragDelta
    {
        get => GetValue(MinimumDragDeltaProperty);
        set => SetValue(MinimumDragDeltaProperty, value);
    }

    public static bool GetIsDropArea(IAvaloniaObject obj)
    {
        return obj.GetValue(IsDropAreaProperty);
    }

    public static void SetIsDropArea(IAvaloniaObject obj, bool value)
    {
        obj.SetValue(IsDropAreaProperty, value);
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
        var root = AssociatedObject.GetVisualRoot();
        var point = e.GetPosition(root);

        _start = point;
        e.Pointer.Capture(AssociatedObject);
    }

    private void PointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (_previewControl is { })
        {
            RemovePreview();
        }

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
        if (e.Pointer.Captured == null)
            return;

        var root = AssociatedObject.GetVisualRoot();
        var point = e.GetPosition(root);

        _dropArea = root.GetVisualsAt(point).OfType<IPanel>().FirstOrDefault(GetIsDropArea);

        if (!_isDragging)
        {
            var delta = _start - point;
            if (Math.Abs(delta.X) > MinimumDragDelta)
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

    private void AddPreview(Point point)
    {
        if (PreviewCanvas is null)
        {
            return;
        }

        if (AssociatedObject?.DataContext is IDragItem item)
        {
            _previewControl = item.CreatePreview();

            Canvas.SetLeft(_previewControl, point.X);
            Canvas.SetTop(_previewControl, point.Y);

            PreviewCanvas.Children.Add(_previewControl);
        }
    }

    private void MovePreview(Point point)
    {
        if (_previewControl is null)
        {
            return;
        }

        Canvas.SetLeft(_previewControl, point.X);
        Canvas.SetTop(_previewControl, point.Y);
    }

    private void UpdatePreview()
    {
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

            Canvas.SetLeft(control, point.X);
            Canvas.SetTop(control, point.Y);

            _dropArea.Children.Add(control);
        }
    }
}
