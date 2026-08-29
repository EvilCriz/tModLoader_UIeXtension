using System;
using System.Collections.Generic;
using Terraria.UI;
using UIeXtension.Styles;

namespace UIeXtension;

public abstract partial class UIExLayout : UIExElement
{
    /// <summary>
    ///     Оборачивает элемент <see cref="UIElement"/> в <typeparamref name="TUIElement"/>
    /// </summary>
    /// <remarks>
    ///     Используйте обертывание, если хотите, чтобы ваш элемент мог использовать:
    ///     <see cref="UIElement.MarginTop"/>; <see cref="UIElement.MarginLeft"/>;
    ///     <see cref="UIElement.MarginRight"/>; <see cref="UIElement.MarginBottom"/>;
    ///     <see cref="UIElement.HAlign"/>; <see cref="UIElement.VAlign"/>
    ///     <para>Использование данных полей без обертки может привести к неожиданным результатам при компоновке.</para>
    /// </remarks>
    /// <typeparam name="TUIElement">Тип <see cref="UIElement"/>, в который должен быть обернут <paramref name="element"/></typeparam>
    /// <param name="element">Элемент пользовательского интерфейса, который должен быть обернут</param>
    /// <param name="stretch">
    ///     Указывает, что <paramref name="element"/> должен иметь Width.Set(0f, 1f) и Height.Set(0f, 1f)
    /// </param>
    /// <param name="center">
    ///     Указывает, что <paramref name="element"/> должен иметь HAlign = 0.5f и VAlign = 0.5f
    /// </param>
    /// <returns>Созданный элемент-обертку, внутрь которого вложен <paramref name="element"/></returns>
    public static TUIElement Wrap<TUIElement>(UIElement element, bool stretch = false, bool center = false)
        where TUIElement : UIElement, new()
            => Wrap(element, new TUIElement(), stretch, center);

    /// <summary>
    ///     Оборачивает элемент <see cref="UIElement"/> в <typeparamref name="TUIElement"/>
    /// </summary>
    /// <remarks>
    ///     Используйте обертывание, если хотите, чтобы ваш элемент мог использовать:
    ///     <see cref="UIElement.MarginTop"/>; <see cref="UIElement.MarginLeft"/>;
    ///     <see cref="UIElement.MarginRight"/>; <see cref="UIElement.MarginBottom"/>;
    ///     <see cref="UIElement.HAlign"/>; <see cref="UIElement.VAlign"/>
    ///     <para>Использование данных полей без обертки может привести к неожиданным результатам при компоновке.</para>
    /// </remarks>
    /// <typeparam name="TUIElement">Тип <see cref="UIElement"/>, в который должен быть обернут <paramref name="element"/></typeparam>
    /// <param name="wrapElement">Уже созданный и переданный элемент-обертка, в который будет обернут <paramref name="element"/></param>
    /// <param name="element">Элемент пользовательского интерфейса, который должен быть обернут</param>
    /// <param name="stretch">
    ///     Указывает, что <paramref name="element"/> должен иметь Width.Set(0f, 1f) и Height.Set(0f, 1f)
    /// </param>
    /// <param name="center">
    ///     Указывает, что <paramref name="element"/> должен иметь HAlign = 0.5f и VAlign = 0.5f
    /// </param>
    /// <returns><paramref name="wrapElement"/>, внутрь которого вложен <paramref name="element"/></returns>
    public static TUIElement Wrap<TUIElement>(UIElement element, TUIElement wrapElement, bool stretch = false, bool center = false)
        where TUIElement : UIElement
    {
        if (stretch)
        {
            element.Width.Set(0f, 1f);
            element.Height.Set(0f, 1f);
        }

        if (center)
        {
            element.HAlign = 0.5f;
            element.VAlign = 0.5f;
        }

        wrapElement.Append(element);
        return wrapElement;
    }




    /// <summary/>
    protected int Max(int value1, int value2) => value1 > value2 ? value1 : value2;
    /// <summary/>
    protected int Min(int value1, int value2) => value1 < value2 ? value1 : value2;




