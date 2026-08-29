using UIeXtension.Styles.Base;

namespace UIeXtension.Styles;

/// <summary>
///     Клас стилів, які є загальними для всіх елементів <see cref="Terraria.UI.UIElement"/>. .
/// </summary>за допомогою System.Collections.Generic;
public class StyleLayoutChildDockPanel : Base.StyleLayoutChildBase
{
    /// <summary>
    ///     Партія, до якої притискається дитячий елемент. 
    /// </summary>
    public Enums.UIExSide Side = Enums.UIExSide.Left;



    ///////////////////// CONSTRUCTORS ///////////////
    ///////////////////// CONSTRUCTORS ///////////////


    /// <summary/>
    public StyleLayoutChildDockPanel() { }

    /// <summary/>
    public StyleLayoutChildDockPanel(Enums.UIExSide side = Enums.UIExSide.Left)
            => SetAllFields(side);



    //////////////////// SETS /////////////////
    //////////////////// SETS /////////////////


    /// <summary>
    ///     Вимагає передати всі можливі значення класу <see cref="StyleLayoutChildDockPanel"/> і встановлюємо їх.
    /// </summary>
    public void Set(
        Enums.UIExSide side = Enums.UIExSide.Left)
            => SetAllFields(side);

    /// <summary/>
    protected void SetAllFields(Enums.UIExSide side)
    {
        Side = side;
    }



    //////////////////// COPY /////////////////
    //////////////////// COPY /////////////////

    /// <inheritdoc/>
    protected override StyleBase Fabricate() => new StyleLayoutChildDockPanel();

    /// <inheritdoc/>
    protected override void CopyBase(StyleBase style)
    {
        if (style is StyleLayoutChildDockPanel style2)
            Copy(style2);
    }

    /// <summary>
    ///     Скопіювати значення передається <see cref="StyleLayoutChildDockPanel"/> 
    /// </summary>
    public void Copy(StyleLayoutChildDockPanel style)
        => SetAllFields(
                side: style.Side);

    /// <summary>
    ///     Створює і повертає копію поточного <see cref="StyleLayoutChildDockPanel"/>
    /// </summary>
    public StyleLayoutChildDockPanel GetCopy()
        => GetCopyBase<StyleLayoutChildDockPanel>();



    //////////////////// EQUALS /////////////////
    //////////////////// EQUALS /////////////////


    /// <inheritdoc/>
    protected override bool EqualsFields(StyleBase other)
    {
        if (other is StyleLayoutChildDockPanel style)
            return Side == style.Side;

        return false;
    }
}
