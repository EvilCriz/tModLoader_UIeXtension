using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI;
using Terraria.UI;
using UIeXtension;
using UIeXtension.Enums;
using UIeXtension.MethodsExtensions;
using UIeXtension.Styles;

namespace UIeXtensionExample;

public class TutorialUIExWrapPanel1State : UIState
{
    // the OnActivate() method in this case is used only
    // for the «hot code reload» functionality in the development environment.
    // for a real interface, it would be more appropriate to use
    // the OnInitialize() method
    public override void OnActivate()
    {
        RemoveAllChildren();

        UIPanel panel = new UIPanel();
        SetPixelsSizeCenter(width: 450f, height: 450f, panel);

        UIExWrapPanel wrapPanel = new();
        wrapPanel.Stretch();

        wrapPanel.SetLayoutLinesInfoForBranch(
            state: true,
            thickness: 3f,
            color: Color.DarkOrange);

        ////////////////////////////

        StyleLayoutContainer styleLayout = wrapPanel.StyleLayout;
        StyleLayoutContainerWrapPanel styleLayoutWrap = styleLayout.WrapPanel();

        styleLayout.Set(
            orientation: UIExOrientation.Horizontal,
            justifyContent: UIExAlignment.Center,
            alignItems: UIExAlignment.Center);

        styleLayoutWrap.Set(
            spacingWithinLine: StyleDimension.FromPixels(10f),
            spacingBetweenLines: StyleDimension.FromPixels(15f),
            alignLines: UIExAlignment.Center,
            // 4 combinations of these two values are shown in the images
            reverseWithinLine: true,
            reverseAll: true); //

        ///////////////////////////

        AddPixelButton(wrapPanel, prefix: 0, width: 200f, height: 200f);
        AddPixelButton(wrapPanel, prefix: 1, width: 200f, height: 200f);
        AddPixelButton(wrapPanel, prefix: 2, width: 200f, height: 200f);
        AddPixelButton(wrapPanel, prefix: 3, width: 200f, height: 200f);

        ///////////////////////////

        panel.Append(wrapPanel);
        Append(panel);
    }

    public void SetPixelsSizeCenter(float width, float height, params UIElement[] elements)
    {
        foreach (var element in elements)
        {
            element.Width.Set(width, 0f);
            element.Height.Set(height, 0f);
            element.HAlign = 0.5f;
            element.VAlign = 0.5f;
        }
    }

    public UIButton<string> AddPixelButton(UIExLayout layout, int prefix, float width, float height)
        => AddPixelButton(layout, prefix.ToString(), width, height);
    public UIButton<string> AddPixelButton(UIExLayout layout, string prefix, float width, float height)
    {
        UIButton<string> button = new($"{prefix}");
        StyleLayoutChild style = button.StyleLayoutChild();
        style.Width.Pixels = width;
        style.Height.Pixels = height;
        layout.Append(button);
        return button;
    }
}