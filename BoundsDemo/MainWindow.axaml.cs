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

        OverlayView.SelectedChanged += (_, _) =>
        {
            UpdatePropertiesEditor(OverlayView.Selected);
        };

        UpdatePropertiesEditor(null);

        DataContext = new ToolBoxViewModel();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        Focus();
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);

        if (e.Source is TextBox)
        {
            return;
        }

        switch (e.Key)
        {
            case Key.Escape:
                OverlayView.Hover(null);
                OverlayView.Select(null);
                break;
            case Key.L:
                OverlayView.HitTestMode = HitTestMode.Logical;
                OverlayView.Hover(null);
                OverlayView.Select(null);
                break;
            case Key.V:
                OverlayView.HitTestMode = HitTestMode.Visual;
                OverlayView.Hover(null);
                OverlayView.Select(null);
                break;
            case Key.H:
                EditorCanvas.RootPanel.IsHitTestVisible = !EditorCanvas.RootPanel.IsHitTestVisible;
                break;
            case Key.R:
                EditorCanvas.ReverseOrder = !EditorCanvas.ReverseOrder;
                OverlayView.Hover(null);
                OverlayView.Select(null);
                break;
            case Key.Delete:
            case Key.Back:
            {
                if (OverlayView.Selected is Control control)
                {
                    OverlayView.Hover(null);
                    OverlayView.Select(null);

                    // TODO:
                    //EditorCanvas.EditablePanel.Children.Remove(control);

                    // TODO:
                    var toolBoxViewModel = DataContext as ToolBoxViewModel;
                    toolBoxViewModel.Remove(control);
                }
                break;
            }
        }
    }

    private void SelectedOnPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        OverlayView.InvalidateVisual();
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
