using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasic.Behaviors;

namespace AvaloniaBasic.ViewModels.Toolbox;

public class WrapPanelViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var wrapPanel = new WrapPanel();
        wrapPanel.Width = 100d;
        wrapPanel.Height = 100d;
        wrapPanel.Background = Brushes.Black;
        return wrapPanel;
    }

    public override Control CreateControl()
    {
        var wrapPanel = new WrapPanel();
        wrapPanel.Width = 100d;
        wrapPanel.Height = 100d;
        wrapPanel.Background = Brushes.LightGray;
        DragSettings.SetIsDropArea(wrapPanel, true);
        DragSettings.SetSnapToGrid(wrapPanel, false);
        return wrapPanel;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not WrapPanel wrapPanel)
        {
            return;
        }

        wrapPanel.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
