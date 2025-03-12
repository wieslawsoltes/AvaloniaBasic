using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Reactive;
using ResizingControlDemo.Controls;

namespace ResizingControlDemo;

public partial class PropertiesView : UserControl
{
    private Control? _selectedControl;
    private IDisposable? _subscription;

    public static readonly StyledProperty<ResizingHostControl?> ResizingHostControlProperty = 
        AvaloniaProperty.Register<PropertiesView, ResizingHostControl?>(nameof(ResizingHostControl));

    [ResolveByName]
    public ResizingHostControl? ResizingHostControl
    {
        get => GetValue(ResizingHostControlProperty);
        set => SetValue(ResizingHostControlProperty, value);
    }

    public PropertiesView()
    {
        InitializeComponent();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);

        InitializeProperties();
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == ResizingHostControlProperty)
        {
            DeInitializeProperties();
            _subscription?.Dispose();

            _subscription = ResizingHostControl?
                .GetObservable(ResizingHostControl.SelectedResizingAdornerControlProperty)
                .Subscribe(new AnonymousObserver<ResizingAdornerControl?>(UpdateProperties));

            InitializeProperties();
        }
    }

    private void InitializeProperties()
    {
        ForegroundColorPicker.IsVisible = false;
        ForegroundColorPicker.ColorChanged += ForegroundColorPickerOnColorChanged;

        BackgroundColorPicker.IsVisible = false;
        BackgroundColorPicker.ColorChanged += BackgroundColorPickerOnColorChanged;
    }

    private void DeInitializeProperties()
    {
        ForegroundColorPicker.IsVisible = false;
        ForegroundColorPicker.ColorChanged -= ForegroundColorPickerOnColorChanged;

        BackgroundColorPicker.IsVisible = false;
        BackgroundColorPicker.ColorChanged -= BackgroundColorPickerOnColorChanged;
    }

    private static Color? GetColor(IBrush? brush)
    {
        switch (brush)
        {
            case null:
                break;
            case ISolidColorBrush solidColorBrush:
                return solidColorBrush.Color;
        }

        return null;
    }
    
    private void UpdateProperties(ResizingAdornerControl? resizingAdornerControl)
    {
        if (resizingAdornerControl is not null)
        {
            _selectedControl = resizingAdornerControl.AdornedElement as Control;

            // Foreground
            switch (_selectedControl)
            {
                case ContentControl contentControl:
                {
                    var color = GetColor(contentControl.Foreground);
                    if (color is not null)
                    {
                        ForegroundColorPicker.Color = color.Value;
                        ForegroundColorPicker.IsVisible = true;
                    }
                    break;
                }
                case TemplatedControl templatedControl:
                {
                    var color = GetColor(templatedControl.Foreground);
                    if (color is not null)
                    {
                        ForegroundColorPicker.Color = color.Value;
                        ForegroundColorPicker.IsVisible = true;
                    }
                    break;
                }
                case TextBlock textBlock:
                {
                    var color = GetColor(textBlock.Foreground);
                    if (color is not null)
                    {
                        ForegroundColorPicker.Color = color.Value;
                        ForegroundColorPicker.IsVisible = true;
                    }

                    break;
                }
            }
                    
            // Background
            switch (_selectedControl)
            {
                case Panel panel:
                {
                    var color = GetColor(panel.Background);
                    if (color is not null)
                    {
                        BackgroundColorPicker.Color = color.Value;
                        BackgroundColorPicker.IsVisible = true;
                    }
                    break;
                }
                case ContentControl contentControl:
                {
                    var color = GetColor(contentControl.Background);
                    if (color is not null)
                    {
                        BackgroundColorPicker.Color = color.Value;
                        BackgroundColorPicker.IsVisible = true;
                    }
                    break;
                }
                case TemplatedControl templatedControl:
                {
                    var color = GetColor(templatedControl.Background);
                    if (color is not null)
                    {
                        BackgroundColorPicker.Color = color.Value;
                        BackgroundColorPicker.IsVisible = true;
                    }
                    break;
                }
                case TextBlock textBlock:
                {
                    var color = GetColor(textBlock.Background);
                    if (color is not null)
                    {
                        BackgroundColorPicker.Color = color.Value;
                        BackgroundColorPicker.IsVisible = true;
                    }
                    break;
                }
            }
                    
        }
        else
        {
            ForegroundColorPicker.IsVisible = false;
            BackgroundColorPicker.IsVisible = false;

            _selectedControl = null; 
        }
    }

    private void ForegroundColorPickerOnColorChanged(object? sender, ColorChangedEventArgs e)
    {
        if (_selectedControl is null)
        {
            return;
        }

        var color = e.NewColor;

        switch (_selectedControl)
        {
            case ContentControl contentControl:
            {
                contentControl.Foreground = new SolidColorBrush(color);
                break;
            }
            case TemplatedControl templatedControl:
            {
                templatedControl.Foreground = new SolidColorBrush(color);
                break;
            }
            case TextBlock textBlock:
            {
                textBlock.Foreground = new SolidColorBrush(color);
                break;
            }
        }
    }

    private void BackgroundColorPickerOnColorChanged(object? sender, ColorChangedEventArgs e)
    {
        if (_selectedControl is null)
        {
            return;
        }

        var color = e.NewColor;

        switch (_selectedControl)
        {
            case Panel panel:
            {
                panel.Background = new SolidColorBrush(color);
                break;
            }
            case ContentControl contentControl:
            {
                contentControl.Background = new SolidColorBrush(color);
                break;
            }
            case TemplatedControl templatedControl:
            {
                templatedControl.Background = new SolidColorBrush(color);
                break;
            }
            case TextBlock textBlock:
            {
                textBlock.Background = new SolidColorBrush(color);
                break;
            }
        }
    }

}

