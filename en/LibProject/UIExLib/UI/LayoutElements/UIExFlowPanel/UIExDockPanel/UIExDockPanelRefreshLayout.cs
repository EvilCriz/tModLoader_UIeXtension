using System.Collections.Generic;
using Terraria.UI;
using UIeXtension.Styles;

namespace UIeXtension;

public partial class UIExDockPanel
{
    /// <inheritdoc/>
    protected override void RefreshLayout()
    {
        base.RefreshLayout();

        if (_elementsContext.Count == 0)
            return;

        CaluclateSideDimensions();

        var style = _context.styleLayoutContainer;

        RefreshLayoutSide(
            elementsIndexes:    _context.leftElementsIdx,
            sideDimensions:     _context.leftInnerDimensions,
            vertical:           true,
            sideStyle:          style.SideLeft);

        RefreshLayoutSide(
            elementsIndexes:    _context.rightElementsIdx,
            sideDimensions:     _context.rightInnerDimensions,
            vertical:           true,
            sideStyle:          style.SideRight);

        ///////////////////

        RefreshLayoutSide(
            elementsIndexes:    _context.topElementsIdx,
            sideDimensions:     _context.topInnerDimensions,
            vertical:           false,
            sideStyle:          style.SideTop);

        RefreshLayoutSide(
            elementsIndexes:    _context.bottomElementsIdx,
            sideDimensions:     _context.bottomInnerDimensions,
            vertical:           false,
            sideStyle:          style.SideBottom);

        RefreshLayoutSide(
            elementsIndexes:    _context.fillElementsIdx,
            sideDimensions:     _context.fillInnerDimensions,
            vertical:           StyleLayout.Orientation == Enums.UIExOrientation.Vertical,
            sideStyle:          style.SideFill);
    }

    /// <summary>
    ///     Composition of elements in a particular line
    /// </summary>
    protected virtual void RefreshLayoutSide(
        List<int> elementsIndexes, 
        CalculatedStyle sideDimensions,
        bool vertical,
        StyleSide sideStyle)
    {
        if (elementsIndexes.Count > 0)
        {
            ResetFlowContext();
            _flowContext.vertical = vertical;

            float sideSize = GetInnerDimensionsSize(
                vertical:                   _flowContext.vertical,
                parentInnerDimensions:      sideDimensions);

            float spacing = sideStyle.Spacing.GetValue(sideSize);
            bool reverse = sideStyle.Reverse;

            _context.SideJustifyContent = sideStyle.JustifyContent;
            _context.SideAlignItems = sideStyle.AlignItems;

            RefreshLayoutLine(
                elementsIndexes:            elementsIndexes,
                spacing:                    spacing,
                reverse:                    reverse,
                parentInnerDimensions:      sideDimensions,
                containerJustifyContent:    GetSideJustifyContent(sideStyle));

            SetElementOffsetBySide(
                elementsIndexes:            elementsIndexes,
                sideOuterDimensions:        sideDimensions);
        }
    }

    /// <summary>
    ///     Returns the alignment on the main axis, not taking into account JustifySelf nested elements.
    /// </summary>
    protected Enums.UIExAlignment GetSideJustifyContent(StyleSide sideInfo)
        => sideInfo.JustifyContent != Enums.UIExAlignment.Auto
            ? sideInfo.JustifyContent : StyleLayout.JustifyContent;


    /// <summary>
    ///     It shifts the element to the position of its side.
    /// </summary>
    protected virtual void SetElementOffsetBySide(List<int> elementsIndexes, CalculatedStyle sideOuterDimensions)
    {
        sideOuterDimensions = GetRelativeDimensions(sideOuterDimensions);

        foreach (var id in elementsIndexes)
        {
            _rectangleContexts[id].Left += sideOuterDimensions.X;
            _rectangleContexts[id].Top += sideOuterDimensions.Y;
        }
    }


