namespace UIeXtension;

public partial class UIExStackPanel
{
    /// <summary>
    ///     Клас, що містить тимчасову інформацію про поточне планування <see cref="UIExFlowPanel"/>. .
    /// </summary>
    protected class UIExStackPanelContext
    {
        /// <summary>
        ///     Фінальний стиль <see cref="UIExStackPanel"/>, що використовується в контексті цієї композиції елементів.
        /// </summary>
        public Styles.StyleLayoutContainerStackPanel styleLayoutContainer;
    }

    /// <summary>
    ///     Зберігати дані для поточного макета <see cref="UIExStackPanel"/>. .
    ///     Відновити тільки під час життєвого циклу макета.
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