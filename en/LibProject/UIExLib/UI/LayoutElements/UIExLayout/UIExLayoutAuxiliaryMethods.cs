using System;
using System.Collections.Generic;
using Terraria.UI;
using UIeXtension.Styles;

namespace UIeXtension;

public abstract partial class UIExLayout : UIExElement
{
    /// <summary>
    ///     Roll over the element <see cref="UIElement"/> into <typeparamref name="TUIElement"/>
    /// </summary>
    /// <remarks>
    ///     Use wrapping if you want your item to be able to use:
    ///     <see cref="UIElement.MarginTop"/>; <see cref="UIElement.MarginLeft"/>;
    ///     <see cref="UIElement.MarginRight"/>; <see cref="UIElement.MarginBottom"/>;
    ///     <see cref="UIElement.HAlign"/>; <see cref="UIElement.VAlign"/>
    ///     <para>Using these fields without wrapping can lead to unexpected results in the layout.</para>
    /// </remarks>
    /// <typeparam name="TUIElement">Type <see cref="UIElement"/>which should be wrapped <paramref name="element"/></typeparam>
    /// <param name="element">A user interface element that needs to be wrapped</param>
    /// <param name="stretch">
    ///     Indicates that <paramref name="element"/> must have Width.Set(0f, 1f) and Height.Set(0f, 1f)
    /// </param>
    /// <param name="center">
    ///     Indicates that <paramref name="element"/> must have HAlign = 0.5f and VAlign = 0.5f
    /// </param>
    /// <returns>Created element-wrapper, inside which is nested <paramref name="element"/></returns>
    public static TUIElement Wrap<TUIElement>(UIElement element, bool stretch = false, bool center = false)
        where TUIElement : UIElement, new()
            => Wrap(element, new TUIElement(), stretch, center);

    /// <summary>
    ///     Roll over the element <see cref="UIElement"/> into <typeparamref name="TUIElement"/>
    /// </summary>
    /// <remarks>
    ///     Use wrapping if you want your item to be able to use:
    ///     <see cref="UIElement.MarginTop"/>; <see cref="UIElement.MarginLeft"/>;
    ///     <see cref="UIElement.MarginRight"/>; <see cref="UIElement.MarginBottom"/>;
    ///     <see cref="UIElement.HAlign"/>; <see cref="UIElement.VAlign"/>
    ///     <para>Using these fields without wrapping can lead to unexpected results in the layout.</para>
    /// </remarks>
    /// <typeparam name="TUIElement">Type <see cref="UIElement"/>which should be wrapped <paramref name="element"/></typeparam>
    /// <param name="wrapElement">Already created and transferred element-wrapper, which will be wrapped <paramref name="element"/></param>
    /// <param name="element">A user interface element that needs to be wrapped</param>
    /// <param name="stretch">
    ///     Indicates that <paramref name="element"/> must have Width.Set(0f, 1f) and Height.Set(0f, 1f)
    /// </param>
    /// <param name="center">
    ///     Indicates that <paramref name="element"/> must have HAlign = 0.5f and VAlign = 0.5f
    /// </param>
    /// <returns><paramref name="wrapElement"/>, inside which is nested <paramref name="element"/></returns>
    public static TUIElement Wrap<TUIElement>(UIElement element, TUIElement wrapElement, bool stretch = false, bool center = false)
        where TUIElement : UIElement
    {
        if (stretch)
        {
            element.Width.Set(0f, 1f);
            element.Height.Set(0f, 1f);
        }

        if (center)
        {
            element.HAlign = 0.5f;
            element.VAlign = 0.5f;
        }

        wrapElement.Append(element);
        return wrapElement;
    }




    /// <summary/>
    protected int Max(int value1, int value2) => value1 > value2 ? value1 : value2;
    /// <summary/>
    protected int Min(int value1, int value2) => value1 < value2 ? value1 : value2;




    /// <summary>
    ///     Turns the list <see cref="UIElement"/> from the transmitted list of indices <see cref="_elementsContext"/>.
    ///     Outside the life cycle, the layout will lead to exclusion.
    /// </summary>
    protected virtual List<UIElement> GetElementsByIndexses(List<int> elementsIndexes)
    {
        List<UIElement> elements = new(elementsIndexes.Count);
        foreach (var idx in elementsIndexes)
            elements.Add(_elementsContext[idx]);
        return elements;
    }

    /// <summary>
    ///     Turns the list from <see cref="_elementsContext"/> index <see cref="UIElement"/>.
    ///     Outside the life cycle, the layout will lead to exclusion.
    /// </summary>
    protected virtual List<int> GetElementsIndexsesByElementsContext()
    {
        List<int> elementsIndexes = new(_elementsContext.Count);
        for (int i = 0; i < _elementsContext.Count; i++)
            elementsIndexes.Add(i);
        return elementsIndexes;
    }

    /// <summary>
    ///     Checks to see if they're different. <see cref="UIElement.GetOuterDimensions"/> 
    ///     and <see cref="CalculatedStyle"/> <paramref name="outerDimensions"/>
    /// </summary>
    protected static bool IsOuterDimensionsNotEquals(UIElement element, CalculatedStyle outerDimensions)
        => IsDimensionsNotEquals(element.GetOuterDimensions(), outerDimensions);

