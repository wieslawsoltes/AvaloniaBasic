using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Threading;

namespace BoundsDemo;

public partial class MainWindow : Window
{
    private readonly ToolBoxViewModel _toolBoxViewModel;

    public MainWindow()
    {
        InitializeComponent();

        PropertiesEditor.EnableEditing += (_, _) =>
        {
            if (DataContext is ToolBoxViewModel toolBoxViewModel)
            {
                toolBoxViewModel.EnableEditing = true;
            }
        };

        UpdatePropertiesEditor(null);

        LayersTreeView.SelectionChanged += LayersTreeViewOnSelectionChanged;

        _toolBoxViewModel = new ToolBoxViewModel();
        _toolBoxViewModel.ControlAdded += ToolBoxViewModelOnControlAdded;
        _toolBoxViewModel.ControlRemoved += ToolBoxViewModelOnControlRemoved;
        _toolBoxViewModel.SelectedChanged += ToolBoxViewModelOnSelectedChanged;

        DataContext = _toolBoxViewModel;
    }

    private void LayersTreeViewOnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (LayersTreeView.SelectedItem is XamlItem xamlItem)
        {
            if (_toolBoxViewModel.TryGetControl(xamlItem, out var control))
            {
                OverlayView.Select(control);
            }
        }
    }

    private void ToolBoxViewModelOnControlAdded(object? sender, EventArgs e)
    {
        // TODO:

        var children = Enumerable.Repeat(_toolBoxViewModel.RootXamlItem, 1);

        LayersTreeView.ItemsSource = children;
    }

    private void ToolBoxViewModelOnControlRemoved(object? sender, EventArgs e)
    {
        // TODO:
    }

    private void ToolBoxViewModelOnSelectedChanged(object? o, EventArgs eventArgs)
    {
        UpdatePropertiesEditor(_toolBoxViewModel.Selected);

        Dispatcher.UIThread.Post(() => PropertiesEditor.OnEnableEditing());

        if (_toolBoxViewModel.Selected is not null)
        {
            if (_toolBoxViewModel.Selected is Control selected)
            {
                _toolBoxViewModel.TryGetXamlItem(selected, out var xamlItem);
                LayersTreeView.SelectedItem = xamlItem;
            }
            else
            {
                LayersTreeView.SelectedItem = null;
            }
        }
        else
        {
            LayersTreeView.SelectedItem = null;
        }
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        Focus();
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);

        if (e.Source is TextBox)
        {
            return;
        }

        switch (e.Key)
        {
            case Key.Escape:
                OverlayView.Hover(null);
                OverlayView.Select(null);
                break;
            case Key.L:
                OverlayView.HitTestMode = HitTestMode.Logical;
                OverlayView.Hover(null);
                OverlayView.Select(null);
                break;
            case Key.V:
                OverlayView.HitTestMode = HitTestMode.Visual;
                OverlayView.Hover(null);
                OverlayView.Select(null);
                break;
            case Key.H:
                EditorCanvas.RootPanel.IsHitTestVisible = !EditorCanvas.RootPanel.IsHitTestVisible;
                break;
            case Key.R:
                EditorCanvas.ReverseOrder = !EditorCanvas.ReverseOrder;
                OverlayView.Hover(null);
                OverlayView.Select(null);
                break;
            case Key.Delete:
            case Key.Back:
            {
                if (_toolBoxViewModel.Selected is Control control)
                {
                    OverlayView.Hover(null);
                    OverlayView.Select(null);

                    // TODO:
                    //EditorCanvas.EditablePanel.Children.Remove(control);

                    // TODO:
                    _toolBoxViewModel.RemoveControl(control);
                }
                break;
            }
        }
    }

    private void SelectedOnPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        OverlayView.InvalidateVisual();
    }

    private void UpdatePropertiesEditor(Visual? selected)
    {
        if (PropertiesEditor.Selected is not null)
        {
            PropertiesEditor.Selected.PropertyChanged -= SelectedOnPropertyChanged;
        }

        if (selected is not null)
        {
            selected.PropertyChanged += SelectedOnPropertyChanged;
        }

        if (DataContext is ToolBoxViewModel toolBoxViewModel)
        {
            toolBoxViewModel.EnableEditing = false;
        }

        PropertiesEditor.Selected = selected;
        PropertiesEditor.UpdatePropertiesEditor();
    }
}
