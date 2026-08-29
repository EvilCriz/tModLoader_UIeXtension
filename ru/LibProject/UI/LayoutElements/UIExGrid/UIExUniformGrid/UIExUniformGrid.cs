using UIeXtension.MethodsExtensions;

namespace UIeXtension;

/// <summary>
///     Контейнер компоновки, позволяющий расположить элементы в виде сетки.
///     Сетка указывается вручную или автоматически.
///     Элементы располагаются автоматически.
/// </summary>
public class UIExUniformGrid : UIExGridBase
{
    /// <inheritdoc/>
    public UIExUniformGrid() : this(new Styles.StyleVisualElement()) { }

    /// <inheritdoc/>
    public UIExUniformGrid(Styles.StyleVisualElement styleVisual) : this(styleVisual, new Styles.StyleLayoutContainer()) { }

    /// <inheritdoc/>
    public UIExUniformGrid(Styles.StyleLayoutContainer styleLayout) : this(new Styles.StyleVisualElement(), styleLayout) { }

    /// <inheritdoc/>
    public UIExUniformGrid(Styles.StyleVisualElement styleVisual, Styles.StyleLayoutContainer styleLayout) : base(styleVisual, styleLayout) { }



    /// <inheritdoc/>
    protected override void PreprareContainerStyleGridContext()
    {
        for (int i = 0; i < _elementsContext.Count; i++)
        {
            Styles.StyleLayoutChildGrid styleChildGrid = new();
            Styles.StyleLayoutChildUniformGrid styleChildUGrid =
                _elementsContext[i].StyleLayoutChild().UniformGrid();

            styleChildGrid.RowSpan = Max(1, styleChildUGrid.RowSpan);
            styleChildGrid.ColumnSpan = Max(1, styleChildUGrid.ColumnSpan);

            _context.stylesChildsGrid.Add(styleChildGrid);
        }

        /////////////////////////////

        Styles.StyleLayoutContainerUniformGrid styleUGrid = StyleLayout.UniformGrid();

        _context.rowsCount = styleUGrid.RowsCount;
        _context.columnsCount = styleUGrid.ColumnsCount;

        CalculateGridDimensions();

        ////////////////////////////

        Styles.StyleLayoutContainerGrid styleGrid = new();
        styleGrid.AddRowDefinition(Styles.UIExGridLength.FromFr(1f), repeat: _context.rowsCount);
        styleGrid.AddColumnDefinition(Styles.UIExGridLength.FromFr(1f), repeat: _context.columnsCount);

        styleGrid.RowsSpace = styleUGrid.RowsSpace;
        styleGrid.ColumnsSpace = styleUGrid.ColumnsSpace;

        styleGrid.RowsAlignment = styleUGrid.RowsAlignment;
        styleGrid.ColumnsAlignment = styleUGrid.ColumnsAlignment;

        _context.styleContainerGrid = styleGrid;
    }

    /// <inheritdoc/>
    protected override void PreprareChildStyleGridContext()
    {
        bool[,] cellsInfo = new bool[_context.rowsCount, _context.columnsCount];

        for (int i = 0; i < _context.stylesChildsGrid.Count; i++)
        {
            var style = _context.stylesChildsGrid[i];

            int row = -1;
            int column = -1;

            for (int r = 0; r < _context.rowsCount; r++)
            {
                for (int c = 0; c < _context.columnsCount; c++)
                {
                    if (cellsInfo[r, c])
                        continue;

                    row = r;
                    column = c;
                    break;
                }

                if (row != -1)
                    break;
            }


            if (row == -1)
            {
                style.Row = _context.rowsCount - 1;
                style.Column = _context.columnsCount - 1;
                style.RowSpan = 1;
                style.ColumnSpan = 1;
                continue;
            }

            style.Row = row;
            style.Column = column;

            int rowSpan = Min(style.RowSpan, _context.rowsCount - row);
            int columnSpan = Min(style.ColumnSpan, _context.columnsCount - column);

            for (int c = 0; c < columnSpan; c++)
            {
                bool occupied = false;

                for (int r = 0; r < rowSpan; r++)
                {
                    if (cellsInfo[row + r, column + c])
                    {
                        occupied = true;
                        break;
                    }
                }

                if (occupied)
                {
                    columnSpan = c;
                    break;
                }
            }


            for (int r = 0; r < rowSpan; r++)
            {
                bool occupied = false;

                for (int c = 0; c < columnSpan; c++)
                {
                    if (cellsInfo[row + r, column + c])
                    {
                        occupied = true;
                        break;
                    }
                }

                if (occupied)
                {
                    rowSpan = r;
                    break;
                }
            }

            rowSpan = Max(1, rowSpan);
            columnSpan = Max(1, columnSpan);

            style.RowSpan = rowSpan;
            style.ColumnSpan = columnSpan;

            for (int r = row; r < row + rowSpan; r++)
            {
                for (int c = column; c < column + columnSpan; c++)
                {
                    cellsInfo[r, c] = true;
                }
            }
        }
    }

    /// <summary>
    ///     Расчет количества строк и столбцов <see cref="UIExUniformGrid"/>
    /// </summary>
    protected virtual void CalculateGridDimensions()
    {
        if (_context.rowsCount > 0 && _context.columnsCount > 0)
            return;

        if (_context.rowsCount > 0)
        {
            _context.columnsCount = 
                (int)System.MathF.Ceiling(
                    (float)_elementsContext.Count / _context.rowsCount);

            return;
        }

        if (_context.columnsCount > 0)
        {
            _context.rowsCount = 
                (int)System.MathF.Ceiling(
                    (float)_elementsContext.Count / _context.columnsCount);

            return;
        }

        if (_elementsContext.Count <= 0)
        {
            _context.rowsCount = 0;
            _context.columnsCount = 0;
            return;
        }

        _context.columnsCount = 
            (int)System.MathF.Ceiling(
                System.MathF.Sqrt(_elementsContext.Count));

        _context.rowsCount = 
            (int)System.MathF.Ceiling(
                (float)_elementsContext.Count / _context.columnsCount);
    }


    /// <inheritdoc/>
    protected override Styles.UIExGridLength GetConvertPrecentDefinition(Styles.UIExGridLength definition, float innerDimensionsSize)
        => definition;

    /// <inheritdoc/>
    protected override Styles.UIExGridLength GetConvertAutoDefinitionWithoutSpan(bool row, int definitionIndex, Styles.UIExGridLength definition)
        => definition;

    /// <inheritdoc/>
    protected override void CalculateAutoDefinitionWithSpan(bool row) { }
}