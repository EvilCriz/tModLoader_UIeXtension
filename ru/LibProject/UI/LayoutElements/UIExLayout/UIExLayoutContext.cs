using System.Collections.Generic;
using Terraria.UI;
using UIeXtension.Styles;

namespace UIeXtension;

public abstract partial class UIExLayout : UIExElement
{
    /// <summary>
    ///     Хранит значения скомпонованных областей. Актуален только во время жизненного цикла компоновки.
    /// </summary>
    protected List<RectangleLayoutContext> _rectangleContexts = null;

    /// <summary>
    ///     Хранит в себе все стили участвующих в компоновке дочерних элементов.  Актуален только во время жизненного цикла компоновки.
    /// </summary>
    protected List<Styles.StyleLayoutChild> _styleElementsContexts = null;

    /// <summary>
    ///     Хранит весь список участвующих в компоновке дочерних элементов.  Актуален только во время жизненного цикла компоновки
    /// </summary>
    protected List<UIElement> _elementsContext = null;

    /// <summary>
    ///     Хранит значение внутренней области компоновщика. Актуально только во время жизненного цикла компоновки
    /// </summary>
    protected CalculatedStyle _innerDimensionsContext;

    /// <summary>
    ///     Хранит значения внешних областей элементов, участвующих в компоновке. Актуально только во время жизненного цикла компоновки
    /// </summary>
    protected List<CalculatedStyle> _elementsOuterDimensionsContext = null;

    /// <summary>
    ///     Хранит значения внешних элементов, участвующих в компоновке. Актуально только во время жизненного цикла компоновки
    /// </summary>
    protected List<CalculatedStyle> _elementsDimensionsContext = null;



    /// <summary>
    ///     Считает количество дочерних элементов, которые НЕ участвуют в компоновке.
    /// </summary>
    protected virtual int GetWithoutLayoutElementsCount()
        => Elements.Count - _elementsContext.Count;


    /// <summary>
    ///     Подготавливает <see cref="_rectangleContexts"/>, <see cref="_styleElementsContexts"/> 
    ///     и <see cref="_elementsContext"/> перед началом компоновки.
    ///     <para>После завершения компоновки освобождает эти ресурсы в <see cref="EndLayoutContext"/></para>
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
    ///     Освобождает ресурсы <see cref="_rectangleContexts"/>, <see cref="_styleElementsContexts"/> 
    ///     и <see cref="_elementsContext"/> после завершения компоновки.
    ///     <para>Данные ресурсы подготавливаются в методе <see cref="BeginLayoutContext"/></para>
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
