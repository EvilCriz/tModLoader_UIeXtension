namespace UIeXtension.Styles;

/// <summary>
///     Styles of layout for <see cref="UIExGridBase"/>
/// </summary>
public class StyleLayoutChildGrid : Base.StyleLayoutChildBase
{
    /// <summary/>
    public int Row = 0;
    /// <summary/>
    public int Column = 0;

    /// <summary/>
    public int RowSpan = 1;
    /// <summary/>
    public int ColumnSpan = 1;

    ///////////////////// CONSTRUCTORS ////////////////////
    ///////////////////// CONSTRUCTORS ////////////////////


    /// <summary/>
    public StyleLayoutChildGrid() { }

    /// <summary/>
    public StyleLayoutChildGrid(
        int row = 0,
        int column = 0,
        int rowSpan = 1,
        int columnSpan = 1)
            => SetAllFields(row, column, rowSpan, columnSpan);



    //////////////////// SETS /////////////////////////
    //////////////////// SETS /////////////////////////


    /// <summary>
    ///     Requires to convey all possible class values <see cref="StyleLayoutChildGrid"/> and sets them up.
    /// </summary>
    public void Set(
        int row = 0,
        int column = 0,
        int rowSpan = 1,
        int columnSpan =  1)
            => SetAllFields(row, column, rowSpan, columnSpan);

    /// <summary/>
    protected void SetAllFields(
        int row,
        int column,
        int rowSpan,
        int columnSpan)
    {
        Row = row;
        Column = column;
        RowSpan = rowSpan;
        ColumnSpan = columnSpan;
    }



    //////////////////// COPY /////////////////////////
    //////////////////// COPY /////////////////////////

    /// <inheritdoc/>
    protected override Base.StyleBase Fabricate() => new StyleLayoutChildGrid();

    /// <inheritdoc/>
    protected override void CopyBase(Base.StyleBase style)
    {
        if (style is StyleLayoutChildGrid style2)
            Copy(style2);
    }

    /// <summary>
    ///     Copy the values of the transmitted <see cref="StyleLayoutChildGrid"/> 
    /// </summary>
    public void Copy(StyleLayoutChildGrid style)
        => SetAllFields(
            style.Row,
            style.Column,
            style.RowSpan,
            style.ColumnSpan);

    /// <summary>
    ///     Creates and returns a copy of the current <see cref="StyleLayoutChildGrid"/>
    /// </summary>
    public StyleLayoutChildGrid GetCopy()
        => GetCopyBase<StyleLayoutChildGrid>();



    //////////////////// EQUALS /////////////////////////
    //////////////////// EQUALS /////////////////////////


    /// <inheritdoc/>
    protected override bool EqualsFields(Base.StyleBase other)
    {
        if (other is StyleLayoutChildGrid style)
            return
                Row == style.Row &&
                Column == style.Column &&
                RowSpan == style.RowSpan && 
                ColumnSpan == style.ColumnSpan;

        return false;
    }
}