    /// <summary>
    ///     Превращает список <see cref="UIElement"/> из переданного списка индексов <see cref="_elementsContext"/>.
    ///     Вне жизненно цикла компоновки приведет к исключению.
    /// </summary>
    protected virtual List<UIElement> GetElementsByIndexses(List<int> elementsIndexes)
    {
        List<UIElement> elements = new(elementsIndexes.Count);
        foreach (var idx in elementsIndexes)
            elements.Add(_elementsContext[idx]);
        return elements;
    }

    /// <summary>
    ///     Превращает список из <see cref="_elementsContext"/> в список индексов <see cref="UIElement"/>.
    ///     Вне жизненно цикла компоновки приведет к исключению.
    /// </summary>
    protected virtual List<int> GetElementsIndexsesByElementsContext()
    {
        List<int> elementsIndexes = new(_elementsContext.Count);
        for (int i = 0; i < _elementsContext.Count; i++)
            elementsIndexes.Add(i);
        return elementsIndexes;
    }

    /// <summary>
    ///     Проверяет, отличаются ли <see cref="UIElement.GetOuterDimensions"/> 
    ///     и <see cref="CalculatedStyle"/> <paramref name="outerDimensions"/>
    /// </summary>
    protected static bool IsOuterDimensionsNotEquals(UIElement element, CalculatedStyle outerDimensions)
        => IsDimensionsNotEquals(element.GetOuterDimensions(), outerDimensions);

    /// <summary>
    ///     Проверяет, отличаются ли <see cref="UIElement.GetInnerDimensions"/> 
    ///     и <see cref="CalculatedStyle"/> <paramref name="innerDimensions"/>
    /// </summary>
    protected static bool IsInnerDimensionsNotEquals(UIElement element, CalculatedStyle innerDimensions)
        => IsDimensionsNotEquals(element.GetInnerDimensions(), innerDimensions);

    /// <summary>
    ///     Проверяет, отличаются ли <see cref="UIElement.GetDimensions"/> и <see cref="CalculatedStyle"/> <paramref name="dimensions"/>
    /// </summary>
    protected static bool IsDimensionsNotEquals(UIElement element, CalculatedStyle dimensions)
        => IsDimensionsNotEquals(element.GetDimensions(), dimensions);

    /// <summary>
    ///     Проверяет, отличаются ли <see cref="CalculatedStyle"/>
    /// </summary>
    private static bool IsDimensionsNotEquals(CalculatedStyle dimensions1, CalculatedStyle dimensions2)
    {
        return
            dimensions1.X != dimensions2.X ||
            dimensions1.Y != dimensions2.Y ||
            dimensions1.Width != dimensions2.Width ||
            dimensions1.Height != dimensions2.Height;
    }

    /// <summary>
    ///     Возвращает <paramref name="dimensions"/>, заменяя X/Y на позицию относительно контейнера
    /// </summary>
    protected CalculatedStyle GetRelativeDimensions(CalculatedStyle dimensions)
    {
        dimensions.X -= _innerDimensionsContext.X;
        dimensions.Y -= _innerDimensionsContext.Y;
        return dimensions;
    }


    /// <summary>
    ///     Определяет <see cref="Enums.UIExAlignment"/> по главной оси.
    ///     <para>
    ///         Если вложенный элемент сам себя позиционирует - возвращает его позиционирование: <see cref="Styles.StyleLayoutChild.JustifySelf"/>. 
    ///         В ином случае возвращает позиционирование контейнера компоновки: <see cref="Styles.StyleLayoutContainer.JustifyContent"/>
    ///     </para>
    /// </summary>
    protected virtual Enums.UIExAlignment GetJustify(int index)
    {
        Styles.StyleLayoutChild style = _styleElementsContexts[index];
        if (style.JustifySelf == Enums.UIExAlignment.Auto)
            return StyleLayout.JustifyContent;
        return style.JustifySelf;
    }

