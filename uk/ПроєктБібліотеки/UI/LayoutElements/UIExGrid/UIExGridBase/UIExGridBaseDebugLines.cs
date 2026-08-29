using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.GameContent;
using Terraria.UI;

namespace UIeXtension;

public abstract partial class UIExGridBase : UIExLayout
{
    /// <summary>
    ///     прапорець, що вказує на те, чи повинні відображатися допоміжні лінії.
    /// </summary>
    public bool ShowGridLines = false;

    /// <summary>
    ///     Розмір допоміжних ліній у пікселів.
    /// </summary>
    public float GridLinesThickness = 1f;

    /// <summary>
    ///     Колір допоміжних ліній.
    /// </summary>
    public Color GridLinesColor = Color.DarkGray;

    /// <summary>
    ///     Містить розмір і положення допоміжних ліній.
    /// </summary>
    protected List<LayoutDebugRectangle> GridLinesDebugRectangles = new();



    /// <summary>
    ///     Встановіть параметри для відображення допоміжних ліній для цілого дерева.
    /// </summary>
    public virtual void SetGridLinesInfoForTree(bool showGridLines, float gridLinesThickness, Color gridLinesColor)
    {
        var root = Utils.UtilsFinder.GetRootParent(this);
        SetGridLinesInfoForBranch(root, showGridLines, gridLinesThickness, gridLinesColor);
    }

    /// <summary>
    ///     Настроювання параметрів для відображення допоміжних ліній для всіх нащадків.
    /// </summary>
    public virtual void SetGridLinesInfoForBranch(bool showGridLines, float gridLinesThickness, Color gridLinesColor)
        => SetGridLinesInfoForBranch(this, showGridLines, gridLinesThickness, gridLinesColor);

    /// <summary>
    ///     Встановити параметри відображення допоміжних ліній для всіх нащадків передається елемента.
    /// </summary>
    protected virtual void SetGridLinesInfoForBranch(
        UIElement element,
        bool showGridLines,
        float gridLinesThickness,
        Color gridLinesColor)
    {
        if (element is UIExGridBase grid)
        { 
            grid.ShowGridLines = showGridLines;
            grid.GridLinesThickness = gridLinesThickness;
            grid.GridLinesColor = gridLinesColor;
        }

        foreach (var child in element.Children)
            SetGridLinesInfoForBranch(child, showGridLines, gridLinesThickness, gridLinesColor);
    }



    /// <summary>
    ///     Рекультивація розташування допоміжних ліній облаштування елемента та допоміжної сітки Grid. .
    /// </summary>
    protected override void RefreshLayoutDebugLines()
    {
        base.RefreshLayoutDebugLines();

        if (ShowLayoutLines)
        {
            foreach (var cell in _context.cells)
                LayoutDebugRectangles.Add(cell.GetLayoutDebugRectangle());

            foreach (var cell in _context.elementsCellDimension)
                LayoutDebugRectangles.Add(cell.GetLayoutDebugRectangle());
        }

        GridLinesDebugRectangles.Clear();
        if (ShowGridLines)
        {
            var firstCell = _context.cells[0, 0];

            float rowOffset = firstCell.Top + _context.rowDefinitions[0].Pixels;
            float columnOffset = firstCell.Left + _context.columnDefinitions[0].Pixels;
            float rowsDefsTotalSize = GetDefinitionsTotalPixelsSize(_context.rowDefinitions, row: true);
            float columnsDefsTotalSize = GetDefinitionsTotalPixelsSize(_context.columnDefinitions, row: false);

            float mutableRowOffset = rowOffset;
            float mutableColumnOffset = columnOffset;

            for (int i = 1; i < _context.rowsCount; i++)
                AddGridLineRectangle(
                    row:            true,
                    mutableOffset:  ref mutableRowOffset,
                    rowOffset:      rowOffset,
                    columnOffset:   firstCell.Left,
                    defSize:        _context.rowDefinitions[i].Pixels,
                    defsTotalSize:  columnsDefsTotalSize,
                    addSpaceLine:   _context.rowsSpace > 0f);

            for (int i = 1; i < _context.columnsCount; i++)
                AddGridLineRectangle(
                    row:            false,
                    mutableOffset:  ref mutableColumnOffset,
                    rowOffset:      firstCell.Top,
                    columnOffset:   columnOffset,
                    defSize:        _context.columnDefinitions[i].Pixels,
                    defsTotalSize:  rowsDefsTotalSize,
                    addSpaceLine:   _context.columnsSpace > 0f);
        }
    }

    /// <summary>
    ///     Розрахунок розмірів і положення допоміжної лінії. <see cref="GridLinesDebugRectangles"/>
    /// </summary>
    protected void AddGridLineRectangle(
        bool row, 
        ref float mutableOffset, 
        float rowOffset, 
        float columnOffset, 
        float defSize, 
        float defsTotalSize, 
        bool addSpaceLine)
    {
        LayoutDebugRectangle rect = new();

        if(row)
            rect.X = columnOffset;
        else
            rect.Y += rowOffset;

        if (row)
        {
            rect.Y += mutableOffset - GridLinesThickness / 2f;
            rect.Height = GridLinesThickness;
            rect.Width = defsTotalSize;
        }
        else
        {
            rect.X += mutableOffset - GridLinesThickness / 2f;
            rect.Height = defsTotalSize;
            rect.Width = GridLinesThickness;
        }

        GridLinesDebugRectangles.Add(rect);

        mutableOffset += defSize;

        if (addSpaceLine)
        {
            if (row)
            {
                rect.Y += _context.rowsSpace;
                mutableOffset += _context.rowsSpace;
            }
            else
            {
                rect.X += _context.columnsSpace;
                mutableOffset += _context.columnsSpace;
            }

            GridLinesDebugRectangles.Add(rect);
        }
    }

    /// <inheritdoc/>
    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        base.DrawSelf(spriteBatch);

        if (ShowGridLines)
            foreach(var rect in GridLinesDebugRectangles)
                spriteBatch.Draw(
                    TextureAssets.MagicPixel.Value,
                    rect.GetXnaRentangle(),
                    GridLinesColor);
    }
}