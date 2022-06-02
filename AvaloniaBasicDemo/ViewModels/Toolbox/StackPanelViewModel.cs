using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Behaviors;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class StackPanelViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var stackPanel = new StackPanel();
        stackPanel.Width = 100d;
        stackPanel.Height = 100d;
        stackPanel.Background = Brushes.Black;
        return stackPanel;
    }

    public override Control CreateControl()
    {
        var stackPanel = new StackPanel();
        stackPanel.Width = 100d;
        stackPanel.Height = 100d;
        stackPanel.Background = Brushes.LightGray;
        DragSettings.SetIsDropArea(stackPanel, true);
        DragSettings.SetSnapToGrid(stackPanel, false);
        return stackPanel;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not StackPanel stackPanel)
        {
            return;
        }

        stackPanel.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
