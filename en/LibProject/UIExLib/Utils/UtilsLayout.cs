using Microsoft.Xna.Framework;
using Terraria.UI;

namespace UIeXtension.Utils;

/// <summary>
///     Auxiliary class for layout elements.
/// </summary>
public static class UtilsLayout
{
    /// <summary>
    ///     Close to the algorithm of work to the method GetDimensionsBasedOnParentDimensions class <see cref="UIElement"/>
    /// </summary>
    /// <remarks>
    ///     <para>Required for the following methods class <see cref="UtilsLayout"/>:</para>
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
    ///     The circumcised method <see cref="UIElement.Recalculate"/>except that, 
    ///     he doesn't think _innerDimensions and at the end of the method is not called <see cref="UIElement.RecalculateChildren"/>
    /// </summary>
    /// <remarks>
    ///     V. <see cref="UIElement"/> method <see cref="UIElement.GetDimensions"/> returns _dimensions, 
    ///     methodical <see cref="UIElement.Recalculate"/>.
    ///     Without this method, it would be necessary to recalculate (during the arrangement of elements) the nested elements using the method. <see cref="UIElement.Recalculate()"/>which, 
    ///     In turn, the method is <see cref="UIElement.RecalculateChildren"/>, and so on all over the branch.
    ///     As long as one element is not finished, it makes no sense, and it significantly reduces performance.
    /// </remarks>
    /// <param name="element">The element for which Dimensions are calculated</param>
    /// <param name="style"></param>
    /// <param name="parentInnerDimensions"></param>
    /// <returns>Dimensions of the element <paramref name="element"/></returns>
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
    ///     The circumcised method <see cref="UIElement.Recalculate"/> at the end of the method is not called <see cref="UIElement.RecalculateChildren"/>
    /// </summary>
    /// <remarks>
    ///     V. <see cref="UIElement"/> method <see cref="UIElement.GetInnerDimensions"/> return _innerDimensions. 
    ///     methodical <see cref="UIElement.Recalculate"/>.
    ///     Without this method, it would be necessary to recalculate (during the arrangement of elements) the nested elements using the method. <see cref="UIElement.Recalculate()"/>which, 
    ///     In turn, the method is <see cref="UIElement.RecalculateChildren"/>, and so on all over the branch.
    ///     As long as one element is not finished, it makes no sense, and it significantly reduces performance.
    /// </remarks>
    /// <param name="element">The element for which it is calculated InnerDimensions</param>
    /// <param name="style"></param>
    /// <param name="parentInnerDimensions"></param>
    /// <returns>InnerDimensions component <paramref name="element"/></returns>
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
