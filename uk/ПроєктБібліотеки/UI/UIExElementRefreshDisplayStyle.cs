using Microsoft.Xna.Framework.Graphics;

namespace UIeXtension;

public partial class UIExElement
{
    /// <summary>
    ///     Відтворює стилі елемента при необхідності.
    /// </summary>
    protected virtual void RecalculateDisplayStyle()
    { 
        
    }

    /// <summary>
    ///     Відтворює стилі елемента.
    /// </summary>
    protected virtual void RefreshDisplayStyle()
    {

    }

    /// <summary>
    ///     Прозорість поточного елемента з стилями користувачів <see cref="StyleDisplay"/>. .
    /// </summary>
    protected virtual void DrawSelfStyleDisplay(SpriteBatch spriteBatch)
    {
    }
}