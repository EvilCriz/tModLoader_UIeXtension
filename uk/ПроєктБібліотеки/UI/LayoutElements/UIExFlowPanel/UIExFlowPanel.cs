namespace UIeXtension;

public abstract partial class UIExFlowPanel
{
    /// <inheritdoc/>
    public UIExFlowPanel()
        : this(
              new Styles.StyleVisualElement(),
              new Styles.StyleLayoutContainer())
    { }

    /// <inheritdoc/>
    public UIExFlowPanel(Styles.StyleVisualElement styleVisual)
        : this(
              styleVisual,
              new Styles.StyleLayoutContainer())
    { }

    /// <inheritdoc/>
    public UIExFlowPanel(Styles.StyleLayoutContainer styleLayout)
        : this(
              new Styles.StyleVisualElement(),
              styleLayout)
    { }

    /// <inheritdoc/>
    public UIExFlowPanel(
        Styles.StyleVisualElement styleVisual,
        Styles.StyleLayoutContainer styleLayout)
        : base(styleVisual, styleLayout) { }
}