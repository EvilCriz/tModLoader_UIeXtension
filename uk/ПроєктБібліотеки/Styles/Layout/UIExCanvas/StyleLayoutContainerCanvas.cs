using UIeXtension.Styles.Base;

namespace UIeXtension.Styles;

/// <summary>
///     Стиль макета для <see cref="UIExCanvas"/> 
/// </summary>
public class StyleLayoutContainerCanvas : Base.StyleLayoutContainerBase
{
    /// <summary>
    ///     Повідомляємо, що елемент може піти за кордоном <see cref="UIExCanvas"/>
    ///     За замовчуванням: false
    /// </summary>
    public bool AllowOverflow = false;



    ///////////////////// CONSTRUCTORS ///////////////
    ///////////////////// CONSTRUCTORS ///////////////


    /// <summary/>
    public StyleLayoutContainerCanvas() { }

    /// <summary/>
    public StyleLayoutContainerCanvas(bool allowOverflow)
            => SetAllFields(allowOverflow);



    //////////////////// SETS /////////////////
    //////////////////// SETS /////////////////


    /// <summary>
    ///     Вимагає передати всі можливі значення класу <see cref="StyleLayoutContainerCanvas"/> і встановлюємо їх.
    /// </summary>
    public void Set(bool allowOverflow = false)
        => SetAllFields(allowOverflow);

    /// <summary/>
    protected void SetAllFields(bool allowOverflow)
    {
        AllowOverflow = allowOverflow;
    }



    //////////////////// COPY /////////////////
    //////////////////// COPY /////////////////

    /// <inheritdoc/>
    protected override StyleBase Fabricate() => new StyleLayoutContainerCanvas();

    /// <inheritdoc/>
    protected override void CopyBase(StyleBase style)
    {
        if (style is StyleLayoutContainerCanvas style2)
            Copy(style2);
    }

    /// <summary>
    ///     Скопіювати значення передається <see cref="StyleLayoutContainerCanvas"/> 
    /// </summary>
    public void Copy(StyleLayoutContainerCanvas style)
        => SetAllFields(
            allowOverflow: style.AllowOverflow);

    /// <summary>
    ///     Створює і повертає копію поточного <see cref="StyleLayoutContainerCanvas"/>
    /// </summary>
    public StyleLayoutContainerCanvas GetCopy()
        => GetCopyBase<StyleLayoutContainerCanvas>();




    //////////////////// EQUALS /////////////////
    //////////////////// EQUALS /////////////////

    /// <inheritdoc/>
    protected override bool EqualsFields(StyleBase other)
    {
        if (other is StyleLayoutContainerCanvas style)
            return AllowOverflow == style.AllowOverflow;

        return false;
    }
}
