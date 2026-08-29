namespace UIeXtension.Styles;

public partial class StyleLayoutContainer : Base.StyleLayoutContainerBase
{
    /// <summary>
    ///     Зберігайте інші стилі контейнерного макета, дані бібліотеки або користувацькі стилі.
    /// </summary>
    private readonly System.Collections.Generic.Dictionary<System.Type, Base.StyleLayoutContainerBase> _styles = new();

    /// <summary>
    ///     Повертає (відтворює до цього, при необхідності) стиль макета <see cref="UIExCanvas"/>
    /// </summary>
    public StyleLayoutContainerCanvas Canvas()
        => GetOrCreateStyle<StyleLayoutContainerCanvas>();

    /// <summary>
    ///     Повертає (відтворює до цього, при необхідності) стиль макета <see cref="UIExStackPanel"/>
    /// </summary>
    public StyleLayoutContainerStackPanel StackPanel()
        => GetOrCreateStyle<StyleLayoutContainerStackPanel>();


    /// <summary>
    ///     Повертає (відтворює до цього, при необхідності) стиль макета <see cref="UIExDockPanel"/>
    /// </summary>
    public StyleLayoutContainerDockPanel DockPanel()
        => GetOrCreateStyle<StyleLayoutContainerDockPanel>();

    /// <summary>
    ///     Повертає (відтворює до цього, при необхідності) стиль макета <see cref="UIExWrapPanel"/>
    /// </summary>
    public StyleLayoutContainerWrapPanel WrapPanel()
        => GetOrCreateStyle<StyleLayoutContainerWrapPanel>();

    /// <summary>
    ///     Повертає (відтворює до цього, при необхідності) стиль макета <see cref="UIExGrid"/>
    /// </summary>
    public StyleLayoutContainerGrid Grid()
        => GetOrCreateStyle<StyleLayoutContainerGrid>();

    /// <summary>
    ///     Повертає (відтворює до цього, при необхідності) стиль макета <see cref="UIExUniformGrid"/>
    /// </summary>
    public StyleLayoutContainerUniformGrid UniformGrid()
        => GetOrCreateStyle<StyleLayoutContainerUniformGrid>();


    /// <summary>
    ///     Повертає (відтворює до цього, при необхідності) стиль перевантаження
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