    /// <summary>
    ///     Определяет <see cref="Enums.UIExAlignment"/> по поперечной оси.
    ///     <para>
    ///         Если вложенный элемент сам себя позиционирует - возвращает его позиционирование: <see cref="Styles.StyleLayoutChild.AlignSelf"/>. 
    ///         В ином случае возвращает позиционирование контейнера компоновки: <see cref="Styles.StyleLayoutContainer.AlignItems"/>
    ///     </para>
    /// </summary>
    protected virtual Enums.UIExAlignment GetAlign(int index)
    {
        Styles.StyleLayoutChild style = _styleElementsContexts[index];
        if (style.AlignSelf == Enums.UIExAlignment.Auto)
            return StyleLayout.AlignItems;
        return style.AlignSelf;
    }

    /// <summary>
    ///     Возвращает максимальный указанный размер переданных элементов.
    ///     Использование вне жизненного цикла компоновки приведет к исключению.
    /// </summary>
    protected virtual float GetMaxSize(List<int> indexses, bool width, Func<int, bool> conditionExclusion = null)
    {
        float max = float.MinValue;

        foreach (var index in indexses)
        {
            if (conditionExclusion is not null && conditionExclusion(index))
                continue;

            UIElement element = _elementsContext[index];
            var outerDimensions = 
                Utils.UtilsLayout.GetForcedCalculatedOuterDimensions(
                    element, 
                    _styleElementsContexts[index],
                    _innerDimensionsContext);

            float size = width ?
                outerDimensions.Width :
                outerDimensions.Height;

            max = Microsoft.Xna.Framework.MathHelper.Max(max, size);
        }

        return max;
    }

    /// <summary>
    ///     Считается начальный и конечный <see cref="UIExThickness"/> в зависимости от переданной оси.
    /// </summary>
    protected virtual void GetOrientationThickness(
        UIExThickness thickness,
        bool vertical,
        out float start,
        out float end,
        float size)
    {
        if (vertical)
        {
            start = thickness.Top.GetValue(size);
            end = thickness.Bottom.GetValue(size);
        }
        else
        {
            start = thickness.Left.GetValue(size);
            end = thickness.Right.GetValue(size);
        }
    }

    /// <summary>
    ///     Считается начальный и конечный <see cref="UIExThickness"/> в зависимости от переданной оси.
    /// </summary>
    protected virtual void GetOrientationThickness(
        UIExThickness thickness,
        bool vertical,
        out float start,
        out float end,
        CalculatedStyle dimension)
    {
        float size = vertical
            ? dimension.Height
            : dimension.Width;

        GetOrientationThickness(
            thickness:  thickness,
            vertical:   vertical,
            start:      out start,
            end:        out end,
            size:       size);
    }

    /// <summary>
    ///     Считается начальный и конечный Margin в зависимости от переданной оси.
    ///     Использование вне жизненного цикла компоновки приведет к исключению.
    /// </summary>
    protected virtual void GetChildMargin(
        Styles.StyleLayoutChild style,
        bool vertical,
        out float marginStart,
        out float marginEnd,
        CalculatedStyle dimension)
            => GetOrientationThickness(
                style.Margin, 
                vertical, 
                out marginStart, 
                out marginEnd, 
                dimension);

    /// <summary>
    ///     Возвращает <see cref="Styles.StyleLayoutContainer.JustifyContent"/> или <see cref="Styles.StyleLayoutContainer.AlignItems"/>, или
    ///     <see cref="Styles.StyleLayoutChild.JustifySelf"/>, или <see cref="Styles.StyleLayoutChild.AlignSelf"/> для элемента, в зависимости от
    ///     указанной оси и правил возврата значения методов <see cref="GetJustify(int)"/> / <see cref="GetAlign(int)"/>
    /// </summary>
    /// <param name="index"></param>
    /// <param name="justify"></param>
    protected Enums.UIExAlignment GetAlignment(int index, bool justify)
        => justify ? GetJustify(index) : GetAlign(index);

