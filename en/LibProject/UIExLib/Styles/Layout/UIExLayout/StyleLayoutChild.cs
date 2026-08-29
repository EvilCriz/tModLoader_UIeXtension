using Terraria.UI;
using UIeXtension.Styles.Base;

namespace UIeXtension.Styles;

/// <summary>
///     A class of styles that are common to all elements <see cref="Terraria.UI.UIElement"/>.
/// </summary>
/// <remarks>
///     Styles apply only to nested <see cref="Terraria.UI.UIElement"/> daughter-class <see cref="UIExLayout"/>
/// </remarks>
public partial class StyleLayoutChild : Base.StyleLayoutChildBase
{
    /// <summary>
    ///     Determines the starting point of the layout <see cref="UIExLayout"/> relative to the main axis.
    ///     <para>meaning <see cref="Enums.UIExAlignment.Auto"/> elemental 
    ///     <see cref="StyleLayoutContainer.JustifyContent"/> builder.</para>
    /// </summary>
    public Enums.UIExAlignment JustifySelf = Enums.UIExAlignment.Auto;

    /// <summary>
    ///     Determines the starting point of the layout <see cref="UIExLayout"/> relative to the transverse axis.
    ///     <para>meaning <see cref="Enums.UIExAlignment.Auto"/> elemental 
    ///     <see cref="StyleLayoutContainer.AlignItems"/> builder.</para>
    /// </summary>
    public Enums.UIExAlignment AlignSelf = Enums.UIExAlignment.Auto;

    /// <summary/>
    public UIExThickness Margin = default(UIExThickness);

    /// <summary>
    ///     A flag indicating that the item should not be included in the layout <see cref="UIExLayout"/>. 
    ///     Even if it is a daughter element of the layout class.
    /// </summary>
    public bool WithoutLayout = false;


    /// <summary>
    ///     This value is used only by the linkers of this library for the elements embedded in them.
    /// </summary>
    public StyleDimension Width = default(StyleDimension);

    /// <summary>
    ///     This value is used only by the linkers of this library for the elements embedded in them.
    /// </summary>
    public StyleDimension Height = default(StyleDimension);




    ///////////////////// CONSTRUCTORS ////////////////////
    ///////////////////// CONSTRUCTORS ////////////////////

    /// <summary/>
    public StyleLayoutChild() { }
    
    /// <summary/>
    public StyleLayoutChild(
        Enums.UIExAlignment justifySelf = Enums.UIExAlignment.Auto,
        Enums.UIExAlignment alignSelf = Enums.UIExAlignment.Auto,
        UIExThickness margin = default(UIExThickness),
        bool withoutLayout = false,
        StyleDimension width = default(StyleDimension),
        StyleDimension height = default(StyleDimension))
            => SetAllFields(justifySelf, alignSelf, margin, withoutLayout, width, height);




    //////////////////// SETS /////////////////////////
    //////////////////// SETS /////////////////////////

    /// <summary>
    ///     Requires to convey all possible class values <see cref="StyleLayoutChild"/> and sets them up.
    /// </summary>
    public void Set(
        Enums.UIExAlignment justifySelf = Enums.UIExAlignment.Auto,
        Enums.UIExAlignment alignSelf = Enums.UIExAlignment.Auto,
        UIExThickness margin = default(UIExThickness),
        bool withoutLayout = false,
        StyleDimension width = default(StyleDimension),
        StyleDimension height = default(StyleDimension))
            => SetAllFields(justifySelf, alignSelf, margin, withoutLayout, width, height);

    /// <summary/>
    protected void SetAllFields(
        Enums.UIExAlignment justifySelf,
        Enums.UIExAlignment alignSelf,
        UIExThickness margin,
        bool withoutLayout,
        StyleDimension width,
        StyleDimension height)
    {
        JustifySelf = justifySelf;
        AlignSelf = alignSelf;
        Margin = margin;
        WithoutLayout = withoutLayout;
        Width = width;
        Height = height;
    }




    //////////////////// COPY /////////////////////////
    //////////////////// COPY /////////////////////////

    /// <inheritdoc/>
    protected override StyleBase Fabricate() => new StyleLayoutChild();

    /// <inheritdoc/>
    protected override void CopyBase(StyleBase style)
    {
        if (style is StyleLayoutChild style2)
            Copy(style2);
    }

    /// <summary>
    ///     Copy the values of the transmitted <see cref="StyleLayoutChild"/> 
    /// </summary>
    public void Copy(StyleLayoutChild style)
        => SetAllFields(
            justifySelf:    style.JustifySelf,
            alignSelf:      style.AlignSelf,
            margin:         style.Margin,
            withoutLayout:  style.WithoutLayout,
            width:          style.Width,
            height:         style.Height);

    /// <summary>
    ///     Creates and returns a copy of the current <see cref="StyleLayoutChild"/>
    /// </summary>
    public StyleLayoutChild GetCopy()
        => GetCopyBase<StyleLayoutChild>();




    //////////////////// EQUALS /////////////////////////
    //////////////////// EQUALS /////////////////////////

    /// <inheritdoc/>
    protected override bool EqualsFields(StyleBase other)
    {
        if (other is StyleLayoutChild style)
            return
                JustifySelf == style.JustifySelf &&
                AlignSelf == style.AlignSelf &&
                Margin.EqualsFields(style.Margin) &&
                WithoutLayout == style.WithoutLayout &&
                Utils.UtilsStyles.EqualsStyleDimensionFields(Width, style.Width) &&
                Utils.UtilsStyles.EqualsStyleDimensionFields(Height, style.Height);

        return false;
    }
}