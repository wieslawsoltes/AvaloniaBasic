using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace BoundsDemo;

public partial class VisualProperties : UserControl
{
    public static readonly StyledProperty<Visual?> SelectedProperty = 
        AvaloniaProperty.Register<VisualProperties, Visual?>(nameof(Selected));

    private bool _isUpdating;

    public Visual? Selected
    {
        get => GetValue(SelectedProperty);
        set => SetValue(SelectedProperty, value);
    }

    public VisualProperties()
    {
        InitializeComponent();
    }

    public void UpdateVisualProperties()
    {
        _isUpdating = true;
        
        if (Selected is { } visual)
        {
            SetIsVisible(visual.IsVisible);

            TextBoxOpacity.Text = visual.Opacity.ToString(CultureInfo.InvariantCulture);
        }

        _isUpdating = false;
    }

    private void SetIsVisible(bool isVisible)
    {
        SvgIsVisibleTrue.IsVisible = isVisible;
        SvgIsVisibleFalse.IsVisible = !isVisible;
    }

    private void ButtonIsVisible_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is { } visual)
        {
            visual.IsVisible = !visual.IsVisible;

            var isVisible = visual.IsVisible;
            SetIsVisible(isVisible);

            if (DataContext is ToolBoxViewModel toolBoxViewModel)
            {
                toolBoxViewModel.UpdatePropertyValue(visual as Control, "IsVisible", $"{isVisible}");
            }
        }
    }

    private void TextBoxOpacity_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (_isUpdating)
        {
            return;
        }

        if (Selected is { } visual)
        {
            var text = TextBoxOpacity.Text;
            var result = double.TryParse(text, CultureInfo.InvariantCulture, out var value);
            if (result)
            {
                visual.Opacity = value;
            
                if (DataContext is ToolBoxViewModel toolBoxViewModel)
                {
                    toolBoxViewModel.UpdatePropertyValue(visual as Control, "Opacity", text);
                }
            }
        }
    }
}

