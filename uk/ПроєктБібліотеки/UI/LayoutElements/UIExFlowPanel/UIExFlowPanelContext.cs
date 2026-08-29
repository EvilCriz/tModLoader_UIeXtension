namespace UIeXtension;

/// <summary>
///     Базовий клас <see cref="UIExStackPanel"/>. . <see cref="UIExDockPanel"/>. . <see cref="UIExWrapPanel"/>
/// </summary>
public abstract partial class UIExFlowPanel : UIExLayout
{
    /// <summary>
    ///     Клас, що містить тимчасову інформацію про поточне планування <see cref="UIExFlowPanel"/>. .
    /// </summary>
    protected class UIExFlowPanelContext
    {
        /// <summary>
        ///     прапорець, що вказує на поточну основну вісь. 
        ///     Зниження консервації <see cref="Styles.StyleLayoutContainer.Orientation"/> ================================================================================================================================================================================================================================================================ <see cref="Enums.UIExOrientation.Vertical"/>
        /// </summary>
        public bool vertical;

        /// <summary>
        ///     Розмір одного <see cref="Enums.UIExAlignment.Stretch"/> для основної осі.
        /// </summary>
        public float justifyStretchSize;

        /// <summary>
        ///     Розмір одного <see cref="Enums.UIExAlignment.Stretch"/> для поперечної осі.
        /// </summary>
        public float alignStretchSize;

        /// <summary>
        ///     Список відступів для кожного елемента на основну вісь.
        /// </summary>
        public float[] justifyOffsets;

        /// <summary>
        ///     Перелік відступів для кожного елемента по поперечній віссі.
        /// </summary>
        public float[] alignOffsets;

        /// <summary>
        ///     Призначає, що поточна верстка не повинна враховувати параметри:
        ///     <see cref="Styles.StyleLayoutContainer.JustifyContent"/> і <see cref="Styles.StyleLayoutChild.JustifySelf"/> 
        ///     (тобто вся основна вісь)
        /// </summary>
        public bool ignoreJustify = false;

        /// <summary>
        ///     Призначає, що поточна верстка не повинна враховувати параметри:
        ///     <see cref="Styles.StyleLayoutContainer.AlignItems"/> і <see cref="Styles.StyleLayoutChild.AlignSelf"/> 
        ///     (i.e. всі поперечні осі)
        /// </summary>
        public bool ignoreAlign = false;

        /// <summary>
        ///     Призначає, що поточна верстка не повинна враховувати параметри:
        ///     <see cref="Styles.StyleLayoutContainer.JustifyContent"/> і <see cref="Styles.StyleLayoutChild.JustifySelf"/>якщо їх значення дорівнює
        ///     <see cref="Enums.UIExAlignment.Stretch"/>
        /// </summary>
        public bool ignoreJustifyStretch = false;

        /// <summary>
        ///     Призначає, що поточна верстка не повинна враховувати параметри:
        ///     <see cref="Styles.StyleLayoutContainer.AlignItems"/> і <see cref="Styles.StyleLayoutChild.AlignSelf"/>якщо їх значення дорівнює
        ///     <see cref="Enums.UIExAlignment.Stretch"/>
        /// </summary>
        public bool ignoreAlignStretch = false;
    }

    /// <summary>
    ///     Зберігати дані для поточного макета <see cref="UIExStackPanel"/>. .
    ///     Відновити тільки під час життєвого циклу макета.
    /// </summary>
    protected UIExFlowPanelContext _flowContext;


    /// <inheritdoc/>
    protected override void BeginLayoutContext()
    {
        base.BeginLayoutContext();
        _flowContext = new();
    }

    /// <inheritdoc/>
    protected override void EndLayoutContext()
    {
        base.EndLayoutContext();
        _flowContext = null;
    }

    /// <summary>
    ///     Створює всі значення контексту.
    /// </summary>
    protected virtual void ResetFlowContext()
        => _flowContext = new();
}