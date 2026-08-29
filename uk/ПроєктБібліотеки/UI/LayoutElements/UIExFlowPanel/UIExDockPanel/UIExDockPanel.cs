namespace UIeXtension;

/// <summary>
///     Компонент. 
///     Дозволяє зав'язувати дочірні елементи до одного з її країв (верхова, низова, ліва або права сторона)
/// </summary>
public partial class UIExDockPanel : UIExFlowPanel
{
    /// <inheritdoc/>
    public UIExDockPanel() : this(new Styles.StyleVisualElement()) { }

    /// <inheritdoc/>
    public UIExDockPanel(Styles.StyleVisualElement styleVisual) : this(styleVisual, new Styles.StyleLayoutContainer()) { }

    /// <inheritdoc/>
    public UIExDockPanel(Styles.StyleLayoutContainer styleLayout) : this(new Styles.StyleVisualElement(), styleLayout) { }

    /// <inheritdoc/>
    public UIExDockPanel(Styles.StyleVisualElement styleVisual, Styles.StyleLayoutContainer styleLayout) : base(styleVisual, styleLayout) { }
}