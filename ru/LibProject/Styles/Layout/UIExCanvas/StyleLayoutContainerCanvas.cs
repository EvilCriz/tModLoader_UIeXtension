using UIeXtension.Styles.Base;

namespace UIeXtension.Styles;

/// <summary>
///     Стиль компоновки для <see cref="UIExCanvas"/> 
/// </summary>
public class StyleLayoutContainerCanvas : Base.StyleLayoutContainerBase
{
    /// <summary>
    ///     Указывает, может ли элемент выходить за границу <see cref="UIExCanvas"/>
    ///     По умолчанию: false
    /// </summary>
    public bool AllowOverflow = false;



    ///////////////////// CONSTRUCTORS ////////////////////
    ///////////////////// CONSTRUCTORS ////////////////////


    /// <summary/>
    public StyleLayoutContainerCanvas() { }

    /// <summary/>
    public StyleLayoutContainerCanvas(bool allowOverflow)
            => SetAllFields(allowOverflow);



    //////////////////// SETS /////////////////////////
    //////////////////// SETS /////////////////////////


    /// <summary>
    ///     Требует передать все возможные значения класса <see cref="StyleLayoutContainerCanvas"/> и устанавливает их.
    /// </summary>
    public void Set(bool allowOverflow = false)
        => SetAllFields(allowOverflow);

    /// <summary/>
    protected void SetAllFields(bool allowOverflow)
    {
        AllowOverflow = allowOverflow;
    }



    //////////////////// COPY /////////////////////////
    //////////////////// COPY /////////////////////////

    /// <inheritdoc/>
    protected override StyleBase Fabricate() => new StyleLayoutContainerCanvas();

    /// <inheritdoc/>
    protected override void CopyBase(StyleBase style)
    {
        if (style is StyleLayoutContainerCanvas style2)
            Copy(style2);
    }

    /// <summary>
    ///     Копирует значения переданного <see cref="StyleLayoutContainerCanvas"/> 
    /// </summary>
    public void Copy(StyleLayoutContainerCanvas style)
        => SetAllFields(
            allowOverflow: style.AllowOverflow);

    /// <summary>
    ///     Создает и возвращает копию текущего <see cref="StyleLayoutContainerCanvas"/>
    /// </summary>
    public StyleLayoutContainerCanvas GetCopy()
        => GetCopyBase<StyleLayoutContainerCanvas>();




    //////////////////// EQUALS /////////////////////////
    //////////////////// EQUALS /////////////////////////

    /// <inheritdoc/>
    protected override bool EqualsFields(StyleBase other)
    {
        if (other is StyleLayoutContainerCanvas style)
            return AllowOverflow == style.AllowOverflow;

        return false;
    }
}
