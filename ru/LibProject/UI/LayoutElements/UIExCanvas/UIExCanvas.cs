namespace UIeXtension;

/// <summary>
///     Позволяет задавать абсолютное позиционирование по заданным параметрам.
/// </summary>
public partial class UIExCanvas : UIExLayout
{
    /// <inheritdoc/>
    public UIExCanvas() : this(new Styles.StyleVisualElement()) { }

    /// <inheritdoc/>
    public UIExCanvas(Styles.StyleVisualElement styleVisual) : this(styleVisual, new Styles.StyleLayoutContainer()) { }

    /// <inheritdoc/>
    public UIExCanvas(Styles.StyleLayoutContainer styleLayout) : this(new Styles.StyleVisualElement(), styleLayout) { }

    /// <inheritdoc/>
    public UIExCanvas(Styles.StyleVisualElement styleVisual, Styles.StyleLayoutContainer styleLayout) : base(styleVisual, styleLayout) { }
}
