using Terraria.UI;
using UIeXtension.Styles.Base;

namespace UIeXtension.Styles;

/// <summary>
///     Класс стилей, которые являются общими для всех элементов <see cref="Terraria.UI.UIElement"/>.
/// </summary>
/// <remarks>
///     Стили применяются только к вложенным <see cref="Terraria.UI.UIElement"/> в дочерний класс <see cref="UIExLayout"/>
/// </remarks>
public partial class StyleLayoutChild : Base.StyleLayoutChildBase
{
    /// <summary>
    ///     Определяет стартовую точку компоновки <see cref="UIExLayout"/> относительное главной оси.
    ///     <para>При значении <see cref="Enums.UIExAlignment.Auto"/> используется значение элемента 
    ///     <see cref="StyleLayoutContainer.JustifyContent"/> компоновщика.</para>
    /// </summary>
    public Enums.UIExAlignment JustifySelf = Enums.UIExAlignment.Auto;

    /// <summary>
    ///     Определяет стартовую точку компоновки <see cref="UIExLayout"/> относительно поперечной оси.
    ///     <para>При значении <see cref="Enums.UIExAlignment.Auto"/> используется значение элемента 
    ///     <see cref="StyleLayoutContainer.AlignItems"/> компоновщика.</para>
    /// </summary>
    public Enums.UIExAlignment AlignSelf = Enums.UIExAlignment.Auto;

    /// <summary/>
    public UIExThickness Margin = default(UIExThickness);

    /// <summary>
    ///     Флаг, указывающий, что элемент не должен учитываться в компоновке <see cref="UIExLayout"/>, 
    ///     даже если он является дочерним элементом класс компоновки.
    /// </summary>
    public bool WithoutLayout = false;


    /// <summary>
    ///     Данное значение используют только компоновщики данной библиотеки у вложенных в них элементов.
    /// </summary>
    public StyleDimension Width = default(StyleDimension);

    /// <summary>
    ///     Данное значение используют только компоновщики данной библиотеки у вложенных в них элементов.
    /// </summary>
    public StyleDimension Height = default(StyleDimension);




    ///////////////////// CONSTRUCTORS ////////////////////
    ///////////////////// CONSTRUCTORS ////////////////////

    /// <summary/>
    public StyleLayoutChild() { }
    
    /// <summary/>
    public StyleLayoutChild(
        Enums.UIExAlignment justifySelf = Enums.UIExAlignment.Auto,
        Enums.UIExAlignment alignSelf = Enums.UIExAlignment.Auto,
        UIExThickness margin = default(UIExThickness),
        bool withoutLayout = false,
        StyleDimension width = default(StyleDimension),
        StyleDimension height = default(StyleDimension))
            => SetAllFields(justifySelf, alignSelf, margin, withoutLayout, width, height);




    //////////////////// SETS /////////////////////////
    //////////////////// SETS /////////////////////////

    /// <summary>
    ///     Требует передать все возможные значения класса <see cref="StyleLayoutChild"/> и устанавливает их.
    /// </summary>
    public void Set(
        Enums.UIExAlignment justifySelf = Enums.UIExAlignment.Auto,
        Enums.UIExAlignment alignSelf = Enums.UIExAlignment.Auto,
        UIExThickness margin = default(UIExThickness),
        bool withoutLayout = false,
        StyleDimension width = default(StyleDimension),
        StyleDimension height = default(StyleDimension))
            => SetAllFields(justifySelf, alignSelf, margin, withoutLayout, width, height);

    /// <summary/>
    protected void SetAllFields(
        Enums.UIExAlignment justifySelf,
        Enums.UIExAlignment alignSelf,
        UIExThickness margin,
        bool withoutLayout,
        StyleDimension width,
        StyleDimension height)
    {
        JustifySelf = justifySelf;
        AlignSelf = alignSelf;
        Margin = margin;
        WithoutLayout = withoutLayout;
        Width = width;
        Height = height;
    }




    //////////////////// COPY /////////////////////////
    //////////////////// COPY /////////////////////////

    /// <inheritdoc/>
    protected override StyleBase Fabricate() => new StyleLayoutChild();

    /// <inheritdoc/>
    protected override void CopyBase(StyleBase style)
    {
        if (style is StyleLayoutChild style2)
            Copy(style2);
    }

    /// <summary>
    ///     Копирует значения переданного <see cref="StyleLayoutChild"/> 
    /// </summary>
    public void Copy(StyleLayoutChild style)
        => SetAllFields(
            justifySelf:    style.JustifySelf,
            alignSelf:      style.AlignSelf,
            margin:         style.Margin,
            withoutLayout:  style.WithoutLayout,
            width:          style.Width,
            height:         style.Height);

    /// <summary>
    ///     Создает и возвращает копию текущего <see cref="StyleLayoutChild"/>
    /// </summary>
    public StyleLayoutChild GetCopy()
        => GetCopyBase<StyleLayoutChild>();




    //////////////////// EQUALS /////////////////////////
    //////////////////// EQUALS /////////////////////////

    /// <inheritdoc/>
    protected override bool EqualsFields(StyleBase other)
    {
        if (other is StyleLayoutChild style)
            return
                JustifySelf == style.JustifySelf &&
                AlignSelf == style.AlignSelf &&
                Margin.EqualsFields(style.Margin) &&
                WithoutLayout == style.WithoutLayout &&
                Utils.UtilsStyles.EqualsStyleDimensionFields(Width, style.Width) &&
                Utils.UtilsStyles.EqualsStyleDimensionFields(Height, style.Height);

        return false;
    }
}