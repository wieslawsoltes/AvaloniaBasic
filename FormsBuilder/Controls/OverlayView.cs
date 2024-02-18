using System;
using Avalonia.Controls;
using Avalonia.Media;

namespace FormsBuilder;

public class OverlayView : Decorator
{
    private SelectionRenderer? _selectionRenderer;
    
    public OverlayView()
    {
        Canvas = new Canvas();
        Child = Canvas;
    }

    private MainViewViewModel? _mainViewModel;

    public Canvas Canvas { get; }

    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);
        
        if (DataContext is MainViewViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            _mainViewModel.OverlayView = this;
            _selectionRenderer = new SelectionRenderer(_mainViewModel.XamlSelection);
        }
        else
        {
            if (_mainViewModel is not null)
            {
                _mainViewModel.OverlayView = null;
                _mainViewModel = null;
            }

            _selectionRenderer = null;
        }
    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);

        _selectionRenderer?.Render(context);
    }
}
