using UIeXtension.Styles.Base;

namespace UIeXtension.Styles;

/// <summary>
///     Style of layout for <see cref="UIExCanvas"/> 
/// </summary>
public class StyleLayoutContainerCanvas : Base.StyleLayoutContainerBase
{
    /// <summary>
    ///     Indicates whether the element can go abroad <see cref="UIExCanvas"/>
    ///     By default: false
    /// </summary>
    public bool AllowOverflow = false;



    ///////////////////// CONSTRUCTORS ////////////////////
    ///////////////////// CONSTRUCTORS ////////////////////


    /// <summary/>
    public StyleLayoutContainerCanvas() { }

    /// <summary/>
    public StyleLayoutContainerCanvas(bool allowOverflow)
            => SetAllFields(allowOverflow);



    //////////////////// SETS /////////////////////////
    //////////////////// SETS /////////////////////////


    /// <summary>
    ///     Requires to convey all possible class values <see cref="StyleLayoutContainerCanvas"/> and sets them up.
    /// </summary>
    public void Set(bool allowOverflow = false)
        => SetAllFields(allowOverflow);

    /// <summary/>
    protected void SetAllFields(bool allowOverflow)
    {
        AllowOverflow = allowOverflow;
    }



    //////////////////// COPY /////////////////////////
    //////////////////// COPY /////////////////////////

    /// <inheritdoc/>
    protected override StyleBase Fabricate() => new StyleLayoutContainerCanvas();

    /// <inheritdoc/>
    protected override void CopyBase(StyleBase style)
    {
        if (style is StyleLayoutContainerCanvas style2)
            Copy(style2);
    }

    /// <summary>
    ///     Copy the values of the transmitted <see cref="StyleLayoutContainerCanvas"/> 
    /// </summary>
    public void Copy(StyleLayoutContainerCanvas style)
        => SetAllFields(
            allowOverflow: style.AllowOverflow);

    /// <summary>
    ///     Creates and returns a copy of the current <see cref="StyleLayoutContainerCanvas"/>
    /// </summary>
    public StyleLayoutContainerCanvas GetCopy()
        => GetCopyBase<StyleLayoutContainerCanvas>();




    //////////////////// EQUALS /////////////////////////
    //////////////////// EQUALS /////////////////////////

    /// <inheritdoc/>
    protected override bool EqualsFields(StyleBase other)
    {
        if (other is StyleLayoutContainerCanvas style)
            return AllowOverflow == style.AllowOverflow;

        return false;
    }
}
