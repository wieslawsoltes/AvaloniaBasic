using Avalonia.Controls;

namespace AvaloniaBasic.ViewModels.Toolbox;

public class TransitioningContentControlViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var transitioningContentControl = new TransitioningContentControl();
        transitioningContentControl.Width = 100d;
        transitioningContentControl.Height = 100d;
        // TODO: Preview
        return transitioningContentControl;
    }

    public override Control CreateControl()
    {
        var transitioningContentControl = new TransitioningContentControl();
        transitioningContentControl.Width = 100d;
        transitioningContentControl.Height = 100d;
        // TODO: Content
        return transitioningContentControl;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not TransitioningContentControl transitioningContentControl)
        {
            return;
        }

        // TODO: Preview
    }

    public override bool IsDropArea() => true;
}
