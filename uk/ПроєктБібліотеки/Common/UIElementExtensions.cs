using Terraria.UI;

namespace UIeXtension.MethodsExtensions;

/// <summary>
///     Додавання методів розширення до <see cref="UIElement"/>
/// </summary>
public static class UIExUIElementExtensions
{
    /// <summary>
    ///     Стилі зберігаються. <see cref="Styles.StyleLayoutChild"/> З'єднувальні елементи всередині контейнера.
    /// </summary>
    private static readonly System.Runtime.CompilerServices.
        ConditionalWeakTable<UIElement, Styles.StyleLayoutChild> _styles = new();

    /// <summary>
    ///     Метод розширення <see cref="UIElement"/>. . 
    ///     Повернення (відтворюється при необхідності) <see cref="Styles.StyleLayoutChild"/> стільниця <see cref="_styles"/>
    /// </summary>
    public static Styles.StyleLayoutChild StyleLayoutChild(this UIElement element)
        => _styles.GetValue(element, _ => new Styles.StyleLayoutChild());

    /// <summary>
    ///     Метод розширення <see cref="UIElement"/>. . 
    ///     Налаштування стилю <see cref="Styles.StyleLayoutChild"/>
    /// </summary>
    public static void SetStyleLayoutChild(
        this UIElement element,
        Styles.StyleLayoutChild style)
    {
        _styles.Remove(element);
        _styles.Add(element, style);
    }
}