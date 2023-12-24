using System.Collections.Generic;

namespace BoundsDemo;

public class ToolBoxViewModel
{
    public ToolBoxViewModel()
    {
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
            new ("ListBox", new Dictionary<string, object>
            {
                ["Items"] = new List<XamlItem>
                {
                    new ("ListBoxItem", new Dictionary<string, object> { ["Content"] = "ListBoxItem 0" }),
                    new ("ListBoxItem", new Dictionary<string, object> { ["Content"] = "ListBoxItem 1" }),
                }
            }),
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
    }

    public List<XamlItem> ToolBoxItems { get; set; }
}
