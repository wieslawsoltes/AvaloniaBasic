using System.Collections.ObjectModel;

namespace DragAdornerDemo;

public class MainViewModel
{
    public ObservableCollection<Item> Items { get; set; }

    public MainViewModel()
    {
        Items = new ObservableCollection<Item>()
        {
            new Item() {Name = "Item 1"},
            new Item() {Name = "Item 2"},
            new Item() {Name = "Item 3"},
            new Item() {Name = "Item 4"},
            new Item() {Name = "Item 5"},
        };
    }
}
