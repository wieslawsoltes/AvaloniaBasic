using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.VisualTree;
using ReactiveUI;

namespace BoundsDemo;

public class CanvasViewModel : ReactiveObject
{
    private readonly Control _host;
    private readonly OverlayView _overlayView;
    private readonly Panel _rootPanel;
    private readonly HashSet<Visual> _ignored;

    public CanvasViewModel(Control host, OverlayView overlayView, Panel rootPanel)
    {
        _host = host;
        _overlayView = overlayView;
        _rootPanel = rootPanel;

        _ignored = new HashSet<Visual>(new Visual[] {_overlayView});

        _host.AddHandler(InputElement.PointerPressedEvent, OnPointerPressed, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        _host.AddHandler(InputElement.PointerMovedEvent, OnPointerMoved, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        _host.AddHandler(InputElement.PointerExitedEvent, OnPointerExited, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        _host.AddHandler(InputElement.PointerCaptureLostEvent, OnPointerCaptureLost, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);

    }
    
    public bool ReverseOrder { get; set; } = true;
    
    public void AddRoot(Control control)
    {
        _rootPanel.Children.Clear();
        _rootPanel.Children.Add(control);
    }

    private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (_rootPanel.IsHitTestVisible)
        {
            return;
        }

        if (e.Source is LightDismissOverlayLayer)
        {
            return;
        }

        var visuals = HitTest(_host, e.GetPosition(null), _overlayView.HitTestMode, _ignored);

        _overlayView.Select(visuals.FirstOrDefault());
        _host.Focus();
    }

    private void OnPointerMoved(object? sender, PointerEventArgs e)
    {
        if (_rootPanel.IsHitTestVisible)
        {
            return;
        }

        var visuals = HitTest(_host, e.GetPosition(null), _overlayView.HitTestMode, _ignored);

        _overlayView.Hover(visuals.FirstOrDefault());
    }

    private void OnPointerExited(object? sender, PointerEventArgs e)
    {
        if (_rootPanel.IsHitTestVisible)
        {
            return;
        }

        _overlayView.Hover(null);
    }

    private void OnPointerCaptureLost(object? sender, PointerCaptureLostEventArgs e)
    {
        if (_rootPanel.IsHitTestVisible)
        {
            return;
        }

        _overlayView.Hover(null);
    }

    private IEnumerable<Visual> HitTest(Interactive interactive, Point point, HitTestMode hitTestMode, HashSet<Visual> ignored)
    {
        var root = interactive.GetVisualRoot();
        if (root is null)
        {
            return Enumerable.Empty<Visual>();
        }

        var descendants = new List<Visual>();

        if (hitTestMode == HitTestMode.Visual)
        {
            descendants.AddRange(interactive.GetVisualDescendants());
        }

        if (hitTestMode == HitTestMode.Logical)
        {
            descendants.AddRange(interactive.GetLogicalDescendants().Cast<Visual>());
        }

        var mainViewModel = _host.DataContext as MainViewViewModel;
        if (mainViewModel is null)
        {
            return Enumerable.Empty<Visual>();
        }

        var visuals = descendants
            .OfType<Control>()
            .Where(visual =>
            {
                if (!mainViewModel.TryGetXamlItem(visual, out _))
                {
                    return false;
                }

                //if (!ignored.Contains(visual) && OverlayView.GetEnableHitTest(visual))
                if (!ignored.Contains(visual))
                {
                    var transformedBounds = visual.GetTransformedBounds();
                    return transformedBounds is not null
                           && transformedBounds.Value.Contains(point);
                }

                return false;
            });

        return ReverseOrder 
            ? visuals.Reverse() 
            : visuals;
    }
}
