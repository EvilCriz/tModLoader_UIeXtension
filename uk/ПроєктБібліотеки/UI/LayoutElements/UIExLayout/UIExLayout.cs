namespace UIeXtension;

/// <summary>
///     Базовий клас для елементів макета даної бібліотеки.
/// </summary>
/// <remarks>
///     Відповідальний за: 
///     Базове визначення життєвого циклу.
///     Інструменти для адаптації до обмежень tModLoader. .
/// </remarks>
public abstract partial class UIExLayout : UIExElement
{
    /// <summary>
    ///     Конструктор за замовчуванням. Використовуйте стиль за замовчуванням.
    /// </summary>
    /// <remarks>
    ///     Стиль візуального відображення за замовчуванням: <see cref="Styles.StyleVisualElement"/>. .
    ///     Стиль розташування контейнера за замовчуванням: <see cref="Styles.StyleLayoutContainer"/>
    /// </remarks>
    public UIExLayout() : this(new Styles.StyleVisualElement()) { }

    /// <summary>
    ///     Дизайнер, який приймає тільки стиль візуального відображення. Використовуйте стиль розташування контейнера за замовчуванням.
    /// </summary>
    /// <remarks>
    ///     Стиль розташування контейнера за замовчуванням: <see cref="Styles.StyleLayoutContainer"/>
    /// </remarks>
    /// <param name="styleVisual">
    ///     Стиль візуального відображення
    /// </param>
    public UIExLayout(Styles.StyleVisualElement styleVisual) : this(styleVisual, new Styles.StyleLayoutContainer()) { }

    /// <summary>
    ///     Конструктор, який приймає тільки стиль контейнера, використовує стиль візуального відображення за замовчуванням.
    /// </summary>
    /// <remarks>
    ///     Стиль візуального відображення за замовчуванням: <see cref="Styles.StyleVisualElement"/>
    /// </remarks>
    /// <param name="styleLayout">
    ///     Стиль макета контейнера
    /// </param>
    public UIExLayout(Styles.StyleLayoutContainer styleLayout) : this(new Styles.StyleVisualElement(), styleLayout) { }

    /// <summary>
    ///     Конструктор, який приймає всі стилі, які підтримує цей клас.
    /// </summary>
    /// <param name="styleVisual">Стиль візуального відображення</param>
    /// <param name="styleLayout">Стиль макета контейнера</param>
    public UIExLayout(Styles.StyleVisualElement styleVisual, Styles.StyleLayoutContainer styleLayout) : base(styleVisual)
        => StyleLayout = styleLayout;
}