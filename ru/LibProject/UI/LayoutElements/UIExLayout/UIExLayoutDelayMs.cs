using Microsoft.Xna.Framework;
using Terraria.UI;

namespace UIeXtension;

/// <summary>
///     Вспомогательный класс для таблицы <see cref="System.Runtime.CompilerServices.ConditionalWeakTable{TKey, TValue}"/>
/// </summary>
public class RecalculateLayoutStateDelay
{
    /// <summary>
    ///     Задержка в миллисекундах
    /// </summary>
    public int? DelayMs = 0;
}

public abstract partial class UIExLayout : UIExElement
{
    /// <summary>
    ///     Хранит таблицу с минимальной задержкой (в миллисекундах) между пересчетом компоновки для всех контейнеров в <see cref="UIState"/>.
    /// </summary>
    /// <remarks>
    ///     Значение минимальной задержки отдельное для каждого <see cref="UIState"/>.
    ///     Если <see cref="UIState"/> не содержится в этой таблице - компоновщик использует значение: <see cref="RecalculateLayoutDelayMs"/>
    /// </remarks>
    private static readonly System.Runtime.CompilerServices.
        ConditionalWeakTable<UIState, RecalculateLayoutStateDelay> _statesRecalculateDalayMs = new();

    /// <summary>
    ///     Минимальная задержка (в миллисекундах) между пересчетом компоновки.
    ///     Ничего не делает, если для текущего <see cref="UIState"/> установлено свое значение в
    ///     таблице <see cref="_statesRecalculateDalayMs"/> is not null
    /// </summary>
    public int RecalculateLayoutDelayMs = 0;

    /// <summary>
    ///     Текущая задержка (в миллисекундах) перед следующим пересчетом компоновки.
    /// </summary>
    private double _remainingRecalculateLayoutDelayMs = 0;



    /// <summary>
    ///     Устанавливает значение <see cref="RecalculateLayoutDelayMs"/> для текущего элемента и всех потомков.
    /// </summary>
    public static void SetRecalculateLayoutDelayMsBranch(UIElement element, int ms)
    {
        if (element is UIExLayout layout)
            layout.RecalculateLayoutDelayMs = ms;

        foreach (var child in element.Children)
            SetRecalculateLayoutDelayMsBranch(child, ms);
    }

    /// <summary>
    ///     Сбрасывает значение <see cref="_remainingRecalculateLayoutDelayMs"/> для элемента и всех потомков.
    /// </summary>
    private static void ResetСurrentRecalculateLayoutDelayMsBranch(UIElement element)
    {
        if (element is UIExLayout layout)
            layout._remainingRecalculateLayoutDelayMs = 0d;

        foreach (var child in element.Children)
            ResetСurrentRecalculateLayoutDelayMsBranch(child);
    }


    /// <summary>
    ///     Устанавливает минимальную задержку перед обновлением компоновки для текущего <see cref="UIState"/>
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
    ///     Имеет базовую реализацию <see cref="UIElement.Update(GameTime)"/> и механизм защиты от слишком частого пересчета компоновки.
    /// </summary>
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (_remainingRecalculateLayoutDelayMs > 0)
            _remainingRecalculateLayoutDelayMs -= gameTime.ElapsedGameTime.TotalMilliseconds;
    }
}