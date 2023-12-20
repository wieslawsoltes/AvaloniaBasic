using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Avalonia.Media.Immutable;

namespace BoundsDemo;

public partial class ShapeProperties : UserControl
{
    public static readonly StyledProperty<Visual?> SelectedProperty = 
        AvaloniaProperty.Register<ShapeProperties, Visual?>(nameof(Selected));

    private bool _isUpdating;
    
    public Visual? Selected
    {
        get => GetValue(SelectedProperty);
        set => SetValue(SelectedProperty, value);
    }

    public ShapeProperties()
    {
        InitializeComponent();
    }
    
    public void UpdateShapeProperties()
    {
        if (Selected is Shape shape)
        {
            if (shape.Fill is ISolidColorBrush fillSolidColorBrush)
            {
                SetFill(fillSolidColorBrush.Color);
            }

            if (shape.Stroke is ISolidColorBrush strokeSolidColorBrush)
            {
                SetStroke(strokeSolidColorBrush.Color);
            }
        }
    }

    private void SetFill(Color color)
    {
        TextBoxFill.Text = $"#{color.ToUInt32():X8}";
        BackgroundColorView.Color = color;
        RectangleFill.Fill = new ImmutableSolidColorBrush(color);
    }

    private void SetStroke(Color color)
    {
        TextBoxStroke.Text = $"#{color.ToUInt32():X8}";
        StrokeColorView.Color = color;
        RectangleStroke.Fill = new ImmutableSolidColorBrush(color);
    }

    private void TextBoxFill_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (_isUpdating)
        {
            return;
        }

        _isUpdating = true;

        if (Selected is Shape shape)
        {
            if (TextBoxFill.Text is not null)
            {
                try
                {
                    var color = Color.Parse(TextBoxFill.Text);
                    shape.Fill = new ImmutableSolidColorBrush(color);

                    SetFill(color);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        _isUpdating = false;
    }

    private void FillColorView_OnColorChanged(object? sender, ColorChangedEventArgs e)
    {
        if (_isUpdating)
        {
            return;
        }

        _isUpdating = true;

        if (Selected is Shape shape)
        {
            try
            {
                var color = e.NewColor;
                shape.Fill = new ImmutableSolidColorBrush(color);

                SetFill(color);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        _isUpdating = false;
    }

    private void TextBoxStroke_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (_isUpdating)
        {
            return;
        }

        _isUpdating = true;

        if (Selected is Shape shape)
        {
            if (TextBoxStroke.Text is not null)
            {
                try
                {
                    var color = Color.Parse(TextBoxStroke.Text);
                    shape.Stroke = new ImmutableSolidColorBrush(color);

                    SetStroke(color);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        _isUpdating = false;
    }

    private void StrokeColorView_OnColorChanged(object? sender, ColorChangedEventArgs e)
    {
        if (_isUpdating)
        {
            return;
        }

        if (Selected is Shape shape)
        {
            try
            {
                var color = e.NewColor;
                shape.Stroke = new ImmutableSolidColorBrush(color);

                SetStroke(color);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        _isUpdating = false;
    }
}