    /// <summary>
    ///     Defines <see cref="Enums.UIExAlignment"/> on the main axis.
    ///     <para>
    ///         If the nested element positions itself, its positioning returns: <see cref="Styles.StyleLayoutChild.JustifySelf"/>.
    ///         Otherwise, if a party positions the elements inside, it returns its positioning: <see cref="Styles.StyleSide.JustifyContent"/>
    ///         Otherwise, returns the positioning of the container layout: <see cref="Styles.StyleLayoutContainer.JustifyContent"/>
    ///     </para>
    /// </summary>
    protected override Enums.UIExAlignment GetJustify(int index)
    {
        Styles.StyleLayoutChild style = _styleElementsContexts[index];

        if (style.JustifySelf != Enums.UIExAlignment.Auto)
            return style.JustifySelf;

        if(_context.SideJustifyContent != Enums.UIExAlignment.Auto)
            return _context.SideJustifyContent;

        return StyleLayout.JustifyContent;
    }

    /// <summary>
    ///     Defines <see cref="Enums.UIExAlignment"/> transverse axis.
    ///     <para>
    ///         If the nested element positions itself, its positioning returns: <see cref="Styles.StyleLayoutChild.AlignSelf"/>. 
    ///         Otherwise, if a party positions the elements inside, it returns its positioning: <see cref="Styles.StyleSide.JustifyContent"/>
    ///         Otherwise, returns the positioning of the container layout: <see cref="Styles.StyleLayoutContainer.AlignItems"/>
    ///     </para>
    /// </summary>
    protected override Enums.UIExAlignment GetAlign(int index)
    {
        Styles.StyleLayoutChild style = _styleElementsContexts[index];

        if (style.AlignSelf != Enums.UIExAlignment.Auto)
            return style.AlignSelf;

        if (_context.SideAlignItems != Enums.UIExAlignment.Auto)
            return _context.SideAlignItems;

        return StyleLayout.AlignItems;
    }




    /// <inheritdoc/>
    protected override void RefreshLayoutDebugLines()
    {
        if (!ShowLayoutLines)
            return;

        base.RefreshLayoutDebugLines();

        LayoutDebugRectangle topSide = new(
            x:          _context.topInnerDimensions.X,
            y:          _context.topInnerDimensions.Y,
            width:      _context.topInnerDimensions.Width,
            height:     _context.topInnerDimensions.Height);

        LayoutDebugRectangle leftSide = new(
            x:          _context.leftInnerDimensions.X,
            y:          _context.leftInnerDimensions.Y,
            width:      _context.leftInnerDimensions.Width,
            height:     _context.leftInnerDimensions.Height);

        LayoutDebugRectangle bottomSide = new(
            x:          _context.bottomInnerDimensions.X,
            y:          _context.bottomInnerDimensions.Y,
            width:      _context.bottomInnerDimensions.Width,
            height:     _context.bottomInnerDimensions.Height);

        LayoutDebugRectangle rightSide = new(
            x:          _context.rightInnerDimensions.X,
            y:          _context.rightInnerDimensions.Y,
            width:      _context.rightInnerDimensions.Width,
            height:     _context.rightInnerDimensions.Height);

        LayoutDebugRectangle fillSide = new(
            x: _context.fillInnerDimensions.X,
            y: _context.fillInnerDimensions.Y,
            width: _context.fillInnerDimensions.Width,
            height: _context.fillInnerDimensions.Height);

        if (topSide.IsNotEmpty())
            LayoutDebugRectangles.Add(topSide);

        if(leftSide.IsNotEmpty())
            LayoutDebugRectangles.Add(leftSide);

        if(bottomSide.IsNotEmpty())
            LayoutDebugRectangles.Add(bottomSide);

        if(rightSide.IsNotEmpty())
            LayoutDebugRectangles.Add(rightSide);

        if (fillSide.IsNotEmpty())
            LayoutDebugRectangles.Add(fillSide);
    }
}