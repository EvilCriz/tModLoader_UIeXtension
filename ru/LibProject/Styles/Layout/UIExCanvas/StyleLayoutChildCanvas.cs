using Terraria.UI;
using UIeXtension.Styles.Base;

namespace UIeXtension.Styles;

/// <summary>
///     Класс стилей, которые являются общими для всех элементов <see cref="Terraria.UI.UIElement"/>.
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
    ///     Указывает, может ли элемент выходить за границу <see cref="UIExCanvas"/>
    ///     Если значение == null, берется значение из <see cref="StyleLayoutContainerCanvas.AllowOverflow"/>.
    ///     По умолчанию: null
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
    ///     Требует передать все возможные значения класса <see cref="StyleLayoutChildCanvas"/> и устанавливает их.
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
    ///     Копирует значения переданного <see cref="StyleLayoutChildCanvas"/> 
    /// </summary>
    public void Copy(StyleLayoutChildCanvas style)
        => SetAllFields(
            left:               style.Left,
            top:                style.Top,
            right:              style.Right,
            bottom:             style.Bottom,
            allowOverflowSelf:  style.AllowOverflowSelf);

    /// <summary>
    ///     Создает и возвращает копию текущего <see cref="StyleLayoutChildCanvas"/>
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
