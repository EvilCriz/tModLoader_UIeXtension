namespace UIeXtension;

/// <summary>
///     The basic class for the elements of the layout of this library.
/// </summary>
/// <remarks>
///     Responsible for: 
///     Basic definition of the life cycle.
///     Tools for Adapting to Restrictions tModLoader.
/// </remarks>
public abstract partial class UIExLayout : UIExElement
{
    /// <summary>
    ///     Default constructor. Uses default style.
    /// </summary>
    /// <remarks>
    ///     Default visual display style: <see cref="Styles.StyleVisualElement"/>.
    ///     Container layout style by default: <see cref="Styles.StyleLayoutContainer"/>
    /// </remarks>
    public UIExLayout() : this(new Styles.StyleVisualElement()) { }

    /// <summary>
    ///     Designer that adopts only visual display style. Uses the default container layout style.
    /// </summary>
    /// <remarks>
    ///     Container layout style by default: <see cref="Styles.StyleLayoutContainer"/>
    /// </remarks>
    /// <param name="styleVisual">
    ///     Visual display style
    /// </param>
    public UIExLayout(Styles.StyleVisualElement styleVisual) : this(styleVisual, new Styles.StyleLayoutContainer()) { }

    /// <summary>
    ///     Constructor that accepts only the container layout style, uses the default visual display style.
    /// </summary>
    /// <remarks>
    ///     Default visual display style: <see cref="Styles.StyleVisualElement"/>
    /// </remarks>
    /// <param name="styleLayout">
    ///     Container layout style
    /// </param>
    public UIExLayout(Styles.StyleLayoutContainer styleLayout) : this(new Styles.StyleVisualElement(), styleLayout) { }

    /// <summary>
    ///     A constructor that accepts all the styles that this class supports.
    /// </summary>
    /// <param name="styleVisual">Visual display style</param>
    /// <param name="styleLayout">Container layout style</param>
    public UIExLayout(Styles.StyleVisualElement styleVisual, Styles.StyleLayoutContainer styleLayout) : base(styleVisual)
        => StyleLayout = styleLayout;
}