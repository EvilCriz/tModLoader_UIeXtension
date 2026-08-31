using System.Collections.Generic;

namespace UIeXtension;

public partial class UIExWrapPanel
{
    /// <summary>
    ///     Class containing temporary information about the current layout <see cref="UIExFlowPanel"/>.
    /// </summary>
    protected class UIExWrapPanelContext
    {
        /// <summary>
        ///     A list of lines, each of which is presented as lists of elements in a line.
        /// </summary>
        public List<List<int>> linesElementsIndexes = new();

        /// <summary>
        ///     Final style <see cref="UIExWrapPanel"/>, used in the context of this arrangement of elements.
        /// </summary>
        public Styles.StyleLayoutContainerWrapPanel styleLayoutContainer;
    }

    /// <summary>
    ///     Stores data for the current layout <see cref="UIExWrapPanel"/>.
    ///     Relevant only during the life cycle of the layout.
    /// </summary>
    protected UIExWrapPanelContext _context;

    /// <inheritdoc/>
    protected override void BeginLayoutContext()
    {
        base.BeginLayoutContext();

        _context = new();
        _context.styleLayoutContainer = StyleLayout.WrapPanel();
    }

    /// <inheritdoc/>
    protected override void EndLayoutContext()
    {
        base.EndLayoutContext();

        _context = null;
    }
}