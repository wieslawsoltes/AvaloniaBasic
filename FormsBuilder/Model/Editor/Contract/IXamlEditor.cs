using System;
using XamlDom;

namespace FormsBuilder;

public interface IXamlEditor<T> where T : class
{
    event EventHandler<EventArgs>? PropertyValueChanged;
    event EventHandler<EventArgs>? ControlAdded;
    event EventHandler<EventArgs>? ControlRemoved;
    XamlItem? RootXamlItem { get; }
    bool EnableEditing { get; set; }
    ICanvasEditor<T>? CanvasViewModel { get; set; }
    void AddControl(T control, XamlItem xamlItem);
    void RemoveControl(T control);
    void InsertXamlItem(XamlItem targetXamlItem, XamlItem xamlItem, double x, double y, bool enableCallback);
    bool RemoveXamlItem(XamlItem xamlItem);
    bool TryGetXamlItem(T control, out XamlItem? xamlItem);
    bool TryGetControl(XamlItem xamlItem, out T? control);
    void UpdatePropertyValue(T control, string propertyName, XamlValue propertyValue);
    T? LoadForDesign(XamlItem xamlItem);
    T? LoadForExport(XamlItem xamlItem);
    void Reload(XamlItem rooXamlItem);
    // TODO:
    void Debug(XamlItem xamlItem);
}
