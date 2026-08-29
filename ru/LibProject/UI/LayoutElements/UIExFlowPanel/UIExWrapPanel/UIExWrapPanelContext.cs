using System.Collections.Generic;

namespace UIeXtension;

public partial class UIExWrapPanel
{
    /// <summary>
    ///     Класс, содержающий временную информацию о текущей компоновке <see cref="UIExFlowPanel"/>.
    /// </summary>
    protected class UIExWrapPanelContext
    {
        /// <summary>
        ///     Список линий, каждая из которых представлена в виде списков элементов в линии.
        /// </summary>
        public List<List<int>> linesElementsIndexes = new();

        /// <summary>
        ///     Окончательный стиль <see cref="UIExWrapPanel"/>, используемый в контексте данной компоновки элементов.
        /// </summary>
        public Styles.StyleLayoutContainerWrapPanel styleLayoutContainer;
    }

    /// <summary>
    ///     Хранит данные для текущей компоновки <see cref="UIExWrapPanel"/>.
    ///     Актуален только во время жизненного цикла компоновки.
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