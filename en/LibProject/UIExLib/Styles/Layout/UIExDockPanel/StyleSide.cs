using Terraria.UI;
using UIeXtension.Styles.Base;

namespace UIeXtension.Styles;

/// <summary>
///     Side information, used by the builder and his styles.
/// </summary>
public class StyleSide : Base.StyleBase
{
    /// <summary>
    ///     Internal areas of the side.
    /// </summary>
    public UIExThickness Padding = default(UIExThickness);

    /// <summary>
    ///     The alignment of the element along the main axis of the side.
    ///     The main axis for the left/right side is vertical.
    ///     The main axis for the upper/lower side is horizontal.
    /// </summary>
    public Enums.UIExAlignment JustifyContent = Enums.UIExAlignment.Auto;

    /// <summary>
    ///     The alignment of the element along the main axis of the side.
    ///     The main axis for the left/right side is vertical.
    ///     The main axis for the upper/lower side is horizontal.
    /// </summary>
    public Enums.UIExAlignment AlignItems = Enums.UIExAlignment.Auto;

    /// <summary>
    ///     The distance between the elements on this side.
    /// </summary>
    public StyleDimension Spacing = default(StyleDimension);

    /// <summary>
    ///     Should the elements be arranged in an inverted order in this line?
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
    ///     It assumes all possible values of this class.
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
    ///     Copy the values of the transmitted <see cref="StyleSide"/> 
    /// </summary>
    public void Copy(StyleSide style)
        => SetAllFields(
            padding:            style.Padding,
            justifyContent:     style.JustifyContent,
            alignItems:         style.AlignItems,
            spacing:            style.Spacing,
            reverse:            style.Reverse);

    /// <summary>
    ///     Creates and returns a copy of the current <see cref="StyleSide"/>
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