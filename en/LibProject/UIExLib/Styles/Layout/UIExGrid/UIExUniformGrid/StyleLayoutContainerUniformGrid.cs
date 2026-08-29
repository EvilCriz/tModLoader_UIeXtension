using System.Collections.Generic;
using Terraria.UI;

namespace UIeXtension.Styles;

/// <summary>
///     Container layout style <see cref="UIExUniformGrid"/>
/// </summary>
public class StyleLayoutContainerUniformGrid : Base.StyleLayoutContainerBase
{
    /// <summary/>
    public StyleDimension RowsSpace = StyleDimension.Empty;
    /// <summary/>
    public StyleDimension ColumnsSpace = StyleDimension.Empty;


    /// <summary>
    ///     Grid alignment on the main axis.
    /// </summary>
    public Enums.UIExAlignment RowsAlignment = Enums.UIExAlignment.Start;

    /// <summary>
    /// Grid alignment along the transverse axis.
    /// </summary>
    public Enums.UIExAlignment ColumnsAlignment = Enums.UIExAlignment.Start;


    /// <summary/>
    public int RowsCount = 0;

    /// <summary/>
    public int ColumnsCount = 0;




    ///////////////////// CONSTRUCTORS ////////////////////
    ///////////////////// CONSTRUCTORS ////////////////////

    /// <summary/>
    public StyleLayoutContainerUniformGrid() { }

    /// <summary/>
    public StyleLayoutContainerUniformGrid(
        int rowsCount = 0,
        int columnsCount = 0,
        Enums.UIExAlignment rowsAlignment = Enums.UIExAlignment.Start,
        Enums.UIExAlignment columnsAlignment = Enums.UIExAlignment.Start,
        StyleDimension rowsSpace = default(StyleDimension),
        StyleDimension columnSpace = default(StyleDimension))
            => SetAllFields(
                rowsCount,
                columnsCount,
                rowsAlignment,
                columnsAlignment,
                rowsSpace,
                columnSpace);




    //////////////////// SETS /////////////////////////
    //////////////////// SETS /////////////////////////

    /// <summary>
    ///     Requires to convey all possible class values <see cref="StyleLayoutContainerUniformGrid"/> and sets them up.
    /// </summary>
    public void Set(
        int rowsCount = 0,
        int columnsCount = 0,
        Enums.UIExAlignment rowsAlignment = Enums.UIExAlignment.Start,
        Enums.UIExAlignment columnsAlignment = Enums.UIExAlignment.Start,
        StyleDimension rowsSpace = default(StyleDimension),
        StyleDimension columnSpace = default(StyleDimension))
            => SetAllFields(
                rowsCount,
                columnsCount,
                rowsAlignment,
                columnsAlignment,
                rowsSpace,
                columnSpace);

    /// <summary/>
    protected void SetAllFields(
        int rowsCount,
        int columnsCount,
        Enums.UIExAlignment rowsAlignment,
        Enums.UIExAlignment columnsAlignment,
        StyleDimension rowsSpace,
        StyleDimension columnSpace)
    {
        RowsCount = rowsCount;
        ColumnsCount = columnsCount;

        RowsAlignment = rowsAlignment;
        ColumnsAlignment = columnsAlignment;

        RowsSpace = rowsSpace;
        ColumnsSpace = columnSpace;
    }




    //////////////////// COPY /////////////////////////
    //////////////////// COPY /////////////////////////

    /// <inheritdoc/>
    protected override Base.StyleBase Fabricate() => new StyleLayoutContainerUniformGrid();

    /// <inheritdoc/>
    protected override void CopyBase(Base.StyleBase style)
    {
        if (style is StyleLayoutContainerUniformGrid style2)
            Copy(style2);
    }

    /// <inheritdoc/>
    public void Copy(StyleLayoutContainerUniformGrid style)
    {
        RowsCount = style.RowsCount;
        ColumnsCount = style.ColumnsCount;

        RowsAlignment = style.RowsAlignment;
        ColumnsAlignment = style.ColumnsAlignment;

        RowsSpace = style.RowsSpace;
        ColumnsSpace = style.ColumnsSpace;
    }

    /// <inheritdoc/>
    public StyleLayoutContainerUniformGrid GetCopy()
        => GetCopyBase<StyleLayoutContainerUniformGrid>();




    //////////////////// EQUALS /////////////////////////
    //////////////////// EQUALS /////////////////////////

    /// <inheritdoc/>
    protected override bool EqualsFields(Base.StyleBase other)
    {
        if (other is StyleLayoutContainerUniformGrid style)
        {
            if (RowsCount != style.RowsCount ||
                ColumnsCount != style.ColumnsCount)
                return false;

            if (RowsAlignment != style.RowsAlignment ||
                ColumnsAlignment != style.ColumnsAlignment)
                return false;

            if (!Utils.UtilsStyles.EqualsStyleDimensionFields(RowsSpace, style.RowsSpace) ||
                !Utils.UtilsStyles.EqualsStyleDimensionFields(ColumnsSpace, style.ColumnsSpace))
                return false;

            return true;
        }

        return false;
    }
}