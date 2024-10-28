using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using XamlDom;

namespace FormsBuilder;

public interface IToolContext
{
    IXamlEditor<Control> XamlEditor { get; }

    IXamlItemFactory XamlItemFactory { get; }

    IXamlSelection XamlSelection { get; }

    IOverlayService OverlayService { get; }

    Control? Host { get; }

    Panel? RootPanel { get; }

    IEnumerable<Visual> HitTest(
        Interactive interactive,
        HitTestMode hitTestMode,
        HashSet<Visual> ignored,
        Func<TransformedBounds, bool> filter);
}
