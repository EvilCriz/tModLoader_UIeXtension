using Terraria.UI;
using UIeXtension.Styles.Base;

namespace UIeXtension.Styles;

/// <summary>
///     Базовый стиль компоновки для <see cref="UIExWrapPanel"/> и всех наследников
/// </summary>
public class StyleLayoutContainerWrapPanel : Base.StyleLayoutContainerBase
{
    /// <summary>
    ///     Определяет отступ между расположенными элементами в <see cref="UIExWrapPanel"/>
    /// </summary>
    public StyleDimension SpacingWithinLine = StyleDimension.Empty;

    /// <summary>
    ///     Определяет отступ между линиями в <see cref="UIExWrapPanel"/>
    /// </summary>
    public StyleDimension SpacingBetweenLines = StyleDimension.Empty;

    /// <summary>
    ///     Выравнивание линий при переносе контента.
    /// </summary>
    public Enums.UIExAlignment AlignLines = Enums.UIExAlignment.Auto;

    /// <summary>
    ///     Флаг, указывающий, что элементы внутри каждой линии <see cref="UIExWrapPanel"/> должны располагаться в обратном порядке.
    /// </summary>
    public bool ReverseWithinLine = false;

    /// <summary>
    ///     Флаг, указывающий, что все элементы <see cref="UIExWrapPanel"/> должны располагаться в обратном порядке.
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
    ///     Требует передать все возможные значения класса <see cref="StyleLayoutContainerWrapPanel"/> и устанавливает их.
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
    ///     Копирует значения переданного <see cref="StyleLayoutContainerWrapPanel"/> 
    /// </summary>
    public void Copy(StyleLayoutContainerWrapPanel style)
        => SetAllFields(
            style.SpacingWithinLine,
            style.SpacingBetweenLines,
            style.AlignLines,
            style.ReverseWithinLine,
            style.ReverseAll);

    /// <summary>
    ///     Создает и возвращает копию текущего <see cref="StyleLayoutContainerWrapPanel"/>
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