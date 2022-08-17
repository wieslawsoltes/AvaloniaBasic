using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasic.Behaviors;

namespace AvaloniaBasic.ViewModels.Toolbox;

public class RelativePanelViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var relativePanel = new RelativePanel();
        relativePanel.Width = 100d;
        relativePanel.Height = 100d;
        relativePanel.Background = Brushes.Black;
        return relativePanel;
    }

    public override Control CreateControl()
    {
        var relativePanel = new RelativePanel();
        relativePanel.Width = 100d;
        relativePanel.Height = 100d;
        relativePanel.Background = Brushes.LightGray;
        DragSettings.SetIsDropArea(relativePanel, true);
        DragSettings.SetSnapToGrid(relativePanel, false);
        return relativePanel;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not RelativePanel relativePanel)
        {
            return;
        }

        relativePanel.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
