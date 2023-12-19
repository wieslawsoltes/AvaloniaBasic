using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace BoundsDemo;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        OverlayControl.SelectedChanged += (_, _) =>
        {
            UpdatePropertiesEditor(OverlayControl.Selected);
        };

        UpdatePropertiesEditor(null);
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        Focus();
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);

        switch (e.Key)
        {
            case Key.Escape:
                OverlayControl.Hover(null);
                OverlayControl.Select(null);
                break;
            case Key.L:
                OverlayControl.HitTestMode = HitTestMode.Logical;
                OverlayControl.Hover(null);
                OverlayControl.Select(null);
                break;
            case Key.V:
                OverlayControl.HitTestMode = HitTestMode.Visual;
                OverlayControl.Hover(null);
                OverlayControl.Select(null);
                break;
            case Key.H:
                EditorCanvas.EditablePanel.IsHitTestVisible = !EditorCanvas.EditablePanel.IsHitTestVisible;
                break;
            case Key.R:
                EditorCanvas.ReverseOrder = !EditorCanvas.ReverseOrder;
                OverlayControl.Hover(null);
                OverlayControl.Select(null);
                break;
        }
    }

    private void SelectedOnPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        OverlayControl.InvalidateVisual();
    }

    private void UpdatePropertiesEditor(Visual? selected)
    {
        if (PropertiesEditor.Selected is not null)
        {
            PropertiesEditor.Selected.PropertyChanged -= SelectedOnPropertyChanged;
        }

        if (selected is not null)
        {
            selected.PropertyChanged += SelectedOnPropertyChanged;
        }

        PropertiesEditor.Selected = selected;
        PropertiesEditor.UpdatePropertiesEditor();
    }
}
