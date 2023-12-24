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
        _isUpdating = true;
        
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

        _isUpdating = false;
    }

    private void SetFill(Color color)
    {
        TextBoxFill.Text = color.ToString(); // $"#{color.ToUInt32():X8}";
        FillColorView.Color = color;
        RectangleFill.Fill = new ImmutableSolidColorBrush(color);
    }

    private void SetStroke(Color color)
    {
        TextBoxStroke.Text = color.ToString(); // $"#{color.ToUInt32():X8}";
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
            var text = TextBoxFill.Text;
            if (text is not null)
            {
                try
                {
                    var color = Color.Parse(text);
                    shape.Fill = new ImmutableSolidColorBrush(color);

                    SetFill(color);

                    if (DataContext is ToolBoxViewModel toolBoxViewModel)
                    {
                        toolBoxViewModel.UpdatePropertyValue(shape, "Fill", text);
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

                if (DataContext is ToolBoxViewModel toolBoxViewModel)
                {
                    toolBoxViewModel.UpdatePropertyValue(shape, "Fill", color.ToString());
                }
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
            var text = TextBoxStroke.Text;
            if (text is not null)
            {
                try
                {
                    var color = Color.Parse(text);
                    shape.Stroke = new ImmutableSolidColorBrush(color);

                    SetStroke(color);

                    if (DataContext is ToolBoxViewModel toolBoxViewModel)
                    {
                        toolBoxViewModel.UpdatePropertyValue(shape, "Stroke", text);
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

                if (DataContext is ToolBoxViewModel toolBoxViewModel)
                {
                    toolBoxViewModel.UpdatePropertyValue(shape, "Stroke", color.ToString());
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

