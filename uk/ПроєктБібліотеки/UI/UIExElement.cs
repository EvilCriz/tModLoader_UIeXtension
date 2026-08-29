using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace UIeXtension;

/// <summary>
///     Базовий клас для всіх UIElements Бібліотека
/// </summary>
/// <remarks>
///     Використовуйте його. <see cref="Styles.StyleVisualElement"/> як основний метод укладання елемента
/// </remarks>
public partial class UIExElement : UIElement
{
    /// <summary>
    ///     Поле для користувача типу даних, які будуть зберігатися для окремого <see cref="UIExElement"/>
    /// </summary>
    public object Data;

    /// <summary>
    ///     Стильний стіл з візуальними параметрами, які кожен використовує UI- елемент цієї бібліотеки.
    /// </summary>
    public Styles.StyleVisualElement StyleDisplay;

    /// <summary>
    /// При використанні цього конструктора перевантаження елемент отримує аркуш стилю за замовчуванням.
    /// </summary>
    public UIExElement() : this(new Styles.StyleVisualElement()) { }


    /// <summary>
    ///     Конструктор перевантаження приймає основний стильний лист UI- Елементи.
    /// </summary>
    /// <param name="style">
    ///     Головний стиль аркуша всіх UI-вибрані
    /// </param>
    public UIExElement(Styles.StyleVisualElement style)
    {
        StyleDisplay = style;
    }


    /// <summary>
    ///     Відтворює стилі елемента, потім дзвінки <see cref="UIElement.Recalculate"/>
    /// </summary>
    public override void Recalculate()
    {
        UpdateLastTMLStyle();

        base.Recalculate();
    }

    /// <summary>
    ///     Оновлення значення <see cref="_lastTMLStyle"/>. .
    ///     Дзвоніння. <see cref="RefreshTMLDisplayStyle"/>Яким чином <see cref="_lastTMLStyle"/> ================================================================================================================================================================================================================================================================ true
    ///     Дзвоніння. <see cref="RecalculateDisplayStyle"/> Яким чином <see cref="_lastTMLStyle"/> ================================================================================================================================================================================================================================================================ false
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