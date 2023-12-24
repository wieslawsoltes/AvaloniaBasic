using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;

namespace BoundsDemo;

public partial class ContentControlProperties : UserControl
{
    public static readonly StyledProperty<Visual?> SelectedProperty = 
        AvaloniaProperty.Register<ContentControlProperties, Visual?>(nameof(Selected));

    private bool _isUpdating;

    public Visual? Selected
    {
        get => GetValue(SelectedProperty);
        set => SetValue(SelectedProperty, value);
    }

    public ContentControlProperties()
    {
        InitializeComponent();
    }
    
    public void UpdateContentControlProperties()
    {
        _isUpdating = true;
        
        if (Selected is ContentControl contentControl)
        {
            SetHorizontalContentAlignment(contentControl.HorizontalContentAlignment);
            SetVerticalContentAlignment(contentControl.VerticalContentAlignment);
        }

        _isUpdating = false;
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
}

