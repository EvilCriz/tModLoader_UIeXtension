namespace UIeXtension;

/// <summary>
///     Позволяет задавать абсолютное позиционирование по заданным параметрам.
/// </summary>
public partial class UIExCanvas : UIExLayout
{
    /// <inheritdoc/>
    protected override void RefreshLayout()
    {
        _context.vertical = StyleLayout.Orientation == Enums.UIExOrientation.Vertical;

        CalculateAlignmentOffsets();
        for (int i = 0; i < _elementsContext.Count; i++)
        {
            SetPosition(i, justify: true);
            SetPosition(i, justify: false);

            if (!TrySetStretchSize(i, justify: true))
                SetNoStretchSize(i, justify: true);

            if (!TrySetStretchSize(i, justify: false))
                SetNoStretchSize(i, justify: false);

            Truncate(i, justify: true);
            Truncate(i, justify: false);
        }
    }

    /// <inheritdoc/>
    protected virtual void CalculateAlignmentOffsets()
    {
        _context.justifyOffsets = new float[_elementsContext.Count];
        _context.alignOffsets = new float[_elementsContext.Count];
        for (int i = 0; i < _elementsContext.Count; i++)
        {
            _context.justifyOffsets[i] = GetAlignmentOffset(justify: true, i);
            _context.alignOffsets[i] = GetAlignmentOffset(justify: false, i);
        }
    }

    /// <inheritdoc/>
    protected virtual float GetAlignmentOffset(bool justify, int index)
    {
        var alignent = justify
            ? GetJustify(index)
            : GetAlign(index);

        bool vertical = justify ? _context.vertical : !_context.vertical;

        float elementSize = vertical
            ? _elementsOuterDimensionsContext[index].Height
            : _elementsOuterDimensionsContext[index].Width;

        float parentSize = vertical
            ? _innerDimensionsContext.Height
            : _innerDimensionsContext.Width;


        return alignent switch
        {
            Enums.UIExAlignment.Center => parentSize / 2f - elementSize / 2f,
            Enums.UIExAlignment.End => parentSize - elementSize,
            _ => 0f
        };
    }


    /// <inheritdoc/>
    protected virtual void SetPosition(int index, bool justify)
    {
        Styles.StyleLayoutChildCanvas stylePosition = _styleChildCanvasContexts[index];
        Styles.StyleLayoutChild styleMargin = _styleElementsContexts[index];

        bool vertical = justify ? _context.vertical : !_context.vertical;
        var alignment = justify ? GetJustify(index) : GetAlign(index);

        float adaptiveParentSize = vertical
            ? _innerDimensionsContext.Height
            : _innerDimensionsContext.Width;

        float elementOuterSize = vertical
            ? _elementsOuterDimensionsContext[index].Height
            : _elementsOuterDimensionsContext[index].Width;

        adaptiveParentSize = alignment switch
        {
            Enums.UIExAlignment.Stretch => adaptiveParentSize,
            Enums.UIExAlignment.Center => adaptiveParentSize / 2f - elementOuterSize / 2f,
            Enums.UIExAlignment.End => 0f,
            _ => adaptiveParentSize - elementOuterSize
        };

        float positon = vertical
            ? stylePosition.Top.GetValue(adaptiveParentSize)
            : stylePosition.Left.GetValue(adaptiveParentSize);

        positon -= vertical
            ? stylePosition.Bottom.GetValue(adaptiveParentSize)
            : stylePosition.Right.GetValue(adaptiveParentSize);

        positon += justify
            ? _context.justifyOffsets[index]
            : _context.alignOffsets[index];

        GetChildMargin(
            style: styleMargin,
            vertical: vertical,
            out float marginStart,
            out float marginEnd,
            dimension: _elementsDimensionsContext[index]);

        positon += marginStart;

        if (vertical)
            _rectangleContexts[index].Top = positon;
        else
            _rectangleContexts[index].Left = positon;
    }

    /// <summary/>
    protected virtual bool TrySetStretchSize(int index, bool justify)
    {
        if (!IsStretchAlignment(index, justify))
            return false;
        
        bool vertical = justify ? _context.vertical : !_context.vertical;

        float parentSize = vertical
            ? _innerDimensionsContext.Height
            : _innerDimensionsContext.Width;

        GetChildMargin(
            _styleElementsContexts[index],
            vertical,
            out float marginStart,
            out float marginEnd,
            _elementsDimensionsContext[index]);

        parentSize -= (marginStart + marginEnd);
        parentSize = Microsoft.Xna.Framework.MathHelper.Max(0f, parentSize);

        if (vertical)
            _rectangleContexts[index].Height = parentSize;
        else
            _rectangleContexts[index].Width = parentSize;

        return true;
    }

    /// <summary/>
    protected virtual void SetNoStretchSize(int index, bool justify)
    {
        if (IsStretchAlignment(index, justify))
            return;

        // justify == true  && vertical == true;    >> vertical     >> true     >> Top/Height
        // justify == true  && vertical == false;   >> vertical     >> false    >> Left/Width
        // justify == false && vertical == true;    >> !vertical    >> false    >> Left/Width
        // justify == false && vertical == false;   >> !vertical    >> true     >> Top/Height
        if (justify ? _context.vertical : !_context.vertical)
            _rectangleContexts[index].Height = _elementsDimensionsContext[index].Height;
        else
            _rectangleContexts[index].Width = _elementsDimensionsContext[index].Width;
    }

    /// <summary/>
    protected virtual void Truncate(int index, bool justify)
    {
        if (GetAllowOverflow(index))
            return;

        RectangleLayoutContext rect = _rectangleContexts[index];

        // justify == true  && vertical == true;    >> vertical     >> true     >> Top/Height
        // justify == true  && vertical == false;   >> vertical     >> false    >> Left/Width
        // justify == false && vertical == true;    >> !vertical    >> false    >> Left/Width
        // justify == false && vertical == false;   >> !vertical    >> true     >> Top/Height
        bool vertical = justify ? _context.vertical : !_context.vertical;

        float parentSize = vertical
            ? _innerDimensionsContext.Height
            : _innerDimensionsContext.Width;

        float elementSize = vertical
            ? rect.Height
            : rect.Width;

        float elementPosition = vertical
            ? rect.Top
            : rect.Left;

        elementPosition = Microsoft.Xna.Framework.MathHelper.Max(0f, elementPosition);

        elementPosition = GetTryTruncate(elementPosition);
        elementSize = GetTryTruncate(elementSize);

        if (vertical)
        {
            rect.Top = elementPosition;
            rect.Height = elementSize;
        }
        else
        {
            rect.Left = elementPosition;
            rect.Width = elementSize;
        }

        float GetTryTruncate(float posOrSize)
        {
            float diff = (elementSize + elementPosition) - parentSize;
            return diff > 0f
                ? Microsoft.Xna.Framework.MathHelper.Max(0f, posOrSize - diff)
                : posOrSize;
        }
    }

    /// <summary/>
    protected bool GetAllowOverflow(int index)
        => _styleChildCanvasContexts[index].AllowOverflowSelf is not null
            ? (bool)_styleChildCanvasContexts[index].AllowOverflowSelf
            : _context.styleLayoutContainer.AllowOverflow;
}