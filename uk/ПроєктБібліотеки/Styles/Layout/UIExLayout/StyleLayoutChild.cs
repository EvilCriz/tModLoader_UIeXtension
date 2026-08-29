using Terraria.UI;
using UIeXtension.Styles.Base;

namespace UIeXtension.Styles;

/// <summary>
///     Клас стилів, які є загальними для всіх елементів <see cref="Terraria.UI.UIElement"/>. .
/// </summary>
/// <remarks>
///     Стилі застосовуються тільки в гніздах <see cref="Terraria.UI.UIElement"/> дочка-клас <see cref="UIExLayout"/>
/// </remarks>
public partial class StyleLayoutChild : Base.StyleLayoutChildBase
{
    /// <summary>
    ///     Визначення початкової точки макета <see cref="UIExLayout"/> відносно основної осі.
    ///     <para>Симфонія <see cref="Enums.UIExAlignment.Auto"/> елементарний 
    ///     <see cref="StyleLayoutContainer.JustifyContent"/> будівельник.</para>
    /// </summary>
    public Enums.UIExAlignment JustifySelf = Enums.UIExAlignment.Auto;

    /// <summary>
    ///     Визначення початкової точки макета <see cref="UIExLayout"/> відносно поперечної осі.
    ///     <para>Симфонія <see cref="Enums.UIExAlignment.Auto"/> елементарний 
    ///     <see cref="StyleLayoutContainer.AlignItems"/> будівельник.</para>
    /// </summary>
    public Enums.UIExAlignment AlignSelf = Enums.UIExAlignment.Auto;

    /// <summary/>
    public UIExThickness Margin = default(UIExThickness);

    /// <summary>
    ///     прапорець, що вказує на те, що елемент не повинен бути включений в макет <see cref="UIExLayout"/>. . 
    ///     Навіть якщо це дочка елемента верстки.
    /// </summary>
    public bool WithoutLayout = false;


    /// <summary>
    ///     Дане значення використовується тільки за допомогою посилань даної бібліотеки для елементів, вбудованих в них.
    /// </summary>
    public StyleDimension Width = default(StyleDimension);

    /// <summary>
    ///     Дане значення використовується тільки за допомогою посилань даної бібліотеки для елементів, вбудованих в них.
    /// </summary>
    public StyleDimension Height = default(StyleDimension);




    ///////////////////// CONSTRUCTORS ///////////////
    ///////////////////// CONSTRUCTORS ///////////////

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




    //////////////////// SETS /////////////////
    //////////////////// SETS /////////////////

    /// <summary>
    ///     Вимагає передати всі можливі значення класу <see cref="StyleLayoutChild"/> і встановлюємо їх.
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




    //////////////////// COPY /////////////////
    //////////////////// COPY /////////////////

    /// <inheritdoc/>
    protected override StyleBase Fabricate() => new StyleLayoutChild();

    /// <inheritdoc/>
    protected override void CopyBase(StyleBase style)
    {
        if (style is StyleLayoutChild style2)
            Copy(style2);
    }

    /// <summary>
    ///     Скопіювати значення передається <see cref="StyleLayoutChild"/> 
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
    ///     Створює і повертає копію поточного <see cref="StyleLayoutChild"/>
    /// </summary>
    public StyleLayoutChild GetCopy()
        => GetCopyBase<StyleLayoutChild>();




    //////////////////// EQUALS /////////////////
    //////////////////// EQUALS /////////////////

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