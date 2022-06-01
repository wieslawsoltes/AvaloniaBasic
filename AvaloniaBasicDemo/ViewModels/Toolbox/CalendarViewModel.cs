using System;
using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Model;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class CalendarViewModel : IDragItem
{
    public string? Name { get; init; }

    public string? Icon { get; init; }

    public Control CreatePreview()
    {
        var calendar = new Calendar();
        calendar.SelectedDate = DateTime.Now;
        calendar.Background = Brushes.Black;
        return calendar;
    }

    public Control CreateControl()
    {
        var calendar = new Calendar();
        calendar.SelectedDate = DateTime.Now;
        //textBox.Background = Brushes.Blue;
        return calendar;
    }

    public void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not Calendar calendar)
        {
            return;
        }

        calendar.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
