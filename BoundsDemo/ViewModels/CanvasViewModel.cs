using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Reactive;
using Avalonia.VisualTree;
using ReactiveUI;

namespace BoundsDemo;

public enum CanvasTool
{
    None,
    Pointer,
    Selection
}

public interface ICanvasViewModel
{
    bool ReverseOrder { get; set; }
    CanvasTool CanvasTool { get; set; }
    IObservable<IReactivePropertyChangedEventArgs<IReactiveObject>> Changing { get; }
    IObservable<IReactivePropertyChangedEventArgs<IReactiveObject>> Changed { get; }
    IObservable<Exception> ThrownExceptions { get; }
    void AttachHost(Control host, Panel rootPanel);
    void DetachHost();
    void AddRoot(Control control);
    IDisposable SuppressChangeNotifications();
    bool AreChangeNotificationsEnabled();
    IDisposable DelayChangeNotifications();
    event PropertyChangingEventHandler? PropertyChanging;
    event PropertyChangedEventHandler? PropertyChanged;
}

public class CanvasViewModel : ReactiveObject, ICanvasViewModel
{
    private readonly OverlayView _overlayView;
    private readonly HashSet<Visual> _ignored;
    private Control? _host;
    private Panel? _rootPanel;
    private IDisposable? _isHitTestVisibleDisposable;
    private Point _startPoint;
    private Point _endPoint;
    private bool _captured;

    public CanvasViewModel(OverlayView overlayView)
    {
        _overlayView = overlayView;
        _ignored = new HashSet<Visual>(new Visual[] {_overlayView});
    }

    public bool ReverseOrder { get; set; } = true;

    public CanvasTool CanvasTool { get; set; } = CanvasTool.Selection;

