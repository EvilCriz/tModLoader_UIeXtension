using System.Collections.Generic;
using Terraria.UI;

namespace UIeXtension;

public abstract partial class UIExGridBase : UIExLayout
{
    /// <inheritdoc/>
    protected override void RefreshLayout()
    {
        PrepareGridDefinitions();
        CalculateCells();
        for (int i = 0; i < _elementsContext.Count; i++)
        {
            CalculateElementCellDimensions(index: i);
            CalculateElementGridPositionAndSize(index: i);
        }
    }

    /// <summary>
    ///     Вказати позицію та розмір елементів в залежності від її рови, стовпця, RowSpan. . ColumnSpan
    /// </summary>
    protected virtual void CalculateElementGridPositionAndSize(int index)
    {
        Enums.UIExAlignment justify = GetJustify(index);
        Enums.UIExAlignment align = GetAlign(index);
        bool vertical = StyleLayout.Orientation == Enums.UIExOrientation.Vertical;

        SetElementAlignment(
            index:      index,
            alignment:  justify,
            vertical:   vertical,
            justify:    true);

        SetElementAlignment(
            index:      index,
            alignment:  align,
            vertical:   vertical,
            justify:    false);

        SetElementSize(
            index:      index, 
            alignment:  justify, 
            vertical:   vertical, 
            justify:    true);

        SetElementSize(
            index:      index, 
            alignment:  align, 
            vertical:   vertical, 
            justify:    false);
    }

    /// <summary>
    ///     Визначає вирівнювання елемента в комірці (клітини)
    /// </summary>
    protected virtual void SetElementAlignment(
        int index,
        Enums.UIExAlignment alignment,
        bool vertical,
        bool justify)
    {
        var rect = _rectangleContexts[index];

        vertical = justify ? vertical : !vertical;

        CalculatedStyle parentDimension = _context.elementsCellDimension[index].GetCalculatedStyle();
        float parentSize = GetInnerDimensionsSize(vertical, parentDimension);
        float elementSize = GetElementSize(index, vertical);
        float offset = alignment switch
        {
            Enums.UIExAlignment.Center => (parentSize - elementSize) / 2f,
            Enums.UIExAlignment.End => parentSize - elementSize,
            _ => 0f
        };

        GetChildMargin(
            _styleElementsContexts[index],
            vertical,
            out float marginStart,
            out float _,
            _innerDimensionsContext);

        if (vertical)
            rect.Top = parentDimension.Y - _innerDimensionsContext.Y + offset + marginStart;
        else
            rect.Left = parentDimension.X - _innerDimensionsContext.X + offset + marginStart;
    }

    /// <summary>
    ///     Визначає вирівнювання елемента всередині клітинки (клітини) вздовж передається вісь
    /// </summary>
    protected virtual void SetElementSize(
        int index,
        Enums.UIExAlignment alignment,
        bool vertical,
        bool justify)
    {
        var rect = _rectangleContexts[index];
        vertical = justify ? vertical : !vertical;

        if (alignment != Enums.UIExAlignment.Stretch)
        {
            if (vertical)
                rect.Height = _styleElementsContexts[index].Height.GetValue(_innerDimensionsContext.Height);
            else
                rect.Width = _styleElementsContexts[index].Width.GetValue(_innerDimensionsContext.Width);

            return;
        }

        GetChildMargin(
            _styleElementsContexts[index],
            vertical,
            out float marginStart,
            out float marginEnd,
            _innerDimensionsContext);

        var cellRect = _context.elementsCellDimension[index];

        if (vertical)
            rect.Height = cellRect.Height - marginStart - marginEnd;
        else
            rect.Width = cellRect.Width - marginStart - marginEnd;
    }




    /// <summary>
    ///     Вказати позицію та розмір площі, в якому буде розташований переведений елемент.
    /// </summary>
    protected virtual void CalculateElementCellDimensions(int index)
    {
        var rect = _context.elementsCellDimension[index];
        var styleGrid = _context.stylesChildsGrid[index];
        //варення styleGrid до + style.Grid();

        int row = styleGrid.Row;
        int column = styleGrid.Column;
        int rowSpan = styleGrid.RowSpan;
        int columnSpan = styleGrid.ColumnSpan;

        var cell = _context.cells[row, column];

        rect.Top = cell.Top;
        rect.Left = cell.Left;

        rect.Height = GetCellSizeBySpan(row, rowSpan, row: true);
        rect.Width = GetCellSizeBySpan(column, columnSpan, row: false);
    }

    /// <summary>
    ///     Повертає загальний розмір клітин, що переноситься елемент займає.
    /// </summary>
    protected float GetCellSizeBySpan(int def, int defSpan, bool row)
    {
        List<int> definitions = new(defSpan);
        for (int i = def; i < def + defSpan; i++)
            definitions.Add(i);

        return GetDefinitionsTotalPixelsSize(definitions, row);
    }

    /// <summary>
    ///     Розрахунок позиції і розміру кожної комірки (у пікселях)
    /// </summary>
    protected virtual void CalculateCells()
    {
        float rowsTotalSize = GetDefinitionsTotalPixelsSize(_context.rowDefinitions, row: true);
        float columnsTotalSize = GetDefinitionsTotalPixelsSize(_context.columnDefinitions, row: false);

        float rowsInnerDim = _innerDimensionsContext.Height;
        float columnsInnerDim = _innerDimensionsContext.Width;

        float rowsAlignOffset = _context.styleContainerGrid.RowsAlignment switch
        {
            Enums.UIExAlignment.Center => (rowsInnerDim - rowsTotalSize) / 2f,
            Enums.UIExAlignment.End => rowsInnerDim - rowsTotalSize,
            _ => 0f
        };
        float columnsAlignOffset = _context.styleContainerGrid.ColumnsAlignment switch
        {
            Enums.UIExAlignment.Center => (columnsInnerDim - columnsTotalSize) / 2f,
            Enums.UIExAlignment.End => columnsInnerDim - columnsTotalSize,
            _ => 0f
        };


        float totalOffsetRows = rowsAlignOffset;
        float totalOffsetColumns = columnsAlignOffset;

        foreach (var cell in _context.cells)
        {
            cell.Left = _innerDimensionsContext.X;
            cell.Top = _innerDimensionsContext.Y;
        }

        for (int i = 0; i < _context.rowsCount; i++)
        {
            float rowDefHeight = _context.rowDefinitions[i].Pixels;

            for (int j = 0; j < _context.columnsCount; j++)
            {
                float columnDefWidth = _context.columnDefinitions[j].Pixels;
                var cell = _context.cells[i, j];
                
                cell.Top += totalOffsetRows;
                cell.Left += totalOffsetColumns;
                cell.Height = rowDefHeight;
                cell.Width = columnDefWidth;
                

                totalOffsetColumns += columnDefWidth + _context.columnsSpace;
            }

            totalOffsetColumns = columnsAlignOffset;
            totalOffsetRows += rowDefHeight + _context.rowsSpace;
        }
    }
}