using System.IO;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Skia.Helpers;
using Avalonia.Threading;
using SkiaSharp;

namespace BoundsDemo;

public class RenderingService
{
    public static async Task Render(Size size, Control control, SKCanvas canvas)
    {
        await Dispatcher.UIThread.InvokeAsync(async () =>
        {
            var window = new Window
            {
                Width = size.Width, 
                Height = size.Height, 
                ShowInTaskbar = false,
                Content = control
            };

            window.Measure(size);
            window.Arrange(new Rect(new Point(), size));
            window.UpdateLayout();

            await DrawingContextHelper.RenderAsync(canvas, control);
        });
    }

    public static async Task RenderAsSvg(Stream stream, Size size, Control control)
    {
        using var managedWStream = new SKManagedWStream(stream);
        var bounds = SKRect.Create(new SKSize((float)size.Width, (float)size.Height));
        using var canvas = SKSvgCanvas.Create(bounds, managedWStream);
        await Render(size, control, canvas);
    }
}
