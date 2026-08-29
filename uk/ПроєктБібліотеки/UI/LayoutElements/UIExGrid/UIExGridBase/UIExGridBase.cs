namespace UIeXtension;

public partial class UIExGridBase : UIExLayout
{
    /// <inheritdoc/>
    public UIExGridBase() : this(new Styles.StyleVisualElement()) { }

    /// <inheritdoc/>
    public UIExGridBase(Styles.StyleVisualElement styleVisual) : this(styleVisual, new Styles.StyleLayoutContainer()) { }

    /// <inheritdoc/>
    public UIExGridBase(Styles.StyleLayoutContainer styleLayout) : this(new Styles.StyleVisualElement(), styleLayout) { }

    /// <inheritdoc/>
    public UIExGridBase(Styles.StyleVisualElement styleVisual, Styles.StyleLayoutContainer styleLayout) : base(styleVisual, styleLayout) { }
}