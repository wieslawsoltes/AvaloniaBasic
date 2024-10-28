using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using XamlDom;

namespace FormsBuilder;

public partial class EditorLayersView : UserControl
{
    private MainViewViewModel? _mainViewViewModel;

    public EditorLayersView()
    {
        InitializeComponent();

        LayersTreeView.SelectionChanged += LayersTreeViewOnSelectionChanged;
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);

        _mainViewViewModel = DataContext as MainViewViewModel;

        if (_mainViewViewModel is not null)
        {
            _mainViewViewModel.XamlEditor.ControlAdded += MainViewOnControlAdded;
            _mainViewViewModel.XamlEditor.ControlRemoved += MainViewOnControlRemoved;
            _mainViewViewModel.XamlSelection.SelectedChanged += MainViewOnSelectedChanged;
            _mainViewViewModel.XamlSelection.SelectedMoved += MainViewOnSelectedMoved;

            SetItemsSource();
        }
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);

        if (_mainViewViewModel is not null)
        {
            _mainViewViewModel.XamlEditor.ControlAdded -= MainViewOnControlAdded;
            _mainViewViewModel.XamlEditor.ControlRemoved -= MainViewOnControlRemoved;
            _mainViewViewModel.XamlSelection.SelectedChanged -= MainViewOnSelectedChanged;
            _mainViewViewModel.XamlSelection.SelectedMoved -= MainViewOnSelectedMoved;

            _mainViewViewModel = null;

            LayersTreeView.ItemsSource = null;
        }
    }

    private void SetItemsSource()
    {
        if (_mainViewViewModel is null)
        {
            return;
        }

        var children = Enumerable.Repeat(_mainViewViewModel.XamlEditor.RootXamlItem, 1);

        LayersTreeView.ItemsSource = children;
    }

    private void MainViewOnControlAdded(object? sender, EventArgs e)
    {
        if (_mainViewViewModel is null)
        {
            return;
        }

        // TODO:

        SetItemsSource();
    }

    private void MainViewOnControlRemoved(object? sender, EventArgs e)
    {
        if (_mainViewViewModel is null)
        {
            return;
        }

        // TODO:
    }

    private void MainViewOnSelectedChanged(object? o, EventArgs eventArgs)
    {
        if (_mainViewViewModel is null)
        {
            return;
        }

        if (_mainViewViewModel.XamlSelection.Selected.Count > 0)
        {
            if (_mainViewViewModel.XamlSelection.Selected.First() is Control selected)
            {
                _mainViewViewModel.XamlEditor.TryGetXamlItem(selected, out var xamlItem);
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

    private void MainViewOnSelectedMoved(object? sender, EventArgs e)
    {
        if (_mainViewViewModel is null)
        {
            return;
        }

        // TODO:
    }

    private void LayersTreeViewOnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (_mainViewViewModel is null)
        {
            return;
        }

        if (LayersTreeView.SelectedItem is XamlItem xamlItem)
        {
            if (_mainViewViewModel.XamlEditor.TryGetControl(xamlItem, out var control))
            {
                if (control is not null)
                {
                    _mainViewViewModel.XamlSelection.Select(Enumerable.Repeat(control, 1));
                }
            }
        }
    }
}
