using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.VisualTree;

namespace BoundsDemo;

public partial class EditorCanvasView : UserControl
{
    public static readonly StyledProperty<Overlay> OverlayControlProperty = 
        AvaloniaProperty.Register<EditorCanvasView, Overlay>(nameof(OverlayControl));

    private readonly HashSet<Visual> _ignored;

    public Overlay OverlayControl
    {
        get => GetValue(OverlayControlProperty);
        set => SetValue(OverlayControlProperty, value);
    }

    public bool ReverseOrder { get; set; } = true;

    public EditorCanvasView()
    {
        InitializeComponent();

        _ignored = new HashSet<Visual>(new Visual[] {OverlayControl});

        AddHandler(PointerPressedEvent, OnPointerPressed, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        AddHandler(PointerMovedEvent, OnPointerMoved, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        AddHandler(PointerExitedEvent, OnPointerExited, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        AddHandler(PointerCaptureLostEvent, OnPointerCaptureLost, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
    }

    private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (e.Source is LightDismissOverlayLayer)
        {
            return;
        }

        var visuals = HitTest(this, e.GetPosition(this), OverlayControl.HitTestMode, _ignored);

        OverlayControl.Select(visuals.FirstOrDefault());
    }

    private void OnPointerMoved(object? sender, PointerEventArgs e)
    {
        var visuals = HitTest(this, e.GetPosition(this), OverlayControl.HitTestMode, _ignored);

        OverlayControl.Hovered = visuals.FirstOrDefault();
        OverlayControl.InvalidateVisual();
    }

    private void OnPointerExited(object? sender, PointerEventArgs e)
    {
        OverlayControl.Hovered = null;
        OverlayControl.InvalidateVisual();
    }

    private void OnPointerCaptureLost(object? sender, PointerCaptureLostEventArgs e)
    {
        OverlayControl.Hovered = null;
        OverlayControl.InvalidateVisual();
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

        var visuals = descendants
            .Where(visual =>
            {
                if (!ignored.Contains(visual) && Overlay.GetEnableHitTest(visual))
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

