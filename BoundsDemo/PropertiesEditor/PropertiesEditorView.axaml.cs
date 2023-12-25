using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Layout;

namespace BoundsDemo;

public partial class PropertiesEditorView : UserControl
{
    public static readonly StyledProperty<Visual?> SelectedProperty = 
        AvaloniaProperty.Register<PropertiesEditorView, Visual?>(nameof(Selected));

    public Visual? Selected
    {
        get => GetValue(SelectedProperty);
        set => SetValue(SelectedProperty, value);
    }

    public event EventHandler? EnableEditing;

    public PropertiesEditorView()
    {
        InitializeComponent();
    }

    public virtual void OnEnableEditing()
    {
        EnableEditing?.Invoke(this, EventArgs.Empty);
    }

    public void UpdatePropertiesEditor()
    {
        if (Selected is not null)
        {
            VisualProperties.UpdateVisualProperties();
            VisualProperties.IsVisible = true;
        }
        else
        {
            VisualProperties.IsVisible = false;
        }

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

        if (Selected is TextBlock)
        {
            TextBlockProperties.UpdateTextBlockProperties();
            TextBlockProperties.IsVisible = true;
        }
        else
        {
            TextBlockProperties.IsVisible = false;
        }

        if (Selected is TextBox)
        {
            TextBoxProperties.UpdateTextBoxProperties();
            TextBoxProperties.IsVisible = true;
        }
        else
        {
            TextBoxProperties.IsVisible = false;
        }

        if (Selected is TemplatedControl)
        {
            TemplatedControlProperties.UpdateTemplatedControlProperties();
            TemplatedControlProperties.IsVisible = true;
        }
        else
        {
            TemplatedControlProperties.IsVisible = false;
        }

        if (Selected is Shape)
        {
            ShapeProperties.UpdateShapeProperties();
            ShapeProperties.IsVisible = true;
        }
        else
        {
            ShapeProperties.IsVisible = false;
        }
    }
}

