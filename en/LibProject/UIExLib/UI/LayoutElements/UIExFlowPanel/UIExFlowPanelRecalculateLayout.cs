using System.Collections.Generic;
using Terraria.UI;

namespace UIeXtension;

/// <summary>
///     Basic class for <see cref="UIExStackPanel"/>. <see cref="UIExDockPanel"/>. <see cref="UIExWrapPanel"/>
/// </summary>
public abstract partial class UIExFlowPanel : UIExLayout
{
    /// <summary>
    ///     Start the layout algorithm. Compose elements in a line according to the specified rules.
    /// </summary>
    protected virtual void RefreshLayoutLine(
        List<int> elementsIndexes, 
        float spacing, 
        bool reverse,
        CalculatedStyle parentInnerDimensions,
        Enums.UIExAlignment containerJustifyContent)
    {
        if (elementsIndexes.Count < 1)
            return;

        ///////////////////////

        //parentInnerDimensions = GetRelativeDimensions(parentInnerDimensions);

        if(!_flowContext.ignoreJustify)
        {

            float innerDimensionJustifySize = GetInnerDimensionsSize(
                vertical:                   _flowContext.vertical,
                parentInnerDimensions:      parentInnerDimensions);

            CalculateElementsJustifyOffsets(
                elementsIndexes:            elementsIndexes,
                spacing:                    spacing,
                reverse:                    reverse,
                innerDimensionsSize:        innerDimensionJustifySize,
                containerJustifyContent:    containerJustifyContent);

            for (int i = 0; i < elementsIndexes.Count; i++)
                SetPosition(
                    index:              elementsIndexes[i],
                    vertical:           _flowContext.vertical,
                    position:           _flowContext.justifyOffsets[i],
                    parentDimenstion:   parentInnerDimensions);
        }

        ///////////////////////

        if(!_flowContext.ignoreAlign)
        {
            float innerDimensionAlignSize = GetInnerDimensionsSize(
                vertical:                   !_flowContext.vertical,
                parentInnerDimensions:      parentInnerDimensions);

            CalculateElementsAlignOffsets(
                elementsIndexes:        elementsIndexes,
                innerDimensionSize:     innerDimensionAlignSize);

            for (int i = 0; i < elementsIndexes.Count; i++)
                SetPosition(
                    index:              elementsIndexes[i],
                    vertical:           !_flowContext.vertical,
                    position:           _flowContext.alignOffsets[i],
                    parentDimenstion:   parentInnerDimensions);
        }

        ///////////////////////


        for (int i = 0; i < elementsIndexes.Count; i++)
        {
            if (!_flowContext.ignoreJustify)
                if(!TrySetStretchSize(
                    index:              elementsIndexes[i],
                    justify:            true,
                    stretchSize:        _flowContext.justifyStretchSize,
                    parentDimensions:   parentInnerDimensions))
                        SetNoStretchSize(
                            index:              elementsIndexes[i],
                            justify:            true,
                            parentDimensions:   parentInnerDimensions);

            if(!_flowContext.ignoreAlign)
                if(!TrySetStretchSize(
                    index:              elementsIndexes[i],
                    justify:            false,
                    stretchSize:        _flowContext.alignStretchSize,
                    parentDimensions:   parentInnerDimensions))
                        SetNoStretchSize(
                            index:              elementsIndexes[i],
                            justify:            false,
                            parentDimensions:   parentInnerDimensions);
        }
    }

    /// <summary>
    ///     Counts and returns the indentation list for each element along the main axis.
    /// </summary>
    protected virtual void CalculateElementsJustifyOffsets(
        List<int> elementsIndexes, 
        float spacing, 
        bool reverse,
        float innerDimensionsSize,
        Enums.UIExAlignment containerJustifyContent)
    {
        float spacingSize = spacing * (elementsIndexes.Count - 1);

        float nonStretchSize = _flowContext.ignoreJustifyStretch 
            ? GetSizeElements(
                elementsIndexes:    elementsIndexes,
                vertical:           _flowContext.vertical)
            : GetSizeNonStretchElements(
                elementsIndexes:    elementsIndexes, 
                vertical:           _flowContext.vertical, 
                justify:            true);

        float availableSpace = Microsoft.Xna.Framework.MathHelper.Max(
            value1:     innerDimensionsSize - nonStretchSize - spacingSize, 
            value2:     0f);

        int stretchCount = _flowContext.ignoreJustifyStretch
            ? 0
            : GetStretchElementsCount(
                elementsIndexes:    elementsIndexes, 
                justify:            true);

        _flowContext.justifyStretchSize = 
            GetStrethSize(
                availableSpace:     availableSpace, 
                stretchCount:       stretchCount);


        float remainingSpace = availableSpace - _flowContext.justifyStretchSize * stretchCount;


        float justifyOffset = containerJustifyContent switch
        {
            Enums.UIExAlignment.Center => remainingSpace / 2f,
            Enums.UIExAlignment.End => remainingSpace,
            _ => 0f
        };


        float[] sizes = new float[elementsIndexes.Count];
        for (int i = 0; i < elementsIndexes.Count; i++)
            sizes[i] = GetElementJustifySize(elementsIndexes[i], _flowContext.justifyStretchSize);


        _flowContext.justifyOffsets = new float[sizes.Length];

        int indexI = reverse ? sizes.Length - 1 : 0;
        int stepI = reverse ? -1 : 1;

        float currentOffset = justifyOffset;

        for (int i = 0; i < sizes.Length; i++)
        {
            _flowContext.justifyOffsets[indexI] = currentOffset;
            currentOffset += sizes[indexI] + spacing;

            indexI += stepI;
        }
    }



