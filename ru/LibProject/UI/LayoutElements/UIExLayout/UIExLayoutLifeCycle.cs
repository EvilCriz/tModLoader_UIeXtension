using Terraria.UI;
using UIeXtension.MethodsExtensions;
namespace UIeXtension;

public abstract partial class UIExLayout : UIExElement
{
    /// <summary>
    ///     Флаг, указывающий на запрет работы метода <see cref="UIElement.RecalculateChildren"/>
    /// </summary>
    /// <remarks>
    ///     Значение true указывает на то, что перерасчет размеров потомков не будет осуществляться.
    ///     <para>
    ///         Используется для того, чтобы компоновка дочерних элементов выполнялась раньше, чем пересчет их размеров.
    ///     </para>
    /// </remarks>
    private bool _pauseRecalculateChildren = false;

    /// <summary>
    ///     Хранит в себе все стили элементов.
    ///     Обновляется в методе <see cref="UpdateStyleLayoutChildArray"/>
    ///     Данный метод проверяется во время проверки на обновление информации с последней компоновки
    /// </summary>
    private Styles.StyleLayoutChild[] _styleLayoutChildArray;

    /// <summary>
    ///     Перерасчет размеров и расположения элементов
    /// </summary>
    /// <remarks>
    ///     Аналог <see cref="UIElement.Recalculate"/> с вызовом метода жизненного цикла компоновки: <see cref="RecalculateLayout"/>
    ///     <para>---</para>
    ///     <para>
    ///         Меняет местами порядок расчета дочерних элементов:
    ///         <para>Было: Recalculate -> RecalulateChildren() -> for(child).Recalculate()</para>
    ///         <para>Стало: Recalculate -> RecalculateLayout() -> RecalulateChildren() -> for(child).Recalculate()</para>
    ///     </para>
    /// </remarks>
    public override void Recalculate()
    {
        _pauseRecalculateChildren = true;
        base.Recalculate();
        _pauseRecalculateChildren = false;
        RecalculateLayout();
        RecalculateChildren();
    }

    /// <summary>
    ///     Аналог <see cref="UIElement.RecalculateChildren"/> с поддержкой флага <see cref="_pauseRecalculateChildren"/>
    /// </summary>
    public override void RecalculateChildren()
    {
        if (_pauseRecalculateChildren)
            return;

        base.RecalculateChildren();
    }



    /// <summary>
    ///     Определяет жизненный цикл перерасчета компоновки дочерних элементов
    /// </summary>
    /// <remarks>
    ///     Не рекомендуется переопределять данный метод и вмешиваться в жизненный цикл компоновки без острой необходимости.
    ///     Вызывается сразу после <see cref="UIElement.Recalculate"/> (который вызывает <see cref="UIElement.RemoveAllChildren"/>).
    ///     Сперва перерасчитываются размеры текущего <see cref="UIExLayout"/>, 
    ///     затем перерасчитываются дочерних элементов, затем вызывается данный метод.
    ///     Подробнее смотрите в описании самого флага и методов <see cref="BeginLayoutPreparation"/> и <see cref="EndLayoutPreparation(UIState)"/>
    /// </remarks>
    protected virtual void RecalculateLayout()
    {
        var innerDimension = GetInnerDimensions();
        if (innerDimension.Width == 0f && innerDimension.Height == 0f)
            return;

        if (IsLayoutPreparation())
            return;

        if (_remainingRecalculateLayoutDelayMs > 0)
            return;

        UpdateStyleLayoutChildArray();

        if (!IsLastLayoutInfoChanged())
            return;

        BeginLayoutContext();

        try
        {
            PreRefreshLayout();
            RefreshLayout();
            PostRefreshLayout();

            ApplyLayout();

            RefreshLayoutDebugLines();
        }
        catch(System.Exception)
        {
            throw new System.Exception("[UIeXtension] RecalculateLayout Exception");
        }
        finally
        {
            EndLayoutContext();
        }

        UpdateLastLayoutInfo();

        int? recalculateLayoutDelayMsState = GetRecalculateLayoutDelayMsState(this);
        _remainingRecalculateLayoutDelayMs =
            recalculateLayoutDelayMsState is not null
                ? (int)recalculateLayoutDelayMsState
                : RecalculateLayoutDelayMs;
    }

    private void UpdateStyleLayoutChildArray()
    {
        _styleLayoutChildArray = new Styles.StyleLayoutChild[Elements.Count];
        for (int i = 0; i < Elements.Count; i++)
            _styleLayoutChildArray[i] = Elements[i].StyleLayoutChild();
    }

    /// <summary>
    ///     Определяет дополнительные действия перед основным этапом компоновки.
    /// </summary>
    /// <remarks>
    ///     Вызывается перед <see cref="RefreshLayout"/>.
    ///     В <see cref="UIExLayout"/> по умолчанию тело метода пустое.
    /// </remarks>
    protected virtual void PreRefreshLayout()
    {
    }

    /// <summary>
    ///     Определяет основную часть алгоритма компоновки.
    /// </summary>
    /// <remarks>
    ///     Вызывается перед <see cref="RefreshLayout"/>.
    ///     В <see cref="UIExLayout"/> по умолчанию тело метода пустое.
    /// </remarks>
    protected virtual void RefreshLayout() {}

    /// <summary>
    ///     Предназначен для применения контекста компоновки к дочерним элементам, участвующих в компоновке
    /// </summary>
    protected virtual void ApplyLayout()
    {
        foreach (var rectContext in _rectangleContexts)
            ApplyElementLayout(rectContext);
    }

    /// <summary>
    ///     Определяет дополнительные действия после основного этапа компоновки.
    /// </summary>
    /// <remarks>
    ///     Вызывается сразу после <see cref="PreRefreshLayout"/> и перед вызовом <see cref="PostRefreshLayout"/>.
    ///     В <see cref="UIExLayout"/> по умолчанию тело метода пустое.
    /// </remarks>
    protected virtual void PostRefreshLayout() {}

    /// <summary/>
    protected virtual void ApplyElementLayout(RectangleLayoutContext rectContext)
    {
        UIElement element = _elementsContext[rectContext.Index];

        element.Top.Set(rectContext.Top, 0f);
        element.Left.Set(rectContext.Left, 0f);
        element.Width.Set(rectContext.Width, 0f);
        element.Height.Set(rectContext.Height, 0f);
    }
}