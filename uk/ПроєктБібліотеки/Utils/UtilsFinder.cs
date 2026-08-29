using Terraria.UI;

namespace UIeXtension.Utils;

/// <summary>
///     Статичні методи, пов'язані з пошуком елементів
/// </summary>
public static class UtilsFinder
{
    /// <summary>
    ///     Повернення кореня <see cref="UIElement"/> елемент-дерев
    /// </summary>
    public static UIElement GetRootParent(UIElement element)
    {
        if (element.Parent is null)
            return element;

        return GetRootParent(element.Parent);
    }
}