    /// <summary>
    ///     Checks to see if they're different. <see cref="UIElement.GetInnerDimensions"/> 
    ///     and <see cref="CalculatedStyle"/> <paramref name="innerDimensions"/>
    /// </summary>
    protected static bool IsInnerDimensionsNotEquals(UIElement element, CalculatedStyle innerDimensions)
        => IsDimensionsNotEquals(element.GetInnerDimensions(), innerDimensions);

    /// <summary>
    ///     Checks to see if they're different. <see cref="UIElement.GetDimensions"/> and <see cref="CalculatedStyle"/> <paramref name="dimensions"/>
    /// </summary>
    protected static bool IsDimensionsNotEquals(UIElement element, CalculatedStyle dimensions)
        => IsDimensionsNotEquals(element.GetDimensions(), dimensions);

    /// <summary>
    ///     Checks to see if they're different. <see cref="CalculatedStyle"/>
    /// </summary>
    private static bool IsDimensionsNotEquals(CalculatedStyle dimensions1, CalculatedStyle dimensions2)
    {
        return
            dimensions1.X != dimensions2.X ||
            dimensions1.Y != dimensions2.Y ||
            dimensions1.Width != dimensions2.Width ||
            dimensions1.Height != dimensions2.Height;
    }

    /// <summary>
    ///     Returns. <paramref name="dimensions"/>Replace X/Y with the position relative to the container.
    /// </summary>
    protected CalculatedStyle GetRelativeDimensions(CalculatedStyle dimensions)
    {
        dimensions.X -= _innerDimensionsContext.X;
        dimensions.Y -= _innerDimensionsContext.Y;
        return dimensions;
    }


    /// <summary>
    ///     Defines <see cref="Enums.UIExAlignment"/> on the main axis.
    ///     <para>
    ///         If the nested element positions itself, its positioning returns: <see cref="Styles.StyleLayoutChild.JustifySelf"/>. 
    ///         Otherwise, returns the positioning of the container layout: <see cref="Styles.StyleLayoutContainer.JustifyContent"/>
    ///     </para>
    /// </summary>
    protected virtual Enums.UIExAlignment GetJustify(int index)
    {
        Styles.StyleLayoutChild style = _styleElementsContexts[index];
        if (style.JustifySelf == Enums.UIExAlignment.Auto)
            return StyleLayout.JustifyContent;
        return style.JustifySelf;
    }

    /// <summary>
    ///     Defines <see cref="Enums.UIExAlignment"/> transverse axis.
    ///     <para>
    ///         If the nested element positions itself, its positioning returns: <see cref="Styles.StyleLayoutChild.AlignSelf"/>. 
    ///         Otherwise, returns the positioning of the container layout: <see cref="Styles.StyleLayoutContainer.AlignItems"/>
    ///     </para>
    /// </summary>
    protected virtual Enums.UIExAlignment GetAlign(int index)
    {
        Styles.StyleLayoutChild style = _styleElementsContexts[index];
        if (style.AlignSelf == Enums.UIExAlignment.Auto)
            return StyleLayout.AlignItems;
        return style.AlignSelf;
    }

    /// <summary>
    ///     Returns the maximum specified size of the transferred elements.
    ///     Using an off-life-cycle layout will result in exclusion.
    /// </summary>
    protected virtual float GetMaxSize(List<int> indexses, bool width, Func<int, bool> conditionExclusion = null)
    {
        float max = float.MinValue;

        foreach (var index in indexses)
        {
            if (conditionExclusion is not null && conditionExclusion(index))
                continue;

            UIElement element = _elementsContext[index];
            var outerDimensions = 
                Utils.UtilsLayout.GetForcedCalculatedOuterDimensions(
                    element, 
                    _styleElementsContexts[index],
                    _innerDimensionsContext);

            float size = width ?
                outerDimensions.Width :
                outerDimensions.Height;

            max = Microsoft.Xna.Framework.MathHelper.Max(max, size);
        }

        return max;
    }

    /// <summary>
    ///     It is considered to be initial and final. <see cref="UIExThickness"/> depending on the transmitted axis.
    /// </summary>
    protected virtual void GetOrientationThickness(
        UIExThickness thickness,
        bool vertical,
        out float start,
        out float end,
        float size)
    {
        if (vertical)
        {
            start = thickness.Top.GetValue(size);
            end = thickness.Bottom.GetValue(size);
        }
        else
        {
            start = thickness.Left.GetValue(size);
            end = thickness.Right.GetValue(size);
        }
    }

    /// <summary>
    ///     It is considered to be initial and final. <see cref="UIExThickness"/> depending on the transmitted axis.
    /// </summary>
    protected virtual void GetOrientationThickness(
        UIExThickness thickness,
        bool vertical,
        out float start,
        out float end,
        CalculatedStyle dimension)
    {
        float size = vertical
            ? dimension.Height
            : dimension.Width;

        GetOrientationThickness(
            thickness:  thickness,
            vertical:   vertical,
            start:      out start,
            end:        out end,
            size:       size);
    }

