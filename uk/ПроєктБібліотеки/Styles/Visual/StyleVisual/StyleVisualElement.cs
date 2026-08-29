using Terraria.UI;
using UIeXtension.Styles.Base;

namespace UIeXtension.Styles;


/// <summary>
///     Стильний стіл, який містить всі UIElements Бібліотека
/// </summary>
public partial class StyleVisualElement : Base.StyleVisualBase
{
    /// <summary>
    ///     Призначає, що базові параметри стилю цього візуального елемента
    ///     повинен бути схожим <see cref="Terraria.GameContent.UI.Elements.UIPanel"/>
    /// </summary>
    public bool tModLoaderStyle = false;

    /// <summary>
    ///     Повідомляє, чи повинен елемент автоматично контролювати значення:
    ///     <see cref="UIElement.PaddingTop"/>
    ///     <see cref="UIElement.PaddingLeft"/>
    ///     <see cref="UIElement.PaddingBottom"/>
    ///     <see cref="UIElement.PaddingRight"/>
    /// </summary>
    public bool PaddingAutoControl = true;


    ///////////////////// CONSTRUCTORS ///////////////
    ///////////////////// CONSTRUCTORS ///////////////

    /// <summary/>
    public StyleVisualElement() { }

    /// <summary/>
    public StyleVisualElement(bool tmodLoaderStyle = false, bool paddingAutoControl = false)
        => SetAllFields(tmodLoaderStyle, paddingAutoControl);




    ///////////////////// SETS ///////////////
    ///////////////////// SETS ///////////////

    /// <summary>
    ///     Вимагає передати всі можливі значення класу <see cref="StyleVisualElement"/> і встановлюємо їх.
    /// </summary>
    public void Set(bool tmodLoaderStyle = false, bool paddingAutoControl = false)
        => SetAllFields(tmodLoaderStyle, paddingAutoControl);

    /// <summary/>
    protected void SetAllFields(bool tmodLoaderStyle, bool paddingAutoControl)
    {
        tModLoaderStyle = tmodLoaderStyle;
        PaddingAutoControl = paddingAutoControl;
    }




    ///////////////////// COPY ///////////////
    ///////////////////// COPY ///////////////

    /// <inheritdoc/>
    protected override StyleBase Fabricate() => new StyleVisualElement();

    /// <inheritdoc/>
    protected override void CopyBase(StyleBase style)
    {
        if (style is StyleVisualElement style2)
            Copy(style2);
    }

    /// <summary>
    ///     Скопіювати значення передається <see cref="StyleVisualElement"/> 
    /// </summary>
    public void Copy(StyleVisualElement style)
        => SetAllFields(
            tmodLoaderStyle:        style.tModLoaderStyle,
            paddingAutoControl:     style.PaddingAutoControl);

    /// <summary>
    ///     Створює і повертає копію поточного <see cref="StyleVisualElement"/>
    /// </summary>
    public StyleVisualElement GetCopy()
        => GetCopyBase<StyleVisualElement>();        




    ///////////////////// EQUALS ///////////////
    ///////////////////// EQUALS ///////////////

    /// <inheritdoc/>
    protected override bool EqualsFields(StyleBase other)
    {
        if(other is StyleVisualElement otherStyle)
            return tModLoaderStyle == otherStyle.tModLoaderStyle &&
                PaddingAutoControl == otherStyle.PaddingAutoControl;

        return false;
    }
}