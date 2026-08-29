using Terraria.UI;
using UIeXtension.Styles.Base;

namespace UIeXtension.Styles;

/// <summary>
///     Информация о стороне. Используется компоновщиком и его стилями.
/// </summary>
public class StyleSide : Base.StyleBase
{
    /// <summary>
    ///     Внутренние области стороны.
    /// </summary>
    public UIExThickness Padding = default(UIExThickness);

    /// <summary>
    ///     Выравнивание элемента по главной оси стороны.
    ///     Главная ось для левой/правой стороны - вертикальная.
    ///     Главная ось для верхней/нижней стороны - горизонтальная.
    /// </summary>
    public Enums.UIExAlignment JustifyContent = Enums.UIExAlignment.Auto;

    /// <summary>
    ///     Выравнивание элемента по главной оси стороны.
    ///     Главная ось для левой/правой стороны - вертикальная.
    ///     Главная ось для верхней/нижней стороны - горизонтальная.
    /// </summary>
    public Enums.UIExAlignment AlignItems = Enums.UIExAlignment.Auto;

    /// <summary>
    ///     Расстояние между элемента на данной стороне.
    /// </summary>
    public StyleDimension Spacing = default(StyleDimension);

    /// <summary>
    ///     Должны ли элементы располагаться в инвертированном порядке в данной строке.
    /// </summary>
    public bool Reverse = false;




    ///////////////////// CONSTRUCTORS ////////////////////
    ///////////////////// CONSTRUCTORS ////////////////////

    /// <summary/>
    public StyleSide() { }

    /// <summary/>
    public StyleSide(
        UIExThickness padding   = default(UIExThickness),
        Enums.UIExAlignment justifyContent = Enums.UIExAlignment.Auto,
        Enums.UIExAlignment alignItems = Enums.UIExAlignment.Auto,
        StyleDimension spacing = default(StyleDimension),
        bool reverse = false)
        => SetAllFields(
            padding:            padding,
            justifyContent:     justifyContent,
            alignItems:         alignItems,
            spacing:            spacing,
            reverse:            reverse);




    //////////////////// SETS /////////////////////////
    //////////////////// SETS /////////////////////////

    /// <summary>
    ///     Принимает все возможные значения данного класса.
    /// </summary>
    public void Set(
        UIExThickness padding = default(UIExThickness),
        Enums.UIExAlignment justifyContent = Enums.UIExAlignment.Auto,
        Enums.UIExAlignment alignItems = Enums.UIExAlignment.Auto,
        StyleDimension spacing = default(StyleDimension),
        bool reverse = false)
            => SetAllFields(padding, justifyContent, alignItems, spacing, reverse);

    /// <summary/>
    protected void SetAllFields(
        UIExThickness padding,
        Enums.UIExAlignment justifyContent,
        Enums.UIExAlignment alignItems,
        StyleDimension spacing,
        bool reverse)
    {
        Padding = padding;
        JustifyContent = justifyContent;
        AlignItems = alignItems;
        Spacing = spacing;
        Reverse = reverse;
    }




    //////////////////// COPY /////////////////////////
    //////////////////// COPY /////////////////////////

    /// <inheritdoc/>
    protected override StyleBase Fabricate() => new StyleSide();

    /// <inheritdoc/>
    protected override void CopyBase(StyleBase style)
    {
        if (style is StyleSide style2)
            Copy(style2);
    }

    /// <summary>
    ///     Копирует значения переданного <see cref="StyleSide"/> 
    /// </summary>
    public void Copy(StyleSide style)
        => SetAllFields(
            padding:            style.Padding,
            justifyContent:     style.JustifyContent,
            alignItems:         style.AlignItems,
            spacing:            style.Spacing,
            reverse:            style.Reverse);

    /// <summary>
    ///     Создает и возвращает копию текущего <see cref="StyleSide"/>
    /// </summary>
    public StyleSide GetCopy()
        => GetCopyBase<StyleSide>();




    //////////////////// EQUALS /////////////////////////
    //////////////////// EQUALS /////////////////////////

    /// <inheritdoc/>
    protected override bool EqualsFields(StyleBase other)
    {
        if (other is StyleSide style)
            return
                Padding.EqualsFields(style.Padding) &&
                JustifyContent == style.JustifyContent &&
                AlignItems == style.AlignItems &&
                Utils.UtilsStyles.EqualsStyleDimensionFields(Spacing, style.Spacing) &&
                Reverse == style.Reverse;

        return false;
    }
}