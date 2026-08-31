using System.Collections.Generic;
using Terraria.UI;
using UIeXtension.Styles;

namespace UIeXtension;

public partial class UIExDockPanel
{
    /// <summary>
    ///     He counts. <see cref="CalculatedStyle"/> on all sides.
    /// </summary>
    protected virtual void CaluclateSideDimensions()
    {
        float leftWidth = GetMaxSize(
            indexses:   _context.leftElementsIdx,
            width:      true,
            padding:    _context.styleLayoutContainer.SideLeft.Padding);

        float rightWidth = GetMaxSize(
            indexses:   _context.rightElementsIdx,
            width:      true,
            padding:    _context.styleLayoutContainer.SideRight.Padding);

        float topHeight = GetMaxSize(
            indexses:   _context.topElementsIdx,
            width:      false,
            padding:    _context.styleLayoutContainer.SideTop.Padding);

        float bottomHeight = GetMaxSize(
            indexses:   _context.bottomElementsIdx,
            width:      false,
            padding:    _context.styleLayoutContainer.SideBottom.Padding);

        //

        foreach (var side in _context.sideOrder)
        {
            switch (side)
            {
                case Enums.UIExSide.Left:
                    _context.leftOuterDimensions =
                        GetAndConsumeSideDimensions(
                            side: Enums.UIExSide.Left,
                            size: leftWidth);

                    _context.leftInnerDimensions = 
                        GetSideWithPadding(
                            dimensions: _context.leftOuterDimensions,
                            padding:    _context.styleLayoutContainer.SideLeft.Padding);

                    continue;

                case Enums.UIExSide.Right:
                    _context.rightOuterDimensions =
                        GetAndConsumeSideDimensions(
                            side: Enums.UIExSide.Right,
                            size: rightWidth);

                    _context.rightInnerDimensions =
                        GetSideWithPadding(
                            dimensions: _context.rightOuterDimensions,
                            padding:    _context.styleLayoutContainer.SideRight.Padding);

                    continue;

                case Enums.UIExSide.Top:
                    _context.topOuterDimensions =
                        GetAndConsumeSideDimensions(
                            side: Enums.UIExSide.Top,
                            size: topHeight);

                    _context.topInnerDimensions =
                        GetSideWithPadding(
                            dimensions: _context.topOuterDimensions,
                            padding:    _context.styleLayoutContainer.SideTop.Padding);

                    continue;

                case Enums.UIExSide.Bottom:
                    _context.bottomOuterDimensions =
                        GetAndConsumeSideDimensions(
                            side: Enums.UIExSide.Bottom,
                            size: bottomHeight);

                    _context.bottomInnerDimensions =
                        GetSideWithPadding(
                            dimensions: _context.bottomOuterDimensions,
                            padding:    _context.styleLayoutContainer.SideBottom.Padding);

                    continue;
            }
        }

        if (_context.fillElementsIdx.Count > 0)
        {
            _context.fillOuterDimensions = _context.remainingDimensions;
            _context.fillInnerDimensions = 
                GetSideWithPadding(
                    _context.fillOuterDimensions, 
                    _context.styleLayoutContainer.SideFill.Padding);
        }
    }

    /// <summary/>
    protected virtual CalculatedStyle GetSideWithPadding(
        CalculatedStyle dimensions, 
        UIExThickness padding)
    {
        float paddingTop = padding.Top.GetValue(dimensions.Height);
        float paddingLeft = padding.Left.GetValue(dimensions.Width);
        float paddingBottom = padding.Bottom.GetValue(dimensions.Height);
        float paddingRight = padding.Right.GetValue(dimensions.Width);

        dimensions.X += paddingLeft;
        dimensions.Y += paddingTop;
        dimensions.Width -= (paddingLeft + paddingRight);
        dimensions.Height -= (paddingTop + paddingBottom);

        return dimensions;
    }


    /// <summary>
    ///     Calculates the size and position of the transferred side.
    ///     The offset is changed for the following parties.
    /// </summary>
    protected virtual CalculatedStyle GetAndConsumeSideDimensions(
        Enums.UIExSide side,
        float size)
    {
        CalculatedStyle result = default(CalculatedStyle);

        switch (side)
        {
            case Enums.UIExSide.Left:
                result.X = _context.remainingDimensions.X;
                result.Y = _context.remainingDimensions.Y;

                result.Width = size;
                result.Height = _context.remainingDimensions.Height;

                _context.remainingDimensions.X += size;
                _context.remainingDimensions.Width -= size;
                break;

            case Enums.UIExSide.Right:
                result.X =
                    _context.remainingDimensions.X +
                    _context.remainingDimensions.Width -
                    size;

                result.Y = _context.remainingDimensions.Y;

                result.Width = size;
                result.Height = _context.remainingDimensions.Height;

                _context.remainingDimensions.Width -= size;
                break;

            case Enums.UIExSide.Top:
                result.X = _context.remainingDimensions.X;
                result.Y = _context.remainingDimensions.Y;
                result.Width = _context.remainingDimensions.Width;
                result.Height = size;

                _context.remainingDimensions.Y += size;
                _context.remainingDimensions.Height -= size;
                break;

            case Enums.UIExSide.Bottom:
                result.X = _context.remainingDimensions.X;

                result.Y =
                    _context.remainingDimensions.Y +
                    _context.remainingDimensions.Height -
                    size;

                result.Width = _context.remainingDimensions.Width;
                result.Height = size;

                _context.remainingDimensions.Height -= size;
                break;
        }

        return result;
    }

    /// <summary>
    ///     Returns the maximum specified size of the transferred elements.
    ///     Adds to the size of the Padding relevant parties.
    ///     Using an off-life-cycle layout will result in exclusion.
    /// </summary>
    protected virtual float GetMaxSize(List<int> indexses, bool width, UIExThickness padding)
    {
        float size = base.GetMaxSize(indexses, width);

        GetOrientationThickness(
            thickness: padding, 
            vertical: !width, 
            start: out float paddingStart, 
            end: out float paddingEnd,
            size: size);

        return size + paddingStart + paddingEnd;
    }
}