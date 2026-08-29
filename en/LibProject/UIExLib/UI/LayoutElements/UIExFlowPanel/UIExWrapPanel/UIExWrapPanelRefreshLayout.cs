using System.Collections.Generic;
namespace UIeXtension;

public partial class UIExWrapPanel
{
    /// <inheritdoc/>
    protected override void PreRefreshLayout()
    {
        base.PreRefreshLayout();

        if (_elementsContext.Count == 0)
            return;

        _flowContext.vertical = StyleLayout.Orientation == Enums.UIExOrientation.Vertical;

        foreach (var style in _styleElementsContexts)
        {
            style.Width.Pixels += style.Width.Precent * _innerDimensionsContext.Width;
            style.Height.Pixels += style.Height.Precent * _innerDimensionsContext.Height;

            style.Width.Precent = 0f;
            style.Height.Precent = 0f;
        }
    }

    /// <inheritdoc/>
    protected override void RefreshLayout()
    {
        base.RefreshLayout();

        if (_elementsContext.Count == 0)
            return;

        float innerDimJustifySize = GetInnerDimensionsSize(
            vertical:                   _flowContext.vertical,
            parentInnerDimensions:      _innerDimensionsContext);

        float innerDimAlignSize = GetInnerDimensionsSize(
            vertical:                   !_flowContext.vertical,
            parentInnerDimensions:      _innerDimensionsContext);

        float spacingWithinLine = _context.styleLayoutContainer.SpacingWithinLine.GetValue(innerDimJustifySize);
        bool reverseWithinLine = _context.styleLayoutContainer.ReverseWithinLine;

        float spacingBetweenLines = _context.styleLayoutContainer.SpacingBetweenLines.GetValue(innerDimAlignSize);
        bool reverseAll = _context.styleLayoutContainer.ReverseAll;


        ///////////////////////

        List<int> elementsIndexes = GetElementsIndexsesByElementsContext();
        if (reverseAll)
            elementsIndexes.Reverse();

        //////////////////////

        CalulateLinesElementsIndexes(
            elementsIndexes:        elementsIndexes, 
            spacingWithinLine:      spacingWithinLine);

        //////////////////////

        List<UIExRectangle> linesRectangles = GetLinesRectangles();

        SetStretchAlignLinesSize(
            linesRectangles:        linesRectangles, 
            spacingBetweenLines:    spacingBetweenLines, 
            innerDimAlignSize:      innerDimAlignSize);

        CalculateLinesRectangelsOffsets(
            linesRectangles:        linesRectangles,
            spacingBetweenLines:    spacingBetweenLines,
            innerDimAlignSize:      innerDimAlignSize);

        /////////////////////

        for (int i = 0; i < _context.linesElementsIndexes.Count; i++)
            base.RefreshLayoutLine(
                elementsIndexes:            _context.linesElementsIndexes[i],
                spacing:                    spacingWithinLine,
                reverse:                    reverseWithinLine,
                parentInnerDimensions:      linesRectangles[i].GetCalculatedStyle(),
                containerJustifyContent:    StyleLayout.JustifyContent);

        ///////////////////////

        for (int i = 0; i < _context.linesElementsIndexes.Count; i++)
        {
            UIExRectangle lineRectangle = linesRectangles[i];
            foreach (var idx in _context.linesElementsIndexes[i])
            {
                _rectangleContexts[idx].Top += lineRectangle.Top;
                _rectangleContexts[idx].Left += lineRectangle.Left;
            }
        }
    }



    /////////////////////////////////////////////
    /////////////////////////////////////////////

    

    /// <inheritdoc/>
    protected override void RefreshLayoutDebugLines()
    {
        base.RefreshLayoutDebugLines();

        if (!ShowLayoutLines)
            return;

        List<UIExRectangle> linesRectangles = GetLinesRectangles();

        foreach(var lineRect in linesRectangles)
        {
            LayoutDebugRectangle lineDebugRectanlge = new(
                x:      lineRect.Left + _innerDimensionsContext.X,
                y:      lineRect.Top + _innerDimensionsContext.Y,
                width:  lineRect.GetWidth(),
                height: lineRect.GetHeight());

            LayoutDebugRectangles.Add(lineDebugRectanlge);
        }
    }



    /////////////////////////////////////////
    /////////////////////////////////////////

    /// <summary>
    ///     Stretching the lines on the transverse axis.
    /// </summary>
    protected virtual void SetStretchAlignLinesSize(
        List<UIExRectangle> linesRectangles,
        float spacingBetweenLines,
        float innerDimAlignSize)
    {
        if (_context.styleLayoutContainer.AlignLines != Enums.UIExAlignment.Stretch)
            return;

        float totalLineSize = GetTotalLineSize(linesRectangles);

        float totalSpacingBetweenLines = spacingBetweenLines * (linesRectangles.Count - 1);
        float availableSpace = totalLineSize + totalSpacingBetweenLines;
        float remaingSpace = innerDimAlignSize - availableSpace;

        float addSize = remaingSpace / linesRectangles.Count;

        for (int i = 0; i < linesRectangles.Count; i++) 
        {
            UIExRectangle lineRectangle = linesRectangles[i];

            lineRectangle.AddSize(
                width: _flowContext.vertical, 
                size: addSize);

            linesRectangles[i] = lineRectangle;
        }
    }


