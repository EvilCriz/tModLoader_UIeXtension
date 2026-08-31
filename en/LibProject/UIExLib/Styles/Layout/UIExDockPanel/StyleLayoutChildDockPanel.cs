using UIeXtension.Styles.Base;

namespace UIeXtension.Styles;

/// <summary>
///     A class of styles that are common to all elements <see cref="Terraria.UI.UIElement"/>.
/// </summary>using System.Collections.Generic;
public class StyleLayoutChildDockPanel : Base.StyleLayoutChildBase
{
    /// <summary>
    ///     The party to which the child element is pressed. 
    /// </summary>
    public Enums.UIExSide Side = Enums.UIExSide.Left;



    ///////////////////// CONSTRUCTORS ////////////////////
    ///////////////////// CONSTRUCTORS ////////////////////


    /// <summary/>
    public StyleLayoutChildDockPanel() { }

    /// <summary/>
    public StyleLayoutChildDockPanel(Enums.UIExSide side = Enums.UIExSide.Left)
            => SetAllFields(side);



    //////////////////// SETS /////////////////////////
    //////////////////// SETS /////////////////////////


    /// <summary>
    ///     Requires to convey all possible class values <see cref="StyleLayoutChildDockPanel"/> and sets them up.
    /// </summary>
    public void Set(
        Enums.UIExSide side = Enums.UIExSide.Left)
            => SetAllFields(side);

    /// <summary/>
    protected void SetAllFields(Enums.UIExSide side)
    {
        Side = side;
    }



    //////////////////// COPY /////////////////////////
    //////////////////// COPY /////////////////////////

    /// <inheritdoc/>
    protected override StyleBase Fabricate() => new StyleLayoutChildDockPanel();

    /// <inheritdoc/>
    protected override void CopyBase(StyleBase style)
    {
        if (style is StyleLayoutChildDockPanel style2)
            Copy(style2);
    }

    /// <summary>
    ///     Copy the values of the transmitted <see cref="StyleLayoutChildDockPanel"/> 
    /// </summary>
    public void Copy(StyleLayoutChildDockPanel style)
        => SetAllFields(
                side: style.Side);

    /// <summary>
    ///     Creates and returns a copy of the current <see cref="StyleLayoutChildDockPanel"/>
    /// </summary>
    public StyleLayoutChildDockPanel GetCopy()
        => GetCopyBase<StyleLayoutChildDockPanel>();



    //////////////////// EQUALS /////////////////////////
    //////////////////// EQUALS /////////////////////////


    /// <inheritdoc/>
    protected override bool EqualsFields(StyleBase other)
    {
        if (other is StyleLayoutChildDockPanel style)
            return Side == style.Side;

        return false;
    }
}
