using System;
using System.Diagnostics;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.VisualTree;
using Avalonia.Xaml.Interactivity;

namespace DragAdornerDemo;

public class ItemDragBehavior : Behavior<ListBoxItem>
{
    public static readonly StyledProperty<Canvas> PreviewCanvasProperty = 
        AvaloniaProperty.Register<ItemDragBehavior, Canvas>(nameof(PreviewCanvas));

    private Point _start;
    private bool _isDragging;
    private TextBlock? _previewTextBlock;
    private Canvas? _dropArea;

    public Canvas PreviewCanvas
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
        var root = AssociatedObject.GetVisualRoot();
        var point = e.GetPosition(root);

        _start = point;
        e.Pointer.Capture(AssociatedObject);
    }

    private void PointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (_previewTextBlock is { })
        {
            PreviewCanvas.Children.Remove(_previewTextBlock);
            _previewTextBlock = null;

            if (_dropArea is { } && AssociatedObject?.DataContext is Item item)
            {
                var point = e.GetPosition(_dropArea);
                var textBlock = new TextBlock() {Text = item.Name};
                textBlock.Foreground = Brushes.Blue;
                Canvas.SetLeft(textBlock, point.X);
                Canvas.SetTop(textBlock, point.Y);
                _dropArea.Children.Add(textBlock);
            }
        }

        _isDragging = false;
        _dropArea = null;
        e.Pointer.Capture(null);
    }

    private void PointerMoved(object? sender, PointerEventArgs e)
    {
        if (e.Pointer.Captured == null)
            return;
            
        var root = AssociatedObject.GetVisualRoot();
        var point = e.GetPosition(root);
        var delta = _start - point;

        // ReSharper disable once ReplaceWithSingleCallToFirstOrDefault
        _dropArea = root
            .GetVisualsAt(point)
            .OfType<Canvas>()
            .Where(x => x.Tag is string s && s == "DropArea")
            .FirstOrDefault();

        if (!_isDragging)
        {
            if (Math.Abs(delta.X) > 5d)
            {
                _isDragging = true;

                if (AssociatedObject?.DataContext is Item item)
                {
                    _previewTextBlock = new TextBlock() {Text = item.Name};
                    Canvas.SetLeft(_previewTextBlock, point.X);
                    Canvas.SetTop(_previewTextBlock, point.Y);
                    PreviewCanvas.Children.Add(_previewTextBlock);
                }
            }
        }
        else
        {
            if (_previewTextBlock is { })
            {
                Canvas.SetLeft(_previewTextBlock, point.X);
                Canvas.SetTop(_previewTextBlock, point.Y);
            }

            Debug.WriteLine($"Point: {point} Delta: {delta}, dropArea: {_dropArea}");
        }

        if (_previewTextBlock is { })
        {
            if (_dropArea is { })
            {
                _previewTextBlock.Foreground = Brushes.Green;
            }
            else
            {
                _previewTextBlock.Foreground = Brushes.Red;
            }
        }
    }
}
