using System.Collections.Generic;

namespace UIeXtension;

public partial class UIExWrapPanel
{
    /// <summary>
    ///     Клас, що містить тимчасову інформацію про поточне планування <see cref="UIExFlowPanel"/>. .
    /// </summary>
    protected class UIExWrapPanelContext
    {
        /// <summary>
        ///     Список ліній, кожен з яких представлений як перелік елементів в рядку.
        /// </summary>
        public List<List<int>> linesElementsIndexes = new();

        /// <summary>
        ///     Фінальний стиль <see cref="UIExWrapPanel"/>, що використовується в контексті цієї композиції елементів.
        /// </summary>
        public Styles.StyleLayoutContainerWrapPanel styleLayoutContainer;
    }

    /// <summary>
    ///     Зберігати дані для поточного макета <see cref="UIExWrapPanel"/>. .
    ///     Відновити тільки під час життєвого циклу макета.
    /// </summary>
    protected UIExWrapPanelContext _context;

    /// <inheritdoc/>
    protected override void BeginLayoutContext()
    {
        base.BeginLayoutContext();

        _context = new();
        _context.styleLayoutContainer = StyleLayout.WrapPanel();
    }

    /// <inheritdoc/>
    protected override void EndLayoutContext()
    {
        base.EndLayoutContext();

        _context = null;
    }
}