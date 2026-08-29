namespace UIeXtension;

/// <summary>
///     Basic class for <see cref="UIExStackPanel"/>. <see cref="UIExDockPanel"/>. <see cref="UIExWrapPanel"/>
/// </summary>
public abstract partial class UIExFlowPanel : UIExLayout
{
    /// <summary>
    ///     Class containing temporary information about the current layout <see cref="UIExFlowPanel"/>.
    /// </summary>
    protected class UIExFlowPanelContext
    {
        /// <summary>
        ///     A flag indicating the current main axis. 
        ///     Reduced conservation <see cref="Styles.StyleLayoutContainer.Orientation"/> === <see cref="Enums.UIExOrientation.Vertical"/>
        /// </summary>
        public bool vertical;

        /// <summary>
        ///     Size of one <see cref="Enums.UIExAlignment.Stretch"/> for the main axis.
        /// </summary>
        public float justifyStretchSize;

        /// <summary>
        ///     Size of one <see cref="Enums.UIExAlignment.Stretch"/> for the transverse axis.
        /// </summary>
        public float alignStretchSize;

        /// <summary>
        ///     List of indentations for each element on the main axis.
        /// </summary>
        public float[] justifyOffsets;

        /// <summary>
        ///     List of indentations for each element along the transverse axis.
        /// </summary>
        public float[] alignOffsets;

        /// <summary>
        ///     Indicates that the current layout should not take into account the parameters:
        ///     <see cref="Styles.StyleLayoutContainer.JustifyContent"/> and <see cref="Styles.StyleLayoutChild.JustifySelf"/> 
        ///     (i.e. the entire main axis)
        /// </summary>
        public bool ignoreJustify = false;

        /// <summary>
        ///     Indicates that the current layout should not take into account the parameters:
        ///     <see cref="Styles.StyleLayoutContainer.AlignItems"/> and <see cref="Styles.StyleLayoutChild.AlignSelf"/> 
        ///     (i.e. all transverse axis)
        /// </summary>
        public bool ignoreAlign = false;

        /// <summary>
        ///     Indicates that the current layout should not take into account the parameters:
        ///     <see cref="Styles.StyleLayoutContainer.JustifyContent"/> and <see cref="Styles.StyleLayoutChild.JustifySelf"/>if their value is equal
        ///     <see cref="Enums.UIExAlignment.Stretch"/>
        /// </summary>
        public bool ignoreJustifyStretch = false;

        /// <summary>
        ///     Indicates that the current layout should not take into account the parameters:
        ///     <see cref="Styles.StyleLayoutContainer.AlignItems"/> and <see cref="Styles.StyleLayoutChild.AlignSelf"/>if their value is equal
        ///     <see cref="Enums.UIExAlignment.Stretch"/>
        /// </summary>
        public bool ignoreAlignStretch = false;
    }

    /// <summary>
    ///     Stores data for the current layout <see cref="UIExStackPanel"/>.
    ///     Relevant only during the life cycle of the layout.
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
    ///     Resets all meanings of context.
    /// </summary>
    protected virtual void ResetFlowContext()
        => _flowContext = new();
}