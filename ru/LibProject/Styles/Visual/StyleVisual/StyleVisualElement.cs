using Terraria.UI;
using UIeXtension.Styles.Base;

namespace UIeXtension.Styles;


/// <summary>
///     Таблица стилей, которую содержат все UI-элементы данной библиотеки
/// </summary>
public partial class StyleVisualElement : Base.StyleVisualBase
{
    /// <summary>
    ///     Указывает, что базовые настройки стиля данного визуального элемента
    ///     должны быть аналогичны <see cref="Terraria.GameContent.UI.Elements.UIPanel"/>
    /// </summary>
    public bool tModLoaderStyle = false;

    /// <summary>
    ///     Указывает, должен ли элемент автоматические управлять значениями:
    ///     <see cref="UIElement.PaddingTop"/>
    ///     <see cref="UIElement.PaddingLeft"/>
    ///     <see cref="UIElement.PaddingBottom"/>
    ///     <see cref="UIElement.PaddingRight"/>
    /// </summary>
    public bool PaddingAutoControl = true;


    ///////////////////// CONSTRUCTORS ////////////////////
    ///////////////////// CONSTRUCTORS ////////////////////

    /// <summary/>
    public StyleVisualElement() { }

    /// <summary/>
    public StyleVisualElement(bool tmodLoaderStyle = false, bool paddingAutoControl = false)
        => SetAllFields(tmodLoaderStyle, paddingAutoControl);




    ///////////////////// SETS ////////////////////
    ///////////////////// SETS ////////////////////

    /// <summary>
    ///     Требует передать все возможные значения класса <see cref="StyleVisualElement"/> и устанавливает их.
    /// </summary>
    public void Set(bool tmodLoaderStyle = false, bool paddingAutoControl = false)
        => SetAllFields(tmodLoaderStyle, paddingAutoControl);

    /// <summary/>
    protected void SetAllFields(bool tmodLoaderStyle, bool paddingAutoControl)
    {
        tModLoaderStyle = tmodLoaderStyle;
        PaddingAutoControl = paddingAutoControl;
    }




    ///////////////////// COPY ////////////////////
    ///////////////////// COPY ////////////////////

    /// <inheritdoc/>
    protected override StyleBase Fabricate() => new StyleVisualElement();

    /// <inheritdoc/>
    protected override void CopyBase(StyleBase style)
    {
        if (style is StyleVisualElement style2)
            Copy(style2);
    }

    /// <summary>
    ///     Копирует значения переданного <see cref="StyleVisualElement"/> 
    /// </summary>
    public void Copy(StyleVisualElement style)
        => SetAllFields(
            tmodLoaderStyle:        style.tModLoaderStyle,
            paddingAutoControl:     style.PaddingAutoControl);

    /// <summary>
    ///     Создает и возвращает копию текущего <see cref="StyleVisualElement"/>
    /// </summary>
    public StyleVisualElement GetCopy()
        => GetCopyBase<StyleVisualElement>();        




    ///////////////////// EQUALS ////////////////////
    ///////////////////// EQUALS ////////////////////

    /// <inheritdoc/>
    protected override bool EqualsFields(StyleBase other)
    {
        if(other is StyleVisualElement otherStyle)
            return tModLoaderStyle == otherStyle.tModLoaderStyle &&
                PaddingAutoControl == otherStyle.PaddingAutoControl;

        return false;
    }
}