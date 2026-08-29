using Microsoft.Xna.Framework.Graphics;

namespace UIeXtension;

public partial class UIExElement
{
    /// <summary>
    ///     Пересчитывает стили элемента при необходимости.
    /// </summary>
    protected virtual void RecalculateDisplayStyle()
    { 
        
    }

    /// <summary>
    ///     Пересчитывает стили элемента.
    /// </summary>
    protected virtual void RefreshDisplayStyle()
    {

    }

    /// <summary>
    ///     Отрисовывает текущий элемент с пользовательскими стилями <see cref="StyleDisplay"/>.
    /// </summary>
    protected virtual void DrawSelfStyleDisplay(SpriteBatch spriteBatch)
    {
    }
}