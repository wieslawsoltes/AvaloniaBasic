using System;
using Avalonia.Controls;
using AvaloniaBasicDemo.ViewModels;

namespace AvaloniaBasicDemo.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        Opened += OnOpened;
    }

    private void OnOpened(object? sender, EventArgs e)
    {
        if (DataContext is MainViewModel mainViewModel)
        {
            mainViewModel.Tree.UpdateLogicalTree(DropAreaCanvas);
        }
    }
}
