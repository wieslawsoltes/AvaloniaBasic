using Avalonia.Controls;
using Avalonia.Media;

namespace AvaloniaBasic.ViewModels.Toolbox;

public class RadioButtonViewModel : ToolBoxItemViewModel
{
    public RadioButtonViewModel()
    {
        Name = "RadioButton";
        Group = "Buttons";
    }

    public override Control CreatePreview()
    {
        var radioButton = new RadioButton();
        radioButton.Content = "RadioButton";
        radioButton.Foreground = Brushes.Black;
        return radioButton;
    }

    public override Control CreateControl()
    {
        var radioButton = new RadioButton();
        radioButton.Content = "RadioButton";
        //radioButton.Foreground = Brushes.Blue;
        // TODO: Support setting Content
        return radioButton;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not RadioButton radioButton)
        {
            return;
        }

        radioButton.Foreground = isPointerOver ? Brushes.Green : Brushes.Red;
    }

    public override bool IsDropArea() => true;
}
