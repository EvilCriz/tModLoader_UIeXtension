using Terraria.UI;
using UIeXtension.Styles.Base;

namespace UIeXtension.Styles;

/// <summary>
///     Побічна інформація, що використовується конструктором та його стилів.
/// </summary>
public class StyleSide : Base.StyleBase
{
    /// <summary>
    ///     Внутрішні ділянки сторони.
    /// </summary>
    public UIExThickness Padding = default(UIExThickness);

    /// <summary>
    ///     Вирівнювання елемента вздовж основної осі сторони.
    ///     Основна вісь для лівої/правої сторони вертикальна.
    ///     Основна вісь для верхньої / нижньої сторони горизонтальна.
    /// </summary>
    public Enums.UIExAlignment JustifyContent = Enums.UIExAlignment.Auto;

    /// <summary>
    ///     Вирівнювання елемента вздовж основної осі сторони.
    ///     Основна вісь для лівої/правої сторони вертикальна.
    ///     Основна вісь для верхньої / нижньої сторони горизонтальна.
    /// </summary>
    public Enums.UIExAlignment AlignItems = Enums.UIExAlignment.Auto;

    /// <summary>
    ///     Відстань між елементами на цьому боці.
    /// </summary>
    public StyleDimension Spacing = default(StyleDimension);

    /// <summary>
    ///     Чи повинні елементи розташовуватися в неперевершеному порядку в цьому рядку?
    /// </summary>
    public bool Reverse = false;




    ///////////////////// CONSTRUCTORS ///////////////
    ///////////////////// CONSTRUCTORS ///////////////

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




    //////////////////// SETS /////////////////
    //////////////////// SETS /////////////////

    /// <summary>
    ///     Припустимо всі можливі значення цього класу.
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




    //////////////////// COPY /////////////////
    //////////////////// COPY /////////////////

    /// <inheritdoc/>
    protected override StyleBase Fabricate() => new StyleSide();

    /// <inheritdoc/>
    protected override void CopyBase(StyleBase style)
    {
        if (style is StyleSide style2)
            Copy(style2);
    }

    /// <summary>
    ///     Скопіювати значення передається <see cref="StyleSide"/> 
    /// </summary>
    public void Copy(StyleSide style)
        => SetAllFields(
            padding:            style.Padding,
            justifyContent:     style.JustifyContent,
            alignItems:         style.AlignItems,
            spacing:            style.Spacing,
            reverse:            style.Reverse);

    /// <summary>
    ///     Створює і повертає копію поточного <see cref="StyleSide"/>
    /// </summary>
    public StyleSide GetCopy()
        => GetCopyBase<StyleSide>();




    //////////////////// EQUALS /////////////////
    //////////////////// EQUALS /////////////////

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