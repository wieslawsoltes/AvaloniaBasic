using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.LogicalTree;
using Avalonia.Media;
using Avalonia.VisualTree;
using ContentControl = Avalonia.Controls.ContentControl;

namespace BoundsDemo;

public partial class MainWindow : Window
{
    private readonly HashSet<Visual> _ignored;

    public MainWindow()
    {
        InitializeComponent();
        _ignored = new HashSet<Visual>(new Visual[] {OverlayControl});
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
                break;
            case Key.L:
                OverlayControl.HitTestMode = HitTestMode.Logical;
                OverlayControl.Hover = null;
                OverlayControl.InvalidateVisual();
                break;
            case Key.V:
                OverlayControl.HitTestMode = HitTestMode.Visual;
                OverlayControl.Hover = null;
                OverlayControl.InvalidateVisual();
                break;
            case Key.H:
                EditablePanel.IsHitTestVisible = !EditablePanel.IsHitTestVisible;
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
            })
            .Reverse();

        return visuals;
    }

    private void ButtonAlignHorizontalLeft_OnClick(object? sender, RoutedEventArgs e)
    {
        if (OverlayControl.Selected is Layoutable layoutable)
        {
            layoutable.HorizontalAlignment = HorizontalAlignment.Left;
        }
    }

    private void ButtonAlignHorizontalCenter_OnClick(object? sender, RoutedEventArgs e)
    {
        if (OverlayControl.Selected is Layoutable layoutable)
        {
            layoutable.HorizontalAlignment = HorizontalAlignment.Center;
        }
    }

    private void ButtonAlignHorizontalRight_OnClick(object? sender, RoutedEventArgs e)
    {
        if (OverlayControl.Selected is Layoutable layoutable)
        {
            layoutable.HorizontalAlignment = HorizontalAlignment.Right;
        }
    }

    private void ButtonAlignHorizontalStretch_OnClick(object? sender, RoutedEventArgs e)
    {
        if (OverlayControl.Selected is Layoutable layoutable)
        {
            layoutable.HorizontalAlignment = HorizontalAlignment.Stretch;
        }
    }

    private void ButtonAlignVerticalTop_OnClick(object? sender, RoutedEventArgs e)
    {
        if (OverlayControl.Selected is Layoutable layoutable)
        {
            layoutable.VerticalAlignment = VerticalAlignment.Top;
        }
    }

    private void ButtonAlignVerticalCenter_OnClick(object? sender, RoutedEventArgs e)
    {
        if (OverlayControl.Selected is Layoutable layoutable)
        {
            layoutable.VerticalAlignment = VerticalAlignment.Center;
        }
    }

    private void ButtonAlignVerticalBottom_OnClick(object? sender, RoutedEventArgs e)
    {
        if (OverlayControl.Selected is Layoutable layoutable)
        {
            layoutable.VerticalAlignment = VerticalAlignment.Bottom;
        }
    }

    private void ButtonAlignVerticalStretch_OnClick(object? sender, RoutedEventArgs e)
    {
        if (OverlayControl.Selected is Layoutable layoutable)
        {
            layoutable.VerticalAlignment = VerticalAlignment.Stretch;
        }
    }

    private void ButtonTextAlignLeft_OnClick(object? sender, RoutedEventArgs e)
    {
        switch (OverlayControl.Selected)
        {
            case TextBlock textBlock:
                textBlock.TextAlignment = TextAlignment.Left;
                break;
            case TextBox textBox:
                textBox.TextAlignment = TextAlignment.Left;
                break;
        }
    }

    private void ButtonTextAlignCenter_OnClick(object? sender, RoutedEventArgs e)
    {
        switch (OverlayControl.Selected)
        {
            case TextBlock textBlock:
                textBlock.TextAlignment = TextAlignment.Center;
                break;
            case TextBox textBox:
                textBox.TextAlignment = TextAlignment.Center;
                break;
        }
    }

    private void ButtonTextAlignRight_OnClick(object? sender, RoutedEventArgs e)
    {
        switch (OverlayControl.Selected)
        {
            case TextBlock textBlock:
                textBlock.TextAlignment = TextAlignment.Right;
                break;
            case TextBox textBox:
                textBox.TextAlignment = TextAlignment.Right;
                break;
        }
    }

    private void ButtonTextAlignJustified_OnClick(object? sender, RoutedEventArgs e)
    {
        switch (OverlayControl.Selected)
        {
            case TextBlock textBlock:
                textBlock.TextAlignment = TextAlignment.Justify;
                break;
            case TextBox textBox:
                textBox.TextAlignment = TextAlignment.Justify;
                break;
        }
    }

    private void ButtonAlignContentHorizontalLeft_OnClick(object? sender, RoutedEventArgs e)
    {
        if (OverlayControl.Selected is ContentControl contentControl)
        {
            contentControl.HorizontalContentAlignment = HorizontalAlignment.Left;
        }
    }

    private void ButtonAlignContentHorizontalCenter_OnClick(object? sender, RoutedEventArgs e)
    {
        if (OverlayControl.Selected is ContentControl contentControl)
        {
            contentControl.HorizontalContentAlignment = HorizontalAlignment.Center;
        }
    }

    private void ButtonAlignContentHorizontalRight_OnClick(object? sender, RoutedEventArgs e)
    {
        if (OverlayControl.Selected is ContentControl contentControl)
        {
            contentControl.HorizontalContentAlignment = HorizontalAlignment.Right;
        }
    }

    private void ButtonAlignContentHorizontalStretch_OnClick(object? sender, RoutedEventArgs e)
    {
        if (OverlayControl.Selected is ContentControl contentControl)
        {
            contentControl.HorizontalContentAlignment = HorizontalAlignment.Stretch;
        }
    }

    private void ButtonAlignContentVerticalTop_OnClick(object? sender, RoutedEventArgs e)
    {
        if (OverlayControl.Selected is ContentControl contentControl)
        {
            contentControl.VerticalContentAlignment = VerticalAlignment.Top;
        }
    }

    private void ButtonAlignContentVerticalCenter_OnClick(object? sender, RoutedEventArgs e)
    {
        if (OverlayControl.Selected is ContentControl contentControl)
        {
            contentControl.VerticalContentAlignment = VerticalAlignment.Center;
        }
    }

    private void ButtonAlignContentVerticalBottom_OnClick(object? sender, RoutedEventArgs e)
    {
        if (OverlayControl.Selected is ContentControl contentControl)
        {
            contentControl.VerticalContentAlignment = VerticalAlignment.Bottom;
        }
    }

    private void ButtonAlignContentVerticalStretch_OnClick(object? sender, RoutedEventArgs e)
    {
        if (OverlayControl.Selected is ContentControl contentControl)
        {
            contentControl.VerticalContentAlignment = VerticalAlignment.Stretch;
        }
    }
}
