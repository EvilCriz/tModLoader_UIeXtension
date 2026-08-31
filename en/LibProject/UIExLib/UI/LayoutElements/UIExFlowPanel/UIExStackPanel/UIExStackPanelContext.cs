namespace UIeXtension;

public partial class UIExStackPanel
{
    /// <summary>
    ///     Class containing temporary information about the current layout <see cref="UIExFlowPanel"/>.
    /// </summary>
    protected class UIExStackPanelContext
    {
        /// <summary>
        ///     Final style <see cref="UIExStackPanel"/>, used in the context of this arrangement of elements.
        /// </summary>
        public Styles.StyleLayoutContainerStackPanel styleLayoutContainer;
    }

    /// <summary>
    ///     Stores data for the current layout <see cref="UIExStackPanel"/>.
    ///     Relevant only during the life cycle of the layout.
    /// </summary>
    protected UIExStackPanelContext _context;

    /// <inheritdoc/>
    protected override void BeginLayoutContext()
    {
        base.BeginLayoutContext();

        _context = new();
        _context.styleLayoutContainer = StyleLayout.StackPanel();
    }

    /// <inheritdoc/>
    protected override void EndLayoutContext()
    {
        base.EndLayoutContext();

        _context = null;
    }
}