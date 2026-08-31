namespace UIeXtension.Styles;

public partial class StyleLayoutContainer : Base.StyleLayoutContainerBase
{
    /// <summary>
    ///     Stores other container layout styles, library data or custom styles.
    /// </summary>
    private readonly System.Collections.Generic.Dictionary<System.Type, Base.StyleLayoutContainerBase> _styles = new();

    /// <summary>
    ///     Returns (creates before this, if necessary) the layout style for <see cref="UIExCanvas"/>
    /// </summary>
    public StyleLayoutContainerCanvas Canvas()
        => GetOrCreateStyle<StyleLayoutContainerCanvas>();

    /// <summary>
    ///     Returns (creates before this, if necessary) the layout style for <see cref="UIExStackPanel"/>
    /// </summary>
    public StyleLayoutContainerStackPanel StackPanel()
        => GetOrCreateStyle<StyleLayoutContainerStackPanel>();


    /// <summary>
    ///     Returns (creates before this, if necessary) the layout style for <see cref="UIExDockPanel"/>
    /// </summary>
    public StyleLayoutContainerDockPanel DockPanel()
        => GetOrCreateStyle<StyleLayoutContainerDockPanel>();

    /// <summary>
    ///     Returns (creates before this, if necessary) the layout style for <see cref="UIExWrapPanel"/>
    /// </summary>
    public StyleLayoutContainerWrapPanel WrapPanel()
        => GetOrCreateStyle<StyleLayoutContainerWrapPanel>();

    /// <summary>
    ///     Returns (creates before this, if necessary) the layout style for <see cref="UIExGrid"/>
    /// </summary>
    public StyleLayoutContainerGrid Grid()
        => GetOrCreateStyle<StyleLayoutContainerGrid>();

    /// <summary>
    ///     Returns (creates before this, if necessary) the layout style for <see cref="UIExUniformGrid"/>
    /// </summary>
    public StyleLayoutContainerUniformGrid UniformGrid()
        => GetOrCreateStyle<StyleLayoutContainerUniformGrid>();


    /// <summary>
    ///     Returns (creates before this, if necessary) the transferred layout style
    /// </summary>
    public T GetOrCreateStyle<T>() where T : Base.StyleLayoutContainerBase, new()
    {
        if (!_styles.TryGetValue(typeof(T), out Base.StyleLayoutContainerBase value))
        {
            value = new T();
            _styles.Add(typeof(T), value);
        }

        return (T)value;
    }


    /// <inheritdoc/>
    protected override sealed void CopyInnerStylesBase(Base.StyleBase other)
    {
        if (other is StyleLayoutContainer style)
        {
            foreach (var key in style._styles.Keys)
                this._styles[key] = (Base.StyleLayoutContainerBase)style._styles[key].GetCopyBase();
        }
    }



    /// <inheritdoc/>
    protected override sealed bool EqualsInnerStylesFields(Base.StyleBase other)
    {
        if (other is null)
            return false;

        if (other is StyleLayoutContainer outerContainer)
            return EqualsInnerStylesFields<Base.StyleLayoutContainerBase>(_styles, outerContainer._styles);

        return false;
    }
}