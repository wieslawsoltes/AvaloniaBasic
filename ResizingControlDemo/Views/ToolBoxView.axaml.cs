using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace ResizingControlDemo;

public partial class ToolBoxView : UserControl
{
    public static readonly StyledProperty<Canvas?> EditorCanvasProperty = 
        AvaloniaProperty.Register<ToolBoxView, Canvas?>(nameof(EditorCanvas));

    [ResolveByName]
    public Canvas? EditorCanvas
    {
        get => GetValue(EditorCanvasProperty);
        set => SetValue(EditorCanvasProperty, value);
    }

    public ToolBoxView()
    {
        InitializeComponent();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);

        var toolboxControls = new List<string>
        {
            // "Pointer",
            "Border",
            "Button",
            "Calendar",
            "Canvas",
            "CheckBox",
            "ComboBox",
            "ContentControl",
            "DataGrid",
            "DatePicker",
            "DockPanel",
            // "DocumentViewer",
            "Ellipse",
            "Expander",
            // "Frame",
            "Grid",
            "GridSplitter",
            "GroupBox",
            "Image",
            "Label",
            "ListBox",
            // "ListView",
            // "MediaElement",
            "Menu",
            // "PasswordBox",
            "ProgressBar",
            "RadioButton",
            "Rectangle",
            // "RichTextBox",
            "ScrollBar",
            "ScrollViewer",
            "Separator",
            "Slider",
            "StackPanel",
            // "StatusBar",
            "TabControl",
            "TextBlock",
            "TextBox",
            // "ToolBar",
            // "ToolBarPanel",
            // "ToolBarTray",
            "TreeView",
            "Viewbox",
            // "WebBrowser",
            // "WindowsFormsHost",
            "WrapPanel"
        };

        ControlsToolBox.ItemsSource = toolboxControls;
    }
}
