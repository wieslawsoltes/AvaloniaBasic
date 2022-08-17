using Avalonia.Controls;
using Avalonia.Media;

namespace AvaloniaBasic.ViewModels.Toolbox;

public class ProgressBarViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var progressBar = new ProgressBar();
        progressBar.Width = 200d;
        progressBar.Height = 30d;
        progressBar.Minimum = 0;
        progressBar.Maximum = 100;
        progressBar.Value = 50;
        progressBar.Background = Brushes.Black;
        return progressBar;
    }

    public override Control CreateControl()
    {
        var progressBar = new ProgressBar();
        progressBar.Width = 200d;
        progressBar.Height = 30d;
        progressBar.Minimum = 0;
        progressBar.Maximum = 100;
        progressBar.Value = 50;
        progressBar.Background = Brushes.LightGray;
        return progressBar;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not ProgressBar progressBar)
        {
            return;
        }

        progressBar.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }

    public override bool IsDropArea() => true;
}
