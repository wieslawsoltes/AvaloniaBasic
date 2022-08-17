using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasic.Behaviors;

namespace AvaloniaBasic.ViewModels.Toolbox;

public class PanelViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var panel = new Panel();
        panel.Width = 100d;
        panel.Height = 100d;
        panel.Background = Brushes.Black;
        return panel;
    }

    public override Control CreateControl()
    {
        var panel = new Panel();
        panel.Width = 100d;
        panel.Height = 100d;
        panel.Background = Brushes.LightGray;
        DragSettings.SetIsDropArea(panel, true);
        DragSettings.SetSnapToGrid(panel, false);
        return panel;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not Panel panel)
        {
            return;
        }

        panel.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
