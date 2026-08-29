using UIeXtension.Styles.TModLoader;

namespace UIeXtension.Styles;

public partial class StyleVisualElement
{
    /// <summary>
    ///     Хранит другие стили визуального отображения элементов. Данной библиотеки или пользовательские.
    /// </summary>
    private readonly System.Collections.Generic.Dictionary<System.Type, Base.StyleVisualBase> _styles = new();

    /// <summary>
    ///     Хранит стили визуального отображения элементов c визуальной частью аналогичной одному из UI элементов tModLoader. 
    ///     Данной библиотеки или пользовательские.
    /// </summary>
    private readonly System.Collections.Generic.Dictionary<System.Type, Base.StyleTmlBase> _stylesTML = new();

    /// <summary>
    ///     Возвращает (создает перед этим при необходимости) переданный стиль визуального отображения элемента
    /// </summary>
    public T GetOrCreateStyle<T>() where T : Base.StyleVisualBase, new()
    {
        if (!_styles.TryGetValue(typeof(T), out Base.StyleVisualBase value))
        {
            value = new T();
            _styles.Add(typeof(T), value);
        }

        return (T)value;
    }

    /// <summary>
    ///     Возвращает (создает перед этим при необходимости) переданный стиль визуального отображения элемента TML
    /// </summary>
    public T GetOrCreateStyleTML<T>() where T : Base.StyleTmlBase, new()
    {
        if (!_stylesTML.TryGetValue(typeof(T), out Base.StyleTmlBase value))
        {
            value = new T();
            _stylesTML.Add(typeof(T), value);
        }

        return (T)value;
    }


    /// <summary>
    ///     Возвращает (создает перед этим при необходимости) стиль визуального отображения элемента. 
    ///     По умолчанию аналогичный: <see cref="Terraria.GameContent.UI.Elements.UIPanel"/>
    /// </summary>
    public StyleTmlUIPanel TmlUIPanel()
        => GetOrCreateStyleTML<StyleTmlUIPanel>();



    /// <inheritdoc/>
    protected override sealed void CopyInnerStylesBase(Base.StyleBase other)
    {
        if (other is StyleVisualElement style)
        {
            foreach (var key in style._styles.Keys)
                this._styles[key] = (Base.StyleVisualBase)style._styles[key].GetCopyBase();
        }
    }


    /// <inheritdoc/>
    protected override sealed bool EqualsInnerStylesFields(Base.StyleBase other)
    {
        if (other is null)
            return false;

        if (other is StyleVisualElement outerContainerBase)
        {
            if (tModLoaderStyle != outerContainerBase.tModLoaderStyle)
                return false;

            if (tModLoaderStyle)
                return EqualsInnerStylesFields(_stylesTML, outerContainerBase._stylesTML);
            else
                return EqualsInnerStylesFields(_styles, outerContainerBase._styles);
        }

        return false;
    }
}