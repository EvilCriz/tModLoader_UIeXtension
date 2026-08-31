using Terraria.UI;
using UIeXtension.Styles.Base;

namespace UIeXtension.Styles;

/// <summary>
///     A class of styles that are common to all elements <see cref="Terraria.UI.UIElement"/>.
/// </summary>
public class StyleLayoutChildCanvas : Base.StyleLayoutChildBase
{

    /// <summary/>
    public StyleDimension Left = StyleDimension.Empty;

    /// <summary/>
    public StyleDimension Top = StyleDimension.Empty;

    /// <summary/>
    public StyleDimension Right = StyleDimension.Empty;

    /// <summary/>
    public StyleDimension Bottom = StyleDimension.Empty;


    /// <summary>
    ///     Indicates whether the element can go abroad <see cref="UIExCanvas"/>
    ///     If the value is = null, takes the meaning from <see cref="StyleLayoutContainerCanvas.AllowOverflow"/>.
    ///     By default: null
    /// </summary>
    public bool? AllowOverflowSelf = null;



    ///////////////////// CONSTRUCTORS ////////////////////
    ///////////////////// CONSTRUCTORS ////////////////////


    /// <summary/>
    public StyleLayoutChildCanvas() { }

    /// <summary/>
    public StyleLayoutChildCanvas(
        StyleDimension left = default(StyleDimension),
        StyleDimension top = default(StyleDimension),
        StyleDimension right = default(StyleDimension),
        StyleDimension bottom = default(StyleDimension),
        bool? allowOverflowSelf = null)
            => SetAllFields(left, top, right, bottom, allowOverflowSelf);



    //////////////////// SETS /////////////////////////
    //////////////////// SETS /////////////////////////


    /// <summary>
    ///     Requires to convey all possible class values <see cref="StyleLayoutChildCanvas"/> and sets them up.
    /// </summary>
    public void Set(
        StyleDimension left = default(StyleDimension),
        StyleDimension top = default(StyleDimension),
        StyleDimension right = default(StyleDimension),
        StyleDimension bottom = default(StyleDimension),
        bool? allowOverflowSelf = null)
            => SetAllFields(left, top, right, bottom, allowOverflowSelf);

    /// <summary/>
    protected void SetAllFields(
        StyleDimension left,
        StyleDimension top,
        StyleDimension right,
        StyleDimension bottom,
        bool? allowOverflowSelf)
    {
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
        AllowOverflowSelf = allowOverflowSelf;
    }



    //////////////////// COPY /////////////////////////
    //////////////////// COPY /////////////////////////

    /// <inheritdoc/>
    protected override StyleBase Fabricate() => new StyleLayoutChildCanvas();

    /// <inheritdoc/>
    protected override void CopyBase(StyleBase style)
    {
        if (style is StyleLayoutChildCanvas style2)
            Copy(style2);
    }

    /// <summary>
    ///     Copy the values of the transmitted <see cref="StyleLayoutChildCanvas"/> 
    /// </summary>
    public void Copy(StyleLayoutChildCanvas style)
        => SetAllFields(
            left:               style.Left,
            top:                style.Top,
            right:              style.Right,
            bottom:             style.Bottom,
            allowOverflowSelf:  style.AllowOverflowSelf);

    /// <summary>
    ///     Creates and returns a copy of the current <see cref="StyleLayoutChildCanvas"/>
    /// </summary>
    public StyleLayoutChildCanvas GetCopy() 
        => GetCopyBase<StyleLayoutChildCanvas>();



    //////////////////// EQUALS /////////////////////////
    //////////////////// EQUALS /////////////////////////


    /// <inheritdoc/>
    protected override bool EqualsFields(StyleBase other)
    {
        if (other is StyleLayoutChildCanvas style)
            return
                Utils.UtilsStyles.EqualsStyleDimensionFields(Left, style.Left) &&
                Utils.UtilsStyles.EqualsStyleDimensionFields(Top, style.Top) &&
                Utils.UtilsStyles.EqualsStyleDimensionFields(Right, style.Right) &&
                Utils.UtilsStyles.EqualsStyleDimensionFields(Bottom, style.Bottom) &&
                AllowOverflowSelf == style.AllowOverflowSelf;

        return false;
    }
}
