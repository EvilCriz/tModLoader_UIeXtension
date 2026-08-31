using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Microsoft.Xna.Framework;
using UIeXtension;
using UIeXtension.Enums;
using UIeXtension.MethodsExtensions;
using UIeXtension.Styles;

namespace UIeXtensionExample;

public class TutorialUIExStackPanelState : UIState
{
    // the OnActivate() method is used in this case only
    // for the «hot reload» feature in the development environment.
    // for a real interface, it would be more appropriate to use
    // the OnInitialize() method
    public override void OnActivate()
    {
        RemoveAllChildren();

        UIPanel panel = new UIPanel();
        SetPrecentSizeCenter(0.8f, 0.8f, panel);

        UIExStackPanel HStackPanel = new();
        HStackPanel.Stretch();

        HStackPanel.StyleLayout.Set(
            orientation: UIExOrientation.Horizontal,
            justifyContent: UIExAlignment.Stretch,
            alignItems: UIExAlignment.Stretch);
        HStackPanel.StyleLayout.StackPanel().Set(
            spacing: new StyleDimension(0f, 0.05f),
            reverse: false);

        panel.Append(HStackPanel);

        //////

        UIPanel HPanel1 = new(), HPanel2 = new(), HPanel3 = new(), HPanel4 = new();
        HStackPanel.Append(HPanel1);
        HStackPanel.Append(HPanel2);
        HStackPanel.Append(HPanel3);
        HStackPanel.Append(HPanel4);

        /////

        UIExStackPanel VStackPanel1 = new(), VStackPanel2 = new(), VStackPanel3 = new(), VStackPanel4 = new();
        SetPrecentSizeCenter(1f, 1f, VStackPanel1, VStackPanel2, VStackPanel3, VStackPanel4);
        HPanel1.Append(VStackPanel1);
        HPanel2.Append(VStackPanel2);
        HPanel3.Append(VStackPanel3);
        HPanel4.Append(VStackPanel4);

        VStackPanel1.StyleLayout.Set(
            orientation: UIExOrientation.Vertical,
            justifyContent: UIExAlignment.Start,
            alignItems: UIExAlignment.Start);
        VStackPanel1.StyleLayout.StackPanel().Set(
            spacing: new StyleDimension(0f, 0.1f),
            reverse: false);

        UIElement[] cubs2_1 = AddCubs(VStackPanel1, pixelSize: 64f, count: 3);


        StyleLayoutChild stylePanel2_1 = cubs2_1[1].StyleLayoutChild();

        stylePanel2_1.AlignSelf = UIExAlignment.End;

        ////

        VStackPanel2.StyleLayout.Set(
            orientation: UIExOrientation.Vertical,
            justifyContent: UIExAlignment.Center,
            alignItems: UIExAlignment.Center);
        VStackPanel2.StyleLayout.StackPanel().Set(
            spacing: new StyleDimension(0f, 0.15f),
            reverse: true);

        UIElement[] cubs2_2 = AddCubs(VStackPanel2, 16f, 32f, 48f, 64f);


        StyleLayoutChild stylePanel2_2 = cubs2_2[2].StyleLayoutChild();

        stylePanel2_2.Set(
            // does nothing, UIExStackPanel does not take JustifySelf into account,
            // except for the Stretch value
            justifySelf: UIExAlignment.End,
            alignSelf: UIExAlignment.Stretch,
            width: stylePanel2_2.Width,
            height: stylePanel2_2.Height,
            margin: new UIExThickness(
                                    pixels: false,
                                    left: 0.25f,
                                    top: 0.075f,
                                    right: 0f,
                                    bottom: 0.015f));

        ////

        VStackPanel3.StyleLayout.Set(
            orientation: UIExOrientation.Vertical,
            justifyContent: UIExAlignment.End,
            alignItems: UIExAlignment.End);
        VStackPanel3.StyleLayout.StackPanel().Set(
            spacing: new StyleDimension(0f, 0.20f),
            reverse: false);

        AddCubs(VStackPanel3, 32f, 48f, 64f);

        ////

        VStackPanel4.StyleLayout.Set(
            orientation: UIExOrientation.Vertical,
            justifyContent: UIExAlignment.Stretch,
            alignItems: UIExAlignment.Stretch);
        VStackPanel4.StyleLayout.StackPanel().Set(
            spacing: new StyleDimension(0f, 0.20f),
            reverse: false);

        AddCubs(VStackPanel4, pixelSize: 60f);
        AddCubs(VStackPanel4, pixelSize: 50f);
        AddCubs(VStackPanel4, pixelSize: 70f);

        ////

        HStackPanel.SetLayoutLinesInfoForBranch(
            state: true,
            thickness: 3f,
            color: Color.DarkOrange);

        ///

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

    public UIElement[] AddCubs(UIElement element, params float[] pixelSizes)
    {
        int count = pixelSizes.Length;
        var panels = new UIElement[count];
        for (int i = 0; i < count; i++)
            panels[i] = AddCubs(element, pixelSizes[i], count: 1)[0];
        return panels;
    }

    public UIElement[] AddCubs(UIElement element, float pixelSize, int count = 1)
    {
        var panels = new UIElement[count];
        for (int i = 0; i < count; i++)
        {
            panels[i] = new UIPanel();
            StyleLayoutChild style = panels[i].StyleLayoutChild();
            style.Width.Set(pixelSize, 0f);
            style.Height.Set(pixelSize, 0f);
            element.Append(panels[i]);
        }
        return panels;
    }
}