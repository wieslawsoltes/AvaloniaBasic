using Avalonia.Controls;

namespace DragAdornerDemo;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        DataContext = new MainViewModel();
    }
}