using Terraria.UI;

namespace UIeXtension.Utils;

/// <summary>
///     Static methods associated with the search for elements
/// </summary>
public static class UtilsFinder
{
    /// <summary>
    ///     Returns the root <see cref="UIElement"/> element-wood
    /// </summary>
    public static UIElement GetRootParent(UIElement element)
    {
        if (element.Parent is null)
            return element;

        return GetRootParent(element.Parent);
    }
}