using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using AvaloniaBasicDemo.Behaviors;
using AvaloniaBasicDemo.Model;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class SeparatorViewModel : IDragItem
{
    public string? Name { get; init; }

    public string? Icon { get; init; }

    public Control CreatePreview()
    {
        var separator = new Separator();
        separator.Width = 100d;
        separator.Height = 1d;
        separator.Background = Brushes.Black;
        return separator;
    }

    public Control CreateControl()
    {
        var separator = new Separator();
        separator.Width = 100d;
        separator.Height = 1d;
        separator.Background = Brushes.LightGray;
        DragSettings.SetIsDropArea(separator, true);
        DragSettings.SetSnapToGrid(separator, false);
        return separator;
    }

    public void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not Separator separator)
        {
            return;
        }

        separator.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