    /// <summary>
    ///     The initial and final Margin is considered depending on the transmitted axis.
    ///     Using an off-life-cycle layout will result in exclusion.
    /// </summary>
    protected virtual void GetChildMargin(
        Styles.StyleLayoutChild style,
        bool vertical,
        out float marginStart,
        out float marginEnd,
        CalculatedStyle dimension)
            => GetOrientationThickness(
                style.Margin, 
                vertical, 
                out marginStart, 
                out marginEnd, 
                dimension);

    /// <summary>
    ///     Returns. <see cref="Styles.StyleLayoutContainer.JustifyContent"/> or <see cref="Styles.StyleLayoutContainer.AlignItems"/>or
    ///     <see cref="Styles.StyleLayoutChild.JustifySelf"/>or <see cref="Styles.StyleLayoutChild.AlignSelf"/> for the element, depending on the
    ///     Axis and rules of return of the value of methods <see cref="GetJustify(int)"/> / <see cref="GetAlign(int)"/>
    /// </summary>
    /// <param name="index"></param>
    /// <param name="justify"></param>
    protected Enums.UIExAlignment GetAlignment(int index, bool justify)
        => justify ? GetJustify(index) : GetAlign(index);

    /// <summary>
    ///     Checks whether the alignment property on the transmitted axis is established for the element <see cref="Enums.UIExAlignment.Stretch"/>
    /// </summary>
    /// <param name="index"></param>
    /// <param name="justify"></param>
    protected bool IsStretchAlignment(int index, bool justify)
        => GetAlignment(index, justify) == Enums.UIExAlignment.Stretch;

    /// <summary>
    ///     Returns the occupied space on a given axis
    /// </summary>
    protected virtual float GetSizeElements(List<int> elementsIndexes, bool vertical)
    {
        float size = 0f;
        for (int i = 0; i < elementsIndexes.Count; i++)
            size += GetElementSize(elementsIndexes[i], vertical);
        return size;
    }

    /// <summary>
    ///     Returns the occupied space along a given axis elements with the property of alignment not installed on the <see cref="Enums.UIExAlignment.Stretch"/> 
    /// </summary>
    /// <param name="vertical">Orientation of the container</param>
    /// <param name="justify">The axis for which the occupied space is considered</param>
    /// <param name="elementsIndexes">Index of elements</param>
    protected virtual float GetSizeNonStretchElements(List<int> elementsIndexes, bool vertical, bool justify)
    {
        float size = 0f;
        for (int i = 0; i < elementsIndexes.Count; i++)
            size += GetSizeNonStretchElement(elementsIndexes[i], vertical, justify);
        return size;
    }

    /// <summary>
    ///     Returns the occupied space by the element, if it does not have an alignment property on the transmitted axis. <see cref="Enums.UIExAlignment.Stretch"/> 
    /// </summary>
    /// <param name="index"></param>
    /// <param name="vertical">Orientation of the container</param>
    /// <param name="justify">The axis for which the occupied space is considered</param>
    protected virtual float GetSizeNonStretchElement(int index, bool vertical, bool justify)
    {
        if (IsStretchAlignment(index, justify))
            return 0f;

        return GetElementSize(index, vertical);
    }

    /// <summary>
    ///     Returns the number of elements with the property of alignment along the transmitted axis <see cref="Enums.UIExAlignment.Stretch"/> 
    /// </summary>
    protected int GetStretchElementsCount(List<int> elementsIndexes, bool justify)
    {
        int stretchCount = 0;
        for (int i = 0; i < elementsIndexes.Count; i++)
            if (IsStretchAlignment(elementsIndexes[i], justify))
                stretchCount++;

        return stretchCount;
    }

    /// <summary>
    ///     Counts and returns the size of one <see cref="Enums.UIExAlignment.Stretch"/> 
    /// </summary>
    /// <param name="availableSpace">Free space for stretching</param>
    /// <param name="stretchCount">Number of elements with property <see cref="Enums.UIExAlignment.Stretch"/></param>
    protected virtual float GetStrethSize(float availableSpace, int stretchCount)
        => stretchCount == 0f ? 0f : availableSpace / stretchCount;

    /// <summary>
    ///     Returns the internal size of the container on the specified axis.
    /// </summary>
    protected float GetInnerDimensionsSize(bool vertical)
        => GetInnerDimensionsSize(vertical, _innerDimensionsContext);

    /// <summary>
    ///     Returns the internal size of the container on the specified axis.
    /// </summary>
    protected float GetInnerDimensionsSize(bool vertical, CalculatedStyle parentInnerDimensions)
        => vertical ? parentInnerDimensions.Height : parentInnerDimensions.Width;

    /// <summary>
    ///     Returns the size of the element on the specified axis.
    /// </summary>
    protected float GetElementSize(int index, bool vertical)
        => vertical ? _elementsOuterDimensionsContext[index].Height : _elementsOuterDimensionsContext[index].Width;
}