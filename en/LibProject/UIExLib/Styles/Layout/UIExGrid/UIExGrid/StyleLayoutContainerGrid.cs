using System.Collections.Generic;
using Terraria.UI;

namespace UIeXtension.Styles;

/// <summary>
///     Container layout style <see cref="UIExGrid"/>
/// </summary>
public class StyleLayoutContainerGrid : Base.StyleLayoutContainerBase
{
    /// <summary/>
    protected List<UIExGridLength> RowsDefinitions = new();
    /// <summary/>
    protected List<UIExGridLength> ColumnsDefinitions = new();


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
    public int RowsCount => RowsDefinitions.Count;

    /// <summary/>
    public int ColumnsCount => ColumnsDefinitions.Count;




    ///////////////////// CONSTRUCTORS ////////////////////
    ///////////////////// CONSTRUCTORS ////////////////////

    /// <summary/>
    public StyleLayoutContainerGrid() { }

    /// <summary/>
    public StyleLayoutContainerGrid(
        List<UIExGridLength> rowsDefinitions = null,
        List<UIExGridLength> columnsDefinitions = null,
        Enums.UIExAlignment rowsAlignment = Enums.UIExAlignment.Start,
        Enums.UIExAlignment columnsAlignment = Enums.UIExAlignment.Start,
        StyleDimension rowsSpace = default(StyleDimension),
        StyleDimension columnSpace = default(StyleDimension))
            => SetAllFields(
                rowsDefinitions,
                columnsDefinitions,
                rowsAlignment,
                columnsAlignment,
                rowsSpace,
                columnSpace);




    //////////////////// DEFINITIONS MANAGEMENT [ADD] //////////////////
    //////////////////// DEFINITIONS MANAGEMENT [ADD] //////////////////

    /// <summary/>
    public void AddRowDefinition(UIExGridLength rowDefinition)
        => AddDefinitions(RowsDefinitions, rowDefinition, repeat: 1);

    /// <summary/>
    public void AddRowDefinition(UIExGridLength rowDefinition, int repeat)
        => AddDefinitions(RowsDefinitions, rowDefinition, repeat);

    /// <summary/>
    public void AddRowDefinitions(params UIExGridLength[] rowsDefinitions)
        => AddDefinitions(RowsDefinitions, rowsDefinitions);

    /// <summary/>
    public void AddRowDefinitions(List<UIExGridLength> rowsDefinitions)
        => AddDefinitions(RowsDefinitions, rowsDefinitions);


    /// <summary/>
    public void AddColumnDefinition(UIExGridLength columnDefinition)
        => AddDefinitions(ColumnsDefinitions, columnDefinition, repeat: 1);

    /// <summary/>
    public void AddColumnDefinition(UIExGridLength columnDefinition, int repeat)
        => AddDefinitions(ColumnsDefinitions, columnDefinition, repeat);

    /// <summary/>
    public void AddColumnDefinitions(params UIExGridLength[] columnsDefinitions)
        => AddDefinitions(ColumnsDefinitions, columnsDefinitions);

    /// <summary/>
    public void AddColumnDefinitions(List<UIExGridLength> columnsDefinitions)
        => AddDefinitions(ColumnsDefinitions, columnsDefinitions);



    /// <summary/>
    protected void AddDefinitions(List<UIExGridLength> listDefinitions, UIExGridLength definitions, int repeat)
    {
        for (int i = 0; i < repeat; i++)
            listDefinitions.Add(definitions);
    }

    /// <summary/>
    protected void AddDefinitions(List<UIExGridLength> definitions, params UIExGridLength[] definitionsArray)
        => definitions.AddRange(definitionsArray);

    /// <summary/>
    protected void AddDefinitions(List<UIExGridLength> definitions, List<UIExGridLength> definitionsArray)
        => definitions.AddRange(definitionsArray);




    //////////////////// DEFINITIONS MANAGEMENT [GET] //////////////////
    //////////////////// DEFINITIONS MANAGEMENT [GET] //////////////////

