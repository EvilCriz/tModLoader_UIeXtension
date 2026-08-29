using System;
using System.Collections.Generic;

namespace UIeXtension.Styles.Base;

/// <summary>
///     Базовий клас для всіх класів стилю
/// </summary>
public abstract class StyleBase
{
    /// <summary>
    ///     Створює копію спадкоємця <see cref="StyleBase"/>. . 
    ///     Використовуються в методі <see cref="GetCopyBase()"/>який повертає тип <see cref="StyleBase"/>
    /// </summary>
    protected abstract StyleBase Fabricate();

    /// <summary>
    ///     Скопіювати значення передається <see cref="StyleBase"/> 
    /// </summary>
    protected abstract void CopyBase(StyleBase style);

    /// <summary>
    ///     Копіювання зареєстрованих стилів.
    /// </summary>
    protected virtual void CopyInnerStylesBase(StyleBase style) { }

    /// <summary>
    ///     Створює і повертає копію спадкоємця <see cref="StyleBase"/>
    /// </summary>
    protected internal T GetCopyBase<T>() where T : StyleBase, new()
    {
        T copy = new();
        copy.CopyBase(this);
        copy.CopyInnerStylesBase(this);
        return copy;
    }

    /// <summary>
    ///     Створює і повертає копію поточного спадкоємця <see cref="StyleBase"/>
    /// </summary>
    protected internal StyleBase GetCopyBase()
    { 
        StyleBase copy = Fabricate();
        copy.CopyBase(this);
        copy.CopyInnerStylesBase(this);
        return copy;
    }



    /// <summary>
    ///     Перевірте, чи всі значення всіх стилів однакові для поточного і переданого елементу
    /// </summary>
    public bool EqualsStylesFields(StyleBase other)
    {
        if (!EqualsFields(other))
            return false;

        return EqualsInnerStylesFields(other);
    }

    /// <summary>
    ///     Перевіряє, чи є властивості струму і переданого елемента
    /// </summary>
    protected abstract bool EqualsFields(StyleBase other);

    /// <summary>
    ///     Перевіряє, чи стилях поточного і переданого елемента матчу
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
    ///     Перевіряє, чи стилях поточного і переданого елемента матчу
    /// </summary>
    protected virtual bool EqualsInnerStylesFields(StyleBase other) => true;
}