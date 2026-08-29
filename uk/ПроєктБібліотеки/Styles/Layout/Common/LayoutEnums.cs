namespace UIeXtension.Enums;

/// <summary>
///     Перелік варіантів вісь поточного елемента
/// </summary>
public enum UIExOrientation : byte
{
    /// <remarks>Горизонтальна вісь</remarks>
    Horizontal,

    /// <remarks>Вертикальна вісь</remarks>
    Vertical
}

/// <summary>
///     Перелік варіантів вирівнювання вмісту елемента макета
/// </summary>
public enum UIExAlignment : byte
{
    /// <remarks>
    ///     Для контейнерів це значення схоже. <see cref="Start"/>
    ///     Для в'язаних елементів, це означатиме, що значення прийме посилання.
    ///  </remarks>
    Auto,

    /// <remarks>Вирівнювання вмісту по початковому краю (верх/ліво в залежності від поточного осі)</remarks>
    Start,

    /// <remarks>Вирівнювання контенту з центром</remarks>
    Center,

    /// <remarks>Вирівнювання контенту по кінцевому краю (низько/право в залежності від поточного осі)</remarks>
    End,

    /// <summary>Розтягування елементів по всій осі</summary>
    Stretch
}


/// <summary>
///     Список параметрів для позначення конкретної сторони (точне значення залежить від посилання)
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
///     Тип рядка/колюмна розмір <see cref="UIExGridBase"/>
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