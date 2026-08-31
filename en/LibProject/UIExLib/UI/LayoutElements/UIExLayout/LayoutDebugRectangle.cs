namespace UIeXtension;

/// <summary/>
public struct LayoutDebugRectangle
{
    /// <summary/>
    public float X;
    /// <summary/>
    public float Y;
    /// <summary/>
    public float Width;
    /// <summary/>
    public float Height;

    /// <summary/>
    public LayoutDebugRectangle(
        float x,
        float y,
        float width,
        float height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    /// <summary/>
    public bool IsEmpty()
        => X == 0f && Y == 0f && Width == 0f && Height == 0f;

    /// <summary/>
    public bool IsNotEmpty()
        => !IsEmpty();

    /// <summary/>
    public Microsoft.Xna.Framework.Rectangle GetXnaRentangle()
        => new((int)X, (int)Y, (int)Width, (int)Height);
}