    /// <summary/>
    public UIExGridLength GetRowDefinition(int row)
        => GetDefinition(RowsDefinitions, row);

    /// <summary/>
    public UIExGridLength[] GetRowDefinitions(int rowStart, int rowEnd)
        => GetDefinitions(RowsDefinitions, rowStart, rowEnd);


    /// <summary/>
    public UIExGridLength GetColumnDefinition(int column)
        => GetDefinition(ColumnsDefinitions, column);

    /// <summary/>
    public UIExGridLength[] GetColumnDefinitions(int columnStart, int columnEnd)
        => GetDefinitions(ColumnsDefinitions, columnStart, columnEnd);


    /// <summary/>
    protected UIExGridLength GetDefinition(List<UIExGridLength> definitions, int index)
        => definitions[index];

    /// <summary/>
    protected UIExGridLength[] GetDefinitions(List<UIExGridLength> definitions, int start, int end)
    {
        int diff = end - start + 1;
        UIExGridLength[] result = new UIExGridLength[diff];
        for(int i = 0; i < diff; i++)
            result[i] = definitions[start++];

        return result;
    }




    //////////////////// DEFINITIONS MANAGEMENT [REMOVE] //////////////////
    //////////////////// DEFINITIONS MANAGEMENT [REMOVE] //////////////////

    /// <summary/>
    public void RemoveRowDefinitionAt(int row)
        => RemoveDefinitionAt(RowsDefinitions, row);

    /// <summary/>
    public void RemoveRowDefinitionsRange(int row, int count)
        => RemoveDefinitionsRange(RowsDefinitions, row, count);


    /// <summary/>
    public void RemoveColumnDefinitionsAt(int column)
        => RemoveDefinitionAt(ColumnsDefinitions, column);

    /// <summary/>
    public void RemoveColumnDefinitionsRange(int column, int count)
        => RemoveDefinitionsRange(ColumnsDefinitions, column, count);


    /// <summary/>
    protected void RemoveDefinitionAt(List<UIExGridLength> definitions, int index)
        => definitions.RemoveAt(index);

    /// <summary/>
    protected void RemoveDefinitionsRange(List<UIExGridLength> definitions, int index, int count)
        => definitions.RemoveRange(index, count);




    //////////////////// DEFINITIONS MANAGEMENT [REPLACE] //////////////////
    //////////////////// DEFINITIONS MANAGEMENT [REPLACE] //////////////////

    /// <summary/>
    public void ReplaceRowDefinition(int row, UIExGridLength definitions)
        => ReplaceDefinition(RowsDefinitions, row, definitions);

    /// <summary/>
    public void ReplaceColumnDefinition(int column, UIExGridLength definitions)
        => ReplaceDefinition(ColumnsDefinitions, column, definitions);

    /// <summary/>
    protected void ReplaceDefinition(List<UIExGridLength> listDefinitions, int index, UIExGridLength definitions)
        => listDefinitions[index] = definitions;




    //////////////////// DEFINITIONS MANAGEMENT [INSERT] //////////////////
    //////////////////// DEFINITIONS MANAGEMENT [INSERT] //////////////////

    /// <summary/>
    public void InsertRowDefinition(int row, UIExGridLength definitions)
        => InsertDefinitions(RowsDefinitions, row, definitions);

    /// <summary/>
    public void InsertRowDefinitions(int row, UIExGridLength[] definitions)
        => InsertDefinitions(RowsDefinitions, row, definitions);

    /// <summary/>
    public void InsertRowDefinitions(int row, List<UIExGridLength> definitions)
        => InsertDefinitions(RowsDefinitions, row, definitions);


    /// <summary/>
    public void InsertColumnDefinition(int column, UIExGridLength definitions)
        => InsertDefinitions(ColumnsDefinitions, column, definitions);

    /// <summary/>
    public void InsertColumnDefinitions(int column, UIExGridLength[] definitions)
        => InsertDefinitions(ColumnsDefinitions, column, definitions);

