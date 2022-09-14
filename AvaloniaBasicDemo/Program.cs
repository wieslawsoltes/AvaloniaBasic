using System;
using Avalonia;

namespace AvaloniaBasicDemo;

class Program
{
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .With(new Win32PlatformOptions
            {
                UseCompositor = true,
                UseDeferredRendering = true,
                AllowEglInitialization = true,
                UseWindowsUIComposition = true
            })
            .With(new X11PlatformOptions
            {
                UseCompositor = true,
                UseDeferredRendering = true,
                UseGpu = true
            })
            .With(new AvaloniaNativePlatformOptions
            {
                UseCompositor = true,
                UseDeferredRendering = true,
                UseGpu = true
            })
            .LogToTrace();
}
