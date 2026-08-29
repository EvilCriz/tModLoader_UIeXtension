namespace UIeXtension;

/// <summary>
///     Component. 
///     Allows you to tie child elements to one of its edges (top, bottom, left or right side)
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