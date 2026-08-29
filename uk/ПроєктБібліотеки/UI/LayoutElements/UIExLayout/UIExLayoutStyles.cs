namespace UIeXtension;

public abstract partial class UIExLayout : UIExElement
{
    private Styles.StyleLayoutContainer _styleLayout = null;

    /// <summary>
    ///     Призначає базовий стиль макету дитячих елементів
    /// </summary>
    public Styles.StyleLayoutContainer StyleLayout
    {
        get => _styleLayout;
        set => _styleLayout = value;
    }



    /// <summary>
    ///     Встановити розмір переданого елемента на весь розмір батьківського елемента.
    /// </summary>
    public virtual void Stretch()
    {
        Top.Set(0f, 0f);
        Left.Set(0f, 0f);
        Height.Set(0f, 1f);
        Width.Set(0f, 1f);
    }
}