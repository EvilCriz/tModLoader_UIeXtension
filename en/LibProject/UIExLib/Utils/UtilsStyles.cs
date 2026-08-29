using System;
using Terraria.UI;

namespace UIeXtension.Utils;

/// <summary>
///     Auxiliary class for layout elements.
/// </summary>
public static class UtilsStyles
{
    /// <summary/>
    public static bool EqualsStyleDimensionFields(StyleDimension style1, StyleDimension style2)
        => style1.Pixels == style2.Pixels &&
            style1.Precent == style2.Precent;

    /// <summary/>
    public static bool EqualsReferences<TRef1, TRef2>(TRef1 ref1, TRef2 ref2)
        where TRef1 : class
        where TRef2 : class
    {
        if (ref1 is null && ref2 is null)
            return true;
        else if (ref1 is not null && ref2 is not null)
            return ref1.Equals(ref2);

        return false;
    }

    /// <summary>
    ///     Comparing two classes <see cref="Styles.Base.StyleBase"/> on Nullable.
    ///     If both objects are not null Comparing their fields using the method 
    ///     <see cref="Styles.Base.StyleBase.EqualsStylesFields(Styles.Base.StyleBase)"/>
    /// </summary>
    public static bool EqualsStyles<T>(T style1, T style2)
        where T : Styles.Base.StyleBase
    {
        if (style1 is null || style2 is null)
            return style1 is null && style2 is null;

        return style1.EqualsStylesFields(style2);
    }
}