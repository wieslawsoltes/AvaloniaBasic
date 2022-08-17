using Avalonia.Controls;
using AvaloniaBasic.Behaviors;

namespace AvaloniaBasic.ViewModels.Toolbox;

public class LayoutTransformControlViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var layoutTransformControl = new LayoutTransformControl();
        layoutTransformControl.Width = 100d;
        layoutTransformControl.Height = 100d;
        // TODO: Preview
        return layoutTransformControl;
    }

    public override Control CreateControl()
    {
        var layoutTransformControl = new LayoutTransformControl();
        layoutTransformControl.Width = 100d;
        layoutTransformControl.Height = 100d;
        // TODO: Content
        DragSettings.SetIsDropArea(layoutTransformControl, true);
        DragSettings.SetSnapToGrid(layoutTransformControl, false);
        return layoutTransformControl;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not LayoutTransformControl layoutTransformControl)
        {
            return;
        }

        // TODO: Preview
    }
}
