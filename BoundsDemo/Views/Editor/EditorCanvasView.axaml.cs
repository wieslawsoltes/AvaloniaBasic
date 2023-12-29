using System.Reactive;
using Avalonia;
using Avalonia.Controls;

namespace BoundsDemo;

public partial class EditorCanvasView : UserControl
{
    public static readonly StyledProperty<OverlayView> OverlayViewProperty = 
        AvaloniaProperty.Register<EditorCanvasView, OverlayView>(nameof(OverlayView));

    internal CanvasViewModel? _canvasViewModel;

    public OverlayView OverlayView
    {
        get => GetValue(OverlayViewProperty);
        set => SetValue(OverlayViewProperty, value);
    }

    public EditorCanvasView()
    {
        InitializeComponent();
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);

        _canvasViewModel = new CanvasViewModel(this, OverlayView, RootPanel);

        Focusable = true;

        RootPanel.GetObservable(Panel.IsHitTestVisibleProperty).Subscribe(new AnonymousObserver<bool>(x =>
        {
            if (OverlayView is not null)
            {
                OverlayView.Hover(null);
                OverlayView.Select(null);
            }
        }));

        if (DataContext is MainViewViewModel mainViewModel)
        {
            var control = mainViewModel.Demo();

            if (control is not null)
            {
                _canvasViewModel.AddRoot(control);
            }
        }
    }
}

