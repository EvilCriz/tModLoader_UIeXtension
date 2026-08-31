using Terraria.UI;
using UIeXtension.Styles.Base;

namespace UIeXtension.Styles;

/// <summary>
///     Basic layout style for <see cref="UIExStackPanel"/> and all heirs
/// </summary>
public class StyleLayoutContainerStackPanel : Base.StyleLayoutContainerBase
{
    /// <summary>
    ///     Determine the indentation between the elements in <see cref="UIExStackPanel"/>
    /// </summary>
    public StyleDimension Spacing = StyleDimension.Empty;

    /// <summary>
    ///     A flag indicating that the elements are inside <see cref="UIExStackPanel"/> They should be placed in reverse order.
    /// </summary>
    public bool Reverse = false;




    ///////////////////// CONSTRUCTORS ////////////////////
    ///////////////////// CONSTRUCTORS ////////////////////

    /// <summary/>
    public StyleLayoutContainerStackPanel() { }

    /// <summary/>
    public StyleLayoutContainerStackPanel(
        StyleDimension spacing = default(StyleDimension),
        bool reverse = false)
            => SetAllFields(spacing, reverse);




    ///////////////////// SETS ////////////////////
    ///////////////////// SETS ////////////////////

    /// <summary>
    ///     Requires to convey all possible class values <see cref="StyleLayoutContainerStackPanel"/> and sets them up.
    /// </summary>
    public void Set(
        StyleDimension spacing = default(StyleDimension),
        bool reverse = false)
            => SetAllFields(spacing, reverse);

    /// <summary/>
    protected void SetAllFields(
        StyleDimension spacing,
        bool reverse)
    {
        Spacing = spacing;
        Reverse = reverse;
    }




    ///////////////////// COPY ////////////////////
    ///////////////////// COPY ////////////////////

    /// <inheritdoc/>
    protected override StyleBase Fabricate() => new StyleLayoutContainerStackPanel();

    /// <inheritdoc/>
    protected override void CopyBase(StyleBase style)
    {
        if (style is StyleLayoutContainerStackPanel style2)
            Copy(style2);
    }

    /// <summary>
    ///     Copy the values of the transmitted <see cref="StyleLayoutContainerStackPanel"/> 
    /// </summary>
    public void Copy(StyleLayoutContainerStackPanel style)
        => SetAllFields(
            style.Spacing,
            style.Reverse);

    /// <summary>
    ///     Creates and returns a copy of the current <see cref="StyleLayoutContainerStackPanel"/>
    /// </summary>
    public StyleLayoutContainerStackPanel GetCopy()
        => GetCopyBase<StyleLayoutContainerStackPanel>();




    ///////////////////// EQUALS ////////////////////
    ///////////////////// EQUALS ////////////////////

    /// <inheritdoc/>
    protected override bool EqualsFields(StyleBase other)
    {
        if (other is StyleLayoutContainerStackPanel style)
            return
                Utils.UtilsStyles.EqualsStyleDimensionFields(Spacing, style.Spacing) &&
                Reverse == style.Reverse;

        return false;
    }
}