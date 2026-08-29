namespace UIeXtension;

/// <summary>
///     Базовый класс для элементов компоновки данной библиотеки.
/// </summary>
/// <remarks>
///     Отвечает за: 
///     1. Базовое определение жизненного цикла.
///     2. Инструменты адаптацию к ограничениям tModLoader.
/// </remarks>
public abstract partial class UIExLayout : UIExElement
{
    /// <summary>
    ///     Конструктор по умолчанию. Использует стиль по умолчанию
    /// </summary>
    /// <remarks>
    ///     Стиль визуального отображения по умолчанию: <see cref="Styles.StyleVisualElement"/>.
    ///     Стиль компоновки контейнера по умолчанию: <see cref="Styles.StyleLayoutContainer"/>
    /// </remarks>
    public UIExLayout() : this(new Styles.StyleVisualElement()) { }

    /// <summary>
    ///     Конструктор принимающий только стиль визуального отображения. Использует стиль компоновки контейнера по умолчанию.
    /// </summary>
    /// <remarks>
    ///     Стиль компоновки контейнера по умолчанию: <see cref="Styles.StyleLayoutContainer"/>
    /// </remarks>
    /// <param name="styleVisual">
    ///     Стиль визуального отображения
    /// </param>
    public UIExLayout(Styles.StyleVisualElement styleVisual) : this(styleVisual, new Styles.StyleLayoutContainer()) { }

    /// <summary>
    ///     Конструктор принимающий только стиль компоновки контейнера. Использует стиль визуального отображения по умолчанию.
    /// </summary>
    /// <remarks>
    ///     Стиль визуального отображения по умолчанию: <see cref="Styles.StyleVisualElement"/>
    /// </remarks>
    /// <param name="styleLayout">
    ///     Стиль компоновки контейнера
    /// </param>
    public UIExLayout(Styles.StyleLayoutContainer styleLayout) : this(new Styles.StyleVisualElement(), styleLayout) { }

    /// <summary>
    ///     Конструктор, принимающий все стили, которые поддерживает данный класс.
    /// </summary>
    /// <param name="styleVisual">Стиль визуального отображения</param>
    /// <param name="styleLayout">Стиль компоновки контейнера</param>
    public UIExLayout(Styles.StyleVisualElement styleVisual, Styles.StyleLayoutContainer styleLayout) : base(styleVisual)
        => StyleLayout = styleLayout;
}