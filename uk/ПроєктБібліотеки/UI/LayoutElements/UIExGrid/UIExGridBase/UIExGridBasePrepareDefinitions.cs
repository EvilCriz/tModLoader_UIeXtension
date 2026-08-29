using System.Collections.Generic;
using System.Security.Principal;

namespace UIeXtension;

/// <summary>
///     Базовий клас для всіх Grid-подібні контейнерні макети.
/// </summary>
public abstract partial class UIExGridBase
{
    /// <summary>
    ///     Перетворення значень <see cref="Styles.UIExGridLength"/> в пікселів.
    /// </summary>
    protected virtual void PrepareGridDefinitions()
    {
        for (int i = 0; i < _context.rowsCount; i++)
        {
            Styles.UIExGridLength definition = _context.styleContainerGrid.GetRowDefinition(i);

            if (definition.Type != Enums.UIExGridLengthType.Pixels)
                definition.Pixels = 0f;

            definition = GetConvertPrecentDefinition(definition, _innerDimensionsContext.Height);
            definition = GetConvertAutoDefinitionWithoutSpan(
                row:                true, 
                definitionIndex:    i, 
                definition:         definition);

            _context.rowDefinitions.Add(definition);
        }

        CalculateAutoDefinitionWithSpan(row: true);
        CalculateFrDefinitions(row: true);



        for (int i = 0; i < _context.columnsCount; i++)
        {
            Styles.UIExGridLength definition = _context.styleContainerGrid.GetColumnDefinition(i);

            if (definition.Type != Enums.UIExGridLengthType.Pixels)
                definition.Pixels = 0f;

            definition = GetConvertPrecentDefinition(definition, _innerDimensionsContext.Width);
            definition = GetConvertAutoDefinitionWithoutSpan(
                row:                false, 
                definitionIndex:    i, 
                definition:         definition);

            _context.columnDefinitions.Add(definition);
        }

        CalculateAutoDefinitionWithSpan(row: false);
        CalculateFrDefinitions(row: false);
    }



    /// <summary>
    ///     Перетворення значень <see cref="Styles.UIExGridLength.Type"/> ================================================================================================================================================================================================================================================================ <see cref="Enums.UIExGridLengthType.Fraction"/> в пікселів.
    /// </summary>
    protected virtual void CalculateFrDefinitions(bool row)
    {
        var definitions = row
            ? _context.rowDefinitions
            : _context.columnDefinitions;

        float totalFr = 0f;
        float totalPixels = 0f;

        for (int i = 0; i < definitions.Count; i++)
        {
            if (IsFrDefinition(i, row))
                totalFr += definitions[i].Fraction;
            else
                totalPixels += definitions[i].Pixels;
        }

        if (totalFr == 0f)
            return;

        float parentSize = row
            ? _innerDimensionsContext.Height
            : _innerDimensionsContext.Width;

        float space = row
            ? _context.rowsSpace
            : _context.columnsSpace;

        float spaceSize = space * (definitions.Count - 1);

        float freeSpace = parentSize - totalPixels - spaceSize;

        if (freeSpace <= 0f)
            return;

        float fr = freeSpace / totalFr;

        for (int i = 0; i < definitions.Count; i++)
        {
            if (!IsFrDefinition(i, row))
                continue;

            var definition = definitions[i];

            definition.Pixels = fr * definition.Fraction;
            definition.Fraction = 0f;

            definitions[i] = definition;
        }
    }



    /// <summary>
    ///     Перетворення значень <see cref="Styles.UIExGridLength.Type"/> ================================================================================================================================================================================================================================================================ <see cref="Enums.UIExGridLengthType.Auto"/> в пікселів.
    /// </summary>
    protected virtual void CalculateAutoDefinitionWithSpan(bool row)
    {
        for (int i = 0; i < _elementsContext.Count; i++)
        {
            var elementDefinitions = GetElementDefinitionsIndexes(i, row);
            int autoCount = GetAutoDefinitionsCount(elementDefinitions, row);

            if (autoCount == 0)
                continue;

            float defTotalSize = GetDefinitionsTotalPixelsSize(elementDefinitions, row);

            float elementSize = row
                ? _elementsOuterDimensionsContext[i].Height
                : _elementsOuterDimensionsContext[i].Width;

            float diff = elementSize - defTotalSize;

            if (diff <= 0f)
                continue;

            float addDefSize = diff / autoCount;

            for (int j = 0; j < elementDefinitions.Count; j++)
            {
                int defIdx = elementDefinitions[j];
                if (!IsAutoDefinition(defIdx, row))
                    continue;

                Styles.UIExGridLength definition = row
                    ? _context.rowDefinitions[defIdx]
                    : _context.columnDefinitions[defIdx];

                definition.Pixels += addDefSize;

                if (row)
                    _context.rowDefinitions[defIdx] = definition;
                else
                    _context.columnDefinitions[defIdx] = definition;
            }
        }
    }

    /// <summary>
    ///     Повертає кількість елементів до рядка/холодильника з значенням <see cref="Enums.UIExGridLengthType.Auto"/>
    /// </summary>
    protected virtual int GetAutoDefinitionsCount(List<int> definitionsIndexes, bool row)
    {
        int count = 0;
        foreach(var defIdx in definitionsIndexes)
            if(IsAutoDefinition(defIdx, row))
                count++;
        return count;
    }

    /// <summary>
    ///     Повертає загальний розмір у пікселях (в тому числі відступів) рядків / стовпців.
    /// </summary>
    protected virtual float GetDefinitionsTotalPixelsSize(
        List<Styles.UIExGridLength> definitions, 
        bool row,
        System.Func<int, bool> excludeIteration = null)
    {
        List<int> definitionsIndexes = new(definitions.Count);
        for (int i = 0; i < definitions.Count; i++)
            definitionsIndexes.Add(i);
        return GetDefinitionsTotalPixelsSize(definitionsIndexes, row, excludeIteration);
    }

