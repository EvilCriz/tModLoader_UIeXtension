namespace UIeXtension.Styles;

public partial class StyleLayoutChild
{
    /// <summary>
    ///     Хранит другие стили компоновки элементов внутри контейнера. Данной библиотеки или пользовательские.
    /// </summary>
    private readonly System.Collections.Generic.Dictionary<System.Type, Base.StyleLayoutChildBase> _styles = new();



    /// <summary>
    ///     Возвращает (создает перед этим при необходимости) стиль компоновки для <see cref="UIExStackPanel"/>
    /// </summary>
    public StyleLayoutChildCanvas Canvas()
        => GetOrCreateStyle<StyleLayoutChildCanvas>();

    /// <summary>
    ///     Возвращает (создает перед этим при необходимости) стиль компоновки для <see cref="UIExDockPanel"/>
    /// </summary>
    public StyleLayoutChildDockPanel DockPanel()
        => GetOrCreateStyle<StyleLayoutChildDockPanel>();

    /// <summary>
    ///     Возвращает (создает перед этим при необходимости) стиль компоновки для <see cref="UIExGrid"/>
    /// </summary>
    public StyleLayoutChildGrid Grid()
        => GetOrCreateStyle<StyleLayoutChildGrid>();

    /// <summary>
    ///     Возвращает (создает перед этим при необходимости) стиль компоновки для <see cref="UIExUniformGrid"/>
    /// </summary>
    public StyleLayoutChildUniformGrid UniformGrid()
        => GetOrCreateStyle<StyleLayoutChildUniformGrid>();


    /// <summary>
    ///     Возвращает (создает перед этим при необходимости) переданный стиль компоновки элементов внутри контейнера
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