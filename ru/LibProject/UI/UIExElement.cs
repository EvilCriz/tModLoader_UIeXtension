using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace UIeXtension;

/// <summary>
///     Базовый класс для всех UI-элементов данной библиотеки
/// </summary>
/// <remarks>
///     Использует <see cref="Styles.StyleVisualElement"/> как основной способ стилизации элемента
/// </remarks>
public partial class UIExElement : UIElement
{
    /// <summary>
    ///     Поле для пользовательского типа данных, который будет сохраняться за отдельным <see cref="UIExElement"/>
    /// </summary>
    public object Data;

    /// <summary>
    ///     Таблица стиля с визуальными параметрами, который использует каждый UI-элемент данной библиотеки.
    /// </summary>
    public Styles.StyleVisualElement StyleDisplay;

    /// <summary>
    /// При использовании данной перегрузки конструктора элемент получает таблицу стилей по умолчанию.
    /// </summary>
    public UIExElement() : this(new Styles.StyleVisualElement()) { }


    /// <summary>
    ///     Перегрузка конструктора, принимающая основную таблицу стилей UI-элементов.
    /// </summary>
    /// <param name="style">
    ///     Основная таблица стилей всех UI-элементов
    /// </param>
    public UIExElement(Styles.StyleVisualElement style)
    {
        StyleDisplay = style;
    }


    /// <summary>
    ///     Пересчитывает стили элемента, затем вызывает <see cref="UIElement.Recalculate"/>
    /// </summary>
    public override void Recalculate()
    {
        UpdateLastTMLStyle();

        base.Recalculate();
    }

    /// <summary>
    ///     Обновляет значение <see cref="_lastTMLStyle"/>.
    ///     Вызывает <see cref="RefreshTMLDisplayStyle"/>, если <see cref="_lastTMLStyle"/> == true
    ///     Вызывает <see cref="RecalculateDisplayStyle"/> если значение <see cref="_lastTMLStyle"/> == false
    /// </summary>
    protected virtual void UpdateLastTMLStyle()
    {
        _lastTMLStyle = StyleDisplay.tModLoaderStyle;

        if (_lastTMLStyle)
            RecalculateTMLDisplayStyle();
        else
            RecalculateDisplayStyle();
    }

    /// <inheritdoc/>
    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        if (_lastTMLStyle)
            DrawSelfTML(spriteBatch);
        else
            DrawSelfStyleDisplay(spriteBatch);
    }
}