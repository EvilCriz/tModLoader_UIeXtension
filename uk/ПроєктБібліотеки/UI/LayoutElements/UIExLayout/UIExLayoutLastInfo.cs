using System.Collections.Generic;
using Terraria.UI;
using UIeXtension.Styles;

namespace UIeXtension;

public abstract partial class UIExLayout : UIExElement
{
    /// <summary>
    ///     Внутрішній розмір поточного елемента макета після останнього перерахунку макета для дитячих елементів.
    /// </summary>
    /// <remarks>
    ///     Використовується як елемент оптимізації.
    ///     <para>Якщо змінився розмір, то перерахунок перераховується.</para>
    ///     <para>
    ///         Якщо розмір не змінився, то розраховується необхідність переконфігурації.
    ///         Надання допомоги <see cref="_lastLayoutElements"/>
    ///     </para>
    /// </remarks>
    private CalculatedStyle _lastInnerDimensions = new CalculatedStyle(float.MinValue, float.MinValue, float.MinValue, float.MinValue);

    /// <summary>
    ///     Список елементів, які в цьому контейнері було після попереднього макета.
    /// </summary>
    /// <remarks>
    ///     Використовується як елемент оптимізації.
    ///     <para>Якщо змінився перелік елементів, то перерахунок перераховується.</para>
    ///     <para>
    ///         Якщо список елементів не змінився, то розраховується потреба в переадресації.
    ///         Надання допомоги <see cref="_lastInnerDimensions"/>
    ///     </para>
    /// </remarks>
    private List<UIElement> _lastLayoutElements = null;

    /// <summary>
    ///     Перелік розмірів елементів, які цей контейнер мав після попереднього макета.
    /// </summary>
    /// <remarks>
    ///     Використовується як елемент оптимізації.
    ///     <para>Якщо змінився перелік елементів, то перерахунок перераховується.</para>
    ///     <para>
    ///         Якщо список елементів не змінився, то розраховується потреба в переадресації.
    ///         Надання допомоги <see cref="_lastInnerDimensions"/> і <see cref="_lastLayoutElements"/>
    ///     </para>
    /// </remarks>
    private List<CalculatedStyle> _lastLayoutElementsOuterDimensions = new();

    /// <summary>
    ///     прапор вказує на те, що наступний <see cref="RecalculateLayout"/> слід ігнорувати
    ///     <see cref="_lastInnerDimensions"/>. . <see cref="_lastLayoutElements"/> і <see cref="_lastLayoutElementsOuterDimensions"/>
    /// </summary>
    /// <remarks>
    ///     Значення прапора true 1-й квартал <see cref="UIElement.Recalculate"/> і викликати метод
    ///     <see cref="EndLayoutPreparation(UIState)"/>Після кожного макета цей прапор впав на значення. false
    /// </remarks>
    private bool _ignoreLastLayoutInfo = true;


    /// <summary>
    ///     Стилі сучасних елементів
    /// </summary>
    protected List<StyleLayoutChild> _currentElementsStyles = new();

    /// <summary>
    ///     Скопіювати стилі елемента після останнього макета
    /// </summary>
    private List<StyleLayoutChild> _lastStyleLayoutChildArray;

    /// <summary>
    ///     Копія стилю контейнера після останнього планування
    /// </summary>
    private StyleLayoutContainer _lastStyleLayoutContainer;

    /// <summary>
    ///     Визначає, чи потрібна рекомпозиція елементів.
    /// </summary>
    /// <remarks>
    ///     В. В. tModLoader <see cref="UIElement.Recalculate"/> засупу 10 UI- гілки елементів кожної рами.
    ///     Алгоритми роботи макета при <see cref="UIElement.Recalculate"/> І вони ресурсно-інтенсивні.
    ///     Цей метод перевіряє:
    ///     <para>Розмір самого конструктора змінився.</para>
    ///     <para>Змінювальні елементи</para>
    ///     <para>Чи змінено розмір елементів дитини?</para>
    ///     <para>
    ///         Ігнорує всі ці перевірки, якщо <see cref="_ignoreLastLayoutInfo"/> ================================================================================================================================================================================================================================================================ true. . 
    ///         Значення прапора true 1-й квартал <see cref="UIElement.Recalculate"/> і викликати метод
    ///         <see cref="EndLayoutPreparation(UIState)"/>Після кожного макета цей прапор впав на значення. false
    ///     </para>
    ///     <para>Ця інформація оновлюється після розмітки (за наявності) в <see cref="UpdateLastLayoutInfo"/></para>
    /// </remarks>
    protected bool IsLastLayoutInfoChanged()
    {
        if (_ignoreLastLayoutInfo)
            return true;

        int elementCount = Elements.Count;

        if (_lastLayoutElements is null)
            return true;

        if (_lastStyleLayoutContainer is null)
            return true;

        if (_lastStyleLayoutChildArray is null)
            return true;

        if (_lastLayoutElements.Count != elementCount)
            return true;
        
        if(IsInnerDimensionsNotEquals(this, _lastInnerDimensions))
            return true;

        for (int i = 0; i < elementCount; i++)
            if (_lastLayoutElements[i] != Elements[i])
                return true;

        if (!_lastStyleLayoutContainer.EqualsStylesFields(StyleLayout))
            return true;

        for (int i = 0; i < elementCount; i++)
        {
            CalculatedStyle outer = _lastLayoutElements[i].GetOuterDimensions();
            if (outer.Width != _lastLayoutElementsOuterDimensions[i].Width ||
                outer.Height != _lastLayoutElementsOuterDimensions[i].Height)
                    return true;
        }

        for (int i = 0; i < elementCount; i++)
            if (!_lastStyleLayoutChildArray[i]
                .EqualsStylesFields(_styleLayoutChildArray[i]))
                return true;

        return false;
    }

    /// <summary>
    ///     Оновлення інформації про останню верстку. Дана інформація використовується в методі <see cref="IsLastLayoutInfoChanged"/>
    /// </summary>
    protected void UpdateLastLayoutInfo()
    {
        _ignoreLastLayoutInfo = false;

        if (_lastLayoutElements is null)
            _lastLayoutElements = new(Elements.Count);
        else
            _lastLayoutElements.Clear();

        _lastLayoutElementsOuterDimensions.Clear();
        _lastInnerDimensions = GetInnerDimensions();

        foreach (var element in Elements)
        {
            _lastLayoutElements.Add(element);
            _lastLayoutElementsOuterDimensions.Add(element.GetOuterDimensions());
        }

        ///////////////////

        if (_lastStyleLayoutChildArray is null)
            _lastStyleLayoutChildArray = new(Elements.Count);
        else
            _lastStyleLayoutChildArray.Clear();
        
        _lastStyleLayoutContainer = StyleLayout.GetCopy();

        foreach(var style in _styleLayoutChildArray)
            _lastStyleLayoutChildArray.Add(style.GetCopy());
    }

    /// <summary>
    ///     Призначає елемент і всі нащадки ігнорувати останні збережені елементи і розміри, коли вони намагаються перерахувати макет.
    /// </summary>
    /// <remarks>
    ///     Детальніше в описі <see cref="_ignoreLastLayoutInfo"/>. . <see cref="IsLastLayoutInfoChanged"/>. .
    ///     <see cref="UpdateLastLayoutInfo"/> і <see cref="_lastInnerDimensions"/>
    /// </remarks>
    private static void ResetLastLayoutInfoForBranch(UIElement element)
    {
        if (element is UIExLayout layout)
            layout._ignoreLastLayoutInfo = true;

        foreach (var child in element.Children)
            ResetLastLayoutInfoForBranch(child);
    }
}