    /// <summary>
    ///     Counts and returns the indentation list for each element along the transverse axis.
    /// </summary>
    protected virtual void CalculateElementsAlignOffsets(List<int> elementsIndexes, float innerDimensionSize)
    {
        _flowContext.alignStretchSize = innerDimensionSize;

        float[] sizes = new float[elementsIndexes.Count];
        for (int i = 0; i < elementsIndexes.Count; i++)
            sizes[i] = GetElementAlignSize(elementsIndexes[i], innerDimensionSize);

        _flowContext.alignOffsets = new float[elementsIndexes.Count];
        for (int i = 0; i < sizes.Length; i++)
        {
            float remainingSpace = _flowContext.alignStretchSize - sizes[i];

            _flowContext.alignOffsets[i] = GetAlign(elementsIndexes[i]) switch
            {
                Enums.UIExAlignment.Center => remainingSpace / 2f,
                Enums.UIExAlignment.End => remainingSpace,
                _ => 0f
            };
        }
    }



    /// <summary>
    ///     Returns the size of the element along the main axis (its own or stretched)
    /// </summary>
    protected virtual float GetElementJustifySize(int index, float stretchSize)
    {
        if (!_flowContext.ignoreJustifyStretch && IsStretchAlignment(index, justify: true))
            return stretchSize;

        return GetElementSize(index, _flowContext.vertical);
    }

    /// <summary>
    ///     Returns the size of the element along the transverse axis (its own or stretched)
    /// </summary>
    protected virtual float GetElementAlignSize(int index, float stretchSize)
    {
        if (!_flowContext.ignoreAlignStretch && IsStretchAlignment(index, justify: false))
            return stretchSize;

        return GetElementSize(index, !_flowContext.vertical);
    }


    /// <summary>
    ///     Set the transmitted position for the element on the transmitted axis.
    /// </summary>
    protected virtual void SetPosition(int index, bool vertical, float position, CalculatedStyle parentDimenstion)
    {
        CalculatedStyle elementsDimensions = _elementsDimensionsContext[index];

        if (vertical)
            position += _styleElementsContexts[index].Margin.Top.GetValue(elementsDimensions.Height);
        else
            position += _styleElementsContexts[index].Margin.Left.GetValue(elementsDimensions.Width);

        if (vertical)
            _rectangleContexts[index].Top = position;
        else
            _rectangleContexts[index].Left = position;
    }

    /// <summary/>
    protected virtual void SetNoStretchSize(int index, bool justify, CalculatedStyle parentDimensions)
    {
        if (IsStretchAlignment(index, justify))
            return;

        // justify == true  && vertical == true;    >> vertical     >> true     >> Top/Height
        // justify == true  && vertical == false;   >> vertical     >> false    >> Left/Width
        // justify == false && vertical == true;    >> !vertical    >> false    >> Left/Width
        // justify == false && vertical == false;   >> !vertical    >> true     >> Top/Height
        bool vertical = justify ? _flowContext.vertical : !_flowContext.vertical;

        var style = _styleElementsContexts[index];
        float size = vertical 
            ? style.Height.GetValue(parentDimensions.Height) 
            : style.Width.GetValue(parentDimensions.Width);

        if (vertical)
            _rectangleContexts[index].Height = size;
        else
            _rectangleContexts[index].Width = size;
    }

    /// <summary>
    ///     Installs <see cref="Enums.UIExAlignment.Stretch"/> size for the element on the transmitted axis (if the element is stretched along the transmitted axis)
    /// </summary>
    protected virtual bool TrySetStretchSize(int index, bool justify, float stretchSize, CalculatedStyle parentDimensions)
    {
        // justify == true  && vertical == true;    >> vertical     >> true     >> Top/Height
        // justify == true  && vertical == false;   >> vertical     >> false    >> Left/Width
        // justify == false && vertical == true;    >> !vertical    >> false    >> Left/Width
        // justify == false && vertical == false;   >> !vertical    >> true     >> Top/Height
        bool vertical = justify ? _flowContext.vertical : !_flowContext.vertical;

        if (justify && _flowContext.ignoreJustifyStretch)
            return false;

        if (!justify && _flowContext.ignoreAlignStretch)
            return false;

        if (!IsStretchAlignment(index, justify))
            return false;

        GetChildMargin(
            _styleElementsContexts[index],
            vertical,
            out float marginStart,
            out float marginEnd,
            _elementsDimensionsContext[index]);

        float elementSize = stretchSize - marginStart - marginEnd;
        elementSize = Microsoft.Xna.Framework.MathHelper.Max(0f, elementSize);

        if (vertical)
            _rectangleContexts[index].Height = elementSize;
        else
            _rectangleContexts[index].Width = elementSize;

        return true;
    }
}