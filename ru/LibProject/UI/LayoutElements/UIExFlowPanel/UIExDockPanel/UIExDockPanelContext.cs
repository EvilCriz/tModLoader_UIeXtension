using System.Collections.Generic;
using Terraria.UI;
using UIeXtension.Styles;

namespace UIeXtension;

public partial class UIExDockPanel
{
    /// <summary>
    ///     Класс, содержающий временную информацию о текущей компоновке <see cref="UIExDockPanel"/>.
    /// </summary>
    protected class UIExDockPanelContext
    {
        /// <summary/>
        public StyleLayoutContainerDockPanel styleLayoutContainer;

        /// <summary/>
        public CalculatedStyle remainingDimensions;

        /// <summary/>
        public CalculatedStyle leftInnerDimensions = default(CalculatedStyle);
        /// <summary/>
        public CalculatedStyle rightInnerDimensions = default(CalculatedStyle);
        /// <summary/>
        public CalculatedStyle topInnerDimensions = default(CalculatedStyle);
        /// <summary/>
        public CalculatedStyle bottomInnerDimensions = default(CalculatedStyle);
        /// <summary/>
        public CalculatedStyle fillInnerDimensions = default(CalculatedStyle);

        /// <summary/>
        public CalculatedStyle leftOuterDimensions = default(CalculatedStyle);
        /// <summary/>
        public CalculatedStyle rightOuterDimensions = default(CalculatedStyle);
        /// <summary/>
        public CalculatedStyle topOuterDimensions = default(CalculatedStyle);
        /// <summary/>
        public CalculatedStyle bottomOuterDimensions = default(CalculatedStyle);
        /// <summary/>
        public CalculatedStyle fillOuterDimensions = default(CalculatedStyle);

        /// <summary/>
        public List<int> leftElementsIdx = null;
        /// <summary/>
        public List<int> rightElementsIdx = null;
        /// <summary/>
        public List<int> topElementsIdx = null;
        /// <summary/>
        public List<int> bottomElementsIdx = null;
        /// <summary/>
        public List<int> fillElementsIdx = null;

        /// <summary>
        ///     Порядок выделения пространства для сторон.
        /// </summary>
        public List<Enums.UIExSide> sideOrder = new();

        /// <summary>
        ///     Выравнивание по главной оси элементов в данной стороне.
        /// </summary>
        public Enums.UIExAlignment SideJustifyContent = Enums.UIExAlignment.Auto;

        /// <summary>
        ///     Выравнивание по поперечной оси элементов в данной стороне.
        /// </summary>
        public Enums.UIExAlignment SideAlignItems = Enums.UIExAlignment.Auto;
    }

    /// <summary>
    ///     Временная информация для компоновки. Актуально только во время жизненного цикла компоновки.
    /// </summary>
    protected UIExDockPanelContext _context = null;


    /// <inheritdoc/>
    protected override void BeginLayoutContext()
    {
        base.BeginLayoutContext();

        _context = new();
        _context.styleLayoutContainer = StyleLayout.DockPanel();
        _context.remainingDimensions = _innerDimensionsContext;

        //

        int count = _elementsContext.Count;

        //

        List<StyleLayoutChildDockPanel> styleChildDockPanel = new(count);
        for (int i = 0; i < count; i++)
        {
            var styleDockPanel = _styleElementsContexts[i].DockPanel();
            styleChildDockPanel.Add(styleDockPanel);
        }

        //

        HashSet<Enums.UIExSide> seen = new();
        foreach (var style in styleChildDockPanel)
            if (seen.Add(style.Side))
                _context.sideOrder.Add(style.Side);

        //

        int countLeft = 0, countRight = 0, countTop = 0, countBottom = 0, countFill = 0;

        foreach (var style in styleChildDockPanel)
        {
            switch (style.Side)
            {
                case Enums.UIExSide.Left:
                    countLeft++;
                    continue;
                case Enums.UIExSide.Right:
                    countRight++;
                    continue;
                case Enums.UIExSide.Top:
                    countTop++;
                    continue;
                case Enums.UIExSide.Bottom:
                    countBottom++;
                    continue;
                case Enums.UIExSide.Fill:
                    countFill++;
                    continue;
            }
        }

        //

        _context.leftElementsIdx = new(countLeft);
        _context.rightElementsIdx = new(countRight);
        _context.topElementsIdx = new(countTop);
        _context.bottomElementsIdx = new(countBottom);
        _context.fillElementsIdx = new(countFill);

        for(int i = 0; i < styleChildDockPanel.Count; i++)
        {
            var style = styleChildDockPanel[i];

            switch (style.Side)
            {
                case Enums.UIExSide.Left:
                    _context.leftElementsIdx.Add(i);
                    continue;
                case Enums.UIExSide.Right:
                    _context.rightElementsIdx.Add(i);
                    continue;
                case Enums.UIExSide.Top:
                    _context.topElementsIdx.Add(i);
                    continue;
                case Enums.UIExSide.Bottom:
                    _context.bottomElementsIdx.Add(i);
                    continue;
                case Enums.UIExSide.Fill:
                    _context.fillElementsIdx.Add(i);
                    continue;
            }
        }

        if (_context.sideOrder.Contains(Enums.UIExSide.Fill))
            _context.sideOrder.Remove(Enums.UIExSide.Fill);
    }

    /// <inheritdoc/>
    protected override void EndLayoutContext()
    {
        base.EndLayoutContext();

        _context = null;
    }
}