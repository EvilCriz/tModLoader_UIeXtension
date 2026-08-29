using Terraria.UI;

namespace UIeXtension.MethodsExtensions;

/// <summary>
///     Добавляет методы расширения для <see cref="UIElement"/>
/// </summary>
public static class UIExUIElementExtensions
{
    /// <summary>
    ///     Хранит стили <see cref="Styles.StyleLayoutChild"/> компоновки элементов внутри контейнера.
    /// </summary>
    private static readonly System.Runtime.CompilerServices.
        ConditionalWeakTable<UIElement, Styles.StyleLayoutChild> _styles = new();

    /// <summary>
    ///     Метод расширения <see cref="UIElement"/>. 
    ///     Возвращает (создает при необходимости) <see cref="Styles.StyleLayoutChild"/> из таблицы <see cref="_styles"/>
    /// </summary>
    public static Styles.StyleLayoutChild StyleLayoutChild(this UIElement element)
        => _styles.GetValue(element, _ => new Styles.StyleLayoutChild());

    /// <summary>
    ///     Метод расширения <see cref="UIElement"/>. 
    ///     Устанавливает стиль <see cref="Styles.StyleLayoutChild"/>
    /// </summary>
    public static void SetStyleLayoutChild(
        this UIElement element,
        Styles.StyleLayoutChild style)
    {
        _styles.Remove(element);
        _styles.Add(element, style);
    }
}