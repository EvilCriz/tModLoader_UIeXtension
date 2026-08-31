namespace UIeXtension;

/// <summary>
///     Lines the child elements in one line after another (vertical or horizontal)
/// </summary>
public partial class UIExStackPanel : UIExFlowPanel
{
    /// <inheritdoc/>
    public UIExStackPanel()
        : this(
              new Styles.StyleVisualElement(),
              new Styles.StyleLayoutContainer())
    { }

    /// <inheritdoc/>
    public UIExStackPanel(Styles.StyleVisualElement styleVisual)
        : this(
              styleVisual,
              new Styles.StyleLayoutContainer())
    { }

    /// <inheritdoc/>
    public UIExStackPanel(Styles.StyleLayoutContainer styleLayout)
        : this(
              new Styles.StyleVisualElement(),
              styleLayout)
    { }

    /// <inheritdoc/>
    public UIExStackPanel(
        Styles.StyleVisualElement styleVisual,
        Styles.StyleLayoutContainer styleLayout)
        : base(styleVisual, styleLayout) { }
}