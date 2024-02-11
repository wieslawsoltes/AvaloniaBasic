using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;

namespace FormsBuilder;

public partial class InputElementProperties : UserControl
{
    public static readonly StyledProperty<Visual?> SelectedProperty = 
        AvaloniaProperty.Register<InputElementProperties, Visual?>(nameof(Selected));

    private bool _isUpdating;

    public Visual? Selected
    {
        get => GetValue(SelectedProperty);
        set => SetValue(SelectedProperty, value);
    }

    public InputElementProperties()
    {
        InitializeComponent();
    }

    public void UpdateInputElementProperties()
    {
        _isUpdating = true;
        
        if (Selected is InputElement inputElement)
        {
            // TODO:
        }

        _isUpdating = false;
    }
}

