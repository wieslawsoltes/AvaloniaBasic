using System.Collections.Generic;
using XamlDom;

namespace FormsBuilder;

public interface IControlMap<T>
{
    Dictionary<string, XamlItem> CreateMap(XamlItem xamlItem);

    Dictionary<string, T> CreateMap(T control);
}
