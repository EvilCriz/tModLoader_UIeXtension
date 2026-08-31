namespace UIeXtension;

/// <summary>
///     Container layout, allowing you to arrange the elements in the form of a grid.
/// </summary>
public class UIExGrid : UIExGridBase
{
    /// <inheritdoc/>
    public UIExGrid() : this(new Styles.StyleVisualElement()) { }

    /// <inheritdoc/>
    public UIExGrid(Styles.StyleVisualElement styleVisual) : this(styleVisual, new Styles.StyleLayoutContainer()) { }

    /// <inheritdoc/>
    public UIExGrid(Styles.StyleLayoutContainer styleLayout) : this(new Styles.StyleVisualElement(), styleLayout) { }

    /// <inheritdoc/>
    public UIExGrid(Styles.StyleVisualElement styleVisual, Styles.StyleLayoutContainer styleLayout) : base(styleVisual, styleLayout) { }



    /// <inheritdoc/>
    protected override void PreprareContainerStyleGridContext()
    {
        _context.styleContainerGrid = StyleLayout.Grid().GetCopy();
    }

    /// <inheritdoc/>
    protected override void PreprareChildStyleGridContext()
    {
        foreach (var styleChildGrid in _styleElementsContexts)
        {
            var styleChild = styleChildGrid.Grid();

            styleChild.Row = Max(0, styleChild.Row);
            styleChild.Column = Max(0, styleChild.Column);

            styleChild.Row = Min(styleChild.Row, _context.styleContainerGrid.RowsCount - 1);
            styleChild.Column = Min(styleChild.Column, _context.styleContainerGrid.ColumnsCount - 1);

            styleChild.RowSpan = Max(1, styleChild.RowSpan);
            styleChild.ColumnSpan = Max(1, styleChild.ColumnSpan);

            styleChild.RowSpan = Min(styleChild.RowSpan, _context.styleContainerGrid.RowsCount - styleChild.Row);
            styleChild.ColumnSpan = Min(styleChild.ColumnSpan, _context.styleContainerGrid.ColumnsCount - styleChild.Column);

            _context.stylesChildsGrid.Add(styleChild);
        }
    }
}