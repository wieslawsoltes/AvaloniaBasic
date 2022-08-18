using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaBasic.Behaviors;
using AvaloniaBasic.Model;
using AvaloniaBasic.ViewModels;

namespace AvaloniaBasic.Editors;

public class DoubleTappedEditor
{
    public Interactive? AssociatedObject { get; set; }

    public Canvas? TargetCanvas { get; set; }

    public void OnDoubleTapped(RoutedEventArgs e)
    {
        if (TargetCanvas is { })
        {
            AddControl(TargetCanvas, new Point(0d, 0d));
        }
    }

    private void AddControl(Control target, Point point)
    {
        if (AssociatedObject?.DataContext is not IDragItem item)
        {
            return;
        }

        var control = item.CreateControl();

        if (item.IsDropArea())
        {
            DragSettings.SetIsDropArea(control, true);
            DragSettings.SetSnapToGrid(control, false);
        }

        ControlEditor.AddControl(control, target, point);

        if (TargetCanvas?.DataContext is MainViewModel mainViewModel)
        {
            mainViewModel.Tree.UpdateLogicalTree(TargetCanvas);
        }
    }
}
