namespace UIeXtension;

/// <summary>
///     Побудує елементи дитини в одному рядку після іншої (вертичної або горизонтальної).
///     Передача елементів в наступну лінію, якщо вони не вписуються в попередній.
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