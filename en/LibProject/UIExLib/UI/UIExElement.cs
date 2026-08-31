using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace UIeXtension;

/// <summary>
///     Basic class for all UIElements of this library
/// </summary>
/// <remarks>
///     Use it. <see cref="Styles.StyleVisualElement"/> as the main method of styling the element
/// </remarks>
public partial class UIExElement : UIElement
{
    /// <summary>
    ///     A field for a user type of data that will be stored for a separate <see cref="UIExElement"/>
    /// </summary>
    public object Data;

    /// <summary>
    ///     Style table with visual parameters that everyone uses UI- an element of this library.
    /// </summary>
    public Styles.StyleVisualElement StyleDisplay;

    /// <summary>
    /// When using this constructor overload, the element receives a default style sheet.
    /// </summary>
    public UIExElement() : this(new Styles.StyleVisualElement()) { }


    /// <summary>
    ///     Constructor overload accepting the main style sheet UI- Elements.
    /// </summary>
    /// <param name="style">
    ///     The main style sheet of all UI-elements
    /// </param>
    public UIExElement(Styles.StyleVisualElement style)
    {
        StyleDisplay = style;
    }


    /// <summary>
    ///     Recalculates the styles of the element, then calls <see cref="UIElement.Recalculate"/>
    /// </summary>
    public override void Recalculate()
    {
        UpdateLastTMLStyle();

        base.Recalculate();
    }

    /// <summary>
    ///     Updates the meaning <see cref="_lastTMLStyle"/>.
    ///     Calling. <see cref="RefreshTMLDisplayStyle"/>if <see cref="_lastTMLStyle"/> === true
    ///     Calling. <see cref="RecalculateDisplayStyle"/> if <see cref="_lastTMLStyle"/> === false
    /// </summary>
    protected virtual void UpdateLastTMLStyle()
    {
        _lastTMLStyle = StyleDisplay.tModLoaderStyle;

        if (_lastTMLStyle)
            RecalculateTMLDisplayStyle();
        else
            RecalculateDisplayStyle();
    }

    /// <inheritdoc/>
    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        if (_lastTMLStyle)
            DrawSelfTML(spriteBatch);
        else
            DrawSelfStyleDisplay(spriteBatch);
    }
}