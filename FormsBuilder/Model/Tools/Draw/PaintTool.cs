using Avalonia;
using Avalonia.Controls;

namespace FormsBuilder;

public class PaintTool : DrawTool
{
    private readonly IToolboxXamlItemProvider _toolboxXamlItemProvider;

    public PaintTool(IToolContext context, IToolboxXamlItemProvider toolboxXamlItemProvider) : base(context)
    {
        _toolboxXamlItemProvider = toolboxXamlItemProvider;
    }

    protected override XamlItem CreateXamlItem(IToolContext context)
    {
        var xamlItem = _toolboxXamlItemProvider.SelectedToolBoxItem;
        var xamlItemCopy = context.XamlFactory.Clone(xamlItem);
        return xamlItemCopy;
    }

    protected override void UpdateXamlItem(Point startPosition, Point endPosition, XamlItem xamlItem)
    {
        var rect = RectHelper.GetSelectionRect(startPosition, endPosition);
        xamlItem.Properties["Canvas.Left"] = rect.TopLeft.X;
        xamlItem.Properties["Canvas.Top"] = rect.TopLeft.Y;
        xamlItem.Properties["Width"] = rect.Width;
        xamlItem.Properties["Height"] = rect.Height;
    }

    protected override void UpdateControl(Point startPosition, Point endPosition, Control control)
    {
        var rect = RectHelper.GetSelectionRect(startPosition, endPosition);
        Canvas.SetLeft(control, rect.TopLeft.X);
        Canvas.SetTop(control, rect.TopLeft.Y);
        control.Width = rect.Width;
        control.Height = rect.Height;
    }
}
