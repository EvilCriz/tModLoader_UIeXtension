using Terraria.UI;

namespace UIeXtension;

/// <summary>
/// Structure for storing coordinates of the layout area and its index
/// </summary>
public class RectangleLayoutContext
{
    /// <summary>
    ///     Current area index of a specific container layout
    /// </summary>
    public int Index;


    /// <summary/>
    public float Top;

    /// <summary/>
    public float Left;

    /// <summary/>
    public float Width;

    /// <summary/>
    public float Height;

    /// <summary/>
    public RectangleLayoutContext(int index) => Index = index;

    /// <summary/>
    public RectangleLayoutContext(int index, UIElement element, CalculatedStyle outer) : this(index)
    {
        Index = index;

        Top = element.Top.GetValue(outer.Height);
        Left = element.Left.GetValue(outer.Width);
        Height = element.Height.GetValue(outer.Height);
        Width = element.Width.GetValue(outer.Width);
    }

    /// <summary/>
    public CalculatedStyle GetCalculatedStyle()
        => new(Left, Top, Width, Height);

    /// <summary/>
    public LayoutDebugRectangle GetLayoutDebugRectangle()
        => new(Left, Top, Width, Height);
}