    /// <summary>
    ///     Повертає загальний розмір у пікселях (в тому числі відступів) рядків / стовпців.
    /// </summary>
    protected virtual float GetDefinitionsTotalPixelsSize(
        List<int> definitionsIndexes, 
        bool row,
        System.Func<int, bool> excludeIteration = null)
    {
        float defTotalSize = 0f;
        int count = 0;

        foreach (var defIdx in definitionsIndexes)
        {
            if (excludeIteration is not null && excludeIteration(defIdx))
                continue;

            count++;

            defTotalSize += row
                ? _context.rowDefinitions[defIdx].Pixels
                : _context.columnDefinitions[defIdx].Pixels;
        }

        float space = row
            ? _context.rowsSpace
            : _context.columnsSpace;

        if(count > 0)
            defTotalSize += space * (count - 1);

        return defTotalSize;
    }



    /// <summary>
    ///     Повертає загальний розмір у пікселях (в тому числі відступів) рядків / стовпців.
    /// </summary>
    protected virtual Styles.UIExGridLength GetConvertPrecentDefinition(Styles.UIExGridLength definition, float innerDimensionsSize)
    {
        if (definition.Type != Enums.UIExGridLengthType.Precent)
            return definition;

        definition.Pixels = definition.Precent * innerDimensionsSize;

        return definition;
    }



    /// <summary>
    ///     Перетворення значень <see cref="Styles.UIExGridLength.Type"/> ================================================================================================================================================================================================================================================================ <see cref="Enums.UIExGridLengthType.Precent"/> в пікселів.
    /// </summary>
    protected virtual Styles.UIExGridLength GetConvertAutoDefinitionWithoutSpan(
        bool row,
        int definitionIndex,
        Styles.UIExGridLength definition)
    {
        if (definition.Type != Enums.UIExGridLengthType.Auto)
            return definition;

        List<int> definitionElementsIndexes = GetDefinitionElementsIndexes(
            definitionIndex: definitionIndex,
            row: row);

        if (definitionElementsIndexes.Count == 0)
            return definition;

        System.Func<int, bool> conditionExclusion = (index) =>
        {
            var style = _context.stylesChildsGrid[index];
            return row
                ? style.RowSpan != 1
                : style.ColumnSpan != 1;
        };

        definition.Pixels = GetMaxSize(definitionElementsIndexes, !row, conditionExclusion);
        if(definition.Pixels == float.MinValue)
            definition.Pixels = 0f;

        return definition;
    }



    /// <summary>
    ///     Повертає індекси всіх рядків / стовпців, зайнятих передачею елемента.
    /// </summary>
    protected virtual List<int> GetElementDefinitionsIndexes(int index, bool row)
    {
        var styleChildGrid = _context.stylesChildsGrid[index];

        int def = row
            ? styleChildGrid.Row
            : styleChildGrid.Column;

        int defSpan = row
            ? styleChildGrid.RowSpan
            : styleChildGrid.ColumnSpan;

        List<int> result = new(defSpan);

        for (int i = def; i < def + defSpan; i++)
            result.Add(i);

        return result;
    }

    /// <summary>
    ///     Повертає всі елементи, які займають переведену клітинку.
    /// </summary>
    protected virtual List<int> GetDefinitionElementsIndexes(int definitionIndex, bool row)
    {
        List<int> result = new List<int>();
        for (int i = 0; i < _context.stylesChildsGrid.Count; i++)
        {
            var style = _context.stylesChildsGrid[i];
            if (IsElementInDefinition(i, definitionIndex, row))
                result.Add(i);
        }

        return result;
    }



    /// <summary>
    ///     Перевіряє, чи передається елемент, що займає передню клітинку.
    /// </summary>
    protected virtual bool IsElementInDefinition(
        int elementIndex,
        int definitionIndex,
        bool row)
    {
        var style = _context.stylesChildsGrid[elementIndex];

        int start = row ? style.Row : style.Column;
        int span = row ? style.RowSpan : style.ColumnSpan;

        return definitionIndex >= start &&
               definitionIndex < start + span;
    }

    /// <summary>
    ///     Перевіряє, чи має значення передається рядок/холодильник <see cref="Styles.UIExGridLength.Type"/> ================================================================================================================================================================================================================================================================ <see cref="Enums.UIExGridLengthType.Auto"/>
    /// </summary>
    protected virtual bool IsAutoDefinition(int defIndex, bool row)
        => IsTypeDefinitions(defIndex, row, Enums.UIExGridLengthType.Auto);

    /// <summary>
    ///     Перевіряє, чи має значення передається рядок/холодильник <see cref="Styles.UIExGridLength.Type"/> ================================================================================================================================================================================================================================================================ <see cref="Enums.UIExGridLengthType.Fraction"/>
    /// </summary>
    protected virtual bool IsFrDefinition(int defIndex, bool row)
        => IsTypeDefinitions(defIndex, row, Enums.UIExGridLengthType.Fraction);

    /// <summary>
    ///     Перевіряє, чи передається рядок/колюмн має однакове значення <see cref="Styles.UIExGridLength.Type"/> перезавантаження <paramref name="type"/>
    /// </summary>
    protected virtual bool IsTypeDefinitions(int defIndex, bool row, Enums.UIExGridLengthType type)
    {
        Enums.UIExGridLengthType type2 = row
                ? _context.rowDefinitions[defIndex].Type
                : _context.columnDefinitions[defIndex].Type;

        return type2 == type;
    }
}