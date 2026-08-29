using Terraria.UI;

namespace UIeXtension.Utils;

/// <summary>
///     Хранит статические методы, связанные с поиском элементов
/// </summary>
public static class UtilsFinder
{
    /// <summary>
    ///     Возвращает корневой <see cref="UIElement"/> древа элементов
    /// </summary>
    public static UIElement GetRootParent(UIElement element)
    {
        if (element.Parent is null)
            return element;

        return GetRootParent(element.Parent);
    }
}