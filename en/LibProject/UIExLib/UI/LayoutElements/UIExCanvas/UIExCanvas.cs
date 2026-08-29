namespace UIeXtension;

/// <summary>
///     Allows you to set absolute positioning according to specified parameters.
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
