namespace UIeXtension;
public partial class UIExCanvas
{
    /// <summary>
    ///     Класс, содержающий временную информацию о текущей компоновке <see cref="UIExCanvas"/>.
    /// </summary>
    protected class UIExCanvasContext
    {
        /// <summary>
        ///     Текущая ориентация главной оси.
        /// </summary>
        public bool vertical;

        /// <summary>
        ///     Отступы элементов по основной оси.
        /// </summary>
        public float[] justifyOffsets;

        /// <summary>
        ///     Отступы элементов по поперечной оси.
        /// </summary>
        public float[] alignOffsets;

        /// <summary>
        ///     Окончательный стиль <see cref="UIExCanvas"/>, используемый в контексте данной компоновки элементов.
        /// </summary>
        public Styles.StyleLayoutContainerCanvas styleLayoutContainer;
    }




    /// <summary>
    ///     Хранит в себе все стили  участвующих в компоновке дочерних элементов.  
    ///     Актуален только во время жизненного цикла компоновки.
    /// </summary>
    protected System.Collections.Generic.List<Styles.StyleLayoutChildCanvas> _styleChildCanvasContexts = null;

    /// <summary>
    ///     Временная информация для компоновки. Актуально только во время жизненного цикла компоновки.
    /// </summary>
    protected UIExCanvasContext _context = null;





    /// <inheritdoc/>
    protected override void BeginLayoutContext()
    {
        base.BeginLayoutContext();

        _context = new();
        _context.styleLayoutContainer = StyleLayout.Canvas();

        _styleChildCanvasContexts = new(_elementsContext.Count);
        for (int i = 0; i < _styleElementsContexts.Count; i++)
            _styleChildCanvasContexts.Add(_styleElementsContexts[i].Canvas());
    }

    /// <inheritdoc/>
    protected override void EndLayoutContext()
    {
        base.EndLayoutContext();

        _context = null;
        _styleChildCanvasContexts = null;
    }
}