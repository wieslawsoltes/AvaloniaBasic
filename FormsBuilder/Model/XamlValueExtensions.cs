using System.Globalization;
using Avalonia;

namespace FormsBuilder;

public static class XamlValueExtensions
{
    public static XamlValue ToXamlValue(this Point value)
    {
        return string.Concat(
            value.X.ToString(CultureInfo.InvariantCulture), 
            ',', 
            value.Y.ToString(CultureInfo.InvariantCulture));
    }
}
