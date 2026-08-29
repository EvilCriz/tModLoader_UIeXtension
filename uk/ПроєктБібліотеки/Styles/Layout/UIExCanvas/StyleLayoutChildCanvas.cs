using Terraria.UI;
using UIeXtension.Styles.Base;

namespace UIeXtension.Styles;

/// <summary>
///     Клас стилів, які є загальними для всіх елементів <see cref="Terraria.UI.UIElement"/>. .
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
    ///     Повідомляємо, що елемент може піти за кордоном <see cref="UIExCanvas"/>
    ///     Якщо значення є = null, приймає значення з <see cref="StyleLayoutContainerCanvas.AllowOverflow"/>. .
    ///     За замовчуванням: null
    /// </summary>
    public bool? AllowOverflowSelf = null;



    ///////////////////// CONSTRUCTORS ///////////////
    ///////////////////// CONSTRUCTORS ///////////////


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



    //////////////////// SETS /////////////////
    //////////////////// SETS /////////////////


    /// <summary>
    ///     Вимагає передати всі можливі значення класу <see cref="StyleLayoutChildCanvas"/> і встановлюємо їх.
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



    //////////////////// COPY /////////////////
    //////////////////// COPY /////////////////

    /// <inheritdoc/>
    protected override StyleBase Fabricate() => new StyleLayoutChildCanvas();

    /// <inheritdoc/>
    protected override void CopyBase(StyleBase style)
    {
        if (style is StyleLayoutChildCanvas style2)
            Copy(style2);
    }

    /// <summary>
    ///     Скопіювати значення передається <see cref="StyleLayoutChildCanvas"/> 
    /// </summary>
    public void Copy(StyleLayoutChildCanvas style)
        => SetAllFields(
            left:               style.Left,
            top:                style.Top,
            right:              style.Right,
            bottom:             style.Bottom,
            allowOverflowSelf:  style.AllowOverflowSelf);

    /// <summary>
    ///     Створює і повертає копію поточного <see cref="StyleLayoutChildCanvas"/>
    /// </summary>
    public StyleLayoutChildCanvas GetCopy() 
        => GetCopyBase<StyleLayoutChildCanvas>();



    //////////////////// EQUALS /////////////////
    //////////////////// EQUALS /////////////////


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
