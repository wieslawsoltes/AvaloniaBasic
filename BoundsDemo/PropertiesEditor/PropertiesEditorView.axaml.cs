using System;
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

        if (Selected is Layoutable)
        {
            LayoutableProperties.UpdateLayoutableProperties();
            LayoutableProperties.IsVisible = true;
        }
        else
        {
            LayoutableProperties.IsVisible = false;
        }

        if (Selected is ContentControl)
        {
            ContentControlProperties.UpdateContentControlProperties();
            ContentControlProperties.IsVisible = true;
        }
        else
        {
            ContentControlProperties.IsVisible = false;
        }

        UpdateTextBlockProperties();

        if (Selected is TemplatedControl)
        {
            TemplatedControlProperties.UpdateTemplatedControlProperties();
            TemplatedControlProperties.IsVisible = true;
        }
        else
        {
            TemplatedControlProperties.IsVisible = false;
        }

        _isUpdating = false;
    }

    public void UpdateTextBlockProperties()
    {
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
}

