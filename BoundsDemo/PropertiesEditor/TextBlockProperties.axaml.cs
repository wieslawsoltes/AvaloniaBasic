using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Immutable;

namespace BoundsDemo;

public partial class TextBlockProperties : UserControl
{
    public static readonly StyledProperty<Visual?> SelectedProperty = 
        AvaloniaProperty.Register<TextBlockProperties, Visual?>(nameof(Selected));

    private bool _isUpdating;
    
    public Visual? Selected
    {
        get => GetValue(SelectedProperty);
        set => SetValue(SelectedProperty, value);
    }

    public TextBlockProperties()
    {
        InitializeComponent();
    }
    
    public void UpdateTextBlockProperties()
    {
        _isUpdating = true;
        
        if (Selected is TextBlock textBlock)
        {
            SetTextAlignment(textBlock.TextAlignment);

            SetText(textBlock.Text);
   
            if (textBlock.Background is ISolidColorBrush backgroundSolidColorBrush)
            {
                SetBackground(backgroundSolidColorBrush.Color);
            }

            if (textBlock.Foreground is ISolidColorBrush foregroundSolidColorBrush)
            {
                SetForeground(foregroundSolidColorBrush.Color);
            }
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

    private void SetBackground(Color color)
    {
        TextBoxBackground.Text = $"#{color.ToUInt32():X8}";
        BackgroundColorView.Color = color;
        RectangleBackground.Fill = new ImmutableSolidColorBrush(color);
    }

    private void SetForeground(Color color)
    {
        TextBoxForeground.Text = $"#{color.ToUInt32():X8}";
        ForegroundColorView.Color = color;
        RectangleForeground.Fill = new ImmutableSolidColorBrush(color);
    }

    private void ButtonTextAlignLeft_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is TextBlock textBlock)
        {
            textBlock.TextAlignment = TextAlignment.Left;
            SetTextAlignment(TextAlignment.Left);
        }
    }

    private void ButtonTextAlignCenter_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is TextBlock textBlock)
        {
            textBlock.TextAlignment = TextAlignment.Center;
            SetTextAlignment(TextAlignment.Center);
        }
    }

    private void ButtonTextAlignRight_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is TextBlock textBlock)
        {
            textBlock.TextAlignment = TextAlignment.Right;
            SetTextAlignment(TextAlignment.Right);
        }
    }

    private void ButtonTextAlignJustified_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is TextBlock textBlock)
        {
            textBlock.TextAlignment = TextAlignment.Justify;
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

        if (Selected is TextBlock textBlock)
        {
            if (TextBoxText.Text is not null)
            {
                try
                {
                    var text = TextBoxText.Text;
                    textBlock.Text = text;
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        _isUpdating = false;
    }
    
    private void TextBoxBackground_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (_isUpdating)
        {
            return;
        }

        _isUpdating = true;

        if (Selected is TextBlock textBlock)
        {
            if (TextBoxBackground.Text is not null)
            {
                try
                {
                    var color = Color.Parse(TextBoxBackground.Text);
                    textBlock.Background = new ImmutableSolidColorBrush(color);

                    SetBackground(color);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        _isUpdating = false;
    }

    private void BackgroundColorView_OnColorChanged(object? sender, ColorChangedEventArgs e)
    {
        if (_isUpdating)
        {
            return;
        }

        _isUpdating = true;

        if (Selected is TextBlock textBlock)
        {
            try
            {
                var color = e.NewColor;
                textBlock.Background = new ImmutableSolidColorBrush(color);

                SetBackground(color);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        _isUpdating = false;
    }

    private void TextBoxForeground_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (_isUpdating)
        {
            return;
        }

        _isUpdating = true;

        if (Selected is TextBlock textBlock)
        {
            if (TextBoxForeground.Text is not null)
            {
                try
                {
                    var color = Color.Parse(TextBoxForeground.Text);
                    textBlock.Foreground = new ImmutableSolidColorBrush(color);

                    SetForeground(color);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        _isUpdating = false;
    }

    private void ForegroundColorView_OnColorChanged(object? sender, ColorChangedEventArgs e)
    {
        if (_isUpdating)
        {
            return;
        }

        if (Selected is TextBlock textBlock)
        {
            try
            {
                var color = e.NewColor;
                textBlock.Foreground = new ImmutableSolidColorBrush(color);

                SetForeground(color);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        _isUpdating = false;
    }
}

