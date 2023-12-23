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
        if (Selected is TextBox textBox)
        {
            SetTextAlignment(textBox.TextAlignment);

            SetText(textBox.Text);
        }
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
        }
    }

    private void ButtonTextAlignCenter_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is TextBox textBox)
        {
            textBox.TextAlignment = TextAlignment.Center;
            SetTextAlignment(TextAlignment.Center);
        }
    }

    private void ButtonTextAlignRight_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is TextBox textBox)
        {
            textBox.TextAlignment = TextAlignment.Right;
            SetTextAlignment(TextAlignment.Right);
        }
    }

    private void ButtonTextAlignJustified_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is TextBox textBox)
        {
            textBox.TextAlignment = TextAlignment.Justify;
            SetTextAlignment(TextAlignment.Justify);
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
            if (TextBoxText.Text is not null)
            {
                try
                {
                    var text = TextBoxText.Text;
                    textBox.Text = text;
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

