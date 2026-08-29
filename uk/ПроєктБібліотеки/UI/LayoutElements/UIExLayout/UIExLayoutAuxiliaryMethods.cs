using System;
using System.Collections.Generic;
using Terraria.UI;
using UIeXtension.Styles;

namespace UIeXtension;

public abstract partial class UIExLayout : UIExElement
{
    /// <summary>
    ///     Рол над елементом <see cref="UIElement"/> Увійти <typeparamref name="TUIElement"/>
    /// </summary>
    /// <remarks>
    ///     Використовуйте обгортання, якщо ви хочете використовувати ваш товар, щоб мати можливість використовувати:
    ///     <see cref="UIElement.MarginTop"/>; <see cref="UIElement.MarginLeft"/>;
    ///     <see cref="UIElement.MarginRight"/>; <see cref="UIElement.MarginBottom"/>;
    ///     <see cref="UIElement.HAlign"/>; <see cref="UIElement.VAlign"/>
    ///     <para>Використання цих полів без обгортання може призвести до несподіваних результатів у макеті.</para>
    /// </remarks>
    /// <typeparam name="TUIElement">Типи <see cref="UIElement"/>які слід загорнути <paramref name="element"/></typeparam>
    /// <param name="element">Інтерфейс користувача, який потрібно загорнути</param>
    /// <param name="stretch">
    ///     Індикатори, які <paramref name="element"/> обов'язково мати Width.Set0 товар(ов) - 0.00 € Height.Set(0ф, 1ф)
    /// </param>
    /// <param name="center">
    ///     Індикатори, які <paramref name="element"/> обов'язково мати HAlign = 0.5ф і VAlign 0 товар(ов)
    /// </param>
    /// <returns>Створено елемент-крапка, всередині якого лежить <paramref name="element"/></returns>
    public static TUIElement Wrap<TUIElement>(UIElement element, bool stretch = false, bool center = false)
        where TUIElement : UIElement, new()
            => Wrap(element, new TUIElement(), stretch, center);

    /// <summary>
    ///     Рол над елементом <see cref="UIElement"/> Увійти <typeparamref name="TUIElement"/>
    /// </summary>
    /// <remarks>
    ///     Використовуйте обгортання, якщо ви хочете використовувати ваш товар, щоб мати можливість використовувати:
    ///     <see cref="UIElement.MarginTop"/>; <see cref="UIElement.MarginLeft"/>;
    ///     <see cref="UIElement.MarginRight"/>; <see cref="UIElement.MarginBottom"/>;
    ///     <see cref="UIElement.HAlign"/>; <see cref="UIElement.VAlign"/>
    ///     <para>Використання цих полів без обгортання може призвести до несподіваних результатів у макеті.</para>
    /// </remarks>
    /// <typeparam name="TUIElement">Типи <see cref="UIElement"/>які слід загорнути <paramref name="element"/></typeparam>
    /// <param name="wrapElement">Уже створене і передане елемент-відгукувач, який буде загорнути <paramref name="element"/></param>
    /// <param name="element">Інтерфейс користувача, який потрібно загорнути</param>
    /// <param name="stretch">
    ///     Індикатори, які <paramref name="element"/> обов'язково мати Width.Set0 товар(ов) - 0.00 € Height.Set(0ф, 1ф)
    /// </param>
    /// <param name="center">
    ///     Індикатори, які <paramref name="element"/> обов'язково мати HAlign = 0.5ф і VAlign 0 товар(ов)
    /// </param>
    /// <returns><paramref name="wrapElement"/>, всередині якого непристойна <paramref name="element"/></returns>
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
    ///     Увімкніть список <see cref="UIElement"/> з передається перелік індексів <see cref="_elementsContext"/>. .
    ///     Зовні життєвого циклу макет призведе до відчуження.
    /// </summary>
    protected virtual List<UIElement> GetElementsByIndexses(List<int> elementsIndexes)
    {
        List<UIElement> elements = new(elementsIndexes.Count);
        foreach (var idx in elementsIndexes)
            elements.Add(_elementsContext[idx]);
        return elements;
    }

