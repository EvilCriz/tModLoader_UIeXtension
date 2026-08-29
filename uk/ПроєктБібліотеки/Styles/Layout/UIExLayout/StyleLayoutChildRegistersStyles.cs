namespace UIeXtension.Styles;

public partial class StyleLayoutChild
{
    /// <summary>
    ///     Зберігати інші стилі макету елементів всередині контейнера, бібліотеки даних або на замовлення.
    /// </summary>
    private readonly System.Collections.Generic.Dictionary<System.Type, Base.StyleLayoutChildBase> _styles = new();



    /// <summary>
    ///     Повертає (відтворює до цього, при необхідності) стиль макета <see cref="UIExStackPanel"/>
    /// </summary>
    public StyleLayoutChildCanvas Canvas()
        => GetOrCreateStyle<StyleLayoutChildCanvas>();

    /// <summary>
    ///     Повертає (відтворює до цього, при необхідності) стиль макета <see cref="UIExDockPanel"/>
    /// </summary>
    public StyleLayoutChildDockPanel DockPanel()
        => GetOrCreateStyle<StyleLayoutChildDockPanel>();

    /// <summary>
    ///     Повертає (відтворює до цього, при необхідності) стиль макета <see cref="UIExGrid"/>
    /// </summary>
    public StyleLayoutChildGrid Grid()
        => GetOrCreateStyle<StyleLayoutChildGrid>();

    /// <summary>
    ///     Повертає (відтворює до цього, при необхідності) стиль макета <see cref="UIExUniformGrid"/>
    /// </summary>
    public StyleLayoutChildUniformGrid UniformGrid()
        => GetOrCreateStyle<StyleLayoutChildUniformGrid>();


    /// <summary>
    ///     Повернення (відтворюється до цього, при необхідності) передається стиль розташування елементів всередині контейнера
    /// </summary>
    public T GetOrCreateStyle<T>() where T : Base.StyleLayoutChildBase, new()
    {
        if (!_styles.TryGetValue(typeof(T), out Base.StyleLayoutChildBase value))
        {
            value = new T();
            _styles.Add(typeof(T), value);
        }

        return (T)value;
    }



    /// <inheritdoc/>
    protected override sealed void CopyInnerStylesBase(Base.StyleBase other)
    {
        if (other is StyleLayoutChild style)
        {
            foreach (var key in style._styles.Keys)
                this._styles[key] = (Base.StyleLayoutChildBase)style._styles[key].GetCopyBase();
        }
    }


    /// <inheritdoc/>
    protected override sealed bool EqualsInnerStylesFields(Base.StyleBase other)
    {
        if (other is null)
            return false;

        if (other is StyleLayoutChild outerChild)
            return EqualsInnerStylesFields<Base.StyleLayoutChildBase>(_styles, outerChild._styles);

        return false;
    }
}