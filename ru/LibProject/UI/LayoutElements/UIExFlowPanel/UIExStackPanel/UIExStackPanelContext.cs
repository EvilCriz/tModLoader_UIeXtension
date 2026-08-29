namespace UIeXtension;

public partial class UIExStackPanel
{
    /// <summary>
    ///     Класс, содержающий временную информацию о текущей компоновке <see cref="UIExFlowPanel"/>.
    /// </summary>
    protected class UIExStackPanelContext
    {
        /// <summary>
        ///     Окончательный стиль <see cref="UIExStackPanel"/>, используемый в контексте данной компоновки элементов.
        /// </summary>
        public Styles.StyleLayoutContainerStackPanel styleLayoutContainer;
    }

    /// <summary>
    ///     Хранит данные для текущей компоновки <see cref="UIExStackPanel"/>.
    ///     Актуален только во время жизненного цикла компоновки.
    /// </summary>
    protected UIExStackPanelContext _context;

    /// <inheritdoc/>
    protected override void BeginLayoutContext()
    {
        base.BeginLayoutContext();

        _context = new();
        _context.styleLayoutContainer = StyleLayout.StackPanel();
    }

    /// <inheritdoc/>
    protected override void EndLayoutContext()
    {
        base.EndLayoutContext();

        _context = null;
    }
}