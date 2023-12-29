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
        _isUpdating = true;
        
        if (Selected is Layoutable layoutable)
        {
            SetHorizontalAlignment(layoutable.HorizontalAlignment);
            SetVerticalAlignment(layoutable.VerticalAlignment);

            TextBoxWidth.Text = layoutable.Width.ToString(CultureInfo.InvariantCulture);
            TextBoxHeight.Text = layoutable.Height.ToString(CultureInfo.InvariantCulture);
            TextBoxMinWidth.Text = layoutable.MinWidth.ToString(CultureInfo.InvariantCulture);
            TextBoxMinHeight.Text = layoutable.MinHeight.ToString(CultureInfo.InvariantCulture);
            TextBoxMaxWidth.Text = layoutable.MaxWidth.ToString(CultureInfo.InvariantCulture);
            TextBoxMaxHeight.Text = layoutable.MaxHeight.ToString(CultureInfo.InvariantCulture);
            TextBoxMargin.Text = layoutable.Margin.ToString();
        }

        _isUpdating = false;
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
            
            if (DataContext is MainViewViewModel mainViewModel)
            {
                mainViewModel.UpdatePropertyValue(layoutable as Control, "HorizontalAlignment", "Left");
            }
        }
    }

    private void ButtonAlignHorizontalCenter_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is Layoutable layoutable)
        {
            layoutable.HorizontalAlignment = HorizontalAlignment.Center;
            SetHorizontalAlignment(HorizontalAlignment.Center);
            
            if (DataContext is MainViewViewModel mainViewModel)
            {
                mainViewModel.UpdatePropertyValue(layoutable as Control, "HorizontalAlignment", "Center");
            }
        }
    }

    private void ButtonAlignHorizontalRight_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is Layoutable layoutable)
        {
            layoutable.HorizontalAlignment = HorizontalAlignment.Right;
            SetHorizontalAlignment(HorizontalAlignment.Right);
            
            if (DataContext is MainViewViewModel mainViewModel)
            {
                mainViewModel.UpdatePropertyValue(layoutable as Control, "HorizontalAlignment", "Right");
            }
        }
    }

    private void ButtonAlignHorizontalStretch_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is Layoutable layoutable)
        {
            layoutable.HorizontalAlignment = HorizontalAlignment.Stretch;
            SetHorizontalAlignment(HorizontalAlignment.Stretch);
            
            if (DataContext is MainViewViewModel mainViewModel)
            {
                mainViewModel.UpdatePropertyValue(layoutable as Control, "HorizontalAlignment", "Stretch");
            }
        }
    }

    private void ButtonAlignVerticalTop_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is Layoutable layoutable)
        {
            layoutable.VerticalAlignment = VerticalAlignment.Top;
            SetVerticalAlignment(VerticalAlignment.Top);
            
            if (DataContext is MainViewViewModel mainViewModel)
            {
                mainViewModel.UpdatePropertyValue(layoutable as Control, "VerticalAlignment", "Top");
            }
        }
    }

    private void ButtonAlignVerticalCenter_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is Layoutable layoutable)
        {
            layoutable.VerticalAlignment = VerticalAlignment.Center;
            SetVerticalAlignment(VerticalAlignment.Center);
            
            if (DataContext is MainViewViewModel mainViewModel)
            {
                mainViewModel.UpdatePropertyValue(layoutable as Control, "VerticalAlignment", "Center");
            }
        }
    }

    private void ButtonAlignVerticalBottom_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is Layoutable layoutable)
        {
            layoutable.VerticalAlignment = VerticalAlignment.Bottom;
            SetVerticalAlignment(VerticalAlignment.Bottom);
            
            if (DataContext is MainViewViewModel mainViewModel)
            {
                mainViewModel.UpdatePropertyValue(layoutable as Control, "VerticalAlignment", "Bottom");
            }
        }
    }

    private void ButtonAlignVerticalStretch_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is Layoutable layoutable)
        {
            layoutable.VerticalAlignment = VerticalAlignment.Stretch;
            SetVerticalAlignment(VerticalAlignment.Stretch);
            
            if (DataContext is MainViewViewModel mainViewModel)
            {
                mainViewModel.UpdatePropertyValue(layoutable as Control, "VerticalAlignment", "Stretch");
            }
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
            var text = TextBoxWidth.Text;
            var result = double.TryParse(text, CultureInfo.InvariantCulture, out var value);
            if (result)
            {
                layoutable.Width = value;
            
                if (DataContext is MainViewViewModel mainViewModel)
                {
                    mainViewModel.UpdatePropertyValue(layoutable as Control, "Width", text);
                }
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
            var text = TextBoxHeight.Text;
            var result = double.TryParse(text, CultureInfo.InvariantCulture, out var value);
            if (result)
            {
                layoutable.Height = value;
            
                if (DataContext is MainViewViewModel mainViewModel)
                {
                    mainViewModel.UpdatePropertyValue(layoutable as Control, "Height", text);
                }
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
            var text = TextBoxMinWidth.Text;
            var result = double.TryParse(text, CultureInfo.InvariantCulture, out var value);
            if (result)
            {
                layoutable.MinWidth = value;
            
                if (DataContext is MainViewViewModel mainViewModel)
                {
                    mainViewModel.UpdatePropertyValue(layoutable as Control, "MinWidth", text);
                }
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
            var text = TextBoxMinHeight.Text;
            var result = double.TryParse(text, CultureInfo.InvariantCulture, out var value);
            if (result)
            {
                layoutable.MinHeight = value;
            
                if (DataContext is MainViewViewModel mainViewModel)
                {
                    mainViewModel.UpdatePropertyValue(layoutable as Control, "MinHeight", text);
                }
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
            var text = TextBoxMaxWidth.Text;
            var result = double.TryParse(text, CultureInfo.InvariantCulture, out var value);
            if (result)
            {
                layoutable.MaxWidth = value;
            
                if (DataContext is MainViewViewModel mainViewModel)
                {
                    mainViewModel.UpdatePropertyValue(layoutable as Control, "MaxWidth", text);
                }
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
            var text = TextBoxMaxHeight.Text;
            var result = double.TryParse(text, CultureInfo.InvariantCulture, out var value);
            if (result)
            {
                layoutable.MaxHeight = value;
            
                if (DataContext is MainViewViewModel mainViewModel)
                {
                    mainViewModel.UpdatePropertyValue(layoutable as Control, "MaxHeight", text);
                }
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
            var text = TextBoxMargin.Text;
            if (text is not null)
            {
                try
                {
                    var thickness = Thickness.Parse(text);
                    layoutable.Margin = thickness;

                    if (DataContext is MainViewViewModel mainViewModel)
                    {
                        mainViewModel.UpdatePropertyValue(layoutable as Control, "Margin", text);
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

