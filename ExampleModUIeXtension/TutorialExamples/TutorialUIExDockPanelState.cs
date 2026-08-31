using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI;
using Terraria.UI;
using UIeXtension;
using UIeXtension.Enums;
using UIeXtension.MethodsExtensions;
using UIeXtension.Styles;

namespace UIeXtensionExample;

public class TutorialUIExDockPanelState : UIState
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

        UIExDockPanel dockPanel = new UIExDockPanel();
        dockPanel.Stretch();

        dockPanel.ShowLayoutLines = true;
        dockPanel.LayoutLinesThickness = 3f;
        dockPanel.LayoutLinesColor = Color.DarkOrange;

        StyleLayoutContainer styleLayout = dockPanel.StyleLayout;
        StyleLayoutContainerDockPanel styleLayoutDock = styleLayout.DockPanel();

        styleLayout.JustifyContent = UIExAlignment.Center;
        styleLayout.AlignItems = UIExAlignment.Center;

        ////////////////

        StyleSide sideBottom = styleLayoutDock.SideBottom;
        sideBottom.Spacing.Pixels = 100f;

        UIElement[] cubsBottom = AddCubs(dockPanel, "b", pixelSize: 50f, 1);
        StyleLayoutChild[] cubsStylesBottom = new StyleLayoutChild[cubsBottom.Length];
        StyleLayoutChildDockPanel[] cubsStylesDockBottom = new StyleLayoutChildDockPanel[cubsBottom.Length];

        for (int i = 0; i < cubsBottom.Length; i++)
        {
            cubsStylesBottom[i] = cubsBottom[i].StyleLayoutChild();
            cubsStylesDockBottom[i] = cubsStylesBottom[i].DockPanel();
            cubsStylesDockBottom[i].Side = UIExSide.Bottom;
        }

        cubsStylesBottom[0].Margin = new(pixels: true, size: 25f);
        cubsStylesBottom[0].JustifySelf = UIExAlignment.Stretch;

        ////////////////

        StyleSide sideLeft = styleLayoutDock.SideLeft;
        sideLeft.Spacing.Pixels = 50f;
        sideLeft.AlignItems = UIExAlignment.End;

        UIElement[] cubsLeft = AddCubs(dockPanel, "l", pixelSize: 50f, 4);
        StyleLayoutChild[] cubsStylesLeft = new StyleLayoutChild[cubsLeft.Length];
        StyleLayoutChildDockPanel[] cubsStylesDockLeft = new StyleLayoutChildDockPanel[cubsLeft.Length];

        for (int i = 0; i < cubsLeft.Length; i++)
        {
            cubsStylesLeft[i] = cubsLeft[i].StyleLayoutChild();
            cubsStylesDockLeft[i] = cubsStylesLeft[i].DockPanel();
            cubsStylesDockLeft[i].Side = UIExSide.Left;
        }

        cubsStylesLeft[1].AlignSelf = UIExAlignment.Start;
        cubsStylesLeft[2].Margin = new(pixels: true, 50f);

        /////////////////

        StyleSide sideTop = styleLayoutDock.SideTop;
        sideTop.Spacing.Pixels = 10f;
        sideTop.JustifyContent = UIExAlignment.End;

        UIElement[] cubsTop = AddCubs(dockPanel, "t", pixelSize: 50f, 3);
        StyleLayoutChild[] cubsStylesTop = new StyleLayoutChild[cubsTop.Length];
        StyleLayoutChildDockPanel[] cubsStylesDockTop = new StyleLayoutChildDockPanel[cubsTop.Length];

        for (int i = 0; i < cubsTop.Length; i++)
        {
            cubsStylesTop[i] = cubsTop[i].StyleLayoutChild();
            cubsStylesDockTop[i] = cubsStylesTop[i].DockPanel();
            cubsStylesDockTop[i].Side = UIExSide.Top;
        }

        cubsStylesTop[1].Margin = new(pixels: true, 10f);

        /////////////////

        StyleSide sideRight = styleLayoutDock.SideRight;
        sideRight.Spacing.Pixels = 30f;
        sideRight.JustifyContent = UIExAlignment.End;
        sideRight.Padding = new UIExThickness(pixels: true, size: 20f);

        UIElement[] cubsRight = AddCubs(dockPanel, "r", pixelSize: 50f, 5);
        StyleLayoutChild[] cubsStylesRight = new StyleLayoutChild[cubsRight.Length];
        StyleLayoutChildDockPanel[] cubsStylesDockRight = new StyleLayoutChildDockPanel[cubsRight.Length];

        for (int i = 0; i < cubsRight.Length; i++)
        {
            cubsStylesRight[i] = cubsRight[i].StyleLayoutChild();
            cubsStylesDockRight[i] = cubsStylesRight[i].DockPanel();
            cubsStylesDockRight[i].Side = UIExSide.Right;
        }

        cubsStylesRight[0].Margin = new(pixels: true, left: 20f, right: 20f);

        /////////////////

        styleLayout.Orientation = UIExOrientation.Horizontal;

        StyleSide fillRight = styleLayoutDock.SideFill;
        fillRight.Spacing.Pixels = 30f;
        fillRight.JustifyContent = UIExAlignment.Center;
        fillRight.Padding = new UIExThickness(pixels: true, size: 20f);

        UIElement[] cubsFill = AddCubs(dockPanel, "f", pixelSize: 50f, 2);
        StyleLayoutChild[] cubsStylesFill = new StyleLayoutChild[cubsFill.Length];
        StyleLayoutChildDockPanel[] cubsStylesDockFill = new StyleLayoutChildDockPanel[cubsFill.Length];

        for (int i = 0; i < cubsFill.Length; i++)
        {
            cubsStylesFill[i] = cubsFill[i].StyleLayoutChild();
            cubsStylesDockFill[i] = cubsStylesFill[i].DockPanel();
            cubsStylesDockFill[i].Side = UIExSide.Fill;
        }

        ///////////////////

        panel.Append(dockPanel);
        Append(panel);
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

    public UIElement[] AddCubs(UIElement element, string prefix, params float[] pixelSizes)
    {
        int count = pixelSizes.Length;
        var buttons = new UIElement[count];
        for (int i = 0; i < count; i++)
            buttons[i] = AddCubs(element, prefix, pixelSizes[i], count: 1)[0];
        return buttons;
    }

    public UIElement[] AddCubs(UIElement element, string prefix, float pixelSize, int count = 1)
    {
        var buttons = new UIElement[count];
        for (int i = 0; i < count; i++)
        {
            buttons[i] = new UIButton<string>($"{prefix}{i}");
            StyleLayoutChild style = buttons[i].StyleLayoutChild();
            style.Width.Set(pixelSize, 0f);
            style.Height.Set(pixelSize, 0f);
            element.Append(buttons[i]);
        }
        return buttons;
    }
}