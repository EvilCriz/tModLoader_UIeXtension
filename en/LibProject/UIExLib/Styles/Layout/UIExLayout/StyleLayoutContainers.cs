using UIeXtension.Styles.Base;

namespace UIeXtension.Styles;

/// <summary>
///     Basic layout style for all elements-components 
/// </summary>
public partial class StyleLayoutContainer : Base.StyleLayoutContainerBase
{

    /// <summary>
    ///     Defines the axis of the container <see cref="UIExStackPanel"/>
    /// </summary>
    public Enums.UIExOrientation Orientation = Enums.UIExOrientation.Vertical;

    /// <summary>
    ///     Determines the starting point of the layout <see cref="UIExLayout"/> relative to the main axis.
    /// </summary>
    public Enums.UIExAlignment JustifyContent = Enums.UIExAlignment.Auto;

    /// <summary>
    ///     Determines the starting point of the layout <see cref="UIExLayout"/> relative to the transverse axis.
    /// </summary>
    public Enums.UIExAlignment AlignItems = Enums.UIExAlignment.Auto;




    ///////////////////// CONSTRUCTORS ////////////////////
    ///////////////////// CONSTRUCTORS ////////////////////

    /// <summary/>
    public StyleLayoutContainer() { }

    /// <summary/>
    public StyleLayoutContainer(
        Enums.UIExOrientation orientation = Enums.UIExOrientation.Vertical,
        Enums.UIExAlignment justifyContent = Enums.UIExAlignment.Auto,
        Enums.UIExAlignment alignItems = Enums.UIExAlignment.Auto)
            => SetAllFields(orientation, justifyContent, alignItems);




    //////////////////// SETS /////////////////////////
    //////////////////// SETS /////////////////////////

    /// <summary>
    ///     Requires to convey all possible class values <see cref="StyleLayoutContainer"/> and sets them up.
    /// </summary>
    public void Set(
        Enums.UIExOrientation orientation = Enums.UIExOrientation.Vertical,
        Enums.UIExAlignment justifyContent = Enums.UIExAlignment.Auto, 
        Enums.UIExAlignment alignItems = Enums.UIExAlignment.Auto)
            => SetAllFields(orientation, justifyContent, alignItems);

    /// <summary/>
    protected void SetAllFields(
        Enums.UIExOrientation orientation, 
        Enums.UIExAlignment justifyContent, 
        Enums.UIExAlignment alignItems)
    {
        Orientation = orientation;
        JustifyContent = justifyContent;
        AlignItems = alignItems;
    }




    //////////////////// COPY /////////////////////////
    //////////////////// COPY /////////////////////////

    /// <inheritdoc/>
    protected override StyleBase Fabricate() => new StyleLayoutContainer();

    /// <inheritdoc/>
    protected override void CopyBase(StyleBase style)
    {
        if (style is StyleLayoutContainer style2)
            Copy(style2);
    }

    /// <summary>
    ///     Copy the values of the transmitted <see cref="StyleLayoutContainer"/> 
    /// </summary>
    public void Copy(StyleLayoutContainer style)
        => SetAllFields(
            orientation:        style.Orientation,
            justifyContent:     style.JustifyContent,
            alignItems:         style.AlignItems);

    /// <summary>
    ///     Creates and returns a copy of the current <see cref="StyleLayoutContainer"/>
    /// </summary>
    public StyleLayoutContainer GetCopy()
        => GetCopyBase<StyleLayoutContainer>();




    //////////////////// EQUALS /////////////////////////
    //////////////////// EQUALS /////////////////////////

    /// <inheritdoc/>
    protected override bool EqualsFields(StyleBase other)
    {
        if (other is StyleLayoutContainer style)
            return
                Orientation == style.Orientation &&
                JustifyContent == style.JustifyContent &&
                AlignItems == style.AlignItems;

        return false;
    }
}