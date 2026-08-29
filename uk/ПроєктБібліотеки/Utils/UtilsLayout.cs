using Microsoft.Xna.Framework;
using Terraria.UI;

namespace UIeXtension.Utils;

/// <summary>
///     Допоміжний клас для елементів планування.
/// </summary>
public static class UtilsLayout
{
    /// <summary>
    ///     Закрити алгоритм роботи до методу GetDimensionsBasedOnParentDimensions клас <see cref="UIElement"/>
    /// </summary>
    /// <remarks>
    ///     <para>Обов'язкові для наступних методів класу <see cref="UtilsLayout"/>Довідник</para>
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
    ///     Обрізаний метод <see cref="UIElement.Recalculate"/>крім того, 
    ///     не думаю, _innerDimensions і в кінці методу не називається <see cref="UIElement.RecalculateChildren"/>
    /// </summary>
    /// <remarks>
    ///     В. В. <see cref="UIElement"/> методологія <see cref="UIElement.GetDimensions"/> повернення _Налаштування, 
    ///     методична <see cref="UIElement.Recalculate"/>. .
    ///     Без цього способу необхідно перерахувати (при оформленні елементів) відстібаються елементи за допомогою методу. <see cref="UIElement.Recalculate()"/>які, 
    ///     У свою чергу метод є <see cref="UIElement.RecalculateChildren"/>, і так по всій гілці.
    ///     До тих пір, поки один елемент не закінчився, він не має сенсу, і він значно знижує продуктивність.
    /// </remarks>
    /// <param name="element">Елементи, для яких розраховується розміри</param>
    /// <param name="style"></param>
    /// <param name="parentInnerDimensions"></param>
    /// <returns>Розміри елемента <paramref name="element"/></returns>
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
    ///     Обрізаний метод <see cref="UIElement.Recalculate"/> в кінці методу не називається <see cref="UIElement.RecalculateChildren"/>
    /// </summary>
    /// <remarks>
    ///     В. В. <see cref="UIElement"/> методологія <see cref="UIElement.GetInnerDimensions"/> Повернутися _innerDimensions. . 
    ///     методична <see cref="UIElement.Recalculate"/>. .
    ///     Без цього способу необхідно перерахувати (при оформленні елементів) відстібаються елементи за допомогою методу. <see cref="UIElement.Recalculate()"/>які, 
    ///     У свою чергу метод є <see cref="UIElement.RecalculateChildren"/>, і так по всій гілці.
    ///     До тих пір, поки один елемент не закінчився, він не має сенсу, і він значно знижує продуктивність.
    /// </remarks>
    /// <param name="element">Елемент для якого обчислюється InnerDimensions</param>
    /// <param name="style"></param>
    /// <param name="parentInnerDimensions"></param>
    /// <returns>InnerDimensions склад <paramref name="element"/></returns>
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
