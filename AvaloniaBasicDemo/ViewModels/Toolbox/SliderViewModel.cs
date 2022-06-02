using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Behaviors;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class SliderViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var slider = new Slider();
        slider.Width = 100d;
        slider.Minimum = 0;
        slider.Maximum = 100;
        slider.Value = 50;
        slider.Background = Brushes.Black;
        return slider;
    }

    public override Control CreateControl()
    {
        var slider = new Slider();
        slider.Width = 100d;
        slider.Minimum = 0;
        slider.Maximum = 100;
        slider.Value = 50;
        slider.Background = Brushes.LightGray;
        DragSettings.SetIsDropArea(slider, true);
        DragSettings.SetSnapToGrid(slider, false);
        return slider;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not Slider slider)
        {
            return;
        }

        slider.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
