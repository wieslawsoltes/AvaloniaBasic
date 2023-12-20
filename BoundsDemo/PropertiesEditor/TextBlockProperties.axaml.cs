using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;

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
        if (Selected is TextBlock textBlock)
        {
            SetTextAlignment(textBlock.TextAlignment);
        }
    }

    private void SetTextAlignment(TextAlignment textAlignment)
    {
        ButtonTextAlignLeft.IsChecked = textAlignment == TextAlignment.Left;
        ButtonTextAlignCenter.IsChecked = textAlignment == TextAlignment.Center;
        ButtonTextAlignRight.IsChecked = textAlignment == TextAlignment.Right;
        ButtonTextAlignJustified.IsChecked = textAlignment == TextAlignment.Justify;
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
}

