using Microsoft.Xna.Framework;
using Terraria.UI;

namespace UIeXtension.Utils;

/// <summary>
///     Вспомогательный класс для элементов компоновки.
/// </summary>
public static class UtilsLayout
{
    /// <summary>
    ///     Близкий по алгоритму работы к методу GetDimensionsBasedOnParentDimensions класса <see cref="UIElement"/>
    /// </summary>
    /// <remarks>
    ///     <para>Необходим для следующих методов класс <see cref="UtilsLayout"/>:</para>
    ///     <para><see cref="GetForcedCalculatedOuterDimensions"/>;</para>
    ///     <para><see cref="GetForcedCalculatedDimensions"/>;</para>
    ///     <para><see cref="GetForcedCalculatedInnerDimensions"/></para>
    /// </remarks>
    public static CalculatedStyle GetForcedCalculatedDimensions(
        UIElement element, 
        Styles.StyleLayoutChild style, 
        CalculatedStyle parentInnerDimensions)
    {
        float
            styleMarginLeft = 0f, 
            styleMarginRight = 0f, 
            styleMarginTop = 0f, 
            styleMarginBottom = 0f;

        CalculatedStyle result = default(CalculatedStyle);

        if (style is not null)
        {
            styleMarginLeft = style.Margin.Left.GetValue(parentInnerDimensions.Width);
            styleMarginRight = style.Margin.Right.GetValue(parentInnerDimensions.Width);
            styleMarginTop = style.Margin.Top.GetValue(parentInnerDimensions.Height);
            styleMarginBottom = style.Margin.Bottom.GetValue(parentInnerDimensions.Height);

            float minWidth = element.MinWidth.GetValue(parentInnerDimensions.Width);
            float maxWidth = element.MaxWidth.GetValue(parentInnerDimensions.Width);
            float minHeight = element.MinHeight.GetValue(parentInnerDimensions.Height);
            float maxHeight = element.MaxHeight.GetValue(parentInnerDimensions.Height);

            result.Width = MathHelper.Clamp(style.Width.GetValue(parentInnerDimensions.Width), minWidth, maxWidth);
            result.Height = MathHelper.Clamp(style.Height.GetValue(parentInnerDimensions.Height), minHeight, maxHeight);
        }
        
        result.X = parentInnerDimensions.X + element.Left.GetValue(parentInnerDimensions.Width);
        result.Y = parentInnerDimensions.Y + element.Top.GetValue(parentInnerDimensions.Height);

        return result;
    }

    /// <summary>
    ///     Обрезанный метод <see cref="UIElement.Recalculate"/>, за исключением того, 
    ///     что он НЕ считает _innerDimensions и в конце метода НЕ вызывается <see cref="UIElement.RecalculateChildren"/>
    /// </summary>
    /// <remarks>
    ///     В <see cref="UIElement"/> метод <see cref="UIElement.GetDimensions"/> возвращает _dimensions, 
    ///     который рассчитывается в методе <see cref="UIElement.Recalculate"/>.
    ///     Без данного метода пришлось бы пересчитывать (во время компоновки элементов) вложенные элементы с помощью метода <see cref="UIElement.Recalculate()"/>, который, 
    ///     в свою очередь, вызывает метод <see cref="UIElement.RecalculateChildren"/>, и так далее по всей ветке.
    ///     Пока один элемент не закончил компоновку - в этом нет смысла, и это значительно понижает производительность.
    /// </remarks>
    /// <param name="element">Элемент, для которого рассчитывается Dimensions</param>
    /// <param name="style"></param>
    /// <param name="parentInnerDimensions"></param>
    /// <returns>Dimensions элемента <paramref name="element"/></returns>
    public static CalculatedStyle GetForcedCalculatedOuterDimensions(UIElement element, Styles.StyleLayoutChild style, CalculatedStyle parentInnerDimensions)
    {
        CalculatedStyle calculatedStyle = GetForcedCalculatedDimensions(element, style, parentInnerDimensions);

        if (style is not null)
        {
            float styleMarginLeft = style.Margin.Left.GetValue(calculatedStyle.Width);
            float styleMarginRight = style.Margin.Right.GetValue(calculatedStyle.Width);
            float styleMarginTop = style.Margin.Top.GetValue(calculatedStyle.Height);
            float styleMarginBottom = style.Margin.Bottom.GetValue(calculatedStyle.Height);

            calculatedStyle.X -= styleMarginLeft;
            calculatedStyle.Y -= styleMarginTop;
            calculatedStyle.Width += styleMarginLeft + styleMarginRight;
            calculatedStyle.Height += styleMarginTop + styleMarginBottom;
        }

        return calculatedStyle;
    }

    /// <summary>
    ///     Обрезанный метод <see cref="UIElement.Recalculate"/> в конце метода НЕ вызывается <see cref="UIElement.RecalculateChildren"/>
    /// </summary>
    /// <remarks>
    ///     В <see cref="UIElement"/> метод <see cref="UIElement.GetInnerDimensions"/> возвращает _innerDimensions, 
    ///     который рассчитывается в методе <see cref="UIElement.Recalculate"/>.
    ///     Без данного метода пришлось бы пересчитывать (во время компоновки элементов) вложенные элементы с помощью метода <see cref="UIElement.Recalculate()"/>, который, 
    ///     в свою очередь, вызывает метод <see cref="UIElement.RecalculateChildren"/>, и так далее по всей ветке.
    ///     Пока один элемент не закончил компоновку - в этом нет смысла, и это значительно понижает производительность.
    /// </remarks>
    /// <param name="element">Элемент, для которого рассчитывается InnerDimensions</param>
    /// <param name="style"></param>
    /// <param name="parentInnerDimensions"></param>
    /// <returns>InnerDimensions элемента <paramref name="element"/></returns>
    public static CalculatedStyle GetForcedCalculatedInnerDimensions(UIElement element, Styles.StyleLayoutChild style, CalculatedStyle parentInnerDimensions)
    {
        CalculatedStyle calculatedStyle = GetForcedCalculatedDimensions(element, style, parentInnerDimensions);
        calculatedStyle.X += element.PaddingLeft;
        calculatedStyle.Y += element.PaddingTop;
        calculatedStyle.Width -= element.PaddingLeft + element.PaddingRight;
        calculatedStyle.Height -= element.PaddingTop + element.PaddingBottom;
        return calculatedStyle;
    }
}
