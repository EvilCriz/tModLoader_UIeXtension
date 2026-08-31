using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI;
using Terraria.UI;
using UIeXtension;
using UIeXtension.Enums;
using UIeXtension.MethodsExtensions;
using UIeXtension.Styles;

namespace UIeXtensionsExample;

public class TutorialUIExCanvasState : UIState
{
    // The OnActivate() method is used here only
    // for the "hot code reload" feature in the development environment.
    // For a real interface, it would be more appropriate to use
    // the OnInitialize() method.
    public override void OnActivate()
    {
        RemoveAllChildren();

        UIPanel panel = new UIPanel();
        SetPrecentSizeCenter(0.8f, 0.8f, panel);

        UIExCanvas canvas = new UIExCanvas();
        canvas.Stretch();
        canvas.ShowLayoutLines = true;
        canvas.LayoutLinesThickness = 3f;
        canvas.LayoutLinesColor = Color.DarkOrange;

        canvas.StyleLayout.Set(
            orientation: UIExOrientation.Horizontal,
            justifyContent: UIExAlignment.Center);

        panel.Append(canvas);
        Append(panel);

        UIElement[] cubs = AddCubs(canvas, 50, 16);
        StyleLayoutChildCanvas[] stylesCanvas = new StyleLayoutChildCanvas[cubs.Length];
        StyleLayoutChild[] styles = new StyleLayoutChild[cubs.Length];

        for (int i = 0; i < cubs.Length; i++)
        {
            styles[i] = cubs[i].StyleLayoutChild();
            stylesCanvas[i] = styles[i].Canvas();
        }


        ////

        styles[0].JustifySelf = UIExAlignment.Start;
        styles[0].AlignSelf = UIExAlignment.Start;

        // styles[1]: JustifySelf == Auto == JustifyContent(canvas) == Center
        // styles[1]: AlignSelf == Auto == AlignContent(canvas) == Auto == Start

        // styles[2]: AlignSelf == Auto == AlignContent(canvas) == Auto == Start
        styles[2].JustifySelf = UIExAlignment.End;

        styles[3].JustifySelf = UIExAlignment.Start;
        styles[3].AlignSelf = UIExAlignment.Center;

        // styles[4]: JustifySelf == Auto == JustifyContent(canvas) == Center
        styles[4].AlignSelf = UIExAlignment.Center;

        styles[5].JustifySelf = UIExAlignment.End;
        styles[5].AlignSelf = UIExAlignment.Center;

        styles[6].JustifySelf = UIExAlignment.Stretch;
        styles[6].AlignSelf = UIExAlignment.End;
        styles[6].Margin = new UIExThickness(
                                    pixels: true,
                                    right: 25f,
                                    bottom: 50f,
                                    left: 75f,
                                    top: 100f);

        ////////////////
        ////////////////
        ////////////////

        CopyStyle4(indexStart: 7, indexEnd: 15);

        stylesCanvas[7].Top.Pixels = 50f;
        stylesCanvas[8].Bottom.Pixels = 50f;
        stylesCanvas[9].Left.Pixels = 50f;
        stylesCanvas[10].Right.Pixels = 50f;

        stylesCanvas[11].Bottom.Pixels = 50f;
        stylesCanvas[11].Right.Pixels = 50f;

        styles[12].Margin = new UIExThickness(
                                pixels: true,
                                bottom: 50f,
                                right: 100f);
        stylesCanvas[12].Left.Precent = -0.5f;

        stylesCanvas[13].Left.Pixels = 100000f;
        stylesCanvas[13].Top.Pixels = 50f;

        stylesCanvas[14].Left.Precent = 0.5f;

        stylesCanvas[15].Left.Precent = 1.1f;
        stylesCanvas[15].AllowOverflowSelf = true;

        void CopyStyle4(int indexStart, int indexEnd)
        {
            for (int i = indexStart; i <= indexEnd; i++)
                styles[i].Copy(styles[4]);
        }
    }

    public void SetPrecentSizeCenter(float width, float height, params UIElement[] elements)
    {
        foreach (var element in elements)
        {
            element.Width.Set(0f, width);
            element.Height.Set(0f, height);
            element.HAlign = 0.5f;
            element.VAlign = 0.5f;
        }
    }

    public UIElement[] AddCubs(UIElement element, params float[] pixelSizes)
    {
        int count = pixelSizes.Length;
        var buttons = new UIElement[count];
        for (int i = 0; i < count; i++)
            buttons[i] = AddCubs(element, pixelSizes[i], count: 1)[0];
        return buttons;
    }

    public UIElement[] AddCubs(UIElement element, float pixelSize, int count = 1)
    {
        var buttons = new UIElement[count];
        for (int i = 0; i < count; i++)
        {
            buttons[i] = new UIButton<string>(i.ToString());
            StyleLayoutChild style = buttons[i].StyleLayoutChild();
            style.Width.Set(pixelSize, 0f);
            style.Height.Set(pixelSize, 0f);
            element.Append(buttons[i]);
        }
        return buttons;
    }
}