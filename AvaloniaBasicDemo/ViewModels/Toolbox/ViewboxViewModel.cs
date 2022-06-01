using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Behaviors;
using AvaloniaBasicDemo.Model;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class ViewboxViewModel : IDragItem
{
    public string? Name { get; init; }

    public string? Icon { get; init; }

    public Control CreatePreview()
    {
        var viewbox = new Viewbox();
        viewbox.Width = 100d;
        viewbox.Height = 100d;
        // TODO: Preview
        return viewbox;
    }

    public Control CreateControl()
    {
        var viewbox = new Viewbox();
        viewbox.Width = 100d;
        viewbox.Height = 100d;
        // TODO: Content
        DragSettings.SetIsDropArea(viewbox, true);
        DragSettings.SetSnapToGrid(viewbox, false);
        return viewbox;
    }

    public void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not Viewbox viewbox)
        {
            return;
        }

        // TODO: Preview
    }
}
