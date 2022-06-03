using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Behaviors;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class CarouselViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var carousel = new Carousel();
        carousel.Width = 100d;
        carousel.Height = 100d;
        carousel.Background = Brushes.Black;
        return carousel;
    }

    public override Control CreateControl()
    {
        var carousel = new Carousel();
        carousel.Width = 100d;
        carousel.Height = 100d;
        carousel.Background = Brushes.LightGray;
        DragSettings.SetIsDropArea(carousel, true);
        DragSettings.SetSnapToGrid(carousel, false);
        return carousel;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not Carousel carousel)
        {
            return;
        }

        carousel.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
