using UIeXtension.Styles.TModLoader;

namespace UIeXtension.Styles;

public partial class StyleVisualElement
{
    /// <summary>
    ///     Зберігати інші стилі візуального відображення елементів, бібліотечних даних або користувачів-генераторів.
    /// </summary>
    private readonly System.Collections.Generic.Dictionary<System.Type, Base.StyleVisualBase> _styles = new();

    /// <summary>
    ///     Стилі візуального відображення елементів з візуальною частиною, схожою на одну з UI склад tModLoader. . 
    ///     Бібліотека або бібліотека користувача.
    /// </summary>
    private readonly System.Collections.Generic.Dictionary<System.Type, Base.StyleTmlBase> _stylesTML = new();

    /// <summary>
    ///     Повертає (відтворює до цього, при необхідності) переведений стиль візуального відображення елемента
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
    ///     Повертає (відтворює до цього, при необхідності) переведений стиль візуального відображення елемента TML
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
    ///     Повертає (відтворює до цього, при необхідності) стиль візуального відображення елемента. 
    ///     За замовчуванням, аналогічний: <see cref="Terraria.GameContent.UI.Elements.UIPanel"/>
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