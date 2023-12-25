using Avalonia;
using Avalonia.Controls;

namespace BoundsDemo;

public partial class VisualProperties : UserControl
{
    public static readonly StyledProperty<Visual?> SelectedProperty = 
        AvaloniaProperty.Register<LayoutableProperties, Visual?>(nameof(Selected));

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
            // TODO:
        }

        _isUpdating = false;
    }
}

