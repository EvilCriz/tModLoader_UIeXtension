using Terraria.UI;

namespace UIeXtension;

public abstract partial class UIExLayout : UIExElement
{
    private static readonly object _preparationStateMarker = new();

    /// <summary>
    ///     Хранит список <see cref="UIState"/>, которые сейчас находятся в состоянии подготовки.
    /// </summary>
    private static readonly System.Runtime.CompilerServices.
        ConditionalWeakTable<UIState, object> _statesPreparationTable = new();




    /// <summary>
    ///     Сообщает всем контейнерам в <see cref="UIState"/>, 
    ///     что компоновка НЕ должна выполняться до вызова <see cref="EndLayoutPreparation(UIState)"/>
    /// </summary>
    public static void BeginLayoutPreparation(UIState state)
        => _statesPreparationTable.TryAdd(state, _preparationStateMarker);


    /// <summary>
    ///     Сообщает всем контейнерам внутри <see cref="UIState"/>, что действие
    ///     <see cref="BeginLayoutPreparation(UIState)"/> завершилось.
    /// </summary>
    /// <remarks>
    ///     Требует у всех вложенных контейнеров выполнить компоновку, игнорируя все внутренние ограничения.
    /// </remarks>
    public static void EndLayoutPreparation(UIState state)
    {
        ResetСurrentRecalculateLayoutDelayMsBranch(state);
        ResetLastLayoutInfoForBranch(state);

        if (_statesPreparationTable.TryGetValue(state, out _))
            _statesPreparationTable.Remove(state);

        state.Recalculate();
    }


    /// <summary>
    ///     Возвращает состояние подготовки компоновки контейнера.
    ///     Если возвращает true, это означает, что компоновка всех контейнеров для текущего <see cref="UIState"/>
    ///     приостановлена.
    /// </summary>
    private bool IsLayoutPreparation()
        => IsLayoutPreparation(this);

    /// <summary>
    ///     Возвращает состояние подготовки компоновки контейнера.
    ///     Если возвращает true, это означает, что компоновка всех контейнеров для текущего <see cref="UIState"/>
    ///     приостановлена.
    /// </summary>
    private static bool IsLayoutPreparation(UIElement element)
    {
        if (element is UIState state)
            return _statesPreparationTable.TryGetValue(state, out _);

        if (element.Parent is not null)
            return IsLayoutPreparation(element.Parent);

        return false;
    }
}