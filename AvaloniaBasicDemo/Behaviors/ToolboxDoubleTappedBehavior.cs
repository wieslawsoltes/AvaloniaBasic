using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaBasicDemo.Model;
using AvaloniaBasicDemo.ViewModels;

namespace AvaloniaBasicDemo.Behaviors;

public class ToolboxDoubleTappedBehavior : DoubleTappedBehavior<Control>
{
    public static readonly StyledProperty<Canvas?> TargetCanvasProperty = 
        AvaloniaProperty.Register<ToolboxDragBehavior, Canvas?>(nameof(TargetCanvas));

    [ResolveByName]
    public Canvas? TargetCanvas
    {
        get => GetValue(TargetCanvasProperty);
        set => SetValue(TargetCanvasProperty, value);
    }

    protected override void OnDoubleTapped(object? sender, RoutedEventArgs e)
    {
        if (TargetCanvas is null)
        {
            return;
        }

        if (AssociatedObject?.DataContext is IDragItem item)
        {
            var control = item.CreateControl();

            ToolboxDragBehavior.AddControl(control, TargetCanvas, new Point(0d, 0d));

            if (TargetCanvas?.DataContext is MainViewModel mainViewModel)
            {
                mainViewModel.Tree.UpdateLogicalTree(TargetCanvas);
            }
        }
    }
}
