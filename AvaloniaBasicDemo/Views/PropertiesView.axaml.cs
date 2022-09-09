using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaBasicDemo.Views;

public partial class PropertiesView : UserControl
{
    public PropertiesView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}

