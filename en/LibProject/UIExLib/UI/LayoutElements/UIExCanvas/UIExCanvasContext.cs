namespace UIeXtension;
public partial class UIExCanvas
{
    /// <summary>
    ///     Class containing temporary information about the current layout <see cref="UIExCanvas"/>.
    /// </summary>
    protected class UIExCanvasContext
    {
        /// <summary>
        ///     Current orientation of the main axis.
        /// </summary>
        public bool vertical;

        /// <summary>
        ///     Departures of elements along the main axis.
        /// </summary>
        public float[] justifyOffsets;

        /// <summary>
        ///     Detachments of elements along the transverse axis.
        /// </summary>
        public float[] alignOffsets;

        /// <summary>
        ///     Final style <see cref="UIExCanvas"/>, used in the context of this arrangement of elements.
        /// </summary>
        public Styles.StyleLayoutContainerCanvas styleLayoutContainer;
    }




    /// <summary>
    ///     Stores all the styles of the child elements involved in the layout.  
    ///     Relevant only during the life cycle of the layout.
    /// </summary>
    protected System.Collections.Generic.List<Styles.StyleLayoutChildCanvas> _styleChildCanvasContexts = null;

    /// <summary>
    ///     Temporary layout information. Only relevant during the layout life cycle.
    /// </summary>
    protected UIExCanvasContext _context = null;





    /// <inheritdoc/>
    protected override void BeginLayoutContext()
    {
        base.BeginLayoutContext();

        _context = new();
        _context.styleLayoutContainer = StyleLayout.Canvas();

        _styleChildCanvasContexts = new(_elementsContext.Count);
        for (int i = 0; i < _styleElementsContexts.Count; i++)
            _styleChildCanvasContexts.Add(_styleElementsContexts[i].Canvas());
    }

    /// <inheritdoc/>
    protected override void EndLayoutContext()
    {
        base.EndLayoutContext();

        _context = null;
        _styleChildCanvasContexts = null;
    }
}