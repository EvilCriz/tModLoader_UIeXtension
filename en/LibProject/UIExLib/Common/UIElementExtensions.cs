using Terraria.UI;

namespace UIeXtension.MethodsExtensions;

/// <summary>
///     Adding methods of expansion to <see cref="UIElement"/>
/// </summary>
public static class UIExUIElementExtensions
{
    /// <summary>
    ///     Styles are kept. <see cref="Styles.StyleLayoutChild"/> Compound elements inside the container.
    /// </summary>
    private static readonly System.Runtime.CompilerServices.
        ConditionalWeakTable<UIElement, Styles.StyleLayoutChild> _styles = new();

    /// <summary>
    ///     Extension method <see cref="UIElement"/>. 
    ///     Returns (creates if necessary) <see cref="Styles.StyleLayoutChild"/> table <see cref="_styles"/>
    /// </summary>
    public static Styles.StyleLayoutChild StyleLayoutChild(this UIElement element)
        => _styles.GetValue(element, _ => new Styles.StyleLayoutChild());

    /// <summary>
    ///     Extension method <see cref="UIElement"/>. 
    ///     Setting the style <see cref="Styles.StyleLayoutChild"/>
    /// </summary>
    public static void SetStyleLayoutChild(
        this UIElement element,
        Styles.StyleLayoutChild style)
    {
        _styles.Remove(element);
        _styles.Add(element, style);
    }
}