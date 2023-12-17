using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.VisualTree;

namespace BoundsDemo;

public partial class MainWindow : Window
{
    private readonly HashSet<Visual> _ignored;
    private bool _reverseOrder = true;

    public MainWindow()
    {
        InitializeComponent();

        _ignored = new HashSet<Visual>(new Visual[] {OverlayControl});

        UpdatePropertiesEditor(null);
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        Focus();
    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);

        var visuals = HitTest(this, e.GetPosition(this), OverlayControl.HitTestMode, _ignored);
        OverlayControl.Selected = visuals.FirstOrDefault();
        OverlayControl.InvalidateVisual();

        UpdatePropertiesEditor(OverlayControl.Selected);
    }

    protected override void OnPointerMoved(PointerEventArgs e)
    {
        base.OnPointerMoved(e);

        var visuals = HitTest(this, e.GetPosition(this), OverlayControl.HitTestMode, _ignored);
        OverlayControl.Hover = visuals.FirstOrDefault();
        OverlayControl.InvalidateVisual();
    }

    protected override void OnPointerExited(PointerEventArgs e)
    {
        base.OnPointerExited(e);
        
        OverlayControl.Hover = null;
        OverlayControl.InvalidateVisual();
    }

    protected override void OnPointerCaptureLost(PointerCaptureLostEventArgs e)
    {
        base.OnPointerCaptureLost(e);
        
        OverlayControl.Hover = null;
        OverlayControl.InvalidateVisual();
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);

        switch (e.Key)
        {
            case Key.Escape:
                OverlayControl.Hover = null;
                OverlayControl.Selected = null;
                OverlayControl.InvalidateVisual();
                UpdatePropertiesEditor(null);
                break;
            case Key.L:
                OverlayControl.HitTestMode = HitTestMode.Logical;
                OverlayControl.Hover = null;
                OverlayControl.Selected = null;
                OverlayControl.InvalidateVisual();
                UpdatePropertiesEditor(null);
                break;
            case Key.V:
                OverlayControl.HitTestMode = HitTestMode.Visual;
                OverlayControl.Hover = null;
                OverlayControl.Selected = null;
                OverlayControl.InvalidateVisual();
                UpdatePropertiesEditor(null);
                break;
            case Key.H:
                EditablePanel.IsHitTestVisible = !EditablePanel.IsHitTestVisible;
                break;
            case Key.R:
                _reverseOrder = !_reverseOrder;
                OverlayControl.Hover = null;
                OverlayControl.Selected = null;
                OverlayControl.InvalidateVisual();
                UpdatePropertiesEditor(null);
                break;
        }
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

        return _reverseOrder 
            ? visuals.Reverse() 
            : visuals;
    }

    private void UpdatePropertiesEditor(Visual? selected)
    {
        PropertiesEditor.Selected = selected;
        PropertiesEditor.UpdatePropertiesEditor();
    }
}
