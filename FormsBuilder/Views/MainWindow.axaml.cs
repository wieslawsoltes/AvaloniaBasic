using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Threading;

namespace FormsBuilder;

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
                mainViewModel.XamlEditor.EnableEditing = true;
            }
        };

        UpdatePropertiesEditor(new HashSet<Visual>());
    }

    private void InitializeMainViewViewModel()
    {
        _mainViewViewModel = new MainViewViewModel(EditorCanvas);
        _mainViewViewModel.XamlEditor.ControlAdded += MainViewOnControlAdded;
        _mainViewViewModel.XamlEditor.ControlRemoved += MainViewOnControlRemoved;
        _mainViewViewModel.XamlSelection.SelectedChanged += MainViewOnSelectedChanged;
        _mainViewViewModel.XamlSelection.SelectedMoved += MainViewOnSelectedMoved;

        DataContext = _mainViewViewModel;
    }

    private void MainViewOnControlAdded(object? sender, EventArgs e)
    {
        // TODO:
    }

    private void MainViewOnControlRemoved(object? sender, EventArgs e)
    {
        // TODO:
    }

    private void MainViewOnSelectedChanged(object? o, EventArgs eventArgs)
    {
        if (_mainViewViewModel is null)
        {
            return;
        }

        UpdatePropertiesEditor(_mainViewViewModel.XamlSelection.Selected);

        Dispatcher.UIThread.Post(() => PropertiesEditor.OnEnableEditing());
    }

    private void MainViewOnSelectedMoved(object? sender, EventArgs e)
    {
        if (_mainViewViewModel is null)
        {
            return;
        }

        UpdatePropertiesEditor(_mainViewViewModel.XamlSelection.Selected);

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
                _mainViewViewModel.XamlSelection.Hover(null);
                _mainViewViewModel.XamlSelection.Select(null);
                break;
            case Key.L:
                _mainViewViewModel.XamlSelection.HitTestMode = HitTestMode.Logical;
                _mainViewViewModel.XamlSelection.Hover(null);
                _mainViewViewModel.XamlSelection.Select(null);
                break;
            case Key.V:
                _mainViewViewModel.XamlSelection.HitTestMode = HitTestMode.Visual;
                _mainViewViewModel.XamlSelection.Hover(null);
                _mainViewViewModel.XamlSelection.Select(null);
                break;
            case Key.H:
                EditorCanvas.RootPanel.IsHitTestVisible = !EditorCanvas.RootPanel.IsHitTestVisible;
                _mainViewViewModel.XamlSelection.Hover(null);
                _mainViewViewModel.XamlSelection.Select(null);
                break;
            case Key.R:
                // EditorCanvas.ReverseOrder = !EditorCanvas.ReverseOrder;
                // _mainViewViewModel.XamlSelection.Hover(null);
                // _mainViewViewModel.XamlSelection.Select(null);
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

        if (_mainViewViewModel.XamlSelection.Selected.Count > 0)
        {
            _mainViewViewModel.XamlSelection.CopySelected();
        }
    }

    private void Cut()
    {
        if (_mainViewViewModel is null)
        {
            return;
        }

        if (_mainViewViewModel.XamlSelection.Selected.Count > 0)
        {
            _mainViewViewModel.XamlSelection.CutSelected();
 
            _mainViewViewModel.XamlSelection.Hover(null);
            _mainViewViewModel.XamlSelection.Select(null);
        }
    }

    private void Paste()
    {
        if (_mainViewViewModel is null)
        {
            return;
        }

        _mainViewViewModel.XamlSelection.PasteSelected();

        // TODO: Select pasted items.
        // _mainViewViewModel.XamlSelection.Hover(null);
        // _mainViewViewModel.XamlSelection.Select(null);
    }

    private void Delete()
    {
        if (_mainViewViewModel is null)
        {
            return;
        }

        if (_mainViewViewModel.XamlSelection.Selected.Count > 0)
        {
            _mainViewViewModel.XamlSelection.RemoveSelected();
 
            _mainViewViewModel.XamlSelection.Hover(null);
            _mainViewViewModel.XamlSelection.Select(null);
        }
    }

    private void SelectedOnPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        _mainViewViewModel.XamlSelection.InvalidateOverlay();
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
            mainViewModel.XamlEditor.EnableEditing = false;
        }

        PropertiesEditor.Selected = selected.FirstOrDefault();
        PropertiesEditor.UpdatePropertiesEditor();
    }
}
