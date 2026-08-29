using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;

namespace UIeXtension;

public partial class UIExElement
{
    private bool _lastTMLStyle = false;

    /// <summary>
    ///     Style saved after the last recalculation tModLoader.
    /// </summary>
    protected Styles.TModLoader.StyleTmlUIPanel _lastTMLStyleUIPanel;



    /// <summary>
    ///     Recalculating. TML Styles of the element, if necessary.
    /// </summary>
    protected virtual void RecalculateTMLDisplayStyle()
    {
        var style = StyleDisplay.TmlUIPanel();

        if (_lastTMLStyleUIPanel is null ||
            !style.EqualsStylesFields(_lastTMLStyleUIPanel))
        {
            _lastTMLStyleUIPanel = style.GetCopy();

            RefreshTMLDisplayStyle();
        }
    }

    /// <summary>
    ///     Recalculating. TML style
    /// </summary>
    protected virtual void RefreshTMLDisplayStyle()
    {
        if (_lastTMLStyle)
        {
            LoadTexturesTML();

            if(StyleDisplay.PaddingAutoControl)
                SetPadding(_lastTMLStyleUIPanel.CornerSize);
        }
    }

    /// <summary>
    ///     Downloads textures. TML style of the element.
    /// </summary>
    protected virtual void LoadTexturesTML()
    {
        if (_lastTMLStyleUIPanel.BorderTexture == null)
            _lastTMLStyleUIPanel.BorderTexture = Main.Assets.Request<Texture2D>("Images/UI/PanelBorder");

        if (_lastTMLStyleUIPanel.BackgroundTexture == null)
            _lastTMLStyleUIPanel.BackgroundTexture = Main.Assets.Request<Texture2D>("Images/UI/PanelBackground");
    }

    private void DrawPanelTML(SpriteBatch spriteBatch, Texture2D texture, Color color)
    {
        int cornerSize = _lastTMLStyleUIPanel.CornerSize;
        int barSize = _lastTMLStyleUIPanel.BarSize;

        CalculatedStyle dimensions = GetDimensions();

        Point point = new Point((int)dimensions.X, (int)dimensions.Y);
        Point point2 = new Point(point.X + (int)dimensions.Width - cornerSize, point.Y + (int)dimensions.Height - cornerSize);

        int width = point2.X - point.X - cornerSize;
        int height = point2.Y - point.Y - cornerSize;
        spriteBatch.Draw(texture, new Rectangle(point.X, point.Y, cornerSize, cornerSize), new Rectangle(0, 0, cornerSize, cornerSize), color);
        spriteBatch.Draw(texture, new Rectangle(point2.X, point.Y, cornerSize, cornerSize), new Rectangle(cornerSize + barSize, 0, cornerSize, cornerSize), color);
        spriteBatch.Draw(texture, new Rectangle(point.X, point2.Y, cornerSize, cornerSize), new Rectangle(0, cornerSize + barSize, cornerSize, cornerSize), color);
        spriteBatch.Draw(texture, new Rectangle(point2.X, point2.Y, cornerSize, cornerSize), new Rectangle(cornerSize + barSize, cornerSize + barSize, cornerSize, cornerSize), color);
        spriteBatch.Draw(texture, new Rectangle(point.X + cornerSize, point.Y, width, cornerSize), new Rectangle(cornerSize, 0, barSize, cornerSize), color);
        spriteBatch.Draw(texture, new Rectangle(point.X + cornerSize, point2.Y, width, cornerSize), new Rectangle(cornerSize, cornerSize + barSize, barSize, cornerSize), color);
        spriteBatch.Draw(texture, new Rectangle(point.X, point.Y + cornerSize, cornerSize, height), new Rectangle(0, cornerSize, cornerSize, barSize), color);
        spriteBatch.Draw(texture, new Rectangle(point2.X, point.Y + cornerSize, cornerSize, height), new Rectangle(cornerSize + barSize, cornerSize, cornerSize, barSize), color);
        spriteBatch.Draw(texture, new Rectangle(point.X + cornerSize, point.Y + cornerSize, width, height), new Rectangle(cornerSize, cornerSize, barSize, barSize), color);
    }

    /// <summary>
    ///     Depicts the element with TML Style.
    /// </summary>
    protected virtual void DrawSelfTML(SpriteBatch spriteBatch)
    {
        if (_lastTMLStyleUIPanel is null)
            return;

        var backgroundTextre = _lastTMLStyleUIPanel.BackgroundTexture;
        var borderTexture = _lastTMLStyleUIPanel.BorderTexture;

        if (backgroundTextre != null)
            DrawPanelTML(spriteBatch, backgroundTextre.Value, _lastTMLStyleUIPanel.BackgroundColor);

        if (borderTexture != null)
            DrawPanelTML(spriteBatch, borderTexture.Value, _lastTMLStyleUIPanel.BorderColor);
    }
}