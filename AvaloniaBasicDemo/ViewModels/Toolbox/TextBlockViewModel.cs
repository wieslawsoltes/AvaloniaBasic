using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Model;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class TextBlockViewModel : IDragItem
{
    public string? Name { get; init; }

    public string? Icon { get; init; }

    public Control CreatePreview()
    {
        var textBlock = new TextBlock();
        textBlock.Text = "Text";
        textBlock.Foreground = Brushes.Black;
        return textBlock;
    }

    public Control CreateControl()
    {
        var textBlock = new TextBlock();
        textBlock.Text = "Text";
        textBlock.Foreground = Brushes.Blue;
        return textBlock;
    }

    public void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not TextBlock textBlock)
        {
            return;
        }

        textBlock.Foreground = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
