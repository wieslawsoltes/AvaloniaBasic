using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Model;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class StackPanelViewModel : IDragItem
{
    public string? Name { get; init; }

    public string? Icon { get; init; }

    public Control CreatePreview()
    {
        var stackPanel = new StackPanel();
        stackPanel.Width = 100d;
        stackPanel.Height = 100d;
        stackPanel.Background = Brushes.Black;
        return stackPanel;
    }

    public Control CreateControl()
    {
        var stackPanel = new StackPanel();
        stackPanel.Width = 100d;
        stackPanel.Height = 100d;
        stackPanel.Background = Brushes.Blue;
        return stackPanel;
    }

    public void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not StackPanel stackPanel)
        {
            return;
        }

        stackPanel.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
