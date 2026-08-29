using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace UIeXtension.Styles.TModLoader;

/// <summary>
///     Клас стилю візуального відображення tModLoader <see cref="Terraria.GameContent.UI.Elements.UIPanel"/>;
/// </summary>
public class StyleTmlUIPanel : Base.StyleTmlBase
{
    /// <summary/>
    public int CornerSize = 12;

    /// <summary/>
    public int BarSize = 4;


    /// <summary/>
    public Asset<Texture2D> BorderTexture;

    /// <summary/>
    public Asset<Texture2D> BackgroundTexture;


    /// <summary/>
    public Color BorderColor = Color.Black;

    /// <summary/>
    public Color BackgroundColor = new Color(63, 82, 151) * 0.7f;




    ///////////////////// CONSTRUCTORS ///////////////
    ///////////////////// CONSTRUCTORS ///////////////

    /// <summary/>
    public StyleTmlUIPanel() { }

    /// <summary/>
    public StyleTmlUIPanel(
        int cornerSize = 12,
        int barSize = 4,
        Asset<Texture2D> borderTexture = null,
        Asset<Texture2D> backgroundTexture = null,
        Color? borderColor = null,
        Color? backgroundColor = null)
            => SetAllFields(
                cornerSize,
                barSize,
                borderTexture,
                backgroundTexture,
                borderColor,
                backgroundColor);




    ///////////////////// SETS ///////////////
    ///////////////////// SETS ///////////////

    /// <summary>
    ///     Вимагає передати всі можливі значення класу <see cref="StyleTmlUIPanel"/> і встановлюємо їх.
    /// </summary>
    public void Set(
        int cornerSize = 12,
        int barSize = 4,
        Asset<Texture2D> borderTexture = null,
        Asset<Texture2D> backgroundTexture = null,
        Color? borderColor = null,
        Color? backgroundColor = null)
            => SetAllFields(
                cornerSize,
                barSize,
                borderTexture,
                backgroundTexture,
                borderColor,
                backgroundColor);

    /// <summary/>
    protected void SetAllFields(
        int cornerSize = 12,
        int barSize = 4,
        Asset<Texture2D> borderTexture = null,
        Asset<Texture2D> backgroundTexture = null,
        Color? borderColor = null,
        Color? backgroundColor = null)
    {
        CornerSize = cornerSize;
        BarSize = barSize;

        if (borderTexture is not null)
            BorderTexture = borderTexture;

        if(backgroundTexture is not null)
            BackgroundTexture = backgroundTexture;

        if (borderColor is not null)
            BorderColor = (Color)borderColor;

        if (backgroundColor is not null)
            BackgroundColor = (Color)backgroundColor;
    }




    ///////////////////// COPY ///////////////
    ///////////////////// COPY ///////////////

    /// <inheritdoc/>
    protected override Base.StyleBase Fabricate() => new StyleTmlUIPanel();

    /// <inheritdoc/>
    protected override void CopyBase(Base.StyleBase style)
    {
        if (style is StyleTmlUIPanel style2)
            Copy(style2);
    }

    /// <summary>
    ///     Скопіювати значення передається <see cref="StyleTmlUIPanel"/> 
    /// </summary>
    public void Copy(StyleTmlUIPanel style)
        => SetAllFields(
                style.CornerSize,
                style.BarSize,
                style.BorderTexture,
                style.BackgroundTexture,
                style.BorderColor,
                style.BackgroundColor);

    /// <summary>
    ///     Створює і повертає копію поточного <see cref="StyleTmlUIPanel"/>
    /// </summary>
    public StyleTmlUIPanel GetCopy()
        => GetCopyBase<StyleTmlUIPanel>();




    ///////////////////// EQUALS ///////////////
    ///////////////////// EQUALS ///////////////

    /// <inheritdoc/>
    protected override bool EqualsFields(Base.StyleBase other)
    {
        if (other is StyleTmlUIPanel otherStyle)
            return CornerSize == otherStyle.CornerSize &&
                BarSize == otherStyle.BarSize &&
                Utils.UtilsStyles.EqualsReferences(BorderTexture, otherStyle.BorderTexture) && 
                Utils.UtilsStyles.EqualsReferences(BackgroundTexture, otherStyle.BackgroundTexture) && 
                BorderColor == otherStyle.BorderColor &&
                BackgroundColor == otherStyle.BackgroundColor;

        return false;
    }
}
