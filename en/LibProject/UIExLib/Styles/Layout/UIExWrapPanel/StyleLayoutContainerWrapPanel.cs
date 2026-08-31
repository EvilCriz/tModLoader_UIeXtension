using Terraria.UI;
using UIeXtension.Styles.Base;

namespace UIeXtension.Styles;

/// <summary>
///     Basic layout style for <see cref="UIExWrapPanel"/> and all heirs
/// </summary>
public class StyleLayoutContainerWrapPanel : Base.StyleLayoutContainerBase
{
    /// <summary>
    ///     Determine the indentation between the elements in <see cref="UIExWrapPanel"/>
    /// </summary>
    public StyleDimension SpacingWithinLine = StyleDimension.Empty;

    /// <summary>
    ///     Determine the indentation between the lines in <see cref="UIExWrapPanel"/>
    /// </summary>
    public StyleDimension SpacingBetweenLines = StyleDimension.Empty;

    /// <summary>
    ///     Line alignment when transferring content.
    /// </summary>
    public Enums.UIExAlignment AlignLines = Enums.UIExAlignment.Auto;

    /// <summary>
    ///     A flag indicating that the elements within each line <see cref="UIExWrapPanel"/> They should be placed in reverse order.
    /// </summary>
    public bool ReverseWithinLine = false;

    /// <summary>
    ///     A flag indicating that all elements <see cref="UIExWrapPanel"/> They should be placed in reverse order.
    /// </summary>
    public bool ReverseAll = false;




    ///////////////////// CONSTRUCTORS ////////////////////
    ///////////////////// CONSTRUCTORS ////////////////////
    
    /// <summary/>
    public StyleLayoutContainerWrapPanel() { }

    /// <summary/>
    public StyleLayoutContainerWrapPanel(
        StyleDimension spacingWithinLine = default(StyleDimension),
        StyleDimension spacingBetweenLines = default(StyleDimension),
        Enums.UIExAlignment alignLines = Enums.UIExAlignment.Auto,
        bool reverseWithinLine = false,
        bool reverseAll = false)
            => SetAllFields(
                spacingWithinLine,
                spacingBetweenLines,
                alignLines,
                reverseWithinLine,
                reverseAll);




    ///////////////////// SETS ////////////////////
    ///////////////////// SETS ////////////////////

    /// <summary>
    ///     Requires to convey all possible class values <see cref="StyleLayoutContainerWrapPanel"/> and sets them up.
    /// </summary>
    public void Set(
        StyleDimension spacingWithinLine = default(StyleDimension),
        StyleDimension spacingBetweenLines = default(StyleDimension),
        Enums.UIExAlignment alignLines = Enums.UIExAlignment.Auto,
        bool reverseWithinLine = false,
        bool reverseAll = false)
            => SetAllFields(
                spacingWithinLine, 
                spacingBetweenLines, 
                alignLines, 
                reverseWithinLine, 
                reverseAll);

    /// <summary/>
    protected void SetAllFields(
        StyleDimension spacingWithinLine,
        StyleDimension spacingBetweenLines,
        Enums.UIExAlignment alignLines,
        bool reverseWithinLine,
        bool reverseAll)
    {
        SpacingWithinLine = spacingWithinLine;
        SpacingBetweenLines = spacingBetweenLines;
        AlignLines = alignLines;
        ReverseWithinLine = reverseWithinLine;
        ReverseAll = reverseAll;
    }




    ///////////////////// COPY ////////////////////
    ///////////////////// COPY ////////////////////

    /// <inheritdoc/>
    protected override StyleBase Fabricate() => new StyleLayoutContainerWrapPanel();

    /// <inheritdoc/>
    protected override void CopyBase(StyleBase style)
    {
        if (style is StyleLayoutContainerWrapPanel style2)
            Copy(style2);
    }

    /// <summary>
    ///     Copy the values of the transmitted <see cref="StyleLayoutContainerWrapPanel"/> 
    /// </summary>
    public void Copy(StyleLayoutContainerWrapPanel style)
        => SetAllFields(
            style.SpacingWithinLine,
            style.SpacingBetweenLines,
            style.AlignLines,
            style.ReverseWithinLine,
            style.ReverseAll);

    /// <summary>
    ///     Creates and returns a copy of the current <see cref="StyleLayoutContainerWrapPanel"/>
    /// </summary>
    public StyleLayoutContainerWrapPanel GetCopy()
        => GetCopyBase<StyleLayoutContainerWrapPanel>();




    ///////////////////// EQUALS ////////////////////
    ///////////////////// EQUALS ////////////////////

    /// <inheritdoc/>
    protected override bool EqualsFields(StyleBase other)
    {
        if (other is StyleLayoutContainerWrapPanel style)
            return
                Utils.UtilsStyles.EqualsStyleDimensionFields(SpacingWithinLine, style.SpacingWithinLine) &&
                Utils.UtilsStyles.EqualsStyleDimensionFields(SpacingBetweenLines, style.SpacingBetweenLines) &&
                AlignLines == style.AlignLines &&
                ReverseWithinLine == style.ReverseWithinLine &&
                ReverseAll == style.ReverseAll;

        return false;
    }
}