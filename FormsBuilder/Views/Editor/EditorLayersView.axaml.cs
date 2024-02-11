using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;

namespace FormsBuilder;

public partial class EditorLayersView : UserControl
{
    public static readonly StyledProperty<OverlayView> OverlayViewProperty = 
        AvaloniaProperty.Register<EditorLayersView, OverlayView>(nameof(OverlayView));

    private MainViewViewModel? _mainViewViewModel;
    
    public OverlayView OverlayView
    {
        get => GetValue(OverlayViewProperty);
        set => SetValue(OverlayViewProperty, value);
    }

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
            _mainViewViewModel.XamlEditorViewModel.ControlAdded += MainViewViewModelOnControlAdded;
            _mainViewViewModel.XamlEditorViewModel.ControlRemoved += MainViewViewModelOnControlRemoved;
            _mainViewViewModel.XamlSelectionViewModel.SelectedChanged += MainViewViewModelOnSelectedChanged;
            _mainViewViewModel.XamlSelectionViewModel.SelectedMoved += MainViewViewModelOnSelectedMoved;

            SetItemsSource();
        }
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);

        if (_mainViewViewModel is not null)
        {
            _mainViewViewModel.XamlEditorViewModel.ControlAdded -= MainViewViewModelOnControlAdded;
            _mainViewViewModel.XamlEditorViewModel.ControlRemoved -= MainViewViewModelOnControlRemoved;
            _mainViewViewModel.XamlSelectionViewModel.SelectedChanged -= MainViewViewModelOnSelectedChanged;
            _mainViewViewModel.XamlSelectionViewModel.SelectedMoved -= MainViewViewModelOnSelectedMoved;

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

        var children = Enumerable.Repeat(_mainViewViewModel.XamlEditorViewModel.RootXamlItem, 1);

        LayersTreeView.ItemsSource = children;
    }

    private void MainViewViewModelOnControlAdded(object? sender, EventArgs e)
    {
        if (_mainViewViewModel is null)
        {
            return;
        }

        // TODO:

        SetItemsSource();
    }

    private void MainViewViewModelOnControlRemoved(object? sender, EventArgs e)
    {
        if (_mainViewViewModel is null)
        {
            return;
        }

        // TODO:
    }

    private void MainViewViewModelOnSelectedChanged(object? o, EventArgs eventArgs)
    {
        if (_mainViewViewModel is null)
        {
            return;
        }

        if (_mainViewViewModel.XamlSelectionViewModel.Selected.Count > 0)
        {
            if (_mainViewViewModel.XamlSelectionViewModel.Selected.First() is Control selected)
            {
                _mainViewViewModel.XamlEditorViewModel.TryGetXamlItem(selected, out var xamlItem);
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

    private void MainViewViewModelOnSelectedMoved(object? sender, EventArgs e)
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
            if (_mainViewViewModel.XamlEditorViewModel.TryGetControl(xamlItem, out var control))
            {
                if (control is not null)
                {
                    OverlayView.Select(Enumerable.Repeat(control, 1));
                }
            }
        }
    }
}
