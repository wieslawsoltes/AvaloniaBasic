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
    public static readonly StyledProperty<OverlayView> OverlayViewProperty = 
        AvaloniaProperty.Register<EditorCanvasView, OverlayView>(nameof(OverlayView));

    private readonly HashSet<Visual> _ignored;

    public OverlayView OverlayView
    {
        get => GetValue(OverlayViewProperty);
        set => SetValue(OverlayViewProperty, value);
    }

    public bool ReverseOrder { get; set; } = true;

    public EditorCanvasView()
    {
        InitializeComponent();

        _ignored = new HashSet<Visual>(new Visual[] {OverlayView});

        AddHandler(PointerPressedEvent, OnPointerPressed, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        AddHandler(PointerMovedEvent, OnPointerMoved, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        AddHandler(PointerExitedEvent, OnPointerExited, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        AddHandler(PointerCaptureLostEvent, OnPointerCaptureLost, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);

        Focusable = true;
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);

        if (DataContext is ToolBoxViewModel toolBoxViewModel)
        {
            Demo(toolBoxViewModel);
        }
    }

    private void Demo(ToolBoxViewModel toolBoxViewModel)
    {
        var stackPanelXamlItem = new XamlItem(name: "StackPanel",
            properties: new Dictionary<string, object>
            {
                ["Children"] = new List<XamlItem>(),
                ["Background"] = "White",
                ["Width"] = "350",
                ["Height"] = "500",
            },
            contentProperty: "Children", 
            childrenProperty: "Children");

        var stackPanel = XamlItemControlFactory.CreateControl(stackPanelXamlItem);
            
        toolBoxViewModel.Add(stackPanel, stackPanelXamlItem);

        toolBoxViewModel.RootXamlItem = stackPanelXamlItem;

        RootPanel.Children.Add(stackPanel);
    }

    private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (e.Source is LightDismissOverlayLayer)
        {
            return;
        }

        var visuals = HitTest(this, e.GetPosition(null), OverlayView.HitTestMode, _ignored);

        OverlayView.Select(visuals.FirstOrDefault());
        Focus();
    }

    private void OnPointerMoved(object? sender, PointerEventArgs e)
    {
        var visuals = HitTest(this, e.GetPosition(null), OverlayView.HitTestMode, _ignored);

        OverlayView.Hover(visuals.FirstOrDefault());
    }

    private void OnPointerExited(object? sender, PointerEventArgs e)
    {
        OverlayView.Hover(null);
    }

    private void OnPointerCaptureLost(object? sender, PointerCaptureLostEventArgs e)
    {
        OverlayView.Hover(null);
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

        var toolBoxViewModel = DataContext as ToolBoxViewModel;
        if (toolBoxViewModel is null)
        {
            return Enumerable.Empty<Visual>();
        }

        var visuals = descendants
            .OfType<Control>()
            .Where(visual =>
            {
                if (!toolBoxViewModel.TryGetXamlItem(visual, out _))
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

