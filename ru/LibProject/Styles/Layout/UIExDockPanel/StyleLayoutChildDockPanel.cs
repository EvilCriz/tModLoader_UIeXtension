using UIeXtension.Styles.Base;

namespace UIeXtension.Styles;

/// <summary>
///     Класс стилей, которые являются общими для всех элементов <see cref="Terraria.UI.UIElement"/>.
/// </summary>using System.Collections.Generic;
public class StyleLayoutChildDockPanel : Base.StyleLayoutChildBase
{
    /// <summary>
    ///     Сторона, к которой прижат данный дочерний элемент. 
    /// </summary>
    public Enums.UIExSide Side = Enums.UIExSide.Left;



    ///////////////////// CONSTRUCTORS ////////////////////
    ///////////////////// CONSTRUCTORS ////////////////////


    /// <summary/>
    public StyleLayoutChildDockPanel() { }

    /// <summary/>
    public StyleLayoutChildDockPanel(Enums.UIExSide side = Enums.UIExSide.Left)
            => SetAllFields(side);



    //////////////////// SETS /////////////////////////
    //////////////////// SETS /////////////////////////


    /// <summary>
    ///     Требует передать все возможные значения класса <see cref="StyleLayoutChildDockPanel"/> и устанавливает их.
    /// </summary>
    public void Set(
        Enums.UIExSide side = Enums.UIExSide.Left)
            => SetAllFields(side);

    /// <summary/>
    protected void SetAllFields(Enums.UIExSide side)
    {
        Side = side;
    }



    //////////////////// COPY /////////////////////////
    //////////////////// COPY /////////////////////////

    /// <inheritdoc/>
    protected override StyleBase Fabricate() => new StyleLayoutChildDockPanel();

    /// <inheritdoc/>
    protected override void CopyBase(StyleBase style)
    {
        if (style is StyleLayoutChildDockPanel style2)
            Copy(style2);
    }

    /// <summary>
    ///     Копирует значения переданного <see cref="StyleLayoutChildDockPanel"/> 
    /// </summary>
    public void Copy(StyleLayoutChildDockPanel style)
        => SetAllFields(
                side: style.Side);

    /// <summary>
    ///     Создает и возвращает копию текущего <see cref="StyleLayoutChildDockPanel"/>
    /// </summary>
    public StyleLayoutChildDockPanel GetCopy()
        => GetCopyBase<StyleLayoutChildDockPanel>();



    //////////////////// EQUALS /////////////////////////
    //////////////////// EQUALS /////////////////////////


    /// <inheritdoc/>
    protected override bool EqualsFields(StyleBase other)
    {
        if (other is StyleLayoutChildDockPanel style)
            return Side == style.Side;

        return false;
    }
}
