using Terraria.UI;
using UIeXtension.Styles.Base;

namespace UIeXtension.Styles;

/// <summary>
///     Базовый стиль компоновки для <see cref="UIExStackPanel"/> и всех наследников
/// </summary>
public class StyleLayoutContainerStackPanel : Base.StyleLayoutContainerBase
{
    /// <summary>
    ///     Определяет отступ между расположенными элементами в <see cref="UIExStackPanel"/>
    /// </summary>
    public StyleDimension Spacing = StyleDimension.Empty;

    /// <summary>
    ///     Флаг, указывающий, что элементы внутри <see cref="UIExStackPanel"/> должны располагаться в обратном порядке.
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
    ///     Требует передать все возможные значения класса <see cref="StyleLayoutContainerStackPanel"/> и устанавливает их.
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
    ///     Копирует значения переданного <see cref="StyleLayoutContainerStackPanel"/> 
    /// </summary>
    public void Copy(StyleLayoutContainerStackPanel style)
        => SetAllFields(
            style.Spacing,
            style.Reverse);

    /// <summary>
    ///     Создает и возвращает копию текущего <see cref="StyleLayoutContainerStackPanel"/>
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