    /// <summary>
    ///     Setting up lines for alignment.
    /// </summary>
    protected virtual void CalculateLinesRectangelsOffsets(
        List<UIExRectangle> linesRectangles,
        float spacingBetweenLines,
        float innerDimAlignSize)
    {
        float totalLineSize = GetTotalLineSize(linesRectangles);

        float totalSpacingBetweenLines = spacingBetweenLines * (linesRectangles.Count - 1);
        float availableSpace = totalLineSize + totalSpacingBetweenLines;
        float remaingSpace = innerDimAlignSize - availableSpace;
        float offset = _context.styleLayoutContainer.AlignLines switch
        {
            Enums.UIExAlignment.Center => (innerDimAlignSize - availableSpace) / 2f,
            Enums.UIExAlignment.End => remaingSpace,
            _ => 0f
        };

        for (int i = 0; i < linesRectangles.Count; i++)
        {
            UIExRectangle lineRectangle = linesRectangles[i];
            if (_flowContext.vertical)
                lineRectangle.AddLeftOffset(offset);
            else
                lineRectangle.AddTopOffset(offset);

            linesRectangles[i] = lineRectangle;
            offset += lineRectangle.GetSize(_flowContext.vertical) + spacingBetweenLines;
        }
    }



    /// <summary>
    ///     The total size of the lines on the transmitted axis.
    /// </summary>
    protected virtual float GetTotalLineSize(List<UIExRectangle> linesRectangles)
    {
        float totalSize = 0f;

        foreach (var lineRectangle in linesRectangles)
            totalSize += lineRectangle.GetSize(width: _flowContext.vertical);

        return totalSize;
    }




    /// <summary>
    ///     Returns the list <see cref="UIExRectangle"/> for all lines.
    /// </summary>
    protected virtual List<UIExRectangle> GetLinesRectangles()
        => GetLinesRectangles(0, _context.linesElementsIndexes.Count);

    /// <summary>
    ///     Returns the list <see cref="UIExRectangle"/> for a range of lines.
    /// </summary>
    protected virtual List<UIExRectangle> GetLinesRectangles(
        int startLineIndex, 
        int endLineIndex)
    {
        startLineIndex = (int)Microsoft.Xna.Framework.MathHelper.Max(0, startLineIndex);
        endLineIndex = (int)Microsoft.Xna.Framework.MathHelper.Min(
            value1: endLineIndex,
            value2: _context.linesElementsIndexes.Count);

        List<UIExRectangle> result = new(endLineIndex - startLineIndex);

        for (int i = startLineIndex; i < endLineIndex; i++)
            result.Add(
                GetLineRectangle(
                    _context.linesElementsIndexes[i]));

        return result;
    }

    /// <summary>
    ///     Returns. <see cref="UIExRectangle"/> of the transmitted line.
    /// </summary>
    protected virtual UIExRectangle GetLineRectangle(List<int> lineElementsIndexes)
    {
        float minLeft, minTop, maxRight, maxBottom;

        if (_flowContext.vertical)
        {
            minTop = 0f;
            maxBottom = _innerDimensionsContext.Height;
            minLeft = float.MaxValue;
            maxRight = float.MinValue;
        }
        else
        {
            minLeft = 0f;
            maxRight = _innerDimensionsContext.Width;
            minTop = float.MaxValue;
            maxBottom = float.MinValue;
        }

        foreach (var idx in lineElementsIndexes)
        {
            RectangleLayoutContext rect = _rectangleContexts[idx];

            float right, bottom;

            if (_flowContext.vertical)
            {
                minLeft = Microsoft.Xna.Framework.MathHelper.Min(minLeft, rect.Left);

                right = _elementsOuterDimensionsContext[idx].Width + rect.Left;
                maxRight = Microsoft.Xna.Framework.MathHelper.Max(maxRight, right);

                bottom = maxBottom;
            }
            else
            {
                minTop = Microsoft.Xna.Framework.MathHelper.Min(minTop, rect.Top);

                bottom = _elementsOuterDimensionsContext[idx].Height + rect.Top;
                maxBottom = Microsoft.Xna.Framework.MathHelper.Max(maxBottom, bottom);

                right = maxRight;
            }
        }

        return new UIExRectangle(
            left: minLeft,
            top: minTop,
            right: maxRight,
            bottom: maxBottom);
    }




    /// <summary>
    ///     Breaks the transferred list of elements into a list of lines
    /// </summary>
    protected virtual void CalulateLinesElementsIndexes(List<int> elementsIndexes, float spacingWithinLine)
    {
        _context.linesElementsIndexes.Clear();

        int start = 0;

        while (start < elementsIndexes.Count)
        {
            int end = GetNextLineEndIndex(
                elementsIndexes:    elementsIndexes,
                start:              start,
                spacing:            spacingWithinLine);

            List<int> lineElementsIndexes = new List<int>(end - start + 1);
            for(int i = start; i <= end; i++)
                lineElementsIndexes.Add(elementsIndexes[i]);

            _context.linesElementsIndexes.Add(lineElementsIndexes);

            start = end + 1;
        }
    }

    /// <summary>
    ///     Returns the index of the last item that is placed in the current line.
    ///     Or the same index that was passed on, 
    ///     if the size of the outer region of the main axis of the 1st element is larger, 
    ///     than the size of the interior of the main axis container.
    /// </summary>
    protected virtual int GetNextLineEndIndex(List<int> elementsIndexes, int start, float spacing)
    {
        int end = start;

        float availableLineSize = _flowContext.vertical
            ? _innerDimensionsContext.Height
            : _innerDimensionsContext.Width;

        for (; end < elementsIndexes.Count; end++)
        {
            int idx = elementsIndexes[end];

            float requiredSize = _flowContext.vertical
                ? _elementsOuterDimensionsContext[idx].Height
                : _elementsOuterDimensionsContext[idx].Width;

            if (end > start)
                requiredSize += spacing;

            if (requiredSize > availableLineSize)
                return end > start ? end - 1 : end;

            availableLineSize -= requiredSize;
        }

        return elementsIndexes.Count - 1;
    }
}