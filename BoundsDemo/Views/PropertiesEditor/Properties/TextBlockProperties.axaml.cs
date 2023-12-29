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
            else
            {
                SetBackground(null);
            }

            if (textBlock.Foreground is ISolidColorBrush foregroundSolidColorBrush)
            {
                SetForeground(foregroundSolidColorBrush.Color);
            }
            else
            {
                SetForeground(null);
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

    private void SetBackground(Color? color)
    {
        if (color is not null)
        {
            TextBoxBackground.Text = color.Value.ToString(); // $"#{color.ToUInt32():X8}";
            BackgroundColorView.Color = color.Value;
            RectangleBackground.Fill = new ImmutableSolidColorBrush(color.Value);
        }
    }

    private void SetForeground(Color? color)
    {
        if (color is not null)
        {
            TextBoxForeground.Text = color.ToString(); // $"#{color.ToUInt32():X8}";
            ForegroundColorView.Color = color.Value;
            RectangleForeground.Fill = new ImmutableSolidColorBrush(color.Value);
        }
    }

    private void ButtonTextAlignLeft_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is TextBlock textBlock)
        {
            textBlock.TextAlignment = TextAlignment.Left;
            SetTextAlignment(TextAlignment.Left);

            if (DataContext is MainViewViewModel mainViewModel)
            {
                mainViewModel.UpdatePropertyValue(textBlock, "TextAlignment", "Left");
            }
        }
    }

    private void ButtonTextAlignCenter_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is TextBlock textBlock)
        {
            textBlock.TextAlignment = TextAlignment.Center;
            SetTextAlignment(TextAlignment.Center);

            if (DataContext is MainViewViewModel mainViewModel)
            {
                mainViewModel.UpdatePropertyValue(textBlock, "TextAlignment", "Center");
            }
        }
    }

    private void ButtonTextAlignRight_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is TextBlock textBlock)
        {
            textBlock.TextAlignment = TextAlignment.Right;
            SetTextAlignment(TextAlignment.Right);
            
            if (DataContext is MainViewViewModel mainViewModel)
            {
                mainViewModel.UpdatePropertyValue(textBlock, "TextAlignment", "Right");
            }
        }
    }

    private void ButtonTextAlignJustified_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is TextBlock textBlock)
        {
            textBlock.TextAlignment = TextAlignment.Justify;
            SetTextAlignment(TextAlignment.Justify);
            
            if (DataContext is MainViewViewModel mainViewModel)
            {
                mainViewModel.UpdatePropertyValue(textBlock, "TextAlignment", "Justify");
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

        if (Selected is TextBlock textBlock)
        {
            var text = TextBoxText.Text;
            if (text is not null)
            {
                try
                {
                    textBlock.Text = text;
            
                    if (DataContext is MainViewViewModel mainViewModel)
                    {
                        mainViewModel.UpdatePropertyValue(textBlock, "Text", text);
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
    
    private void TextBoxBackground_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (_isUpdating)
        {
            return;
        }

        _isUpdating = true;

        if (Selected is TextBlock textBlock)
        {
            var text = TextBoxBackground.Text;
            if (text is not null)
            {
                try
                {
                    var color = Color.Parse(text);
                    textBlock.Background = new ImmutableSolidColorBrush(color);

                    SetBackground(color);
            
                    if (DataContext is MainViewViewModel mainViewModel)
                    {
                        mainViewModel.UpdatePropertyValue(textBlock, "Background", text);
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
            
                if (DataContext is MainViewViewModel mainViewModel)
                {
                    mainViewModel.UpdatePropertyValue(textBlock, "Background", color.ToString());
                }
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
            var text = TextBoxForeground.Text;
            if (text is not null)
            {
                try
                {
                    var color = Color.Parse(text);
                    textBlock.Foreground = new ImmutableSolidColorBrush(color);

                    SetForeground(color);
            
                    if (DataContext is MainViewViewModel mainViewModel)
                    {
                        mainViewModel.UpdatePropertyValue(textBlock, "Foreground", text);
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
            
                if (DataContext is MainViewViewModel mainViewModel)
                {
                    mainViewModel.UpdatePropertyValue(textBlock, "Foreground", color.ToString());
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        _isUpdating = false;
    }
}