    /// <summary>
    ///     Поверніть список з <see cref="_elementsContext"/> Індекс <see cref="UIElement"/>. .
    ///     Зовні життєвого циклу макет призведе до відчуження.
    /// </summary>
    protected virtual List<int> GetElementsIndexsesByElementsContext()
    {
        List<int> elementsIndexes = new(_elementsContext.Count);
        for (int i = 0; i < _elementsContext.Count; i++)
            elementsIndexes.Add(i);
        return elementsIndexes;
    }

    /// <summary>
    ///     Перевірити, щоб побачити, чи вони різні. <see cref="UIElement.GetOuterDimensions"/> 
    ///     і <see cref="CalculatedStyle"/> <paramref name="outerDimensions"/>
    /// </summary>
    protected static bool IsOuterDimensionsNotEquals(UIElement element, CalculatedStyle outerDimensions)
        => IsDimensionsNotEquals(element.GetOuterDimensions(), outerDimensions);

    /// <summary>
    ///     Перевірити, щоб побачити, чи вони різні. <see cref="UIElement.GetInnerDimensions"/> 
    ///     і <see cref="CalculatedStyle"/> <paramref name="innerDimensions"/>
    /// </summary>
    protected static bool IsInnerDimensionsNotEquals(UIElement element, CalculatedStyle innerDimensions)
        => IsDimensionsNotEquals(element.GetInnerDimensions(), innerDimensions);

    /// <summary>
    ///     Перевірити, щоб побачити, чи вони різні. <see cref="UIElement.GetDimensions"/> і <see cref="CalculatedStyle"/> <paramref name="dimensions"/>
    /// </summary>
    protected static bool IsDimensionsNotEquals(UIElement element, CalculatedStyle dimensions)
        => IsDimensionsNotEquals(element.GetDimensions(), dimensions);

    /// <summary>
    ///     Перевірити, щоб побачити, чи вони різні. <see cref="CalculatedStyle"/>
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
    ///     Повернення. <paramref name="dimensions"/>Замініть X / Y з положенням відносно контейнера.
    /// </summary>
    protected CalculatedStyle GetRelativeDimensions(CalculatedStyle dimensions)
    {
        dimensions.X -= _innerDimensionsContext.X;
        dimensions.Y -= _innerDimensionsContext.Y;
        return dimensions;
    }


    /// <summary>
    ///     Декорини <see cref="Enums.UIExAlignment"/> на головній віссі.
    ///     <para>
    ///         Якщо ж непристойні позиції елемента, його позиціонування повертається: <see cref="Styles.StyleLayoutChild.JustifySelf"/>. . 
    ///         В іншому випадку повертає позиціонування контейнерного макета: <see cref="Styles.StyleLayoutContainer.JustifyContent"/>
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
    ///     Декорини <see cref="Enums.UIExAlignment"/> поперечної осі.
    ///     <para>
    ///         Якщо ж непристойні позиції елемента, його позиціонування повертається: <see cref="Styles.StyleLayoutChild.AlignSelf"/>. . 
    ///         В іншому випадку повертає позиціонування контейнерного макета: <see cref="Styles.StyleLayoutContainer.AlignItems"/>
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
    ///     Повертаємо максимальну заданий розмір переданих елементів.
    ///     Використання макета off-life-cycle призведе до виключення.
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
    ///     Він вважається початковим і кінцевим. <see cref="UIExThickness"/> в залежності від передається вісь.
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
    ///     Він вважається початковим і кінцевим. <see cref="UIExThickness"/> в залежності від передається вісь.
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
    ///     Початкова і остаточна Маргін розглядається в залежності від передається осі.
    ///     Використання макета off-life-cycle призведе до виключення.
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
    ///     Повернення. <see cref="Styles.StyleLayoutContainer.JustifyContent"/> або <see cref="Styles.StyleLayoutContainer.AlignItems"/>або
    ///     <see cref="Styles.StyleLayoutChild.JustifySelf"/>або <see cref="Styles.StyleLayoutChild.AlignSelf"/> для елемента, в залежності від
    ///     Вісь і правила повернення значень методів <see cref="GetJustify(int)"/> та <see cref="GetAlign(int)"/>
    /// </summary>
    /// <param name="index"></param>
    /// <param name="justify"></param>
    protected Enums.UIExAlignment GetAlignment(int index, bool justify)
        => justify ? GetJustify(index) : GetAlign(index);

