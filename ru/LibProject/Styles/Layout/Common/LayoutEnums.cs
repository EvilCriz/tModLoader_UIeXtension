namespace UIeXtension.Enums;

/// <summary>
///     Список вариантов текущей оси элемента
/// </summary>
public enum UIExOrientation : byte
{
    /// <remarks>Горизонтальная ось</remarks>
    Horizontal,

    /// <remarks>Вертикальная ось</remarks>
    Vertical
}

/// <summary>
///     Список вариантов выравнивания содержимого элемента компоновки
/// </summary>
public enum UIExAlignment : byte
{
    /// <remarks>
    ///     Для контейнеров это значение аналогично <see cref="Start"/>
    ///     Для вложенных элементов это будет означать, что берется значение компоновщика
    ///  </remarks>
    Auto,

    /// <remarks>Выравнивание содержимого по стартовому краю (верх / лево в зависимости от текущей оси)</remarks>
    Start,

    /// <remarks>Выравнивание содержимого со центру</remarks>
    Center,

    /// <remarks>Выравнивание содержимого по конечному краю (низ / право в зависимости от текущей оси)</remarks>
    End,

    /// <summary>Растягивание элементов по всей оси</summary>
    Stretch
}


/// <summary>
///     Список вариантов указания на конкретную сторону (точное значение зависит от компоновщика)
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
///     Тип размерности строки/столбца <see cref="UIExGridBase"/>
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