using System.Collections.Generic;
using Terraria.UI;
using UIeXtension.Styles;

namespace UIeXtension;

public abstract partial class UIExLayout : UIExElement
{
    /// <summary>
    ///     Внутренний размер текущего элемента компоновки после прошлого пересчета компоновки для дочерних элементов.
    /// </summary>
    /// <remarks>
    ///     Используется, как элемент оптимизации.
    ///     <para>Если размер изменился, компоновка пересчитывается.</para>
    ///     <para>
    ///         Если размер НЕ изменился, то необходимость перекомпоновки вычисляется
    ///         с помощью <see cref="_lastLayoutElements"/>
    ///     </para>
    /// </remarks>
    private CalculatedStyle _lastInnerDimensions = new CalculatedStyle(float.MinValue, float.MinValue, float.MinValue, float.MinValue);

    /// <summary>
    ///     Список Elements, которые были у данного контейнера после прошлой компоновки.
    /// </summary>
    /// <remarks>
    ///     Используется, как элемент оптимизации.
    ///     <para>Если список элементов изменился, компоновка пересчитывается.</para>
    ///     <para>
    ///         Если список элементов НЕ изменился, то необходимость перекомпоновки вычисляется
    ///         с помощью <see cref="_lastInnerDimensions"/>
    ///     </para>
    /// </remarks>
    private List<UIElement> _lastLayoutElements = null;

    /// <summary>
    ///     Список размеров Elements, которые были у данного контейнера, после прошлой компоновки.
    /// </summary>
    /// <remarks>
    ///     Используется, как элемент оптимизации.
    ///     <para>Если список элементов изменился, компоновка пересчитывается.</para>
    ///     <para>
    ///         Если список элементов НЕ изменился, то необходимость перекомпоновки вычисляется
    ///         с помощью <see cref="_lastInnerDimensions"/> и <see cref="_lastLayoutElements"/>
    ///     </para>
    /// </remarks>
    private List<CalculatedStyle> _lastLayoutElementsOuterDimensions = new();

    /// <summary>
    ///     Флаг, указывающий, что следующий <see cref="RecalculateLayout"/> должен игнорировать
    ///     <see cref="_lastInnerDimensions"/>, <see cref="_lastLayoutElements"/> и <see cref="_lastLayoutElementsOuterDimensions"/>
    /// </summary>
    /// <remarks>
    ///     Значение данного флага true при первом вызове <see cref="UIElement.Recalculate"/> и при вызове метода
    ///     <see cref="EndLayoutPreparation(UIState)"/>. После каждой компоновки данный флаг сбрасывается до значения false
    /// </remarks>
    private bool _ignoreLastLayoutInfo = true;


    /// <summary>
    ///     Стили текущих элементов
    /// </summary>
    protected List<StyleLayoutChild> _currentElementsStyles = new();

    /// <summary>
    ///     Копия стилей элементов после последней компоновке
    /// </summary>
    private List<StyleLayoutChild> _lastStyleLayoutChildArray;

    /// <summary>
    ///     Копия стиля компоновки контейнера после последней компоновки
    /// </summary>
    private StyleLayoutContainer _lastStyleLayoutContainer;

    /// <summary>
    ///     Определяет, требуется ли перекомпоновка элементам.
    /// </summary>
    /// <remarks>
    ///     В tModLoader <see cref="UIElement.Recalculate"/> вызывается всей UI-ветки элементов каждый кадр.
    ///     Алгоритмы компоновки срабатывают при <see cref="UIElement.Recalculate"/> и являются затратными по ресурсам.
    ///     Данный метод проверяет:
    ///     <para>Изменились размеры самого компоновщика</para>
    ///     <para>Изменились дочерние элементы</para>
    ///     <para>Изменились ли размеры дочерних элементов</para>
    ///     <para>
    ///         Игнорирует все эти проверки, если <see cref="_ignoreLastLayoutInfo"/> == true. 
    ///         Значение данного флага true при первом вызове <see cref="UIElement.Recalculate"/> и при вызове метода
    ///         <see cref="EndLayoutPreparation(UIState)"/>. После каждой компоновки данный флаг сбрасывается до значения false
    ///     </para>
    ///     <para>Данная информация обновляется после компоновки (если она была) в <see cref="UpdateLastLayoutInfo"/></para>
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
    ///     Обновляет информацию о последней компоновке. Данная информация используется в методе <see cref="IsLastLayoutInfoChanged"/>
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
    ///     Указывает элементу и всем потомкам, что нужно игнорировать последние сохраненные элементы и размеры при следующей попытке пересчитать компоновку.
    /// </summary>
    /// <remarks>
    ///     Смотрите подробнее в описании <see cref="_ignoreLastLayoutInfo"/>, <see cref="IsLastLayoutInfoChanged"/>,
    ///     <see cref="UpdateLastLayoutInfo"/> и <see cref="_lastInnerDimensions"/>
    /// </remarks>
    private static void ResetLastLayoutInfoForBranch(UIElement element)
    {
        if (element is UIExLayout layout)
            layout._ignoreLastLayoutInfo = true;

        foreach (var child in element.Children)
            ResetLastLayoutInfoForBranch(child);
    }
}