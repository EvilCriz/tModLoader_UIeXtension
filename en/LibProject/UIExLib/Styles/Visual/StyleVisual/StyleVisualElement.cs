using Terraria.UI;
using UIeXtension.Styles.Base;

namespace UIeXtension.Styles;


/// <summary>
///     Style table, which contains all UIElements of this library
/// </summary>
public partial class StyleVisualElement : Base.StyleVisualBase
{
    /// <summary>
    ///     Indicates that the basic style settings of this visual element
    ///     must be similar <see cref="Terraria.GameContent.UI.Elements.UIPanel"/>
    /// </summary>
    public bool tModLoaderStyle = false;

    /// <summary>
    ///     Indicates whether the element should automatically control the values:
    ///     <see cref="UIElement.PaddingTop"/>
    ///     <see cref="UIElement.PaddingLeft"/>
    ///     <see cref="UIElement.PaddingBottom"/>
    ///     <see cref="UIElement.PaddingRight"/>
    /// </summary>
    public bool PaddingAutoControl = true;


    ///////////////////// CONSTRUCTORS ////////////////////
    ///////////////////// CONSTRUCTORS ////////////////////

    /// <summary/>
    public StyleVisualElement() { }

    /// <summary/>
    public StyleVisualElement(bool tmodLoaderStyle = false, bool paddingAutoControl = false)
        => SetAllFields(tmodLoaderStyle, paddingAutoControl);




    ///////////////////// SETS ////////////////////
    ///////////////////// SETS ////////////////////

    /// <summary>
    ///     Requires to convey all possible class values <see cref="StyleVisualElement"/> and sets them up.
    /// </summary>
    public void Set(bool tmodLoaderStyle = false, bool paddingAutoControl = false)
        => SetAllFields(tmodLoaderStyle, paddingAutoControl);

    /// <summary/>
    protected void SetAllFields(bool tmodLoaderStyle, bool paddingAutoControl)
    {
        tModLoaderStyle = tmodLoaderStyle;
        PaddingAutoControl = paddingAutoControl;
    }




    ///////////////////// COPY ////////////////////
    ///////////////////// COPY ////////////////////

    /// <inheritdoc/>
    protected override StyleBase Fabricate() => new StyleVisualElement();

    /// <inheritdoc/>
    protected override void CopyBase(StyleBase style)
    {
        if (style is StyleVisualElement style2)
            Copy(style2);
    }

    /// <summary>
    ///     Copy the values of the transmitted <see cref="StyleVisualElement"/> 
    /// </summary>
    public void Copy(StyleVisualElement style)
        => SetAllFields(
            tmodLoaderStyle:        style.tModLoaderStyle,
            paddingAutoControl:     style.PaddingAutoControl);

    /// <summary>
    ///     Creates and returns a copy of the current <see cref="StyleVisualElement"/>
    /// </summary>
    public StyleVisualElement GetCopy()
        => GetCopyBase<StyleVisualElement>();        




    ///////////////////// EQUALS ////////////////////
    ///////////////////// EQUALS ////////////////////

    /// <inheritdoc/>
    protected override bool EqualsFields(StyleBase other)
    {
        if(other is StyleVisualElement otherStyle)
            return tModLoaderStyle == otherStyle.tModLoaderStyle &&
                PaddingAutoControl == otherStyle.PaddingAutoControl;

        return false;
    }
}