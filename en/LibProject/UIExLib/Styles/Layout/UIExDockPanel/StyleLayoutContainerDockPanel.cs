using UIeXtension.Styles.Base;

namespace UIeXtension.Styles;

/// <summary>
///     Style of layout for <see cref="UIExDockPanel"/> 
/// </summary>
public class StyleLayoutContainerDockPanel : Base.StyleLayoutContainerBase
{
    /// <summary/>
    public StyleSide SideLeft;
    /// <summary/>
    public StyleSide SideRight;
    /// <summary/>
    public StyleSide SideTop;
    /// <summary/>
    public StyleSide SideBottom;
    /// <summary/>
    public StyleSide SideFill;
    


    ///////////////////// CONSTRUCTORS ////////////////////
    ///////////////////// CONSTRUCTORS ////////////////////


    /// <summary/>
    public StyleLayoutContainerDockPanel() : this(
        sideLeft:       new(),
        sideRight:      new(),
        sideTop:        new(),
        sideBottom:     new(),
        sideFill:       new())
    { }


    /// <summary/>
    public StyleLayoutContainerDockPanel(
        StyleSide sideLeft,
        StyleSide sideRight,
        StyleSide sideTop,
        StyleSide sideBottom,
        StyleSide sideFill)
            => SetAllFields(
                sideLeft:       sideLeft,
                sideRight:      sideRight,
                sideTop:        sideTop,
                sideBottom:     sideBottom,
                sideFill:       sideFill);



    //////////////////// SETS /////////////////////////
    //////////////////// SETS /////////////////////////


    /// <summary>
    ///     Requires to convey all possible class values <see cref="StyleLayoutContainerDockPanel"/> and sets them up.
    /// </summary>
    public void Set(
        StyleSide sideLeft = null,
        StyleSide sideRight = null,
        StyleSide sideTop = null,
        StyleSide sideBottom = null,
        StyleSide sideFill = null)
            => SetAllFields(sideLeft, sideRight, sideTop, sideBottom, sideFill);

    /// <summary/>
    protected void SetAllFields(
        StyleSide sideLeft,
        StyleSide sideRight,
        StyleSide sideTop,
        StyleSide sideBottom,
        StyleSide sideFill)
    {
        if(sideLeft is not null)
            SideLeft = sideLeft;

        if (sideRight is not null)
            SideRight = sideRight;

        if (sideTop is not null)
            SideTop = sideTop;

        if (sideBottom is not null)
            SideBottom = sideBottom;

        if (sideFill is not null)
            SideFill = sideFill;
    }



    //////////////////// COPY /////////////////////////
    //////////////////// COPY /////////////////////////

    /// <inheritdoc/>
    protected override StyleBase Fabricate() => new StyleLayoutContainerDockPanel();

    /// <inheritdoc/>
    protected override void CopyBase(StyleBase style)
    {
        if (style is StyleLayoutContainerDockPanel style2)
            Copy(style2);
    }

    /// <summary>
    ///     Copy the values of the transmitted <see cref="StyleLayoutContainerDockPanel"/> 
    /// </summary>
    public void Copy(StyleLayoutContainerDockPanel style)
        => SetAllFields(
            sideLeft:        style.SideLeft.GetCopy(),
            sideRight:       style.SideRight.GetCopy(),
            sideTop:         style.SideTop.GetCopy(),
            sideBottom:      style.SideBottom.GetCopy(),
            sideFill:        style.SideFill.GetCopy());

    /// <summary>
    ///     Creates and returns a copy of the current <see cref="StyleLayoutContainerDockPanel"/>
    /// </summary>
    public StyleLayoutContainerDockPanel GetCopy()
        => GetCopyBase<StyleLayoutContainerDockPanel>();



    //////////////////// EQUALS /////////////////////////
    //////////////////// EQUALS /////////////////////////


    /// <inheritdoc/>
    protected override bool EqualsFields(StyleBase other)
    {
        if (other is StyleLayoutContainerDockPanel style)
        {
            return
                Utils.UtilsStyles.EqualsStyles(SideLeft, style.SideLeft) &&
                Utils.UtilsStyles.EqualsStyles(SideRight, style.SideRight) &&
                Utils.UtilsStyles.EqualsStyles(SideTop, style.SideTop) &&
                Utils.UtilsStyles.EqualsStyles(SideBottom, style.SideBottom) &&
                Utils.UtilsStyles.EqualsStyles(SideFill, style.SideFill);
        }

        return false;
    }
}