using Avalonia;
using Avalonia.Controls;

namespace BoundsDemo;

public partial class StyledElementProperties : UserControl
{
    public static readonly StyledProperty<Visual?> SelectedProperty = 
        AvaloniaProperty.Register<StyledElementProperties, Visual?>(nameof(Selected));

    private bool _isUpdating;

    public Visual? Selected
    {
        get => GetValue(SelectedProperty);
        set => SetValue(SelectedProperty, value);
    }

    public StyledElementProperties()
    {
        InitializeComponent();
    }

    public void UpdateControlProperties()
    {
        _isUpdating = true;
        
        if (Selected is StyledElement styledElement)
        {
            // TODO:
        }

        _isUpdating = false;
    }
}

