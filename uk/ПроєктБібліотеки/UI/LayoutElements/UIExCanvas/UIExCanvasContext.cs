namespace UIeXtension;
public partial class UIExCanvas
{
    /// <summary>
    ///     Клас, що містить тимчасову інформацію про поточне планування <see cref="UIExCanvas"/>. .
    /// </summary>
    protected class UIExCanvasContext
    {
        /// <summary>
        ///     Поточна спрямованість головної осі.
        /// </summary>
        public bool vertical;

        /// <summary>
        ///     Відправки елементів вздовж основної осі.
        /// </summary>
        public float[] justifyOffsets;

        /// <summary>
        ///     Знімки елементів вздовж поперечної осі.
        /// </summary>
        public float[] alignOffsets;

        /// <summary>
        ///     Фінальний стиль <see cref="UIExCanvas"/>, що використовується в контексті цієї композиції елементів.
        /// </summary>
        public Styles.StyleLayoutContainerCanvas styleLayoutContainer;
    }




    /// <summary>
    ///     Зберігає всі стилі дочірніх елементів, залучених до макету.  
    ///     Відновити тільки під час життєвого циклу макета.
    /// </summary>
    protected System.Collections.Generic.List<Styles.StyleLayoutChildCanvas> _styleChildCanvasContexts = null;

    /// <summary>
    ///     Тимчасова розмітка інформації. Тільки актуальна при плануванні життєвого циклу.
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