using Microsoft.Xna.Framework.Graphics;

namespace UIeXtension;

public partial class UIExElement
{
    /// <summary>
    ///     Recalculates the styles of the element if necessary.
    /// </summary>
    protected virtual void RecalculateDisplayStyle()
    { 
        
    }

    /// <summary>
    ///     Recalculates the styles of the element.
    /// </summary>
    protected virtual void RefreshDisplayStyle()
    {

    }

    /// <summary>
    ///     Depicts the current element with user styles <see cref="StyleDisplay"/>.
    /// </summary>
    protected virtual void DrawSelfStyleDisplay(SpriteBatch spriteBatch)
    {
    }
}