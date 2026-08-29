using System;
using System.Collections.Generic;

namespace UIeXtension.Styles.Base;

/// <summary>
///     Базовый класс для всех классов стилей
/// </summary>
public abstract class StyleBase
{
    /// <summary>
    ///     Создает экземпляр наследника <see cref="StyleBase"/>. 
    ///     Используется в методе <see cref="GetCopyBase()"/>, который возвращает тип <see cref="StyleBase"/>
    /// </summary>
    protected abstract StyleBase Fabricate();

    /// <summary>
    ///     Копирует значения переданного <see cref="StyleBase"/> 
    /// </summary>
    protected abstract void CopyBase(StyleBase style);

    /// <summary>
    ///     Копирует зарегистрированные стили.
    /// </summary>
    protected virtual void CopyInnerStylesBase(StyleBase style) { }

    /// <summary>
    ///     Создает и возвращает копию наследника <see cref="StyleBase"/>
    /// </summary>
    protected internal T GetCopyBase<T>() where T : StyleBase, new()
    {
        T copy = new();
        copy.CopyBase(this);
        copy.CopyInnerStylesBase(this);
        return copy;
    }

    /// <summary>
    ///     Создает и возвращает копию текущего наследника <see cref="StyleBase"/>
    /// </summary>
    protected internal StyleBase GetCopyBase()
    { 
        StyleBase copy = Fabricate();
        copy.CopyBase(this);
        copy.CopyInnerStylesBase(this);
        return copy;
    }



    /// <summary>
    ///     Проверяет, совпадают ли все ЗНАЧЕНИЯ ВСЕХ стилей у текущего и переданного элемента
    /// </summary>
    public bool EqualsStylesFields(StyleBase other)
    {
        if (!EqualsFields(other))
            return false;

        return EqualsInnerStylesFields(other);
    }

    /// <summary>
    ///     Проверяет, совпадают ли свойства у текущего и переданного элемента
    /// </summary>
    protected abstract bool EqualsFields(StyleBase other);

    /// <summary>
    ///     Проверяет, совпадают ли стили у текущего и переданного элемента
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
    ///     Проверяет, совпадают ли стили у текущего и переданного элемента
    /// </summary>
    protected virtual bool EqualsInnerStylesFields(StyleBase other) => true;
}