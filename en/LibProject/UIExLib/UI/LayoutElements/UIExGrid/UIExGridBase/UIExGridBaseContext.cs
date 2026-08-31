using System.Collections.Generic;

namespace UIeXtension;

public abstract partial class UIExGridBase
{
    /// <summary>
    ///     Temporary given <see cref="UIExGridBase"/> during the layout.
    /// </summary>
    protected class UIExGridContext
    {
        /// <summary/>
        public float rowsSpace;
        /// <summary/>
        public float columnsSpace;

        /// <summary/>
        public Styles.StyleLayoutContainerGrid styleContainerGrid;
        /// <summary/>
        public List<Styles.StyleLayoutChildGrid> stylesChildsGrid;

        /// <summary/>
        public int rowsCount;
        /// <summary/>
        public int columnsCount;

        /// <summary/>
        public List<Styles.UIExGridLength> rowDefinitions;
        /// <summary/>
        public List<Styles.UIExGridLength> columnDefinitions;

        /// <summary>
        ///     Area of each cell
        /// </summary>
        public RectangleLayoutContext[,] cells;

        /// <summary>
        ///     Area of cells for each element
        /// </summary>
        public List<RectangleLayoutContext> elementsCellDimension;
    }

    /// <summary>
    ///     Temporary given <see cref="UIExGridBase"/> during the layout.
    /// </summary>
    protected UIExGridContext _context;

    /// <inheritdoc/>
    protected override void BeginLayoutContext()
    {
        base.BeginLayoutContext();
        _context = new UIExGridContext();

        _context.elementsCellDimension = new(_elementsContext.Count);
        for (int i = 0; i < _elementsContext.Count; i++)
            _context.elementsCellDimension.Add(
                new(i, _elementsContext[i], _innerDimensionsContext));

        /////////////////////////////////////////

        _context.stylesChildsGrid = new(_styleElementsContexts.Count);

        PreprareContainerStyleGridContext();

        var styleContainer = _context.styleContainerGrid;
        if (styleContainer.RowsCount == 0)
            styleContainer.AddRowDefinition(Styles.UIExGridLength.FromAuto());
        if (styleContainer.ColumnsCount == 0)
            styleContainer.AddColumnDefinition(Styles.UIExGridLength.FromAuto());

        PreprareChildStyleGridContext();

        //////////////////////////////


        _context.rowsCount = _context.styleContainerGrid.RowsCount;
        _context.columnsCount = _context.styleContainerGrid.ColumnsCount;

        _context.cells = new RectangleLayoutContext[_context.rowsCount, _context.columnsCount];
        for (int i = 0; i < _context.rowsCount; i++)
            for (int j = 0; j < _context.columnsCount; j++)
                _context.cells[i, j] = new RectangleLayoutContext(i * _context.columnsCount + j);

        _context.rowDefinitions = new(_context.rowsCount);
        _context.columnDefinitions = new(_context.columnsCount);

        ////////////////////////////////

        _context.rowsSpace = styleContainer.RowsSpace.GetValue(_innerDimensionsContext.Height);
        _context.columnsSpace = styleContainer.ColumnsSpace.GetValue(_innerDimensionsContext.Width);
    }

    /// <inheritdoc/>
    protected override void EndLayoutContext()
    {
        base.EndLayoutContext();
        _context = null;
    }

    /// <summary>
    ///     Preparing style <see cref="Styles.StyleLayoutContainerGrid"/> for the current layout.
    /// </summary>
    protected virtual void PreprareContainerStyleGridContext() { }

    /// <summary>
    ///     Preparing styles <see cref="Styles.StyleLayoutContainerGrid"/> for the current layout.
    /// </summary>
    protected virtual void PreprareChildStyleGridContext() { }
}