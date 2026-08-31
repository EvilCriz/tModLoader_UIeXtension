namespace UIeXtension.Enums;

/// <summary>
///     List of options for the current axis of the element
/// </summary>
public enum UIExOrientation : byte
{
    /// <remarks>Horizontal axis</remarks>
    Horizontal,

    /// <remarks>Vertical axis</remarks>
    Vertical
}

/// <summary>
///     List of options for aligning the contents of the layout element
/// </summary>
public enum UIExAlignment : byte
{
    /// <remarks>
    ///     For containers, this value is similar. <see cref="Start"/>
    ///     For nested elements, this will mean that the value of the linker is taken.
    ///  </remarks>
    Auto,

    /// <remarks>Alignment of the contents along the starting edge (top/left depending on the current axis)</remarks>
    Start,

    /// <remarks>Alignment of contents with the centre</remarks>
    Center,

    /// <remarks>Content alignment along the final edge (low/right depending on the current axis)</remarks>
    End,

    /// <summary>Stretching of elements along the whole axis</summary>
    Stretch
}


/// <summary>
///     List of options for pointing to a particular side (the exact value depends on the linker)
/// </summary>
public enum UIExSide : byte
{
    /// <summary/>
    Left,
    /// <summary/>
    Right,
    /// <summary/>
    Top,
    /// <summary/>
    Bottom,
    /// <summary/>
    Fill
}

/// <summary>
///     Type of row/column dimension <see cref="UIExGridBase"/>
/// </summary>
public enum UIExGridLengthType : byte
{
    /// <summary/>
    Pixels,
    /// <summary/>
    Precent,
    /// <summary/>
    Fraction,
    /// <summary/>
    Auto
}