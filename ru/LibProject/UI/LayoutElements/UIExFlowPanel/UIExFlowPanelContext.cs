namespace UIeXtension;

/// <summary>
///     Базовый класс для <see cref="UIExStackPanel"/>, <see cref="UIExDockPanel"/>, <see cref="UIExWrapPanel"/>
/// </summary>
public abstract partial class UIExFlowPanel : UIExLayout
{
    /// <summary>
    ///     Класс, содержающий временную информацию о текущей компоновке <see cref="UIExFlowPanel"/>.
    /// </summary>
    protected class UIExFlowPanelContext
    {
        /// <summary>
        ///     Флаг, указывающий на текущую главную ось. 
        ///     Сокращенное сохранение <see cref="Styles.StyleLayoutContainer.Orientation"/> == <see cref="Enums.UIExOrientation.Vertical"/>
        /// </summary>
        public bool vertical;

        /// <summary>
        ///     Размер одного <see cref="Enums.UIExAlignment.Stretch"/> для главной оси.
        /// </summary>
        public float justifyStretchSize;

        /// <summary>
        ///     Размер одного <see cref="Enums.UIExAlignment.Stretch"/> для поперечной оси.
        /// </summary>
        public float alignStretchSize;

        /// <summary>
        ///     Список отступов для каждого элемента по главной оси.
        /// </summary>
        public float[] justifyOffsets;

        /// <summary>
        ///     Список отступов для каждого элемента по поперечной оси.
        /// </summary>
        public float[] alignOffsets;

        /// <summary>
        ///     Указывает, что в текущей компоновке не должны учитываться параметры:
        ///     <see cref="Styles.StyleLayoutContainer.JustifyContent"/> и <see cref="Styles.StyleLayoutChild.JustifySelf"/> 
        ///     (т.е. вся главная ось)
        /// </summary>
        public bool ignoreJustify = false;

        /// <summary>
        ///     Указывает, что в текущей компоновке не должны учитываться параметры:
        ///     <see cref="Styles.StyleLayoutContainer.AlignItems"/> и <see cref="Styles.StyleLayoutChild.AlignSelf"/> 
        ///     (т.е. вся поперечная ось)
        /// </summary>
        public bool ignoreAlign = false;

        /// <summary>
        ///     Указывает, что в текущей компоновке не должны учитываться параметры:
        ///     <see cref="Styles.StyleLayoutContainer.JustifyContent"/> и <see cref="Styles.StyleLayoutChild.JustifySelf"/>, если их значение равное
        ///     <see cref="Enums.UIExAlignment.Stretch"/>
        /// </summary>
        public bool ignoreJustifyStretch = false;

        /// <summary>
        ///     Указывает, что в текущей компоновке не должны учитываться параметры:
        ///     <see cref="Styles.StyleLayoutContainer.AlignItems"/> и <see cref="Styles.StyleLayoutChild.AlignSelf"/>, если их значение равное
        ///     <see cref="Enums.UIExAlignment.Stretch"/>
        /// </summary>
        public bool ignoreAlignStretch = false;
    }

    /// <summary>
    ///     Хранит данные для текущей компоновки <see cref="UIExStackPanel"/>.
    ///     Актуален только во время жизненного цикла компоновки.
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
    ///     Сбрасывает все значения контекста.
    /// </summary>
    protected virtual void ResetFlowContext()
        => _flowContext = new();
}