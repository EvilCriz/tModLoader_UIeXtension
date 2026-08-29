using Terraria.UI;

namespace UIeXtension.Styles;

/// <summary>
///     Описывает толщину отступов/границ отдельно для каждой из четырех сторон:
/// </summary>
public struct UIExThickness
{
    /// <summary/>
    public StyleDimension Left = StyleDimension.Empty;
    /// <summary/>
    public StyleDimension Top = StyleDimension.Empty;

    /// <summary/>
    public StyleDimension Right = StyleDimension.Empty;
    /// <summary/>
    public StyleDimension Bottom = StyleDimension.Empty;

    /// <summary/>
    public UIExThickness()
    { }

    /// <summary/>
    public UIExThickness(bool pixels, float size)
        => Set(
            pixels:     pixels, 
            size:       size);

    /// <summary/>
    public UIExThickness(bool pixels = true, float left = 0f, float top = 0f, float right = 0f, float bottom = 0f) 
        => Set(
            pixels:     pixels, 
            left:       left, 
            top:        top, 
            right:      right, 
            bottom:     bottom);

    /// <summary/>
    public void SetPixels(float left = 0f, float top = 0f, float right = 0f, float bottom = 0f)
        => Set(
            pixels:     true, 
            left:       left, 
            top:        top, 
            right:      right, 
            bottom:     bottom);

    /// <summary/>
    public void SetPixels(float size)
        => Set(
            pixels:     true, 
            size:       size);


    /// <summary/>
    public void SetPrecent(float left, float top = 0f, float right = 0f, float bottom = 0f)
        => Set(
            pixels:     false, 
            left:       left, 
            top:        top, 
            right:      right, 
            bottom:     bottom);

    /// <summary/>
    public void SetPrecent(float size)
        => Set(
            pixels:     false, 
            size:       size);

    /// <summary/>
    private void Set(bool pixels, float size)
        => Set(
            pixels:     pixels, 
            top:        size, 
            left:       size, 
            right:      size, 
            bottom:     size);

    /// <summary/>
    private void Set(bool pixels, float left, float top, float right, float bottom)
    {
        float pixelsLeft = pixels ? left : Left.Pixels;
        float pixelsTop = pixels ? top : Top.Pixels;
        float pixelsRight = pixels ? right : Right.Pixels;
        float pixelsBottom = pixels ? bottom : Bottom.Pixels;

        bool precent = !pixels;

        float precentLeft = precent ? left : Left.Precent;
        float precentTop = precent ? top : Top.Precent;
        float precentRight = precent ? right : Right.Precent;
        float precentBottom = precent ? bottom : Bottom.Precent;

        Left.Set(pixelsLeft, precentLeft);
        Top.Set(pixelsTop, precentTop);
        Right.Set(pixelsRight, precentRight);
        Bottom.Set(pixelsBottom, precentBottom);
    }

    /// <summary/>
    public readonly bool EqualsFields(UIExThickness other)
        =>  Utils.UtilsStyles.EqualsStyleDimensionFields(Left, other.Left) &&
            Utils.UtilsStyles.EqualsStyleDimensionFields(Top, other.Top) &&
            Utils.UtilsStyles.EqualsStyleDimensionFields(Right, other.Right) &&
            Utils.UtilsStyles.EqualsStyleDimensionFields(Bottom, other.Bottom);
}
