using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI;
using Terraria.UI;
using UIeXtension;
using UIeXtension.Enums;
using UIeXtension.MethodsExtensions;
using UIeXtension.Styles;

namespace UIeXtensionExample;

public class TutorialUIExWrapPanel2State : UIState
{
    // the OnActivate() method in this case is used only
    // for the «hot code reload» functionality in the development environment.
    // for a real interface, it would be more appropriate to use
    // the OnInitialize() method
    public override void OnActivate()
    {
        RemoveAllChildren();

        UIPanel panel = new UIPanel();
        SetPixelsSizeCenter(width: 450f, height: 650f, panel);

        UIExWrapPanel wrapPanel = new();
        wrapPanel.Stretch();

        ////////////////////////////

        StyleLayoutContainer styleLayout = wrapPanel.StyleLayout;
        StyleLayoutContainerWrapPanel styleLayoutWrap = styleLayout.WrapPanel();

        styleLayout.Set(
            orientation: UIExOrientation.Horizontal,
            justifyContent: UIExAlignment.Center,
            alignItems: UIExAlignment.Center);

        styleLayoutWrap.Set(
            spacingWithinLine: StyleDimension.FromPixels(10f),
            spacingBetweenLines: StyleDimension.FromPixels(5f),
            alignLines: UIExAlignment.Center,
            reverseWithinLine: false,
            reverseAll: false);

        ///////////////////////////

        AppendButton(wrapPanel, prefix: "RESULT:");

        AppendButton(wrapPanel, prefix: "AC");
        AppendButton(wrapPanel, prefix: "%");
        AppendButton(wrapPanel, prefix: "<<");
        AppendButton(wrapPanel, prefix: "/");

        AppendButton(wrapPanel, prefix: 7);
        AppendButton(wrapPanel, prefix: 8);
        AppendButton(wrapPanel, prefix: 9);
        AppendButton(wrapPanel, prefix: "*");

        AppendButton(wrapPanel, prefix: 4);
        AppendButton(wrapPanel, prefix: 5);
        AppendButton(wrapPanel, prefix: 6);
        AppendButton(wrapPanel, prefix: "-");

        AppendButton(wrapPanel, prefix: 1);
        AppendButton(wrapPanel, prefix: 2);
        AppendButton(wrapPanel, prefix: 3);
        AppendButton(wrapPanel, prefix: "+");

        AppendButton(wrapPanel, prefix: 0);
        AppendButton(wrapPanel, prefix: ",");
        AppendButton(wrapPanel, prefix: "=");

        int count = 0;
        foreach (var child in wrapPanel.Children)
            count++;

        UIButton<string>[] buttons = new UIButton<string>[count];
        count = 0;
        foreach (var child in wrapPanel.Children)
            buttons[count++] = (UIButton<string>)child;

        for (int i = 0; i < count; i++)
        {
            StyleLayoutChild style = buttons[i].StyleLayoutChild();
            if (i == 0)
            {
                style.Width.Precent = 1f;
                style.Height.Precent = 2f / 7.5f;
            }
            else
            {
                style.Width.Precent = 1 / 4.5f;
                style.Height.Precent = 1 / 7.5f;
            }

            if (i == count - 1)
                style.Width.Precent *= 2f;
        }

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

    public UIButton<string> AppendButton(UIExLayout layout, int prefix)
        => AppendButton(layout, prefix.ToString());
    public UIButton<string> AppendButton(UIExLayout layout, string prefix)
    {
        UIButton<string> button = new(prefix);
        layout.Append(button);
        return button;
    }
}