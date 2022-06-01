using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Behaviors;
using AvaloniaBasicDemo.Model;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class ExpanderViewModel : IDragItem
{
    public string? Name { get; init; }

    public string? Icon { get; init; }

    public Control CreatePreview()
    {
        var expander = new Expander();
        expander.Width = 100d;
        expander.Height = 100d;
        expander.Background = Brushes.Black;
        return expander;
    }

    public Control CreateControl()
    {
        var expander = new Expander();
        expander.Width = 100d;
        expander.Height = 100d;
        expander.Background = Brushes.LightGray;
        DragSettings.SetIsDropArea(expander, true);
        DragSettings.SetSnapToGrid(expander, false);
        return expander;
    }

    public void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not Expander expander)
        {
            return;
        }

        expander.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
