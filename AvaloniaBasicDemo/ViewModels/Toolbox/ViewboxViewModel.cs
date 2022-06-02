using Avalonia.Controls;
using AvaloniaBasicDemo.Behaviors;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class ViewboxViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var viewbox = new Viewbox();
        viewbox.Width = 100d;
        viewbox.Height = 100d;
        // TODO: Preview
        return viewbox;
    }

    public override Control CreateControl()
    {
        var viewbox = new Viewbox();
        viewbox.Width = 100d;
        viewbox.Height = 100d;
        // TODO: Content
        DragSettings.SetIsDropArea(viewbox, true);
        DragSettings.SetSnapToGrid(viewbox, false);
        return viewbox;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not Viewbox viewbox)
        {
            return;
        }

        // TODO: Preview
    }
}
