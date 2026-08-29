using Microsoft.Xna.Framework;
using Terraria.UI;

namespace UIeXtension;

/// <summary>
///     Допоміжний клас для столу <see cref="System.Runtime.CompilerServices.ConditionalWeakTable{TKey, TValue}"/>
/// </summary>
public class RecalculateLayoutStateDelay
{
    /// <summary>
    ///     Мілісекунд затримка
    /// </summary>
    public int? DelayMs = 0;
}

public abstract partial class UIExLayout : UIExElement
{
    /// <summary>
    ///     Зберігати таблицю з мінімальною затримкою (в мілісекундах) між переадресацією макета для всіх контейнерів в <see cref="UIState"/>. .
    /// </summary>
    /// <remarks>
    ///     Мінімальна вартість затримки окрема для кожного <see cref="UIState"/>. .
    ///     Зареєструватися <see cref="UIState"/> не міститься в цьому столі - фіксатор використовує значення: <see cref="RecalculateLayoutDelayMs"/>
    /// </remarks>
    private static readonly System.Runtime.CompilerServices.
        ConditionalWeakTable<UIState, RecalculateLayoutStateDelay> _statesRecalculateDalayMs = new();

    /// <summary>
    ///     Мінімальна затримка (в мілісекундах) між перерахунокм макета.
    ///     Це не робить нічого для поточного <see cref="UIState"/> була створена в
    ///     стільниця <see cref="_statesRecalculateDalayMs"/> нема null
    /// </summary>
    public int RecalculateLayoutDelayMs = 0;

    /// <summary>
    ///     Поточна затримка (в мілісекундах) перед наступним відпочинком макета.
    /// </summary>
    private double _remainingRecalculateLayoutDelayMs = 0;



    /// <summary>
    ///     Налаштування значення <see cref="RecalculateLayoutDelayMs"/> для поточного елемента і всіх нащадків.
    /// </summary>
    public static void SetRecalculateLayoutDelayMsBranch(UIElement element, int ms)
    {
        if (element is UIExLayout layout)
            layout.RecalculateLayoutDelayMs = ms;

        foreach (var child in element.Children)
            SetRecalculateLayoutDelayMsBranch(child, ms);
    }

    /// <summary>
    ///     Вона скидає. <see cref="_remainingRecalculateLayoutDelayMs"/> для елемента і всіх нащадків.
    /// </summary>
    private static void ResetСurrentRecalculateLayoutDelayMsBranch(UIElement element)
    {
        if (element is UIExLayout layout)
            layout._remainingRecalculateLayoutDelayMs = 0d;

        foreach (var child in element.Children)
            ResetСurrentRecalculateLayoutDelayMsBranch(child);
    }


    /// <summary>
    ///     Встановити мінімальну затримку перед оновленням макета для поточного <see cref="UIState"/>
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
    ///     Має базову реалізацію <see cref="UIElement.Update(GameTime)"/> і механізм захисту від занадто частого перерахунку макета.
    /// </summary>
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (_remainingRecalculateLayoutDelayMs > 0)
            _remainingRecalculateLayoutDelayMs -= gameTime.ElapsedGameTime.TotalMilliseconds;
    }
}