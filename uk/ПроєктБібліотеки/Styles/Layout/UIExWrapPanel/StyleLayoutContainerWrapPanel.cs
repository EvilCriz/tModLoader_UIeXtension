using Terraria.UI;
using UIeXtension.Styles.Base;

namespace UIeXtension.Styles;

/// <summary>
///     Базовий стиль макета для <see cref="UIExWrapPanel"/> і всі спадкоємці
/// </summary>
public class StyleLayoutContainerWrapPanel : Base.StyleLayoutContainerBase
{
    /// <summary>
    ///     Визначити відступ між елементами в <see cref="UIExWrapPanel"/>
    /// </summary>
    public StyleDimension SpacingWithinLine = StyleDimension.Empty;

    /// <summary>
    ///     Визначити відступ між лініями в <see cref="UIExWrapPanel"/>
    /// </summary>
    public StyleDimension SpacingBetweenLines = StyleDimension.Empty;

    /// <summary>
    ///     Вирівнювання лінії при передачі вмісту.
    /// </summary>
    public Enums.UIExAlignment AlignLines = Enums.UIExAlignment.Auto;

    /// <summary>
    ///     прапорець, що вказує на те, що елементи в кожному рядку <see cref="UIExWrapPanel"/> Вони повинні бути розміщені в зворотному порядку.
    /// </summary>
    public bool ReverseWithinLine = false;

    /// <summary>
    ///     Вказаний прапор, що вказує на всі елементи <see cref="UIExWrapPanel"/> Вони повинні бути розміщені в зворотному порядку.
    /// </summary>
    public bool ReverseAll = false;




    ///////////////////// CONSTRUCTORS ///////////////
    ///////////////////// CONSTRUCTORS ///////////////
    
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




    ///////////////////// SETS ///////////////
    ///////////////////// SETS ///////////////

    /// <summary>
    ///     Вимагає передати всі можливі значення класу <see cref="StyleLayoutContainerWrapPanel"/> і встановлюємо їх.
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




    ///////////////////// COPY ///////////////
    ///////////////////// COPY ///////////////

    /// <inheritdoc/>
    protected override StyleBase Fabricate() => new StyleLayoutContainerWrapPanel();

    /// <inheritdoc/>
    protected override void CopyBase(StyleBase style)
    {
        if (style is StyleLayoutContainerWrapPanel style2)
            Copy(style2);
    }

    /// <summary>
    ///     Скопіювати значення передається <see cref="StyleLayoutContainerWrapPanel"/> 
    /// </summary>
    public void Copy(StyleLayoutContainerWrapPanel style)
        => SetAllFields(
            style.SpacingWithinLine,
            style.SpacingBetweenLines,
            style.AlignLines,
            style.ReverseWithinLine,
            style.ReverseAll);

    /// <summary>
    ///     Створює і повертає копію поточного <see cref="StyleLayoutContainerWrapPanel"/>
    /// </summary>
    public StyleLayoutContainerWrapPanel GetCopy()
        => GetCopyBase<StyleLayoutContainerWrapPanel>();




    ///////////////////// EQUALS ///////////////
    ///////////////////// EQUALS ///////////////

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