    /// <summary>
    ///     Перевіряє, чи встановлюється вирівнювальна властивість на передньому віссі елементу <see cref="Enums.UIExAlignment.Stretch"/>
    /// </summary>
    /// <param name="index"></param>
    /// <param name="justify"></param>
    protected bool IsStretchAlignment(int index, bool justify)
        => GetAlignment(index, justify) == Enums.UIExAlignment.Stretch;

    /// <summary>
    ///     Повертає зайнятий простір на вісь
    /// </summary>
    protected virtual float GetSizeElements(List<int> elementsIndexes, bool vertical)
    {
        float size = 0f;
        for (int i = 0; i < elementsIndexes.Count; i++)
            size += GetElementSize(elementsIndexes[i], vertical);
        return size;
    }

    /// <summary>
    ///     Повертає зайнятий простір по заданій віссю елементів з майном вирівнювання не встановлена на <see cref="Enums.UIExAlignment.Stretch"/> 
    /// </summary>
    /// <param name="vertical">Орієнтація контейнера</param>
    /// <param name="justify">Вісь, для якого вважається зайнятий простір</param>
    /// <param name="elementsIndexes">Індекс елементів</param>
    protected virtual float GetSizeNonStretchElements(List<int> elementsIndexes, bool vertical, bool justify)
    {
        float size = 0f;
        for (int i = 0; i < elementsIndexes.Count; i++)
            size += GetSizeNonStretchElement(elementsIndexes[i], vertical, justify);
        return size;
    }

    /// <summary>
    ///     Повертає зайнятий простір елементом, якщо він не має вирівнювання майна на передньому вісь. <see cref="Enums.UIExAlignment.Stretch"/> 
    /// </summary>
    /// <param name="index"></param>
    /// <param name="vertical">Орієнтація контейнера</param>
    /// <param name="justify">Вісь, для якого вважається зайнятий простір</param>
    protected virtual float GetSizeNonStretchElement(int index, bool vertical, bool justify)
    {
        if (IsStretchAlignment(index, justify))
            return 0f;

        return GetElementSize(index, vertical);
    }

    /// <summary>
    ///     Повертає кількість елементів з властивістю вирівняти по переведеній осі <see cref="Enums.UIExAlignment.Stretch"/> 
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
    ///     Розрахунки та повернення розміру одного <see cref="Enums.UIExAlignment.Stretch"/> 
    /// </summary>
    /// <param name="availableSpace">Безкоштовний простір для розтягування</param>
    /// <param name="stretchCount">Кількість елементів з майном <see cref="Enums.UIExAlignment.Stretch"/></param>
    protected virtual float GetStrethSize(float availableSpace, int stretchCount)
        => stretchCount == 0f ? 0f : availableSpace / stretchCount;

    /// <summary>
    ///     Повертаємо внутрішній розмір контейнера на вказану вісь.
    /// </summary>
    protected float GetInnerDimensionsSize(bool vertical)
        => GetInnerDimensionsSize(vertical, _innerDimensionsContext);

    /// <summary>
    ///     Повертаємо внутрішній розмір контейнера на вказану вісь.
    /// </summary>
    protected float GetInnerDimensionsSize(bool vertical, CalculatedStyle parentInnerDimensions)
        => vertical ? parentInnerDimensions.Height : parentInnerDimensions.Width;

    /// <summary>
    ///     Повертає розмір елемента на вказану вісь.
    /// </summary>
    protected float GetElementSize(int index, bool vertical)
        => vertical ? _elementsOuterDimensionsContext[index].Height : _elementsOuterDimensionsContext[index].Width;
}
