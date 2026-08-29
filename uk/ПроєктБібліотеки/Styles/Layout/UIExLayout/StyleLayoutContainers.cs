using UIeXtension.Styles.Base;

namespace UIeXtension.Styles;

/// <summary>
///     Базовий стиль макета для всіх елементів-компонентів 
/// </summary>
public partial class StyleLayoutContainer : Base.StyleLayoutContainerBase
{

    /// <summary>
    ///     Визначаємо вісь контейнера <see cref="UIExStackPanel"/>
    /// </summary>
    public Enums.UIExOrientation Orientation = Enums.UIExOrientation.Vertical;

    /// <summary>
    ///     Визначення початкової точки макета <see cref="UIExLayout"/> відносно основної осі.
    /// </summary>
    public Enums.UIExAlignment JustifyContent = Enums.UIExAlignment.Auto;

    /// <summary>
    ///     Визначення початкової точки макета <see cref="UIExLayout"/> відносно поперечної осі.
    /// </summary>
    public Enums.UIExAlignment AlignItems = Enums.UIExAlignment.Auto;




    ///////////////////// CONSTRUCTORS ///////////////
    ///////////////////// CONSTRUCTORS ///////////////

    /// <summary/>
    public StyleLayoutContainer() { }

    /// <summary/>
    public StyleLayoutContainer(
        Enums.UIExOrientation orientation = Enums.UIExOrientation.Vertical,
        Enums.UIExAlignment justifyContent = Enums.UIExAlignment.Auto,
        Enums.UIExAlignment alignItems = Enums.UIExAlignment.Auto)
            => SetAllFields(orientation, justifyContent, alignItems);




    //////////////////// SETS /////////////////
    //////////////////// SETS /////////////////

    /// <summary>
    ///     Вимагає передати всі можливі значення класу <see cref="StyleLayoutContainer"/> і встановлюємо їх.
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




    //////////////////// COPY /////////////////
    //////////////////// COPY /////////////////

    /// <inheritdoc/>
    protected override StyleBase Fabricate() => new StyleLayoutContainer();

    /// <inheritdoc/>
    protected override void CopyBase(StyleBase style)
    {
        if (style is StyleLayoutContainer style2)
            Copy(style2);
    }

    /// <summary>
    ///     Скопіювати значення передається <see cref="StyleLayoutContainer"/> 
    /// </summary>
    public void Copy(StyleLayoutContainer style)
        => SetAllFields(
            orientation:        style.Orientation,
            justifyContent:     style.JustifyContent,
            alignItems:         style.AlignItems);

    /// <summary>
    ///     Створює і повертає копію поточного <see cref="StyleLayoutContainer"/>
    /// </summary>
    public StyleLayoutContainer GetCopy()
        => GetCopyBase<StyleLayoutContainer>();




    //////////////////// EQUALS /////////////////
    //////////////////// EQUALS /////////////////

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