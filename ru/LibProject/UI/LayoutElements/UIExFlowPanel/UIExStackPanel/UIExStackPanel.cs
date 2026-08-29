namespace UIeXtension;

/// <summary>
///     Выстраивает дочерние элементы в одну линию друг за другом (по вертикали или горизонтали)
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