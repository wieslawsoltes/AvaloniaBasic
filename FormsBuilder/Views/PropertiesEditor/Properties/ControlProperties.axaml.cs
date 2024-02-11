using Avalonia;
using Avalonia.Controls;

namespace FormsBuilder;

public partial class ControlProperties : UserControl
{
    public static readonly StyledProperty<Visual?> SelectedProperty = 
        AvaloniaProperty.Register<ControlProperties, Visual?>(nameof(Selected));

    private bool _isUpdating;

    public Visual? Selected
    {
        get => GetValue(SelectedProperty);
        set => SetValue(SelectedProperty, value);
    }

    public ControlProperties()
    {
        InitializeComponent();
    }

    public void UpdateControlProperties()
    {
        _isUpdating = true;
        
        if (Selected is Control control)
        {
            // TODO:
        }

        _isUpdating = false;
    }
}

