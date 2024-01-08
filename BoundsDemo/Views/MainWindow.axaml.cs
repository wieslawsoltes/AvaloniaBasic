using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Threading;

namespace BoundsDemo;

public partial class MainWindow : Window
{
    private readonly KeyGesture? _copyGesture = Application.Current?.PlatformSettings?.HotkeyConfiguration.Copy.FirstOrDefault();
    private readonly KeyGesture? _cutGesture = Application.Current?.PlatformSettings?.HotkeyConfiguration.Cut.FirstOrDefault();
    private readonly KeyGesture? _pasteGesture = Application.Current?.PlatformSettings?.HotkeyConfiguration.Paste.FirstOrDefault();
    private MainViewViewModel? _mainViewViewModel;

    public MainWindow()
    {
        InitializeComponent();

        InitializePropertiesEditor();

        InitializeMainViewViewModel();
    }

    private void InitializePropertiesEditor()
    {
        PropertiesEditor.EnableEditing += (_, _) =>
        {
            if (DataContext is MainViewViewModel mainViewModel)
            {
                mainViewModel.EnableEditing = true;
            }
        };

        UpdatePropertiesEditor(new HashSet<Visual>());
    }

    private void InitializeMainViewViewModel()
    {
        _mainViewViewModel = new MainViewViewModel(EditorCanvas);
        _mainViewViewModel.ControlAdded += MainViewViewModelOnControlAdded;
        _mainViewViewModel.ControlRemoved += MainViewViewModelOnControlRemoved;
        _mainViewViewModel.SelectedChanged += MainViewViewModelOnSelectedChanged;

        DataContext = _mainViewViewModel;
    }

    private void MainViewViewModelOnControlAdded(object? sender, EventArgs e)
    {
        // TODO:
    }

    private void MainViewViewModelOnControlRemoved(object? sender, EventArgs e)
    {
        // TODO:
    }

    private void MainViewViewModelOnSelectedChanged(object? o, EventArgs eventArgs)
    {
        if (_mainViewViewModel is null)
        {
            return;
        }

        UpdatePropertiesEditor(_mainViewViewModel.Selected);

        Dispatcher.UIThread.Post(() => PropertiesEditor.OnEnableEditing());
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

        if (_copyGesture is not null && _copyGesture.Matches(e))
        {
            Copy();
            return;
        }
        else if (_cutGesture is not null && _cutGesture.Matches(e))
        {
            Cut();
            return;
        }
        else if (_pasteGesture is not null && _pasteGesture.Matches(e))
        {
            Paste();
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
                OverlayView.Hover(null);
                OverlayView.Select(null);
                break;
            case Key.R:
                // EditorCanvas.ReverseOrder = !EditorCanvas.ReverseOrder;
                // OverlayView.Hover(null);
                // OverlayView.Select(null);
                break;
            case Key.Delete:
            case Key.Back:
            {
                Delete();
                break;
            }
        }
    }

    private void Copy()
    {
        if (_mainViewViewModel is null)
        {
            return;
        }

        if (_mainViewViewModel.Selected.Count > 0)
        {
            _mainViewViewModel.CopySelected();
        }
    }

    private void Cut()
    {
        if (_mainViewViewModel is null)
        {
            return;
        }

        if (_mainViewViewModel.Selected.Count > 0)
        {
            _mainViewViewModel.CutSelected();
 
            OverlayView.Hover(null);
            OverlayView.Select(null);
        }
    }

    private void Paste()
    {
        if (_mainViewViewModel is null)
        {
            return;
        }

        _mainViewViewModel.PasteSelected();

        // TODO: Select pasted items.

        OverlayView.Hover(null);
        OverlayView.Select(null);
    }

    private void Delete()
    {
        if (_mainViewViewModel is null)
        {
            return;
        }

        if (_mainViewViewModel.Selected.Count > 0)
        {
            _mainViewViewModel.RemoveSelected();
 
            OverlayView.Hover(null);
            OverlayView.Select(null);
        }
    }

    private void SelectedOnPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        OverlayView.InvalidateVisual();
    }

    private void UpdatePropertiesEditor(HashSet<Visual> selected)
    {
        if (PropertiesEditor.Selected is not null)
        {
            PropertiesEditor.Selected.PropertyChanged -= SelectedOnPropertyChanged;
        }

        if (selected.Count > 0)
        {
            selected.First().PropertyChanged += SelectedOnPropertyChanged;
        }

        if (DataContext is MainViewViewModel mainViewModel)
        {
            mainViewModel.EnableEditing = false;
        }

        PropertiesEditor.Selected = selected.FirstOrDefault();
        PropertiesEditor.UpdatePropertiesEditor();
    }
}
