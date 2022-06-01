using System;
using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaBasicDemo.Model;

namespace AvaloniaBasicDemo.ViewModels.Toolbox;

public class DatePickerViewModel : IDragItem
{
    public string? Name { get; init; }

    public string? Icon { get; init; }

    public Control CreatePreview()
    {
        var datePicker = new DatePicker();
        datePicker.SelectedDate = DateTimeOffset.Now;
        datePicker.Background = Brushes.Black;
        return datePicker;
    }

    public Control CreateControl()
    {
        var datePicker = new DatePicker();
        datePicker.SelectedDate = DateTimeOffset.Now;
        //textBox.Background = Brushes.Blue;
        return datePicker;
    }

    public void UpdatePreview(Control control, bool isPointerOver)
    {
        if (control is not DatePicker datePicker)
        {
            return;
        }

        datePicker.Background = isPointerOver ? Brushes.Green : Brushes.Red;
    }
}
