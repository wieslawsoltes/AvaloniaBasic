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
        _isUpdating = true;
        
        if (Selected is TemplatedControl templatedControl)
        {
            if (templatedControl.Background is ISolidColorBrush backgroundSolidColorBrush)
            {
                SetBackground(backgroundSolidColorBrush.Color);
            }
            else
            {
                SetBackground(null);
            }

            if (templatedControl.Foreground is ISolidColorBrush foregroundSolidColorBrush)
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
            TextBoxForeground.Text = color.Value.ToString(); // $"#{color.ToUInt32():X8}";
            ForegroundColorView.Color = color.Value;
            RectangleForeground.Fill = new ImmutableSolidColorBrush(color.Value);
        }
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
            var text = TextBoxBackground.Text;
            if (text is not null)
            {
                try
                {
                    var color = Color.Parse(text);
                    templatedControl.Background = new ImmutableSolidColorBrush(color);

                    SetBackground(color);

                    if (DataContext is ToolBoxViewModel toolBoxViewModel)
                    {
                        toolBoxViewModel.UpdatePropertyValue(templatedControl, "Background", text);
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

        if (Selected is TemplatedControl templatedControl)
        {
            try
            {
                var color = e.NewColor;
                templatedControl.Background = new ImmutableSolidColorBrush(color);

                SetBackground(color);

                if (DataContext is ToolBoxViewModel toolBoxViewModel)
                {
                    toolBoxViewModel.UpdatePropertyValue(templatedControl, "Background", color.ToString());
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

        if (Selected is TemplatedControl templatedControl)
        {
            var text = TextBoxForeground.Text;
            if (text is not null)
            {
                try
                {
                    var color = Color.Parse(text);
                    templatedControl.Foreground = new ImmutableSolidColorBrush(color);

                    SetForeground(color);

                    if (DataContext is ToolBoxViewModel toolBoxViewModel)
                    {
                        toolBoxViewModel.UpdatePropertyValue(templatedControl, "Background", text);
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

        if (Selected is TemplatedControl templatedControl)
        {
            try
            {
                var color = e.NewColor;
                templatedControl.Foreground = new ImmutableSolidColorBrush(color);

                SetForeground(color);

                if (DataContext is ToolBoxViewModel toolBoxViewModel)
                {
                    toolBoxViewModel.UpdatePropertyValue(templatedControl, "Background", color.ToString());
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

