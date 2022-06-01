using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Behaviors;
using AvaloniaBasicDemo.Model;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class DockPanelViewModel : IDragItem
{
    public string? Name { get; init; }

    public string? Icon { get; init; }

    public Control CreatePreview()
    {
        var dockPanel = new DockPanel();
        dockPanel.Width = 100d;
        dockPanel.Height = 100d;
        dockPanel.Background = Brushes.Black;
        return dockPanel;
    }

    public Control CreateControl()
    {
        var dockPanel = new DockPanel();
        dockPanel.Width = 100d;
        dockPanel.Height = 100d;
        dockPanel.Background = Brushes.LightGray;
        DragSettings.SetIsDropArea(dockPanel, true);
        DragSettings.SetSnapToGrid(dockPanel, false);
        return dockPanel;
    }

    public void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not DockPanel dockPanel)
        {
            return;
        }

        dockPanel.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
