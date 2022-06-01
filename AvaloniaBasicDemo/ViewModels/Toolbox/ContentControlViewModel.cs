using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Behaviors;
using AvaloniaBasicDemo.Model;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class ContentControlViewModel : IDragItem
{
    public string? Name { get; init; }

    public string? Icon { get; init; }

    public Control CreatePreview()
    {
        var contentControl = new ContentControl();
        contentControl.Width = 100d;
        contentControl.Height = 100d;
        contentControl.Background = Brushes.Black;
        return contentControl;
    }

    public Control CreateControl()
    {
        var contentControl = new ContentControl();
        contentControl.Width = 100d;
        contentControl.Height = 100d;
        contentControl.Background = Brushes.LightGray;
        DragSettings.SetIsDropArea(contentControl, true);
        DragSettings.SetSnapToGrid(contentControl, false);
        return contentControl;
    }

    public void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not ContentControl contentControl)
        {
            return;
        }

        contentControl.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