    public void AttachHost(Control host, Panel rootPanel)
    {
        _host = host;
        _host.AddHandler(InputElement.PointerPressedEvent, OnPointerPressed, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        _host.AddHandler(InputElement.PointerReleasedEvent, OnPointerReleased, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        _host.AddHandler(InputElement.PointerMovedEvent, OnPointerMoved, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        _host.AddHandler(InputElement.PointerExitedEvent, OnPointerExited, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        _host.AddHandler(InputElement.PointerCaptureLostEvent, OnPointerCaptureLost, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);

        _rootPanel = rootPanel;

        _isHitTestVisibleDisposable = _rootPanel.GetObservable(InputElement.IsHitTestVisibleProperty).Subscribe(new AnonymousObserver<bool>(_ =>
        {
            _overlayView.Hover(null);
            _overlayView.Select(null);
        }));
    }

    public void DetachHost()
    {
        if (_host is null)
        {
            return;
        }

        _host.RemoveHandler(InputElement.PointerPressedEvent, OnPointerPressed);
        _host.RemoveHandler(InputElement.PointerMovedEvent, OnPointerMoved);
        _host.RemoveHandler(InputElement.PointerExitedEvent, OnPointerExited);
        _host.RemoveHandler(InputElement.PointerCaptureLostEvent, OnPointerCaptureLost);
        _host = null;

        _isHitTestVisibleDisposable?.Dispose();
        _rootPanel = null;
    }

    public void AddRoot(Control control)
    {
        if (_rootPanel is null)
        {
            return;
        }

        _rootPanel.Children.Clear();
        _rootPanel.Children.Add(control);
    }

    private Rect GetSelectionRect()
    {
        var topLeft = new Point(
            Math.Min(_startPoint.X, _endPoint.X),
            Math.Min(_startPoint.Y, _endPoint.Y));
        var bottomRight = new Point(
            Math.Max(_startPoint.X, _endPoint.X),
            Math.Max(_startPoint.Y, _endPoint.Y));
        return new Rect(topLeft, bottomRight);
    }

    private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (_host is null || _rootPanel is null)
        {
            return;
        }

        if (_rootPanel.IsHitTestVisible)
        {
            return;
        }

        if (e.Source is LightDismissOverlayLayer)
        {
            return;
        }
        
        var properties = e.GetCurrentPoint(null).Properties;
        if (!properties.IsLeftButtonPressed)
        {
            return;
        }

        switch (CanvasTool)
        {
            case CanvasTool.None:
            {
                break;
            }
            case CanvasTool.Pointer:
            {
                var point = e.GetPosition(null);
                var visuals = HitTest(_host, _overlayView.HitTestMode, _ignored, x => x.Contains(point));
                var first = visuals.FirstOrDefault();
                _overlayView.Select(first is null ? Enumerable.Empty<Visual>() : Enumerable.Repeat(first, 1));
                _host.Focus();
                break;
            }
            case CanvasTool.Selection:
            {
                _overlayView.Hover(null);
                _overlayView.Select(null);
                _overlayView.ClearSelection();
                _startPoint = e.GetPosition(null);
                _endPoint = _startPoint;
                e.Pointer.Capture(_host);
                _captured = true;
                UpdateRectSelection();
                _host.Focus();
                break;
            }
        }
    }

    private void OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (_host is null || _rootPanel is null)
        {
            return;
        }

        if (_rootPanel.IsHitTestVisible)
        {
            return;
        }

        if (e.Source is LightDismissOverlayLayer)
        {
            return;
        }

        switch (CanvasTool)
        {
            case CanvasTool.None:
            {
                break;
            }
            case CanvasTool.Pointer:
            {
                break;
            }
            case CanvasTool.Selection:
            {
                if (e.Pointer.Captured is not null && _captured)
                {
                    _endPoint = e.GetPosition(null);
                    _overlayView.Selection(_startPoint, _endPoint);
                    UpdateRectSelection();
                    e.Pointer.Capture(null);
                    _captured = false;
                    _overlayView.ClearSelection();
                }
                break;
            }
        }
    }

    private void OnPointerMoved(object? sender, PointerEventArgs e)
    {
        if (_host is null || _rootPanel is null)
        {
            return;
        }

        if (_rootPanel.IsHitTestVisible)
        {
            return;
        }

        switch (CanvasTool)
        {
            case CanvasTool.None:
            {
                break;
            }
            case CanvasTool.Pointer:
            {
                var point = e.GetPosition(null);
                var visuals = HitTest(_host, _overlayView.HitTestMode, _ignored, x => x.Contains(point));
                _overlayView.Hover(visuals.FirstOrDefault());
                break;
            }
            case CanvasTool.Selection:
            {
                if (e.Pointer.Captured is not null && _captured)
                {
                    _endPoint = e.GetPosition(null);
                    _overlayView.Selection(_startPoint, _endPoint);
                    UpdateRectSelection();
                }
                break;
            }
        }
    }

    private void OnPointerExited(object? sender, PointerEventArgs e)
    {
        if (_rootPanel is null)
        {
            return;
        }

        if (_rootPanel.IsHitTestVisible)
        {
            return;
        }

        _overlayView.Hover(null);
        e.Pointer.Capture(null);
        _captured = false;
        _overlayView.ClearSelection();
    }

    private void OnPointerCaptureLost(object? sender, PointerCaptureLostEventArgs e)
    {
        if (_rootPanel is null)
        {
            return;
        }

        if (_rootPanel.IsHitTestVisible)
        {
            return;
        }

        _overlayView.Hover(null);
        e.Pointer.Capture(null);
        _captured = false;
        _overlayView.ClearSelection();
    }

    private void UpdateRectSelection()
    {
        if (_host is null)
        {
            return;
        }

        // TODO:
        var rect = GetSelectionRect();
        var visuals = HitTest(_host, _overlayView.HitTestMode, _ignored, 
            x => 
                {
                    var transformedRect = x.Bounds.TransformToAABB(x.Transform);
                    return rect.Intersects(transformedRect);
                }).Reverse().Skip(1);

        _overlayView.Select(visuals);
#if false
        Console.WriteLine("[HitTest]");
        foreach (var visual in visuals)
        {
            Console.WriteLine($"{visual}");
        }
#endif
    }

    private IEnumerable<Visual> HitTest(Interactive interactive, HitTestMode hitTestMode, HashSet<Visual> ignored, Func<TransformedBounds, bool> filter)
    {
        if (_host?.DataContext is not MainViewViewModel mainViewModel)
        {
            return Enumerable.Empty<Visual>();
        }

        var root = interactive.GetVisualRoot();
        if (root is null)
        {
            return Enumerable.Empty<Visual>();
        }

        var descendants = new List<Visual>();

        switch (hitTestMode)
        {
            case HitTestMode.Visual:
                descendants.AddRange(interactive.GetVisualDescendants());
                break;
            case HitTestMode.Logical:
                descendants.AddRange(interactive.GetLogicalDescendants().Cast<Visual>());
                break;
        }

        var visuals = descendants
            .OfType<Control>()
            .Select(visual =>
            {
                mainViewModel.XamlEditorViewModel.TryGetXamlItem(visual, out var xamlItem);
                return new
                {
                    Visual = visual,
                    XamlItem = xamlItem,
                    IsIgnored = ignored.Contains(visual),
                    TransformedBounds = visual.GetTransformedBounds()
                };
            })
            .Where(x =>
            {
                if (x.XamlItem is null || x.IsIgnored || x.TransformedBounds is null)
                {
                    return false;
                }
                var transformedBounds = x.TransformedBounds.Value;
                return filter(transformedBounds);
            })
            .Select(x => x.Visual);

        return ReverseOrder 
            ? visuals.Reverse() 
            : visuals;
    }
}
