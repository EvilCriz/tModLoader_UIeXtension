using System.Collections.Generic;
using Terraria.UI;
using UIeXtension.Styles;

namespace UIeXtension;

public abstract partial class UIExLayout : UIExElement
{
    /// <summary>
    ///     Stores the values of the assembled areas. It's only relevant during the layout life cycle.
    /// </summary>
    protected List<RectangleLayoutContext> _rectangleContexts = null;

    /// <summary>
    ///     It stores all the styles of the child elements involved in the layout. It is relevant only during the layout life cycle.
    /// </summary>
    protected List<Styles.StyleLayoutChild> _styleElementsContexts = null;

    /// <summary>
    ///     Stores the entire list of child elements involved in the layout, only relevant during the life cycle of the layout
    /// </summary>
    protected List<UIElement> _elementsContext = null;

    /// <summary>
    ///     Stores the value of the internal area of the linker. Only relevant during the life cycle of the layout.
    /// </summary>
    protected CalculatedStyle _innerDimensionsContext;

    /// <summary>
    ///     Stores the value of the external areas of the elements involved in the layout. Relevant only during the layout life cycle
    /// </summary>
    protected List<CalculatedStyle> _elementsOuterDimensionsContext = null;

    /// <summary>
    ///     Stores the value of the external elements involved in the layout. Relevant only during the layout life cycle.
    /// </summary>
    protected List<CalculatedStyle> _elementsDimensionsContext = null;



    /// <summary>
    ///     Counts the number of child elements that are NOT involved in the layout.
    /// </summary>
    protected virtual int GetWithoutLayoutElementsCount()
        => Elements.Count - _elementsContext.Count;


    /// <summary>
    ///     Preparing. <see cref="_rectangleContexts"/>. <see cref="_styleElementsContexts"/> 
    ///     and <see cref="_elementsContext"/> before starting the layout.
    ///     <para>Upon completion of the arrangement, these resources are released into <see cref="EndLayoutContext"/></para>
    /// </summary>
    protected virtual void BeginLayoutContext()
    {
        _innerDimensionsContext = GetInnerDimensions();

        int countContextElements = 0;

        for(int i = 0; i < _styleLayoutChildArray.Length; i++)
            if (!_styleLayoutChildArray[i].WithoutLayout)
                countContextElements++;

        _styleElementsContexts = new(countContextElements);
        _elementsContext = new(countContextElements);

        _elementsOuterDimensionsContext = new(countContextElements);
        _elementsDimensionsContext = new(countContextElements);

        _rectangleContexts = new(countContextElements);

        for(int i = 0; i < Elements.Count; i++)
        {
            UIElement element = Elements[i];
            StyleLayoutChild styleLayoutElemnt = _styleLayoutChildArray[i].GetCopy();

            if (styleLayoutElemnt.WithoutLayout)
                continue;

            _styleElementsContexts.Add(styleLayoutElemnt);
            _elementsContext.Add(element);

            _elementsOuterDimensionsContext.Add(
                Utils.UtilsLayout.GetForcedCalculatedOuterDimensions(
                    element:                element,
                    style:                  styleLayoutElemnt,
                    parentInnerDimensions:  _innerDimensionsContext));

            _elementsDimensionsContext.Add(
                Utils.UtilsLayout.GetForcedCalculatedDimensions(
                    element:                element,
                    style:                  styleLayoutElemnt,
                    parentInnerDimensions:  _innerDimensionsContext));
        }

        for (int i = 0; i < _elementsContext.Count; i++)
        {
            _elementsContext[i].Top = StyleDimension.Empty;
            _elementsContext[i].Left = StyleDimension.Empty;
        }

        for (int i = 0; i < _elementsContext.Count; i++)
            _rectangleContexts.Add(
                new RectangleLayoutContext(
                    i,
                    _elementsContext[i],
                    _elementsOuterDimensionsContext[i]));
    }

    /// <summary>
    ///     Frees up resources <see cref="_rectangleContexts"/>. <see cref="_styleElementsContexts"/> 
    ///     and <see cref="_elementsContext"/> after completion of the layout.
    ///     <para>These resources are prepared in the method <see cref="BeginLayoutContext"/></para>
    /// </summary>
    protected virtual void EndLayoutContext()
    {
        _rectangleContexts = null;
        _styleElementsContexts = null;
        _elementsContext = null;
        _elementsOuterDimensionsContext = null;
        _elementsDimensionsContext = null;
    }
}