using System;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Reactive;

namespace BoundsDemo;

public partial class PropertiesEditorView : UserControl
{
    public static readonly StyledProperty<Visual?> SelectedProperty = 
        AvaloniaProperty.Register<PropertiesEditorView, Visual?>(nameof(Selected));

    private bool _isUpdating;
    
    public Visual? Selected
    {
        get => GetValue(SelectedProperty);
        set => SetValue(SelectedProperty, value);
    }

    public PropertiesEditorView()
    {
        InitializeComponent();

        this.GetObservable(SelectedProperty).Subscribe(new AnonymousObserver<Visual?>(_ =>
        {
            // UpdatePropertiesEditor();
        }));
    }

    public void UpdatePropertiesEditor()
    {
        _isUpdating = true;

        if (Selected is Layoutable layoutable)
        {
            LayoutStackPanel.IsVisible = true;
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
        else
        {
            LayoutStackPanel.IsVisible = false;
        }

        if (Selected is ContentControl contentControl)
        {
            ContentLayoutStackPanel.IsVisible = true;
            SetHorizontalContentAlignment(contentControl.HorizontalContentAlignment);
            SetVerticalContentAlignment(contentControl.VerticalContentAlignment);
        }
        else
        {
            ContentLayoutStackPanel.IsVisible = false;
        }

        if (Selected is TextBlock or TextBox)
        {
            TextLayoutStackPanel.IsVisible = true;

            if (Selected is TextBlock textBlock)
            {
                SetTextAlignment(textBlock.TextAlignment);
            }

            if (Selected is TextBox textBox)
            {
                SetTextAlignment(textBox.TextAlignment);
            }
        }
        else
        {
            TextLayoutStackPanel.IsVisible = false;
        }

        if (Selected is TemplatedControl templatedControl)
        {
            TemplatedControlStackPanel.IsVisible = true;

            if (templatedControl.Background is ISolidColorBrush backgroundSolidColorBrush)
            {
                SetBackground(backgroundSolidColorBrush.Color);
            }

            if (templatedControl.Foreground is ISolidColorBrush foregroundSolidColorBrush)
            {
                SetForeground(foregroundSolidColorBrush.Color);
            }
        }
        else
        {
            TemplatedControlStackPanel.IsVisible = false;
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

    private void SetHorizontalContentAlignment(HorizontalAlignment horizontalAlignment)
    {
        ButtonAlignContentHorizontalLeft.IsChecked = horizontalAlignment == HorizontalAlignment.Left;
        ButtonAlignContentHorizontalCenter.IsChecked = horizontalAlignment == HorizontalAlignment.Center;
        ButtonAlignContentHorizontalRight.IsChecked = horizontalAlignment == HorizontalAlignment.Right;
        ButtonAlignContentHorizontalStretch.IsChecked = horizontalAlignment == HorizontalAlignment.Stretch;
    }

    private void SetVerticalContentAlignment(VerticalAlignment verticalAlignment)
    {
        ButtonAlignContentVerticalTop.IsChecked = verticalAlignment == VerticalAlignment.Top;
        ButtonAlignContentVerticalCenter.IsChecked = verticalAlignment == VerticalAlignment.Center;
        ButtonAlignContentVerticalBottom.IsChecked = verticalAlignment == VerticalAlignment.Bottom;
        ButtonAlignContentVerticalStretch.IsChecked = verticalAlignment == VerticalAlignment.Stretch;
    }

    private void SetTextAlignment(TextAlignment textAlignment)
    {
        ButtonTextAlignLeft.IsChecked = textAlignment == TextAlignment.Left;
        ButtonTextAlignCenter.IsChecked = textAlignment == TextAlignment.Center;
        ButtonTextAlignRight.IsChecked = textAlignment == TextAlignment.Right;
        ButtonTextAlignJustified.IsChecked = textAlignment == TextAlignment.Justify;
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

    private void ButtonAlignContentHorizontalLeft_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is ContentControl contentControl)
        {
            contentControl.HorizontalContentAlignment = HorizontalAlignment.Left;
            SetHorizontalContentAlignment(HorizontalAlignment.Left);
        }
    }

    private void ButtonAlignContentHorizontalCenter_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is ContentControl contentControl)
        {
            contentControl.HorizontalContentAlignment = HorizontalAlignment.Center;
            SetHorizontalContentAlignment(HorizontalAlignment.Center);
        }
    }

