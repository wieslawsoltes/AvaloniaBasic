using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Layout;
using Avalonia.Media;

namespace ResizingControlDemo.Controls;

public class ControlFactory
{
    public static Control? CreateControl(string typeName)
    {
        switch (typeName)
        {
            case "Border":
            {
                return new Border
                {
                    Background = Brushes.LightGray,
                    Width = 128,
                    Height = 128,
                };
            }
            case "Button":
            {
                return new Button
                {
                    Content = "Button",
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Width = 128,
                    Height = 32,
                };
            }
            case "Calendar":
            {
                return new Calendar
                {
                    Width = 128,
                    Height = 128,
                };
            }
            case "Canvas":
            {
                return new Canvas
                {
                    Background = Brushes.LightGray,
                    Width = 128,
                    Height = 128,
                };
            }
            case "CheckBox":
            {
                return new CheckBox
                {
                    Content = "CheckBox",
                    VerticalAlignment = VerticalAlignment.Center,
                    Width = 128,
                    Height = 32,
                };
            }
            case "ComboBox":
            {
                return new ComboBox
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    Width = 128,
                    Height = 32,
                };
            }
            case "ContentControl":
            {
                return new ContentControl
                {
                    Content = "ContentControl",
                    Background = Brushes.LightGray,
                    Width = 128,
                    Height = 128,
                };
            }
            case "DataGrid":
            {
                return new DataGrid
                {
                    Background = Brushes.LightGray,
                    Width = 128,
                    Height = 128,
                };
            } 
            case "DatePicker":
            {
                return new DatePicker
                {
                    Width = 296,
                    Height = 24,
                };
            }
            case "DockPanel":
            {
                return new DockPanel
                {
                    Background = Brushes.LightGray,
                    Width = 128,
                    Height = 160,
                };
            }
            case "Ellipse":
            {
                return new Ellipse
                {
                    Fill = Brushes.Gray,
                    Width = 48,
                    Height = 48,
                };
            } 
            case "Expander":
            {
                return new Expander
                {
                    Content = "Expander",
                    Background = Brushes.LightGray,
                    Width = 128,
                    Height = 32,
                };
            }
            case "Grid":
            {
                return new Grid
                {
                    Background = Brushes.LightGray,
                    Width = 128,
                    Height = 160,
                };
            }
            case "GridSplitter":
            {
                return new GridSplitter
                {
                    Background = Brushes.LightGray,
                    Width = 128,
                    Height = 8,
                };
            }
            case "GroupBox":
            {
                return new GroupBox.Avalonia.GroupBox
                {
                    Header = "GroupBox",
                    Width = 128,
                    Height = 96
                };
            }  
            case "Image":
            {
                return new Image
                {
                    Width = 48,
                    Height = 48,
                };
            }
            case "Label":
            {
                return new Label
                {
                    Content = "Label",
                    HorizontalContentAlignment = HorizontalAlignment.Left,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Width = 128,
                    Height = 32,
                };
            }
            case "ListBox":
            {
                return new ListBox
                {
                    Background = Brushes.LightGray,
                    Width = 128,
                    Height = 160,
                };
            }  
            case "Menu":
            {
                return new Menu
                {
                    Background = Brushes.LightGray,
                    Width = 128,
                    Height = 24,
                };
            }   
            case "ProgressBar":
            {
                return new ProgressBar
                {
                    Width = 128,
                    Height = 24,
                };
            }
            case "RadioButton":
            {
                return new RadioButton
                {
                    Content = "RadioButton",
                    VerticalAlignment = VerticalAlignment.Center,
                    Width = 128,
                    Height = 32,
                };
            }
            case "Rectangle":
            {
                return new Rectangle
                {
                    Fill = Brushes.Gray,
                    Width = 48,
                    Height = 48,
                };
            }
            case "ScrollBar":
            {
                return new ScrollBar
                {
                    Width = 24,
                    Height = 128,
                    AllowAutoHide = false,
                };
            }
            case "ScrollViewer":
            {
                return new ScrollViewer
                {
                    Background = Brushes.LightGray,
                    Width = 128,
                    Height = 160,
                };
            } 
            case "Separator":
            {
                return new Separator
                {
                    Background = Brushes.LightGray,
                    Width = 128,
                    Height = 1,
                };
            }
            case "Slider":
            {
                return new Slider
                {
                    Width = 128,
                    Height = 48,
                };
            }
            case "StackPanel":
            {
                return new StackPanel
                {
                    Background = Brushes.LightGray,
                    Width = 128,
                    Height = 160,
                };
            }
            case "TabControl":
            {
                return new TabControl
                {
                    Background = Brushes.LightGray,
                    Width = 128,
                    Height = 160,
                };
            }
            case "TextBlock":
            {
                return new TextBlock
                {
                    Text = "TextBlock",
                    Width = 128,
                    Height = 16,
                };
            }
            case "TextBox":
            {
                return new TextBox
                {
                    Text = "TextBox",
                    Width = 128,
                    Height = 32,
                };
            }
            case "TreeView":
            {
                return new TreeView
                {
                    Background = Brushes.LightGray,
                    Width = 128,
                    Height = 160,
                };
            }
            case "Viewbox":
            {
                return new Viewbox
                {
                    Width = 128,
                    Height = 128,
                };
            }
            case "WrapPanel":
            {
                return new WrapPanel
                {
                    Background = Brushes.LightGray,
                    Width = 128,
                    Height = 160,
                };
            }
        }

        return null;
    }
}
