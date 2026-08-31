namespace UIeXtension;

public abstract partial class UIExLayout : UIExElement
{
    private Styles.StyleLayoutContainer _styleLayout = null;

    /// <summary>
    ///     Indicates the basic style of the layout of child elements
    /// </summary>
    public Styles.StyleLayoutContainer StyleLayout
    {
        get => _styleLayout;
        set => _styleLayout = value;
    }



    /// <summary>
    ///     Set the size of the transferred element to the entire size of the parent element.
    /// </summary>
    public virtual void Stretch()
    {
        Top.Set(0f, 0f);
        Left.Set(0f, 0f);
        Height.Set(0f, 1f);
        Width.Set(0f, 1f);
    }
}