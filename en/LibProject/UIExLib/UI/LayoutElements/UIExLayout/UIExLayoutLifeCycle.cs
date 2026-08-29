using Terraria.UI;
using UIeXtension.MethodsExtensions;
namespace UIeXtension;

public abstract partial class UIExLayout : UIExElement
{
    /// <summary>
    ///     Flag indicating the prohibition of the method <see cref="UIElement.RecalculateChildren"/>
    /// </summary>
    /// <remarks>
    ///     Meaning. true This indicates that the recalculation of the size of the descendants will not be carried out.
    ///     <para>
    ///         It is used so that the layout of the child elements is performed earlier than the recalculation of their sizes.
    ///     </para>
    /// </remarks>
    private bool _pauseRecalculateChildren = false;

    /// <summary>
    ///     It retains all styles of elements.
    ///     Updated in the method <see cref="UpdateStyleLayoutChildArray"/>
    ///     This method is checked during the check for updating information from the latest layout.
    /// </summary>
    private Styles.StyleLayoutChild[] _styleLayoutChildArray;

    /// <summary>
    ///     Recalculation of sizes and arrangement of elements
    /// </summary>
    /// <remarks>
    ///     Analogue <see cref="UIElement.Recalculate"/> with the call of the method of life cycle layout: <see cref="RecalculateLayout"/>
    ///     <para>---</para>
    ///     <para>
    ///         Changes the order of calculation of child elements:
    ///         <para>Was: Recalculate -> RecalulateChildren() -> for(child).Recalculate()</para>
    ///         <para>Became: Recalculate -> RecalculateLayout(sic) RecalulateChildren() -> for(child).Recalculate()</para>
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
    ///     Analogue <see cref="UIElement.RecalculateChildren"/> flag-supported <see cref="_pauseRecalculateChildren"/>
    /// </summary>
    public override void RecalculateChildren()
    {
        if (_pauseRecalculateChildren)
            return;

        base.RecalculateChildren();
    }



    /// <summary>
    ///     Determines the life cycle of recalculation of the layout of child elements
    /// </summary>
    /// <remarks>
    ///     It is not recommended to override this method and interfere with the life cycle of the layout without an urgent need.
    ///     Called right after. <see cref="UIElement.Recalculate"/> which causes <see cref="UIElement.RemoveAllChildren"/>).
    ///     First, the current size is recalculated. <see cref="UIExLayout"/>. 
    ///     Then the child elements are recalculated, then this method is called.
    ///     See the description of the flag itself and its methods. <see cref="BeginLayoutPreparation"/> and <see cref="EndLayoutPreparation(UIState)"/>
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
    ///     Defines additional actions before the main stage of the layout.
    /// </summary>
    /// <remarks>
    ///     Called before. <see cref="RefreshLayout"/>.
    ///     V. <see cref="UIExLayout"/> By default the body of the method is empty.
    /// </remarks>
    protected virtual void PreRefreshLayout()
    {
    }

    /// <summary>
    ///     Defines the main part of the layout algorithm.
    /// </summary>
    /// <remarks>
    ///     Called before. <see cref="RefreshLayout"/>.
    ///     V. <see cref="UIExLayout"/> By default the body of the method is empty.
    /// </remarks>
    protected virtual void RefreshLayout() {}

    /// <summary>
    ///     Designed to apply the layout context to the child elements involved in the layout
    /// </summary>
    protected virtual void ApplyLayout()
    {
        foreach (var rectContext in _rectangleContexts)
            ApplyElementLayout(rectContext);
    }

    /// <summary>
    ///     Determines additional actions after the main stage of the layout.
    /// </summary>
    /// <remarks>
    ///     Called right after. <see cref="PreRefreshLayout"/> and before the call <see cref="PostRefreshLayout"/>.
    ///     V. <see cref="UIExLayout"/> By default the body of the method is empty.
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