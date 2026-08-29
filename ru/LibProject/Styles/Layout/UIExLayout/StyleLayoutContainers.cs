using UIeXtension.Styles.Base;

namespace UIeXtension.Styles;

/// <summary>
///     Базовый стиль компоновки всех для элементов-компоновки 
/// </summary>
public partial class StyleLayoutContainer : Base.StyleLayoutContainerBase
{

    /// <summary>
    ///     Определяет ось контейнера <see cref="UIExStackPanel"/>
    /// </summary>
    public Enums.UIExOrientation Orientation = Enums.UIExOrientation.Vertical;

    /// <summary>
    ///     Определяет стартовую точку компоновки <see cref="UIExLayout"/> относительное главной оси.
    /// </summary>
    public Enums.UIExAlignment JustifyContent = Enums.UIExAlignment.Auto;

    /// <summary>
    ///     Определяет стартовую точку компоновки <see cref="UIExLayout"/> относительно поперечной оси.
    /// </summary>
    public Enums.UIExAlignment AlignItems = Enums.UIExAlignment.Auto;




    ///////////////////// CONSTRUCTORS ////////////////////
    ///////////////////// CONSTRUCTORS ////////////////////

    /// <summary/>
    public StyleLayoutContainer() { }

    /// <summary/>
    public StyleLayoutContainer(
        Enums.UIExOrientation orientation = Enums.UIExOrientation.Vertical,
        Enums.UIExAlignment justifyContent = Enums.UIExAlignment.Auto,
        Enums.UIExAlignment alignItems = Enums.UIExAlignment.Auto)
            => SetAllFields(orientation, justifyContent, alignItems);




    //////////////////// SETS /////////////////////////
    //////////////////// SETS /////////////////////////

    /// <summary>
    ///     Требует передать все возможные значения класса <see cref="StyleLayoutContainer"/> и устанавливает их.
    /// </summary>
    public void Set(
        Enums.UIExOrientation orientation = Enums.UIExOrientation.Vertical,
        Enums.UIExAlignment justifyContent = Enums.UIExAlignment.Auto, 
        Enums.UIExAlignment alignItems = Enums.UIExAlignment.Auto)
            => SetAllFields(orientation, justifyContent, alignItems);

    /// <summary/>
    protected void SetAllFields(
        Enums.UIExOrientation orientation, 
        Enums.UIExAlignment justifyContent, 
        Enums.UIExAlignment alignItems)
    {
        Orientation = orientation;
        JustifyContent = justifyContent;
        AlignItems = alignItems;
    }




    //////////////////// COPY /////////////////////////
    //////////////////// COPY /////////////////////////

    /// <inheritdoc/>
    protected override StyleBase Fabricate() => new StyleLayoutContainer();

    /// <inheritdoc/>
    protected override void CopyBase(StyleBase style)
    {
        if (style is StyleLayoutContainer style2)
            Copy(style2);
    }

    /// <summary>
    ///     Копирует значения переданного <see cref="StyleLayoutContainer"/> 
    /// </summary>
    public void Copy(StyleLayoutContainer style)
        => SetAllFields(
            orientation:        style.Orientation,
            justifyContent:     style.JustifyContent,
            alignItems:         style.AlignItems);

    /// <summary>
    ///     Создает и возвращает копию текущего <see cref="StyleLayoutContainer"/>
    /// </summary>
    public StyleLayoutContainer GetCopy()
        => GetCopyBase<StyleLayoutContainer>();




    //////////////////// EQUALS /////////////////////////
    //////////////////// EQUALS /////////////////////////

    /// <inheritdoc/>
    protected override bool EqualsFields(StyleBase other)
    {
        if (other is StyleLayoutContainer style)
            return
                Orientation == style.Orientation &&
                JustifyContent == style.JustifyContent &&
                AlignItems == style.AlignItems;

        return false;
    }
}