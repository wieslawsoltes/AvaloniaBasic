using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasic.Behaviors;

namespace AvaloniaBasic.ViewModels.Toolbox;

public class ExpanderViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var expander = new Expander();
        expander.Width = 100d;
        expander.Height = 100d;
        expander.Background = Brushes.Black;
        return expander;
    }

    public override Control CreateControl()
    {
        var expander = new Expander();
        expander.Width = 100d;
        expander.Height = 100d;
        expander.Background = Brushes.LightGray;
        DragSettings.SetIsDropArea(expander, true);
        DragSettings.SetSnapToGrid(expander, false);
        return expander;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not Expander expander)
        {
            return;
        }

        expander.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
