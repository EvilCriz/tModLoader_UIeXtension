using Terraria.UI;
using UIeXtension.MethodsExtensions;
namespace UIeXtension;

public abstract partial class UIExLayout : UIExElement
{
    /// <summary>
    ///     Прапор із зазначенням заборони способу <see cref="UIElement.RecalculateChildren"/>
    /// </summary>
    /// <remarks>
    ///     Значення. true Це свідчить про те, що перерахунок розмірів нащадків не буде здійснюватися.
    ///     <para>
    ///         Застосовується таким чином, що планування елементів дитини виконується раніше, ніж перерахунок їх розмірів.
    ///     </para>
    /// </remarks>
    private bool _pauseRecalculateChildren = false;

    /// <summary>
    ///     Підтримує всі стилі елементів.
    ///     Оновлено в методі <see cref="UpdateStyleLayoutChildArray"/>
    ///     Цей метод перевіряється під час перевірки для оновлення інформації з останнього макета.
    /// </summary>
    private Styles.StyleLayoutChild[] _styleLayoutChildArray;

    /// <summary>
    ///     Регуляція розмірів і розташування елементів
    /// </summary>
    /// <remarks>
    ///     Аналоговий <see cref="UIElement.Recalculate"/> з викликом методу планування життєвого циклу: <see cref="RecalculateLayout"/>
    ///     <para>Доля</para>
    ///     <para>
    ///         Зміни порядку розрахунку дитячих елементів:
    ///         <para>Був: Recalculate -> RecalulateChildren() -> для(child).Recalculate()</para>
    ///         <para>Became: Recalculate -> Редакція -> RecalculateLayout(сік) RecalulateChildren() -> для(child).Recalculate()</para>
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
    ///     Аналоговий <see cref="UIElement.RecalculateChildren"/> прапор-підтримка <see cref="_pauseRecalculateChildren"/>
    /// </summary>
    public override void RecalculateChildren()
    {
        if (_pauseRecalculateChildren)
            return;

        base.RecalculateChildren();
    }



    /// <summary>
    ///     Визначення життєвого циклу рекалькуляції макету дитячих елементів
    /// </summary>
    /// <remarks>
    ///     Не рекомендується перевизначити цей метод і заважати життєвий цикл макета без невідкладної потреби.
    ///     Зателефонуйте прямо після. <see cref="UIElement.Recalculate"/> які причини <see cref="UIElement.RemoveAllChildren"/>. . .
    ///     По-перше, поточний розмір рекалькулюється. <see cref="UIExLayout"/>. . 
    ///     Потім перераховують елементи дитини, потім цей метод називається.
    ///     Дивитися опис самого прапора і його методів. <see cref="BeginLayoutPreparation"/> і <see cref="EndLayoutPreparation(UIState)"/>
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
    ///     Визначте додаткові дії перед основною черги макета.
    /// </summary>
    /// <remarks>
    ///     Зателефонуйте до. <see cref="RefreshLayout"/>. .
    ///     В. В. <see cref="UIExLayout"/> За замовчуванням тіло методу порожній.
    /// </remarks>
    protected virtual void PreRefreshLayout()
    {
    }

    /// <summary>
    ///     Визначте основну частину алгоритму макета.
    /// </summary>
    /// <remarks>
    ///     Зателефонуйте до. <see cref="RefreshLayout"/>. .
    ///     В. В. <see cref="UIExLayout"/> За замовчуванням тіло методу порожній.
    /// </remarks>
    protected virtual void RefreshLayout() {}

    /// <summary>
    ///     Призначений для застосування контексту макета до дочірніх елементів, залучених до макета
    /// </summary>
    protected virtual void ApplyLayout()
    {
        foreach (var rectContext in _rectangleContexts)
            ApplyElementLayout(rectContext);
    }

    /// <summary>
    ///     Визначає додаткові дії після основного етапу макета.
    /// </summary>
    /// <remarks>
    ///     Зателефонуйте прямо після. <see cref="PreRefreshLayout"/> і до виклику <see cref="PostRefreshLayout"/>. .
    ///     В. В. <see cref="UIExLayout"/> За замовчуванням тіло методу порожній.
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