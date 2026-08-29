using Terraria.UI;
using UIeXtension.Styles.Base;

namespace UIeXtension.Styles;

/// <summary>
///     Базовий стиль макета для <see cref="UIExStackPanel"/> і всі спадкоємці
/// </summary>
public class StyleLayoutContainerStackPanel : Base.StyleLayoutContainerBase
{
    /// <summary>
    ///     Визначити відступ між елементами в <see cref="UIExStackPanel"/>
    /// </summary>
    public StyleDimension Spacing = StyleDimension.Empty;

    /// <summary>
    ///     прапорець, що вказує на те, що елементи знаходяться всередині <see cref="UIExStackPanel"/> Вони повинні бути розміщені в зворотному порядку.
    /// </summary>
    public bool Reverse = false;




    ///////////////////// CONSTRUCTORS ///////////////
    ///////////////////// CONSTRUCTORS ///////////////

    /// <summary/>
    public StyleLayoutContainerStackPanel() { }

    /// <summary/>
    public StyleLayoutContainerStackPanel(
        StyleDimension spacing = default(StyleDimension),
        bool reverse = false)
            => SetAllFields(spacing, reverse);




    ///////////////////// SETS ///////////////
    ///////////////////// SETS ///////////////

    /// <summary>
    ///     Вимагає передати всі можливі значення класу <see cref="StyleLayoutContainerStackPanel"/> і встановлюємо їх.
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




    ///////////////////// COPY ///////////////
    ///////////////////// COPY ///////////////

    /// <inheritdoc/>
    protected override StyleBase Fabricate() => new StyleLayoutContainerStackPanel();

    /// <inheritdoc/>
    protected override void CopyBase(StyleBase style)
    {
        if (style is StyleLayoutContainerStackPanel style2)
            Copy(style2);
    }

    /// <summary>
    ///     Скопіювати значення передається <see cref="StyleLayoutContainerStackPanel"/> 
    /// </summary>
    public void Copy(StyleLayoutContainerStackPanel style)
        => SetAllFields(
            style.Spacing,
            style.Reverse);

    /// <summary>
    ///     Створює і повертає копію поточного <see cref="StyleLayoutContainerStackPanel"/>
    /// </summary>
    public StyleLayoutContainerStackPanel GetCopy()
        => GetCopyBase<StyleLayoutContainerStackPanel>();




    ///////////////////// EQUALS ///////////////
    ///////////////////// EQUALS ///////////////

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