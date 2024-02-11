using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace FormsBuilder;

public partial class DockPanelAttachedProperties : UserControl
{
    public static readonly StyledProperty<Visual?> SelectedProperty = 
        AvaloniaProperty.Register<DockPanelAttachedProperties, Visual?>(nameof(Selected));

    private bool _isUpdating;
    
    public Visual? Selected
    {
        get => GetValue(SelectedProperty);
        set => SetValue(SelectedProperty, value);
    }

    public DockPanelAttachedProperties()
    {
        InitializeComponent();
    }
    
    public void UpdateDockPanelAttachedProperties()
    {
        _isUpdating = true;
        
        if (Selected is Control control)
        {
            SetDock(DockPanel.GetDock(control));
        }

        _isUpdating = false;
    }

    private void SetDock(Dock? dock)
    {
        ButtonDockLeft.IsChecked = dock == Dock.Left;
        ButtonDockRight.IsChecked = dock == Dock.Right;
        ButtonDockTop.IsChecked = dock == Dock.Top;
        ButtonDockBottom.IsChecked = dock == Dock.Bottom;
    }

    private void ButtonDockLeft_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is Control control)
        {
            DockPanel.SetDock(control, Dock.Left);
            SetDock(Dock.Left);

            if (DataContext is MainViewViewModel mainViewModel)
            {
                mainViewModel.XamlEditorViewModel.UpdatePropertyValue(control, "(DockPanel.Dock)", "Left");
            }
        }
    }

    private void ButtonDockRight_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is Control control)
        {
            DockPanel.SetDock(control, Dock.Right);
            SetDock(Dock.Right);

            if (DataContext is MainViewViewModel mainViewModel)
            {
                mainViewModel.XamlEditorViewModel.UpdatePropertyValue(control, "(DockPanel.Dock)", "Right");
            }
        }
    }

    private void ButtonDockTop_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is Control control)
        {
            DockPanel.SetDock(control, Dock.Top);
            SetDock(Dock.Top);
            
            if (DataContext is MainViewViewModel mainViewModel)
            {
                mainViewModel.XamlEditorViewModel.UpdatePropertyValue(control, "(DockPanel.Dock)", "Top");
            }
        }
    }

    private void ButtonDockBottom_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Selected is Control control)
        {
            DockPanel.SetDock(control, Dock.Bottom);
            SetDock(Dock.Bottom);
            
            if (DataContext is MainViewViewModel mainViewModel)
            {
                mainViewModel.XamlEditorViewModel.UpdatePropertyValue(control, "(DockPanel.Dock)", "Bottom");
            }
        }
    }
}