    /// <summary/>
    public void InsertColumnDefinitions(int column, List<UIExGridLength> definitions)
        => InsertDefinitions(ColumnsDefinitions, column, definitions);


    /// <summary/>
    protected void InsertDefinitions(List<UIExGridLength> listDefinitions, int index, params UIExGridLength[] definitionsArray)
    { 
        foreach(var definition in definitionsArray)
            listDefinitions.Insert(index++, definition);
    }

    /// <summary/>
    protected void InsertDefinitions(List<UIExGridLength> listDefinitions, int index, List<UIExGridLength> definitionsArray)
    {
        foreach (var definition in definitionsArray)
            listDefinitions.Insert(index++, definition);
    }


    //////////////////// SETS /////////////////////////
    //////////////////// SETS /////////////////////////

    /// <summary>
    ///     Requires to convey all possible class values <see cref="StyleLayoutContainerGrid"/> and sets them up.
    /// </summary>
    public void Set(
        List<UIExGridLength> rowsDefinitions = null,
        List<UIExGridLength> columnsDefinitions = null,
        Enums.UIExAlignment rowsAlignment = Enums.UIExAlignment.Start,
        Enums.UIExAlignment columnsAlignment = Enums.UIExAlignment.Start,
        StyleDimension rowsSpace = default(StyleDimension),
        StyleDimension columnSpace = default(StyleDimension))
            => SetAllFields(
                rowsDefinitions, 
                columnsDefinitions, 
                rowsAlignment,
                columnsAlignment,
                rowsSpace,
                columnSpace);

    /// <summary/>
    protected void SetAllFields(
        List<UIExGridLength> rowsDefinitions,
        List<UIExGridLength> columnsDefinitions,
        Enums.UIExAlignment rowsAlignment,
        Enums.UIExAlignment columnsAlignment,
        StyleDimension rowsSpace,
        StyleDimension columnSpace)
    {
        if (rowsDefinitions is not null)
            RowsDefinitions = new(rowsDefinitions);

        if (columnsDefinitions is not null)
            ColumnsDefinitions = new(columnsDefinitions);

        RowsAlignment = rowsAlignment;
        ColumnsAlignment = columnsAlignment;

        RowsSpace = rowsSpace;
        ColumnsSpace = columnSpace;
    }




    //////////////////// COPY /////////////////////////
    //////////////////// COPY /////////////////////////

    /// <inheritdoc/>
    protected override Base.StyleBase Fabricate() => new StyleLayoutContainerGrid();

    /// <inheritdoc/>
    protected override void CopyBase(Base.StyleBase style)
    {
        if (style is StyleLayoutContainerGrid style2)
            Copy(style2);
    }

    /// <inheritdoc/>
    public void Copy(StyleLayoutContainerGrid style) 
    {
        RowsDefinitions = new(style.RowsDefinitions);
        ColumnsDefinitions = new(style.ColumnsDefinitions);

        RowsAlignment = style.RowsAlignment;
        ColumnsAlignment = style.ColumnsAlignment;

        RowsSpace = style.RowsSpace;
        ColumnsSpace = style.ColumnsSpace;
    }

    /// <inheritdoc/>
    public StyleLayoutContainerGrid GetCopy()
        => GetCopyBase<StyleLayoutContainerGrid>();




    //////////////////// EQUALS /////////////////////////
    //////////////////// EQUALS /////////////////////////

    /// <inheritdoc/>
    protected override bool EqualsFields(Base.StyleBase other)
    {
        if (other is StyleLayoutContainerGrid style)
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

            for (int i = 0; i < RowsCount; i++)
                if (!RowsDefinitions[i].EqualsFields(style.RowsDefinitions[i]))
                    return false;

            for (int i = 0; i < ColumnsCount; i++)
                if (!ColumnsDefinitions[i].EqualsFields(style.ColumnsDefinitions[i]))
                    return false;

            return true;
        }

        return false;
    }
}