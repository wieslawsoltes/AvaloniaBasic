using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace FormsBuilder;

public interface IOverlayService
{
    Visual? Overlay { get; }
    Canvas Canvas { get; }
    void Invalidate();
}

public class DecoratorOverlayService : IOverlayService
{
    private readonly Action _invalidate;

    public DecoratorOverlayService(Visual? overlay, Canvas canvas, Action invalidate)
    {
        _invalidate = invalidate;
        Overlay = overlay;
        Canvas = canvas;
    }

    public Visual? Overlay { get; }
    
    public Canvas Canvas { get; }

    public void Invalidate() => _invalidate();
}

public class OverlayView : Decorator
{
    private readonly Canvas _canvas;
    private SelectionRenderer? _selectionRenderer;
    private MainViewViewModel? _mainViewModel;

    public OverlayView()
    {
        _canvas = new Canvas();
        Child = _canvas;
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);
        
        if (DataContext is MainViewViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            _mainViewModel.OverlayService = new DecoratorOverlayService(this, _canvas, InvalidateVisual);
            _selectionRenderer = new SelectionRenderer(_mainViewModel.XamlSelection);
        }
        else
        {
            if (_mainViewModel is not null)
            {
                _mainViewModel.OverlayService = null;
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