    /// <summary>
    ///     Проверяет, установлено ли для элемента свойство выравнивания по переданной оси на <see cref="Enums.UIExAlignment.Stretch"/>
    /// </summary>
    /// <param name="index"></param>
    /// <param name="justify"></param>
    protected bool IsStretchAlignment(int index, bool justify)
        => GetAlignment(index, justify) == Enums.UIExAlignment.Stretch;

    /// <summary>
    ///     Возвращает занятое элементами пространство по заданной оси
    /// </summary>
    protected virtual float GetSizeElements(List<int> elementsIndexes, bool vertical)
    {
        float size = 0f;
        for (int i = 0; i < elementsIndexes.Count; i++)
            size += GetElementSize(elementsIndexes[i], vertical);
        return size;
    }

    /// <summary>
    ///     Возвращает занятое пространство по заданной оси элементами с свойством выравнивания НЕ установленным на <see cref="Enums.UIExAlignment.Stretch"/> 
    /// </summary>
    /// <param name="vertical">Ориентация контейнера</param>
    /// <param name="justify">Ось, для которой считается занятое пространство</param>
    /// <param name="elementsIndexes">Индекс элементов</param>
    protected virtual float GetSizeNonStretchElements(List<int> elementsIndexes, bool vertical, bool justify)
    {
        float size = 0f;
        for (int i = 0; i < elementsIndexes.Count; i++)
            size += GetSizeNonStretchElement(elementsIndexes[i], vertical, justify);
        return size;
    }

    /// <summary>
    ///     Возвращает занятое пространство элементом, если у него НЕ установлено свойство выравнивания по переданной оси <see cref="Enums.UIExAlignment.Stretch"/> 
    /// </summary>
    /// <param name="index"></param>
    /// <param name="vertical">Ориентация контейнера</param>
    /// <param name="justify">Ось, для которой считается занятое пространство</param>
    protected virtual float GetSizeNonStretchElement(int index, bool vertical, bool justify)
    {
        if (IsStretchAlignment(index, justify))
            return 0f;

        return GetElementSize(index, vertical);
    }

    /// <summary>
    ///     Возвращает количество элементов со свойством выравнивания по переданной оси <see cref="Enums.UIExAlignment.Stretch"/> 
    /// </summary>
    protected int GetStretchElementsCount(List<int> elementsIndexes, bool justify)
    {
        int stretchCount = 0;
        for (int i = 0; i < elementsIndexes.Count; i++)
            if (IsStretchAlignment(elementsIndexes[i], justify))
                stretchCount++;

        return stretchCount;
    }

    /// <summary>
    ///     Считает и возвращает размер одного <see cref="Enums.UIExAlignment.Stretch"/> 
    /// </summary>
    /// <param name="availableSpace">Свободное пространство для растягивание</param>
    /// <param name="stretchCount">Количество элементов со свойством <see cref="Enums.UIExAlignment.Stretch"/></param>
    protected virtual float GetStrethSize(float availableSpace, int stretchCount)
        => stretchCount == 0f ? 0f : availableSpace / stretchCount;

    /// <summary>
    ///     Возвращает внутренний размер контейнера по указанной оси.
    /// </summary>
    protected float GetInnerDimensionsSize(bool vertical)
        => GetInnerDimensionsSize(vertical, _innerDimensionsContext);

    /// <summary>
    ///     Возвращает внутренний размер контейнера по указанной оси.
    /// </summary>
    protected float GetInnerDimensionsSize(bool vertical, CalculatedStyle parentInnerDimensions)
        => vertical ? parentInnerDimensions.Height : parentInnerDimensions.Width;

    /// <summary>
    ///     Возвращает размер элемента по указанной оси.
    /// </summary>
    protected float GetElementSize(int index, bool vertical)
        => vertical ? _elementsOuterDimensionsContext[index].Height : _elementsOuterDimensionsContext[index].Width;
}