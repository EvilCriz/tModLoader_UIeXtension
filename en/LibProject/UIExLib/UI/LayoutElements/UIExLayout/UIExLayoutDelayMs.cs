using Microsoft.Xna.Framework;
using Terraria.UI;

namespace UIeXtension;

/// <summary>
///     Auxiliary class for the table <see cref="System.Runtime.CompilerServices.ConditionalWeakTable{TKey, TValue}"/>
/// </summary>
public class RecalculateLayoutStateDelay
{
    /// <summary>
    ///     Millisecond delay
    /// </summary>
    public int? DelayMs = 0;
}

public abstract partial class UIExLayout : UIExElement
{
    /// <summary>
    ///     Stores a table with a minimum delay (in milliseconds) between the restatement of the layout for all containers in <see cref="UIState"/>.
    /// </summary>
    /// <remarks>
    ///     The minimum delay value is separate for each <see cref="UIState"/>.
    ///     If <see cref="UIState"/> is not contained in this table - the linker uses the value: <see cref="RecalculateLayoutDelayMs"/>
    /// </remarks>
    private static readonly System.Runtime.CompilerServices.
        ConditionalWeakTable<UIState, RecalculateLayoutStateDelay> _statesRecalculateDalayMs = new();

    /// <summary>
    ///     Minimum delay (in milliseconds) between recalculation of the layout.
    ///     It does not do anything for the current <see cref="UIState"/> has been established in
    ///     table <see cref="_statesRecalculateDalayMs"/> not null
    /// </summary>
    public int RecalculateLayoutDelayMs = 0;

    /// <summary>
    ///     The current delay (in milliseconds) before the next restatement of the layout.
    /// </summary>
    private double _remainingRecalculateLayoutDelayMs = 0;



    /// <summary>
    ///     Setting the value <see cref="RecalculateLayoutDelayMs"/> for the current element and all descendants.
    /// </summary>
    public static void SetRecalculateLayoutDelayMsBranch(UIElement element, int ms)
    {
        if (element is UIExLayout layout)
            layout.RecalculateLayoutDelayMs = ms;

        foreach (var child in element.Children)
            SetRecalculateLayoutDelayMsBranch(child, ms);
    }

    /// <summary>
    ///     It resets. <see cref="_remainingRecalculateLayoutDelayMs"/> for the element and all descendants.
    /// </summary>
    private static void ResetСurrentRecalculateLayoutDelayMsBranch(UIElement element)
    {
        if (element is UIExLayout layout)
            layout._remainingRecalculateLayoutDelayMs = 0d;

        foreach (var child in element.Children)
            ResetСurrentRecalculateLayoutDelayMsBranch(child);
    }


    /// <summary>
    ///     Set the minimum delay before updating the layout for the current <see cref="UIState"/>
    /// </summary>
    public static void SetRecalculateLayoutDelayMsState(UIState state, int? delayMs)
    {
        if (_statesRecalculateDalayMs.TryGetValue(state, out RecalculateLayoutStateDelay delay))
            delay.DelayMs = delayMs;
        else
        {
            RecalculateLayoutStateDelay delayNew = new() { DelayMs = delayMs };
            _statesRecalculateDalayMs.Add(state, delayNew);
        }
    }


    private int? GetRecalculateLayoutDelayMsState(UIElement element)
    {
        if (element is UIState state)
            return GetRecalculateLayoutDelayMsState(state);

        if (element.Parent is not null)
            return GetRecalculateLayoutDelayMsState(element.Parent);

        return null;
    }

    /// <summary>
    ///     
    /// </summary>
    public static int? GetRecalculateLayoutDelayMsState(UIState state)
    {
        if (_statesRecalculateDalayMs.TryGetValue(state, out RecalculateLayoutStateDelay delay))
            return delay.DelayMs;

        return null;
    }

    /// <summary>
    ///     Has a basic implementation <see cref="UIElement.Update(GameTime)"/> and a mechanism to protect against too frequent recalculation of the layout.
    /// </summary>
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (_remainingRecalculateLayoutDelayMs > 0)
            _remainingRecalculateLayoutDelayMs -= gameTime.ElapsedGameTime.TotalMilliseconds;
    }
}