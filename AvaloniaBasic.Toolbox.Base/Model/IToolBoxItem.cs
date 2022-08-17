namespace AvaloniaBasic.Model;

public interface IToolBoxItem
{
    string? Name { get; set; }

    string? Group { get; set; }

    string? Icon { get; set; }

    bool IsExpanded { get; set; }
}
