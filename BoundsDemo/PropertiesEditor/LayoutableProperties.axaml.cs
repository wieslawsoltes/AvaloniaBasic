using System;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;

namespace BoundsDemo;

public partial class LayoutableProperties : UserControl
{
    public static readonly StyledProperty<Visual?> SelectedProperty = 
        AvaloniaProperty.Register<LayoutableProperties, Visual?>(nameof(Selected));

    private bool _isUpdating;

    public Visual? Selected
    {
        get => GetValue(SelectedProperty);
        set => SetValue(SelectedProperty, value);
    }

    public LayoutableProperties()
    {
        InitializeComponent();
    }

    public void UpdateLayoutableProperties()
    {
        if (Selected is Layoutable layoutable)
        {
            SetHorizontalAlignment(layoutable.HorizontalAlignment);
            SetVerticalAlignment(layoutable.VerticalAlignment);
            TextBoWidth.Text = layoutable.Width.ToString(CultureInfo.InvariantCulture);
            TextBoxHeight.Text = layoutable.Height.ToString(CultureInfo.InvariantCulture);
            TextBoxMinWidth.Text = layoutable.MinWidth.ToString(CultureInfo.InvariantCulture);
            TextBoxMinHeight.Text = layoutable.MinHeight.ToString(CultureInfo.InvariantCulture);
            TextBoxMaxWidth.Text = layoutable.MaxWidth.ToString(CultureInfo.InvariantCulture);
            TextBoxMaxHeight.Text = layoutable.MaxHeight.ToString(CultureInfo.InvariantCulture);
            TextBoxMargin.Text = layoutable.Margin.ToString();
        }
    }

    private void SetHorizontalAlignment(HorizontalAlignment horizontalAlignment)
    {
        ButtonAlignHorizontalLeft.IsChecked = horizontalAlignment == HorizontalAlignment.Left;
        ButtonAlignHorizontalCenter.IsChecked = horizontalAlignment == HorizontalAlignment.Center;
        ButtonAlignHorizontalRight.IsChecked = horizontalAlignment == HorizontalAlignment.Right;
        ButtonAlignHorizontalStretch.IsChecked = horizontalAlignment == HorizontalAlignment.Stretch;
    }

    private void SetVerticalAlignment(VerticalAlignment verticalAlignment)
    {
        ButtonAlignVerticalTop.IsChecked = verticalAlignment == VerticalAlignment.Top;
        ButtonAlignVerticalCenter.IsChecked = verticalAlignment == VerticalAlignment.Center;
        ButtonAlignVerticalBottom.IsChecked = verticalAlignment == VerticalAlignment.Bottom;
        ButtonAlignVerticalStretch.IsChecked = verticalAlignment == VerticalAlignment.Stretch;
    }

    
    private void ButtonAlignHorizontalLeft_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is Layoutable layoutable)
        {
            layoutable.HorizontalAlignment = HorizontalAlignment.Left;
            SetHorizontalAlignment(HorizontalAlignment.Left);
        }
    }

    private void ButtonAlignHorizontalCenter_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is Layoutable layoutable)
        {
            layoutable.HorizontalAlignment = HorizontalAlignment.Center;
            SetHorizontalAlignment(HorizontalAlignment.Center);
        }
    }

    private void ButtonAlignHorizontalRight_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is Layoutable layoutable)
        {
            layoutable.HorizontalAlignment = HorizontalAlignment.Right;
            SetHorizontalAlignment(HorizontalAlignment.Right);
        }
    }

    private void ButtonAlignHorizontalStretch_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is Layoutable layoutable)
        {
            layoutable.HorizontalAlignment = HorizontalAlignment.Stretch;
            SetHorizontalAlignment(HorizontalAlignment.Stretch);
        }
    }

    private void ButtonAlignVerticalTop_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is Layoutable layoutable)
        {
            layoutable.VerticalAlignment = VerticalAlignment.Top;
            SetVerticalAlignment(VerticalAlignment.Top);
        }
    }

    private void ButtonAlignVerticalCenter_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is Layoutable layoutable)
        {
            layoutable.VerticalAlignment = VerticalAlignment.Center;
            SetVerticalAlignment(VerticalAlignment.Center);
        }
    }

    private void ButtonAlignVerticalBottom_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is Layoutable layoutable)
        {
            layoutable.VerticalAlignment = VerticalAlignment.Bottom;
            SetVerticalAlignment(VerticalAlignment.Bottom);
        }
    }

    private void ButtonAlignVerticalStretch_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is Layoutable layoutable)
        {
            layoutable.VerticalAlignment = VerticalAlignment.Stretch;
            SetVerticalAlignment(VerticalAlignment.Stretch);
        }
    }

    private void TextBoxWidth_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (_isUpdating)
        {
            return;
        }

        if (Selected is Layoutable layoutable)
        {
            var result = double.TryParse(TextBoWidth.Text, CultureInfo.InvariantCulture, out var value);
            if (result)
            {
                layoutable.Width = value;
            }
        }
    }

    private void TextBoxHeight_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (_isUpdating)
        {
            return;
        }

        if (Selected is Layoutable layoutable)
        {
            var result = double.TryParse(TextBoxHeight.Text, CultureInfo.InvariantCulture, out var value);
            if (result)
            {
                layoutable.Height = value;
            }
        }
    }

    private void TextBoxMinWidth_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (_isUpdating)
        {
            return;
        }

        if (Selected is Layoutable layoutable)
        {
            var result = double.TryParse(TextBoxMinWidth.Text, CultureInfo.InvariantCulture, out var value);
            if (result)
            {
                layoutable.MinWidth = value;
            }
        }
    }

    private void TextBoxMinHeight_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (_isUpdating)
        {
            return;
        }

        if (Selected is Layoutable layoutable)
        {
            var result = double.TryParse(TextBoxMinHeight.Text, CultureInfo.InvariantCulture, out var value);
            if (result)
            {
                layoutable.MinHeight = value;
            }
        }
    }

    private void TextBoxMaxWidth_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (_isUpdating)
        {
            return;
        }

        if (Selected is Layoutable layoutable)
        {
            var result = double.TryParse(TextBoxMaxWidth.Text, CultureInfo.InvariantCulture, out var value);
            if (result)
            {
                layoutable.MaxWidth = value;
            }
        }
    }

    private void TextBoxMaxHeight_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (_isUpdating)
        {
            return;
        }

        if (Selected is Layoutable layoutable)
        {
            var result = double.TryParse(TextBoxMaxHeight.Text, CultureInfo.InvariantCulture, out var value);
            if (result)
            {
                layoutable.MaxHeight = value;
            }
        }
    }

    private void TextBoxMargin_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (_isUpdating)
        {
            return;
        }

        _isUpdating = true;

        if (Selected is Layoutable layoutable)
        {
            if (TextBoxMargin.Text is not null)
            {
                try
                {
                    var thickness = Thickness.Parse(TextBoxMargin.Text);
                    layoutable.Margin = thickness;
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

