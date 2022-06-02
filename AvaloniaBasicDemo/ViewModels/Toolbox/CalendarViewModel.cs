using System;
using Avalonia.Controls;
using Avalonia.Media;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class CalendarViewModel : DragItemViewModel
{
    public override Control CreatePreview()
    {
        var calendar = new Calendar();
        calendar.SelectedDate = DateTime.Now;
        calendar.Background = Brushes.Black;
        return calendar;
    }

    public override Control CreateControl()
    {
        var calendar = new Calendar();
        calendar.SelectedDate = DateTime.Now;
        //textBox.Background = Brushes.Blue;
        return calendar;
    }

    public override void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not Calendar calendar)
        {
            return;
        }

        calendar.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
