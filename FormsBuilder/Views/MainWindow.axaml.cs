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
    private readonly List<KeyGesture>? _copyGesture = Application.Current?.PlatformSettings?.HotkeyConfiguration.Copy;
    private readonly List<KeyGesture>? _cutGesture = Application.Current?.PlatformSettings?.HotkeyConfiguration.Cut;
    private readonly List<KeyGesture>? _pasteGesture = Application.Current?.PlatformSettings?.HotkeyConfiguration.Paste;
    private readonly List<KeyGesture>? _duplicateGesture = new List<KeyGesture>()
    {
        new KeyGesture(Key.D, Application.Current.PlatformSettings.HotkeyConfiguration.CommandModifiers)
    };
    private readonly List<KeyGesture>? _selectAllGesture = Application.Current?.PlatformSettings?.HotkeyConfiguration.SelectAll;

    private MainViewViewModel? _mainViewViewModel;

    public MainWindow()
    {
        InitializeComponent();

        InitializePropertiesEditor();

        InitializeMainViewViewModel();
    }

    private bool Match(List<KeyGesture> gestures, KeyEventArgs e)
    {
        return gestures.Any(g => g.Matches(e));
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

        if (_copyGesture is not null && Match(_copyGesture, e))
        {
            Copy();
            return;
        }
        else if (_cutGesture is not null && Match(_cutGesture, e))
        {
            Cut();
            return;
        }
        else if (_pasteGesture is not null && Match(_pasteGesture, e))
        {
            Paste();
            return;
        }
        else if (_duplicateGesture is not null && Match(_duplicateGesture, e))
        {
            Copy();
            Paste();
            return;
        }
        else if (_selectAllGesture is not null && Match(_selectAllGesture, e))
        {
            SelectAll();
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

    private void SelectAll()
    {
        if (_mainViewViewModel is null)
        {
            return;
        }

        if (_mainViewViewModel.XamlEditor.RootXamlItem is not null)
        {
            _mainViewViewModel.XamlSelection.SelectItems(_mainViewViewModel.XamlEditor.RootXamlItem.Children);
        }
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
