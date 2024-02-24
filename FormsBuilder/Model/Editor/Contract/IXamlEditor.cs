using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.LogicalTree;

namespace FormsBuilder;

public interface IXamlEditor
{
    event EventHandler<EventArgs>? PropertyValueChanged;
    event EventHandler<EventArgs>? ControlAdded;
    event EventHandler<EventArgs>? ControlRemoved;
    XamlItem? RootXamlItem { get; }
    bool EnableEditing { get; set; }
    ICanvasEditor? CanvasViewModel { get; set; }
    IXamlItemIdManager IdManager { get; }
    void AddControl(Control control, XamlItem xamlItem);
    void RemoveControl(Control control);
    void InsertXamlItem(XamlItem targetXamlItem, XamlItem xamlItem, Point position, bool enableCallback);
    bool RemoveXamlItem(XamlItem xamlItem);
    bool TryGetXamlItem(Control control, out XamlItem? xamlItem);
    bool TryGetControl(XamlItem xamlItem, out Control? control);
    void UpdatePropertyValue(Control control, string propertyName, string propertyValue);
    Control? LoadForDesign(XamlItem xamlItem);
    Control? LoadForExport(XamlItem xamlItem);
    void Reload(XamlItem rooXamlItem);
    Control? HitTest(IEnumerable<Visual> descendants, Point position, HashSet<Visual> ignored);
    Control? HitTest(ILogical root, Point position, HashSet<Visual> ignored);
    // TODO:
    void Debug(XamlItem xamlItem);
}
