using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Media.Immutable;

namespace BoundsDemo;

public partial class TemplatedControlProperties : UserControl
{
    public static readonly StyledProperty<Visual?> SelectedProperty = 
        AvaloniaProperty.Register<TemplatedControlProperties, Visual?>(nameof(Selected));

    private bool _isUpdating;
    
    public Visual? Selected
    {
        get => GetValue(SelectedProperty);
        set => SetValue(SelectedProperty, value);
    }

    public TemplatedControlProperties()
    {
        InitializeComponent();
    }
    
    public void UpdateTemplatedControlProperties()
    {
        if (Selected is TemplatedControl templatedControl)
        {
            if (templatedControl.Background is ISolidColorBrush backgroundSolidColorBrush)
            {
                SetBackground(backgroundSolidColorBrush.Color);
            }

            if (templatedControl.Foreground is ISolidColorBrush foregroundSolidColorBrush)
            {
                SetForeground(foregroundSolidColorBrush.Color);
            }
        }
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

    private void TextBoxBackground_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (_isUpdating)
        {
            return;
        }

        _isUpdating = true;

        if (Selected is TemplatedControl templatedControl)
        {
            if (TextBoxBackground.Text is not null)
            {
                try
                {
                    var color = Color.Parse(TextBoxBackground.Text);
                    templatedControl.Background = new ImmutableSolidColorBrush(color);

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

        if (Selected is TemplatedControl templatedControl)
        {
            try
            {
                var color = e.NewColor;
                templatedControl.Background = new ImmutableSolidColorBrush(color);

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

        if (Selected is TemplatedControl templatedControl)
        {
            if (TextBoxForeground.Text is not null)
            {
                try
                {
                    var color = Color.Parse(TextBoxForeground.Text);
                    templatedControl.Foreground = new ImmutableSolidColorBrush(color);

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

        if (Selected is TemplatedControl templatedControl)
        {
            try
            {
                var color = e.NewColor;
                templatedControl.Foreground = new ImmutableSolidColorBrush(color);

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

