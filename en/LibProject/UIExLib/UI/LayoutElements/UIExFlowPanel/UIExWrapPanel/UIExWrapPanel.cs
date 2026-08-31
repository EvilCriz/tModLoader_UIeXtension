namespace UIeXtension;

/// <summary>
///     It builds child elements in one line after another (vertical or horizontal).
///     Transfers elements to the next line if they do not fit in the previous one.
/// </summary>
public partial class UIExWrapPanel : UIExFlowPanel
{
    /// <inheritdoc/>
    public UIExWrapPanel()
        : this(
              new Styles.StyleVisualElement(),
              new Styles.StyleLayoutContainer())
    { }

    /// <inheritdoc/>
    public UIExWrapPanel(Styles.StyleVisualElement styleVisual)
        : this(
              styleVisual,
              new Styles.StyleLayoutContainer())
    { }

    /// <inheritdoc/>
    public UIExWrapPanel(Styles.StyleLayoutContainer styleLayout)
        : this(
              new Styles.StyleVisualElement(),
              styleLayout)
    { }

    /// <inheritdoc/>
    public UIExWrapPanel(
        Styles.StyleVisualElement styleVisual,
        Styles.StyleLayoutContainer styleLayout)
        : base(styleVisual, styleLayout) { }
}