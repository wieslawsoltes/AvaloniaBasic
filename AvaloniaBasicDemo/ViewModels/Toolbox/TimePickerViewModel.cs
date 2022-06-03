using System;
using Avalonia.Controls;
using Avalonia.Media;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class TimePickerViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var timePicker = new TimePicker();
        timePicker.SelectedTime = DateTime.Now.TimeOfDay;
        timePicker.Background = Brushes.Black;
        return timePicker;
    }

    public override Control CreateControl()
    {
        var timePicker = new TimePicker();
        timePicker.SelectedTime = DateTime.Now.TimeOfDay;
        //timePicker.Background = Brushes.Blue;
        return timePicker;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not TimePicker timePicker)
        {
            return;
        }

        timePicker.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
