using Terraria.UI;

namespace UIeXtension;

public abstract partial class UIExLayout : UIExElement
{
    private static readonly object _preparationStateMarker = new();

    /// <summary>
    ///     Зберігати список. <see cref="UIState"/>Ті, хто в даний час в процесі підготовки.
    /// </summary>
    private static readonly System.Runtime.CompilerServices.
        ConditionalWeakTable<UIState, object> _statesPreparationTable = new();




    /// <summary>
    ///     Визначаємо всі контейнери в контейнерах <see cref="UIState"/>. . 
    ///     що верстка не повинна виконуватися до виклику <see cref="EndLayoutPreparation(UIState)"/>
    /// </summary>
    public static void BeginLayoutPreparation(UIState state)
        => _statesPreparationTable.TryAdd(state, _preparationStateMarker);


    /// <summary>
    ///     Визначаємо всі контейнери всередині <see cref="UIState"/>що дія
    ///     <see cref="BeginLayoutPreparation(UIState)"/> Це над.
    /// </summary>
    /// <remarks>
    ///     Потрібні всі контейнери для завершення макета, ігнорування всіх внутрішніх обмежень.
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
    ///     Повертає стан приготування контейнерного макету.
    ///     Якщо він робить. trueThis означає, що макет всіх контейнерів для поточного <see cref="UIState"/>
    ///     підвішені.
    /// </summary>
    private bool IsLayoutPreparation()
        => IsLayoutPreparation(this);

    /// <summary>
    ///     Повертає стан приготування контейнерного макету.
    ///     Якщо він робить. trueThis означає, що макет всіх контейнерів для поточного <see cref="UIState"/>
    ///     підвішені.
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