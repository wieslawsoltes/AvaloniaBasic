namespace AvaloniaBasicDemo.Model;

public interface IToolBoxItem
{
    string? Name { get; set; }
    string? Icon { get; set; }
    bool IsExpanded { get; set; }
}
