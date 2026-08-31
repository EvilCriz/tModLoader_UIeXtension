namespace UIeXtension;

public partial class UIExStackPanel
{
    /// <inheritdoc/>
    protected override void PreRefreshLayout()
    {
        base.PreRefreshLayout();

        if (_elementsContext.Count == 0)
            return;

        _flowContext.vertical = StyleLayout.Orientation == Enums.UIExOrientation.Vertical;
    }

    /// <inheritdoc/>
    protected override void RefreshLayout()
    {
        base.RefreshLayout();

        float innerDimSize = GetInnerDimensionsSize(
            vertical:               _flowContext.vertical, 
            parentInnerDimensions:  _innerDimensionsContext);

        float spacing = _context.styleLayoutContainer.Spacing.GetValue(innerDimSize);
        bool reverse = _context.styleLayoutContainer.Reverse;

        base.RefreshLayoutLine(
            elementsIndexes:            GetElementsIndexsesByElementsContext(), 
            spacing:                    spacing, 
            reverse:                    reverse,
            parentInnerDimensions:      _innerDimensionsContext,
            containerJustifyContent:    StyleLayout.JustifyContent);
    }
}