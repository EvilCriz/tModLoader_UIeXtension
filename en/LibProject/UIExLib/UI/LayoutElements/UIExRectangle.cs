using Terraria.UI;

namespace UIeXtension;

/// <summary/>
public struct UIExRectangle
{
    /// <summary/>
    public float Top;
    /// <summary/>
    public float Left;
    /// <summary/>
    public float Right;
    /// <summary/>
    public float Bottom;

    /// <summary/>
    public UIExRectangle() { }

    /// <summary/>
    public UIExRectangle(float left, float top, float right, float bottom)
    {
        this.Left = left;
        this.Top = top;
        this.Right = right;
        this.Bottom = bottom;
    }

    /// <summary/>
    public float GetHeight() => Bottom - Top;

    /// <summary/>
    public float GetWidth() => Right - Left;

    /// <summary/>
    public float GetSize(bool width)
        => width ? GetWidth() : GetHeight();

    /// <summary/>
    public void SetSize(bool width, float size)
    {
        if (width)
            Right = Left + size;
        else
            Bottom = Top + size;
    }

    /// <summary/>
    public void AddSize(bool width, float size)
    {
        if (width)
            Right += size;
        else
            Bottom += size;
    }

    /// <summary/>
    public CalculatedStyle GetCalculatedStyle()
        => new CalculatedStyle(Left, Top, GetWidth(), GetHeight());

    /// <summary/>
    public void AddTopOffset(float offset)
    {
        Top += offset;
        Bottom += offset;
    }

    /// <summary/>
    public void AddLeftOffset(float offset)
    {
        Left += offset;
        Right += offset;
    }
}