using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace BoundsDemo;

public partial class TextBoxProperties : UserControl
{
    public static readonly StyledProperty<Visual?> SelectedProperty = 
        AvaloniaProperty.Register<TextBoxProperties, Visual?>(nameof(Selected));

    private bool _isUpdating;
    
    public Visual? Selected
    {
        get => GetValue(SelectedProperty);
        set => SetValue(SelectedProperty, value);
    }

    public TextBoxProperties()
    {
        InitializeComponent();
    }
    
    public void UpdateTextBoxProperties()
    {
        _isUpdating = true;

        if (Selected is TextBox textBox)
        {
            SetTextAlignment(textBox.TextAlignment);

            SetText(textBox.Text);
        }

        _isUpdating = false;
    }

    private void SetTextAlignment(TextAlignment textAlignment)
    {
        ButtonTextAlignLeft.IsChecked = textAlignment == TextAlignment.Left;
        ButtonTextAlignCenter.IsChecked = textAlignment == TextAlignment.Center;
        ButtonTextAlignRight.IsChecked = textAlignment == TextAlignment.Right;
        ButtonTextAlignJustified.IsChecked = textAlignment == TextAlignment.Justify;
    }

    private void SetText(string? text)
    {
        TextBoxText.Text = text;
    }

    private void ButtonTextAlignLeft_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is TextBox textBox)
        {
            textBox.TextAlignment = TextAlignment.Left;
            SetTextAlignment(TextAlignment.Left);

            if (DataContext is MainViewViewModel mainViewModel)
            {
                mainViewModel.UpdatePropertyValue(textBox, "TextAlignment", "Left");
            }
        }
    }

    private void ButtonTextAlignCenter_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is TextBox textBox)
        {
            textBox.TextAlignment = TextAlignment.Center;
            SetTextAlignment(TextAlignment.Center);

            if (DataContext is MainViewViewModel mainViewModel)
            {
                mainViewModel.UpdatePropertyValue(textBox, "TextAlignment", "Center");
            }
        }
    }

    private void ButtonTextAlignRight_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is TextBox textBox)
        {
            textBox.TextAlignment = TextAlignment.Right;
            SetTextAlignment(TextAlignment.Right);

            if (DataContext is MainViewViewModel mainViewModel)
            {
                mainViewModel.UpdatePropertyValue(textBox, "TextAlignment", "Right");
            }
        }
    }

    private void ButtonTextAlignJustified_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is TextBox textBox)
        {
            textBox.TextAlignment = TextAlignment.Justify;
            SetTextAlignment(TextAlignment.Justify);

            if (DataContext is MainViewViewModel mainViewModel)
            {
                mainViewModel.UpdatePropertyValue(textBox, "TextAlignment", "Justify");
            }
        }
    }

    private void TextBoxText_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (_isUpdating)
        {
            return;
        }

        _isUpdating = true;

        if (Selected is TextBox textBox)
        {
            var text = TextBoxText.Text;
            if (text is not null)
            {
                try
                {
                    textBox.Text = text;

                    if (DataContext is MainViewViewModel mainViewModel)
                    {
                        mainViewModel.UpdatePropertyValue(textBox, "Text", text);
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        _isUpdating = false;
    }
}

