using Avalonia.Controls;
using AvaloniaBasic.Services;
using AvaloniaBasic.Services.PropertyEditors;
using AvaloniaBasic.ViewModels.Settings;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaBasic.ViewModels;

[ObservableObject]
public partial class MainViewModel
{
    [ObservableProperty] private ToolboxViewModel _toolbox;
    [ObservableProperty] private TreeViewModel _tree;
    [ObservableProperty] private DragSettingsViewModel _dragSettings;
    [ObservableProperty] private GridSettingsViewModel _gridSettings;
    [ObservableProperty] private Canvas? _previewCanvas;
    [ObservableProperty] private Canvas? _dropAreaCanvas;

    public MainViewModel()
    {
        var toolboxItemProvider = new DefaultToolboxItemProvider();

        _toolbox = new ToolboxViewModel(toolboxItemProvider);

        var propertyEditorFactory = new PropertyEditorFactory();
        propertyEditorFactory.Register(new BooleanPropertyEditor());
        propertyEditorFactory.Register(new StringPropertyEditor());
        propertyEditorFactory.Register(new EnumPropertyEditor());
        propertyEditorFactory.Register(new DefaultPropertyEditor());

        _tree = new TreeViewModel(propertyEditorFactory);

        _dragSettings = new DragSettingsViewModel();
        _gridSettings = new GridSettingsViewModel();
    }
}
