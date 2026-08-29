namespace UIeXtension;

public abstract partial class UIExLayout : UIExElement
{
    private Styles.StyleLayoutContainer _styleLayout = null;

    /// <summary>
    ///     Указывает базовый стиль компоновки дочерних элементов
    /// </summary>
    public Styles.StyleLayoutContainer StyleLayout
    {
        get => _styleLayout;
        set => _styleLayout = value;
    }



    /// <summary>
    ///     Устанавливает размер переданного элемента в весь размер родительского элемента.
    /// </summary>
    public virtual void Stretch()
    {
        Top.Set(0f, 0f);
        Left.Set(0f, 0f);
        Height.Set(0f, 1f);
        Width.Set(0f, 1f);
    }
}