using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace BoundsDemo;

public partial class MainWindow : Window
{
    public List<ToolBoxItem> ToolBoxItems { get; set; }
    
    public MainWindow()
    {
        InitializeComponent();

        OverlayView.SelectedChanged += (_, _) =>
        {
            UpdatePropertiesEditor(OverlayView.Selected);
        };

        UpdatePropertiesEditor(null);

        ToolBoxItems = new List<ToolBoxItem>
        {
            //
            new ("TextBlock", new Dictionary<string, string> { ["Text"] = "TextBlock" }),
            new ("Label", new Dictionary<string, string> { ["Content"] = "Label" }),
            new ("TextBox", new Dictionary<string, string> { ["Text"] = "TextBox" }),
            //
            new ("Button", new Dictionary<string, string> { ["Content"] = "Button" }),
            new ("CheckBox", new Dictionary<string, string> { ["Content"] = "CheckBox" }),
            new ("RadioButton", new Dictionary<string, string> { ["Content"] = "RadioButton" }),
            //
            new ("Border", new Dictionary<string, string>()),
            new ("Decorator", new Dictionary<string, string>()),
            //
            new ("Panel", new Dictionary<string, string>()),
            new ("StackPanel", new Dictionary<string, string>()),
            new ("DockPanel", new Dictionary<string, string>()),
            new ("WrapPanel", new Dictionary<string, string>()),
            new ("Grid", new Dictionary<string, string>()),
            //
            new ("ItemsControl", new Dictionary<string, string>()),
            new ("ListBox", new Dictionary<string, string>()),
            new ("ListBoxItem", new Dictionary<string, string>()),
            new ("ComboBox", new Dictionary<string, string>()),
            new ("ComboBoxItem", new Dictionary<string, string>()),
            //
            new ("ProgressBar", new Dictionary<string, string>()),
            new ("Slider", new Dictionary<string, string>()),
            new ("DatePicker", new Dictionary<string, string>()),
            //
            new ("Rectangle", new Dictionary<string, string>()),
            new ("Ellipse", new Dictionary<string, string>()),
            new ("Line", new Dictionary<string, string>()),
            new ("Path", new Dictionary<string, string>()),
            //
            new ("Image", new Dictionary<string, string>()),
            new ("PathIcon", new Dictionary<string, string>()),
            //
            new ("ScrollViewer", new Dictionary<string, string>()),
        };

        DataContext = this;
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
                EditorCanvas.EditablePanel.IsHitTestVisible = !EditorCanvas.EditablePanel.IsHitTestVisible;
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
                    EditorCanvas.EditablePanel.Children.Remove(control);
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
