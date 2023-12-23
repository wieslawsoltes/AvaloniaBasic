using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace BoundsDemo;

public partial class MainWindow : Window
{
    public List<XamlItem> ToolBoxItems { get; set; }
    
    public MainWindow()
    {
        InitializeComponent();

        OverlayView.SelectedChanged += (_, _) =>
        {
            UpdatePropertiesEditor(OverlayView.Selected);
        };

        UpdatePropertiesEditor(null);

        ToolBoxItems = new List<XamlItem>
        {
            //
            new ("TextBlock", new Dictionary<string, object> { ["Text"] = "TextBlock" }),
            new ("Label", new Dictionary<string, object> { ["Content"] = "Label" }),
            new ("TextBox", new Dictionary<string, object> { ["Text"] = "TextBox" }),
            //
            new ("Button", new Dictionary<string, object> { ["Content"] = "Button" }),
            new ("CheckBox", new Dictionary<string, object> { ["Content"] = "CheckBox" }),
            new ("RadioButton", new Dictionary<string, object> { ["Content"] = "RadioButton" }),
            //
            new ("Border", new Dictionary<string, object>()),
            new ("Decorator", new Dictionary<string, object>
            {
                ["Child"] = new XamlItem("Button", new Dictionary<string, object> { ["Content"] = "Button" })
            }),
            //
            new ("Panel", new Dictionary<string, object>()),
            new ("StackPanel", new Dictionary<string, object>
            {
                ["Children"] = new List<XamlItem>
                {
                    new ("TextBlock", new Dictionary<string, object> { ["Text"] = "TextBlock" }),
                    new ("TextBox", new Dictionary<string, object> { ["Text"] = "TextBox" }),
                }
            }),
            new ("DockPanel", new Dictionary<string, object>()),
            new ("WrapPanel", new Dictionary<string, object>()),
            new ("Grid", new Dictionary<string, object>()),
            //
            new ("ItemsControl", new Dictionary<string, object>()),
            new ("ListBox", new Dictionary<string, object>()),
            new ("ListBoxItem", new Dictionary<string, object>()),
            new ("ComboBox", new Dictionary<string, object>()),
            new ("ComboBoxItem", new Dictionary<string, object>()),
            //
            new ("ProgressBar", new Dictionary<string, object>()),
            new ("Slider", new Dictionary<string, object>()),
            new ("DatePicker", new Dictionary<string, object>()),
            //
            new ("Rectangle", new Dictionary<string, object>()),
            new ("Ellipse", new Dictionary<string, object>()),
            new ("Line", new Dictionary<string, object>()),
            new ("Path", new Dictionary<string, object>()),
            //
            new ("Image", new Dictionary<string, object>()),
            new ("PathIcon", new Dictionary<string, object>()),
            //
            new ("ScrollViewer", new Dictionary<string, object>()),
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
