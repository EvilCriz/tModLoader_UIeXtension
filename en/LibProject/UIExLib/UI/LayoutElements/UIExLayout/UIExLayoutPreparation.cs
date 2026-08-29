using Terraria.UI;

namespace UIeXtension;

public abstract partial class UIExLayout : UIExElement
{
    private static readonly object _preparationStateMarker = new();

    /// <summary>
    ///     Keeps the list. <see cref="UIState"/>Those who are currently in the process of preparation.
    /// </summary>
    private static readonly System.Runtime.CompilerServices.
        ConditionalWeakTable<UIState, object> _statesPreparationTable = new();




    /// <summary>
    ///     Notifies all containers in <see cref="UIState"/>. 
    ///     that the layout should not be performed before the call <see cref="EndLayoutPreparation(UIState)"/>
    /// </summary>
    public static void BeginLayoutPreparation(UIState state)
        => _statesPreparationTable.TryAdd(state, _preparationStateMarker);


    /// <summary>
    ///     Notifies all containers inside <see cref="UIState"/>that action
    ///     <see cref="BeginLayoutPreparation(UIState)"/> It's over.
    /// </summary>
    /// <remarks>
    ///     Requires all nested containers to complete the layout, ignoring all internal constraints.
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
    ///     Returns the state of preparation of the container layout.
    ///     If he does. trueThis means that the layout of all containers for the current <see cref="UIState"/>
    ///     suspended.
    /// </summary>
    private bool IsLayoutPreparation()
        => IsLayoutPreparation(this);

    /// <summary>
    ///     Returns the state of preparation of the container layout.
    ///     If he does. trueThis means that the layout of all containers for the current <see cref="UIState"/>
    ///     suspended.
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