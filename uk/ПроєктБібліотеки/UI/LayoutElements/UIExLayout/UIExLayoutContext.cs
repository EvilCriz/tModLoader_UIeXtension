using System.Collections.Generic;
using Terraria.UI;
using UIeXtension.Styles;

namespace UIeXtension;

public abstract partial class UIExLayout : UIExElement
{
    /// <summary>
    ///     Зберігає значення зібраних площ. Це тільки актуальне при плануванні життєвого циклу.
    /// </summary>
    protected List<RectangleLayoutContext> _rectangleContexts = null;

    /// <summary>
    ///     Він зберігає всі стилі дочірні елементи, залучені до макета. Він доречний тільки під час планування життєвого циклу.
    /// </summary>
    protected List<Styles.StyleLayoutChild> _styleElementsContexts = null;

    /// <summary>
    ///     Зберігає весь перелік дитячих елементів, які беруть участь у макеті, тільки актуальні в життєвому циклі макета
    /// </summary>
    protected List<UIElement> _elementsContext = null;

    /// <summary>
    ///     Зберігає значення внутрішньої зони фіксатора. Тільки актуально під час життєвого циклу макета.
    /// </summary>
    protected CalculatedStyle _innerDimensionsContext;

    /// <summary>
    ///     Зберігає значення зовнішніх зон елементів, залучених до макета. Виконується тільки під час планування життєвого циклу
    /// </summary>
    protected List<CalculatedStyle> _elementsOuterDimensionsContext = null;

    /// <summary>
    ///     Зберігає значення зовнішніх елементів, залучених до макета. Залишається тільки під час планування життєвого циклу.
    /// </summary>
    protected List<CalculatedStyle> _elementsDimensionsContext = null;



    /// <summary>
    ///     Кількість дочірніх елементів, які NOT участь у макеті.
    /// </summary>
    protected virtual int GetWithoutLayoutElementsCount()
        => Elements.Count - _elementsContext.Count;


    /// <summary>
    ///     Підготовка. <see cref="_rectangleContexts"/>. . <see cref="_styleElementsContexts"/> 
    ///     і <see cref="_elementsContext"/> перед початком макета.
    ///     <para>По завершенню розташування ці ресурси випускаються в <see cref="EndLayoutContext"/></para>
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
    ///     Безкоштовні ресурси <see cref="_rectangleContexts"/>. . <see cref="_styleElementsContexts"/> 
    ///     і <see cref="_elementsContext"/> після завершення макета.
    ///     <para>Ці ресурси готуються в методі <see cref="BeginLayoutContext"/></para>
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