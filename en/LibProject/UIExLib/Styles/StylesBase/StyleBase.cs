using System;
using System.Collections.Generic;

namespace UIeXtension.Styles.Base;

/// <summary>
///     Basic class for all style classes
/// </summary>
public abstract class StyleBase
{
    /// <summary>
    ///     Creates a copy of the heir <see cref="StyleBase"/>. 
    ///     Used in the method <see cref="GetCopyBase()"/>which returns the type <see cref="StyleBase"/>
    /// </summary>
    protected abstract StyleBase Fabricate();

    /// <summary>
    ///     Copy the values of the transmitted <see cref="StyleBase"/> 
    /// </summary>
    protected abstract void CopyBase(StyleBase style);

    /// <summary>
    ///     Copying registered styles.
    /// </summary>
    protected virtual void CopyInnerStylesBase(StyleBase style) { }

    /// <summary>
    ///     Creates and returns a copy of the heir <see cref="StyleBase"/>
    /// </summary>
    protected internal T GetCopyBase<T>() where T : StyleBase, new()
    {
        T copy = new();
        copy.CopyBase(this);
        copy.CopyInnerStylesBase(this);
        return copy;
    }

    /// <summary>
    ///     Creates and returns a copy of the current heir <see cref="StyleBase"/>
    /// </summary>
    protected internal StyleBase GetCopyBase()
    { 
        StyleBase copy = Fabricate();
        copy.CopyBase(this);
        copy.CopyInnerStylesBase(this);
        return copy;
    }



    /// <summary>
    ///     Checks whether all the values of all styles are the same for the current and transmitted element
    /// </summary>
    public bool EqualsStylesFields(StyleBase other)
    {
        if (!EqualsFields(other))
            return false;

        return EqualsInnerStylesFields(other);
    }

    /// <summary>
    ///     Checks whether the properties of the current and transmitted element are the same
    /// </summary>
    protected abstract bool EqualsFields(StyleBase other);

    /// <summary>
    ///     Checks whether the styles of the current and transferred element match
    /// </summary>
    protected bool EqualsInnerStylesFields<TStyleBase>(Dictionary<Type, TStyleBase> styles1, Dictionary<Type, TStyleBase> styles2) 
        where TStyleBase : StyleBase
    {
        if (styles1.Count != styles2.Count)
            return false;

        foreach (var key in styles1.Keys)
        {
            if (!styles2.ContainsKey(key))
                return false;

            if (!styles1[key].EqualsFields(styles2[key]))
                return false;
        }

        return true;
    }

    /// <summary>
    ///     Checks whether the styles of the current and transferred element match
    /// </summary>
    protected virtual bool EqualsInnerStylesFields(StyleBase other) => true;
}