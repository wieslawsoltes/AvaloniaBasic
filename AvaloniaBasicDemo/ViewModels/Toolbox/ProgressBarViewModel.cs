using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Behaviors;
using AvaloniaBasicDemo.Model;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class ProgressBarViewModel : IDragItem
{
    public string? Name { get; init; }

    public string? Icon { get; init; }

    public Control CreatePreview()
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

    public Control CreateControl()
    {
        var progressBar = new ProgressBar();
        progressBar.Width = 200d;
        progressBar.Height = 30d;
        progressBar.Minimum = 0;
        progressBar.Maximum = 100;
        progressBar.Value = 50;
        progressBar.Background = Brushes.LightGray;
        DragSettings.SetIsDropArea(progressBar, true);
        DragSettings.SetSnapToGrid(progressBar, false);
        return progressBar;
    }

    public void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not ProgressBar progressBar)
        {
            return;
        }

        progressBar.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
