namespace UIeXtension;

/// <summary>
///     Выстраивает дочерние элементы в одну линию друг за другом (по вертикали или горизонтали).
///     Переносит элементы на следующую строку, если они не вмещаются в предыдущую.
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