    private void ButtonAlignContentHorizontalRight_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is ContentControl contentControl)
        {
            contentControl.HorizontalContentAlignment = HorizontalAlignment.Right;
            SetHorizontalContentAlignment(HorizontalAlignment.Right);
        }
    }

    private void ButtonAlignContentHorizontalStretch_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is ContentControl contentControl)
        {
            contentControl.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            SetHorizontalContentAlignment(HorizontalAlignment.Stretch);
        }
    }

    private void ButtonAlignContentVerticalTop_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is ContentControl contentControl)
        {
            contentControl.VerticalContentAlignment = VerticalAlignment.Top;
            SetVerticalContentAlignment(VerticalAlignment.Top);
        }
    }

    private void ButtonAlignContentVerticalCenter_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is ContentControl contentControl)
        {
            contentControl.VerticalContentAlignment = VerticalAlignment.Center;
            SetVerticalContentAlignment(VerticalAlignment.Center);
        }
    }

    private void ButtonAlignContentVerticalBottom_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is ContentControl contentControl)
        {
            contentControl.VerticalContentAlignment = VerticalAlignment.Bottom;
            SetVerticalContentAlignment(VerticalAlignment.Bottom);
        }
    }

    private void ButtonAlignContentVerticalStretch_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is ContentControl contentControl)
        {
            contentControl.VerticalContentAlignment = VerticalAlignment.Stretch;
            SetVerticalContentAlignment(VerticalAlignment.Stretch);
        }
    }

    private void ButtonTextAlignLeft_OnClick(object? sender, RoutedEventArgs e)
    {
        switch (Selected)
        {
            case TextBlock textBlock:
                textBlock.TextAlignment = TextAlignment.Left;
                SetTextAlignment(TextAlignment.Left);
                break;
            case TextBox textBox:
                textBox.TextAlignment = TextAlignment.Left;
                SetTextAlignment(TextAlignment.Left);
                break;
        }
    }

    private void ButtonTextAlignCenter_OnClick(object? sender, RoutedEventArgs e)
    {
        switch (Selected)
        {
            case TextBlock textBlock:
                textBlock.TextAlignment = TextAlignment.Center;
                SetTextAlignment(TextAlignment.Center);
                break;
            case TextBox textBox:
                textBox.TextAlignment = TextAlignment.Center;
                SetTextAlignment(TextAlignment.Center);
                break;
        }
    }

    private void ButtonTextAlignRight_OnClick(object? sender, RoutedEventArgs e)
    {
        switch (Selected)
        {
            case TextBlock textBlock:
                textBlock.TextAlignment = TextAlignment.Right;
                SetTextAlignment(TextAlignment.Right);
                break;
            case TextBox textBox:
                textBox.TextAlignment = TextAlignment.Right;
                SetTextAlignment(TextAlignment.Right);
                break;
        }
    }

    private void ButtonTextAlignJustified_OnClick(object? sender, RoutedEventArgs e)
    {
        switch (Selected)
        {
            case TextBlock textBlock:
                textBlock.TextAlignment = TextAlignment.Justify;
                SetTextAlignment(TextAlignment.Justify);
                break;
            case TextBox textBox:
                textBox.TextAlignment = TextAlignment.Justify;
                SetTextAlignment(TextAlignment.Justify);
                break;
        }
    }

    private void TextBoWidth_OnTextChanged(object? sender, TextChangedEventArgs e)
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

