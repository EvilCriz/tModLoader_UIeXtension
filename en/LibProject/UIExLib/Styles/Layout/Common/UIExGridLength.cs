namespace UIeXtension.Styles;

/// <summary>
///     Line/column size <see cref="UIExGridBase"/>
/// </summary>
public struct UIExGridLength
{
    /// <summary>
    ///     Type of row/column dimension <see cref="UIExGridBase"/>
    /// </summary>
    public Enums.UIExGridLengthType Type = Enums.UIExGridLengthType.Auto;

    /// <summary/>
    public float Pixels = 0f;
    /// <summary/>
    public float Precent = 0f;
    /// <summary/>
    public float Fraction = 0f;

    /// <summary/>
    public UIExGridLength(float size, Enums.UIExGridLengthType type = Enums.UIExGridLengthType.Pixels)
        => SetSize(size, type);

    /// <summary/>
    public void SetSize(float size) => SetSize(size, Type);
    /// <summary/>
    public void SetSize(float size, Enums.UIExGridLengthType type)
    {
        Type = type;

        switch (type)
        {
            case Enums.UIExGridLengthType.Pixels:
                Pixels = size;
                break;
            case Enums.UIExGridLengthType.Precent:
                Precent = size;
                break;
            case Enums.UIExGridLengthType.Fraction:
                Fraction = size;
                break;
        }
    }

    /// <summary/>
    public bool EqualsFields(UIExGridLength other)
    {
        if (this.Type != other.Type)
            return false;

        switch (Type)
        {
            case Enums.UIExGridLengthType.Pixels:
                return Pixels == other.Pixels;
            case Enums.UIExGridLengthType.Precent:
                return Precent == other.Precent;
            case Enums.UIExGridLengthType.Fraction:
                return Fraction == other.Fraction;
            case Enums.UIExGridLengthType.Auto:
                return true;
        }

        return false;
    }



    /// <summary/>
    public static UIExGridLength FromPx(float pixels) 
        => new UIExGridLength(
            size:   pixels, 
            type:   Enums.UIExGridLengthType.Pixels);

    /// <summary/>
    public static UIExGridLength FromPr(float precent)
        => new UIExGridLength(
            size:   precent,
            type:   Enums.UIExGridLengthType.Precent);

    /// <summary/>
    public static UIExGridLength FromFr(float fraction)
        => new UIExGridLength(
            size:   fraction,
            type:   Enums.UIExGridLengthType.Fraction);

    /// <summary/>
    public static UIExGridLength FromAuto() => 
        new UIExGridLength(
            size:   0f, 
            type:   Enums.UIExGridLengthType.Auto);

    /// <summary/>
    public static UIExGridLength Empty() => new UIExGridLength(
        size:       0f,
        type:       Enums.UIExGridLengthType.Pixels);
}