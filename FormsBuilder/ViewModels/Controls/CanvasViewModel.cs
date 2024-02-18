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

namespace FormsBuilder;

public interface ICanvasViewModel
{
    bool ReverseOrder { get; set; }
    IObservable<IReactivePropertyChangedEventArgs<IReactiveObject>> Changing { get; }
    IObservable<IReactivePropertyChangedEventArgs<IReactiveObject>> Changed { get; }
    IObservable<Exception> ThrownExceptions { get; }
    void AttachHost(Control host, Panel rootPanel, GridLines gridLines);
    void DetachHost();
    void AddToRoot(Control control);
    IDisposable SuppressChangeNotifications();
    bool AreChangeNotificationsEnabled();
    IDisposable DelayChangeNotifications();
    event PropertyChangingEventHandler? PropertyChanging;
    event PropertyChangedEventHandler? PropertyChanged;
}

public interface IToolContext
{
    IXamlEditor XamlEditor { get; }

    IXamlSelection XamlSelection { get; }

    OverlayView OverlayView { get; }

    Control? Host { get; }

    Panel? RootPanel { get; }

    IEnumerable<Visual> HitTest(
        Interactive interactive,
        HitTestMode hitTestMode,
        HashSet<Visual> ignored,
        Func<TransformedBounds, bool> filter);
}

public class CanvasViewModel : ReactiveObject, ICanvasViewModel, IToolContext
{
    private readonly OverlayView _overlayView;
    private readonly IXamlEditor _xamlEditor;
    private readonly IXamlSelection _xamlSelection;
    private Control? _host;
    private Panel? _rootPanel;
    private GridLines? _gridLines;
    private IDisposable? _isHitTestVisibleDisposable;
    private readonly List<Tool> _tools;
    private Tool? _currentTool;

    public CanvasViewModel(
        OverlayView overlayView, 
        IXamlEditor xamlEditor, 
        IXamlSelection xamlSelection)
    {
        _overlayView = overlayView;
        _xamlEditor = xamlEditor;
        _xamlSelection = xamlSelection;

        _tools = new List<Tool>
        {
            new NoneTool(),
            // Selection
            new PointerTool(this),
            new SelectionTool(this),
            // Draw
            new RectangleTool(this),
            new LineTool(),
            new EllipseTool(),
            new TextTool(),
        };
        _currentTool = _tools[2];
    }

    public IXamlEditor XamlEditor => _xamlEditor;

    public IXamlSelection XamlSelection => _xamlSelection;

    public OverlayView OverlayView => _overlayView;
 
    public Control? Host => _host;

    public Panel? RootPanel => _rootPanel;

    public GridLines? GridLines => _gridLines;

    public bool ReverseOrder { get; set; } = true;

    public void AttachHost(Control host, Panel rootPanel, GridLines gridLines)
    {
        _host = host;

        _host.AddHandler(InputElement.KeyDownEvent, OnKeyDown, RoutingStrategies.Tunnel);
        _host.AddHandler(InputElement.PointerPressedEvent, OnPointerPressed, RoutingStrategies.Tunnel);
        _host.AddHandler(InputElement.PointerReleasedEvent, OnPointerReleased, RoutingStrategies.Tunnel);
        _host.AddHandler(InputElement.PointerMovedEvent, OnPointerMoved, RoutingStrategies.Tunnel);
        _host.AddHandler(InputElement.PointerExitedEvent, OnPointerExited, RoutingStrategies.Tunnel);
        _host.AddHandler(InputElement.PointerCaptureLostEvent, OnPointerCaptureLost, RoutingStrategies.Tunnel);

        _rootPanel = rootPanel;
        _gridLines = gridLines;

        _isHitTestVisibleDisposable = _rootPanel.GetObservable(InputElement.IsHitTestVisibleProperty).Subscribe(new AnonymousObserver<bool>(_ =>
        {
            _xamlSelection.Hover(null);
            _xamlSelection.Select(null);
        }));
    }

    public void DetachHost()
    {
        if (_host is null)
        {
            return;
        }

        _host.RemoveHandler(InputElement.KeyDownEvent, OnKeyDown);
        _host.RemoveHandler(InputElement.PointerPressedEvent, OnPointerPressed);
        _host.RemoveHandler(InputElement.PointerMovedEvent, OnPointerMoved);
        _host.RemoveHandler(InputElement.PointerExitedEvent, OnPointerExited);
        _host.RemoveHandler(InputElement.PointerCaptureLostEvent, OnPointerCaptureLost);

        _host = null;

        _isHitTestVisibleDisposable?.Dispose();
        _rootPanel = null;
        _gridLines = null;
    }

    public void AddToRoot(Control control)
    {
        if (_rootPanel is null || _gridLines is null)
        {
            return;
        }

        _rootPanel.Children.Clear();
        _rootPanel.Children.Add(control);

        _gridLines.Width = control.Width;
        _gridLines.Height = control.Height;
    }

    private void OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Source is LightDismissOverlayLayer || e.Source is TextBox)
        {
            return;
        }

        switch (e.Key)
        {
            case Key.G:
            {
                if (_gridLines is not null)
                {
                    _gridLines.IsVisible = !_gridLines.IsVisible;
                }
                break;
            }
        }

        _currentTool = e.Key switch
        {
            Key.N => _tools.FirstOrDefault(x => x is NoneTool),
            // Selection
            Key.P => _tools.FirstOrDefault(x => x is PointerTool),
            Key.V => _tools.FirstOrDefault(x => x is SelectionTool),
            // Draw
            Key.R => _tools.FirstOrDefault(x => x is RectangleTool),
            Key.L => _tools.FirstOrDefault(x => x is LineTool),
            Key.O => _tools.FirstOrDefault(x => x is EllipseTool),
            // Text
            Key.T => _tools.FirstOrDefault(x => x is TextTool),
            _ => _currentTool
        };
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

        _currentTool?.OnPointerPressed(this, sender, e);
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

        _currentTool?.OnPointerReleased(this, sender, e);
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

        if (e.Source is LightDismissOverlayLayer)
        {
            return;
        }

        _currentTool?.OnPointerMoved(this, sender, e);
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

        _currentTool?.OnPointerExited(this, sender, e);
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

        _currentTool?.OnPointerCaptureLost(this, sender, e);
    }

    public IEnumerable<Visual> HitTest(Interactive interactive, HitTestMode hitTestMode, HashSet<Visual> ignored, Func<TransformedBounds, bool> filter)
    {
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
            default:
                throw new ArgumentOutOfRangeException(nameof(hitTestMode), hitTestMode, null);
        }

        var visuals = descendants
            .OfType<Control>()
            .Select(visual =>
            {
                _xamlEditor.TryGetXamlItem(visual, out var xamlItem);

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
