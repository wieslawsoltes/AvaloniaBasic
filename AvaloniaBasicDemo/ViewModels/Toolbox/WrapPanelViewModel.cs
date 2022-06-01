using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Behaviors;
using AvaloniaBasicDemo.Model;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class WrapPanelViewModel : IDragItem
{
    public string? Name { get; init; }

    public string? Icon { get; init; }

    public Control CreatePreview()
    {
        var wrapPanel = new WrapPanel();
        wrapPanel.Width = 100d;
        wrapPanel.Height = 100d;
        wrapPanel.Background = Brushes.Black;
        return wrapPanel;
    }

    public Control CreateControl()
    {
        var wrapPanel = new WrapPanel();
        wrapPanel.Width = 100d;
        wrapPanel.Height = 100d;
        wrapPanel.Background = Brushes.LightGray;
        DragSettings.SetIsDropArea(wrapPanel, true);
        DragSettings.SetSnapToGrid(wrapPanel, false);
        return wrapPanel;
    }

    public void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not WrapPanel wrapPanel)
        {
            return;
        }

        wrapPanel.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
