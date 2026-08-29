namespace UIeXtension.Styles;

/// <summary>
///     Стилі макета для <see cref="UIExGridBase"/>
/// </summary>
public class StyleLayoutChildUniformGrid : Base.StyleLayoutChildBase
{
    /// <summary/>
    public int RowSpan = 1;
    /// <summary/>
    public int ColumnSpan = 1;

    ///////////////////// CONSTRUCTORS ///////////////
    ///////////////////// CONSTRUCTORS ///////////////


    /// <summary/>
    public StyleLayoutChildUniformGrid() { }

    /// <summary/>
    public StyleLayoutChildUniformGrid(
        int rowSpan = 1,
        int columnSpan = 1)
            => SetAllFields(rowSpan, columnSpan);



    //////////////////// SETS /////////////////
    //////////////////// SETS /////////////////


    /// <summary>
    ///     Вимагає передати всі можливі значення класу <see cref="StyleLayoutChildUniformGrid"/> і встановлюємо їх.
    /// </summary>
    public void Set(
        int rowSpan = 1,
        int columnSpan = 1)
            => SetAllFields(rowSpan, columnSpan);

    /// <summary/>
    protected void SetAllFields(
        int rowSpan,
        int columnSpan)
    {
        RowSpan = rowSpan;
        ColumnSpan = columnSpan;
    }



    //////////////////// COPY /////////////////
    //////////////////// COPY /////////////////

    /// <inheritdoc/>
    protected override Base.StyleBase Fabricate() => new StyleLayoutChildUniformGrid();

    /// <inheritdoc/>
    protected override void CopyBase(Base.StyleBase style)
    {
        if (style is StyleLayoutChildUniformGrid style2)
            Copy(style2);
    }

    /// <summary>
    ///     Скопіювати значення передається <see cref="StyleLayoutChildUniformGrid"/> 
    /// </summary>
    public void Copy(StyleLayoutChildUniformGrid style)
        => SetAllFields(
            style.RowSpan,
            style.ColumnSpan);

    /// <summary>
    ///     Створює і повертає копію поточного <see cref="StyleLayoutChildUniformGrid"/>
    /// </summary>
    public StyleLayoutChildUniformGrid GetCopy()
        => GetCopyBase<StyleLayoutChildUniformGrid>();



    //////////////////// EQUALS /////////////////
    //////////////////// EQUALS /////////////////


    /// <inheritdoc/>
    protected override bool EqualsFields(Base.StyleBase other)
    {
        if (other is StyleLayoutChildUniformGrid style)
            return
                RowSpan == style.RowSpan &&
                ColumnSpan == style.ColumnSpan;

        return false;
    }
}