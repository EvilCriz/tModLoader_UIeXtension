using System.Collections.Generic;
using Terraria.UI;
using UIeXtension.Styles;

namespace UIeXtension;

public abstract partial class UIExLayout : UIExElement
{
    /// <summary>
    ///     The internal size of the current layout element after the last recalculation of the layout for the child elements.
    /// </summary>
    /// <remarks>
    ///     It is used as an element of optimization.
    ///     <para>If the size has changed, the layout is recalculated.</para>
    ///     <para>
    ///         If the size has not changed, the need for reconfiguration is calculated.
    ///         assisted <see cref="_lastLayoutElements"/>
    ///     </para>
    /// </remarks>
    private CalculatedStyle _lastInnerDimensions = new CalculatedStyle(float.MinValue, float.MinValue, float.MinValue, float.MinValue);

    /// <summary>
    ///     List of Elements that this container had after the previous layout.
    /// </summary>
    /// <remarks>
    ///     It is used as an element of optimization.
    ///     <para>If the list of items has changed, the layout is recalculated.</para>
    ///     <para>
    ///         If the list of elements has not changed, the need for recomposition is calculated.
    ///         assisted <see cref="_lastInnerDimensions"/>
    ///     </para>
    /// </remarks>
    private List<UIElement> _lastLayoutElements = null;

    /// <summary>
    ///     A list of the Elements sizes that this container had after the previous layout.
    /// </summary>
    /// <remarks>
    ///     It is used as an element of optimization.
    ///     <para>If the list of items has changed, the layout is recalculated.</para>
    ///     <para>
    ///         If the list of elements has not changed, the need for recomposition is calculated.
    ///         assisted <see cref="_lastInnerDimensions"/> and <see cref="_lastLayoutElements"/>
    ///     </para>
    /// </remarks>
    private List<CalculatedStyle> _lastLayoutElementsOuterDimensions = new();

    /// <summary>
    ///     A flag indicating that the following <see cref="RecalculateLayout"/> should be ignored
    ///     <see cref="_lastInnerDimensions"/>. <see cref="_lastLayoutElements"/> and <see cref="_lastLayoutElementsOuterDimensions"/>
    /// </summary>
    /// <remarks>
    ///     Meaning of the flag true first-call <see cref="UIElement.Recalculate"/> and invoking the method
    ///     <see cref="EndLayoutPreparation(UIState)"/>After each layout, this flag is dropped to a value. false
    /// </remarks>
    private bool _ignoreLastLayoutInfo = true;


    /// <summary>
    ///     Styles of current elements
    /// </summary>
    protected List<StyleLayoutChild> _currentElementsStyles = new();

    /// <summary>
    ///     Copy of element styles after the last layout
    /// </summary>
    private List<StyleLayoutChild> _lastStyleLayoutChildArray;

    /// <summary>
    ///     A copy of the container layout style after the last layout
    /// </summary>
    private StyleLayoutContainer _lastStyleLayoutContainer;

    /// <summary>
    ///     Determines whether recomposition of the elements is required.
    /// </summary>
    /// <remarks>
    ///     V. tModLoader <see cref="UIElement.Recalculate"/> summoned UI- branches of elements every frame.
    ///     Algorithms of the layout work when <see cref="UIElement.Recalculate"/> And they're resource-intensive.
    ///     This method checks:
    ///     <para>The size of the builder itself has changed.</para>
    ///     <para>Subsidiary elements changed</para>
    ///     <para>Has the size of the child elements changed?</para>
    ///     <para>
    ///         Ignores all of these checks if <see cref="_ignoreLastLayoutInfo"/> === true. 
    ///         Meaning of the flag true first-call <see cref="UIElement.Recalculate"/> and invoking the method
    ///         <see cref="EndLayoutPreparation(UIState)"/>After each layout, this flag is dropped to a value. false
    ///     </para>
    ///     <para>This information is updated after the layout (if any) in <see cref="UpdateLastLayoutInfo"/></para>
    /// </remarks>
    protected bool IsLastLayoutInfoChanged()
    {
        if (_ignoreLastLayoutInfo)
            return true;

        int elementCount = Elements.Count;

        if (_lastLayoutElements is null)
            return true;

        if (_lastStyleLayoutContainer is null)
            return true;

        if (_lastStyleLayoutChildArray is null)
            return true;

        if (_lastLayoutElements.Count != elementCount)
            return true;
        
        if(IsInnerDimensionsNotEquals(this, _lastInnerDimensions))
            return true;

        for (int i = 0; i < elementCount; i++)
            if (_lastLayoutElements[i] != Elements[i])
                return true;

        if (!_lastStyleLayoutContainer.EqualsStylesFields(StyleLayout))
            return true;

        for (int i = 0; i < elementCount; i++)
        {
            CalculatedStyle outer = _lastLayoutElements[i].GetOuterDimensions();
            if (outer.Width != _lastLayoutElementsOuterDimensions[i].Width ||
                outer.Height != _lastLayoutElementsOuterDimensions[i].Height)
                    return true;
        }

        for (int i = 0; i < elementCount; i++)
            if (!_lastStyleLayoutChildArray[i]
                .EqualsStylesFields(_styleLayoutChildArray[i]))
                return true;

        return false;
    }

    /// <summary>
    ///     Updates information about the latest layout. This information is used in the method <see cref="IsLastLayoutInfoChanged"/>
    /// </summary>
    protected void UpdateLastLayoutInfo()
    {
        _ignoreLastLayoutInfo = false;

        if (_lastLayoutElements is null)
            _lastLayoutElements = new(Elements.Count);
        else
            _lastLayoutElements.Clear();

        _lastLayoutElementsOuterDimensions.Clear();
        _lastInnerDimensions = GetInnerDimensions();

        foreach (var element in Elements)
        {
            _lastLayoutElements.Add(element);
            _lastLayoutElementsOuterDimensions.Add(element.GetOuterDimensions());
        }

        ///////////////////

        if (_lastStyleLayoutChildArray is null)
            _lastStyleLayoutChildArray = new(Elements.Count);
        else
            _lastStyleLayoutChildArray.Clear();
        
        _lastStyleLayoutContainer = StyleLayout.GetCopy();

        foreach(var style in _styleLayoutChildArray)
            _lastStyleLayoutChildArray.Add(style.GetCopy());
    }

    /// <summary>
    ///     Indicates the item and all descendants to ignore the last stored items and dimensions when they try to recalculate the layout.
    /// </summary>
    /// <remarks>
    ///     See more in the description <see cref="_ignoreLastLayoutInfo"/>. <see cref="IsLastLayoutInfoChanged"/>.
    ///     <see cref="UpdateLastLayoutInfo"/> and <see cref="_lastInnerDimensions"/>
    /// </remarks>
    private static void ResetLastLayoutInfoForBranch(UIElement element)
    {
        if (element is UIExLayout layout)
            layout._ignoreLastLayoutInfo = true;

        foreach (var child in element.Children)
            ResetLastLayoutInfoForBranch(child);
    }
}