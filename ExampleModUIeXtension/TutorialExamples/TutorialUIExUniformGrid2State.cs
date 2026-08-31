using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI;
using Terraria.UI;
using UIeXtension;
using UIeXtension.Enums;
using UIeXtension.MethodsExtensions;
using UIeXtension.Styles;

namespace UIeXtensionExample;

public class TutorialUIExUniformGrid2State : UIState
{
    // The OnActivate() method is used here only
    // for the "hot code reload" feature in the development environment.
    // For a real interface, it would be more appropriate to use
    // the OnInitialize() method.
    public override void OnActivate()
    {
        RemoveAllChildren();

        UIPanel panel = new UIPanel();
        SetPixelsSizeCenter(width: 450f, height: 650f, panel);

        UIExUniformGrid uGrid = new();
        uGrid.Stretch();

        ////////////////////////////

        StyleLayoutContainer styleLayout = uGrid.StyleLayout;
        StyleLayoutContainerUniformGrid styleLayoutUGrid = styleLayout.UniformGrid();

        styleLayout.Set(
            orientation: UIExOrientation.Vertical,
            justifyContent: UIExAlignment.Stretch,
            alignItems: UIExAlignment.Stretch);



        styleLayoutUGrid.Set(
            rowsCount: 7,
            columnsCount: 4,
            rowsSpace: StyleDimension.FromPixels(10f),
            columnSpace: StyleDimension.FromPixels(15f));

        ///////////////////////////

        AppendButton(uGrid, prefix: "RESULT:", rowSpan: 2, columnSpan: 4);

        AppendButton(uGrid, prefix: "AC");
        AppendButton(uGrid, prefix: "%");
        AppendButton(uGrid, prefix: "<<");
        AppendButton(uGrid, prefix: "/");

        AppendButton(uGrid, prefix: 7);
        AppendButton(uGrid, prefix: 8);
        AppendButton(uGrid, prefix: 9);
        AppendButton(uGrid, prefix: "*");

        AppendButton(uGrid, prefix: 4);
        AppendButton(uGrid, prefix: 5);
        AppendButton(uGrid, prefix: 6);
        AppendButton(uGrid, prefix: "-");

        AppendButton(uGrid, prefix: 1);
        AppendButton(uGrid, prefix: 2);
        AppendButton(uGrid, prefix: 3);
        AppendButton(uGrid, prefix: "+");

        AppendButton(uGrid, prefix: 0);
        AppendButton(uGrid, prefix: ",");
        AppendButton(uGrid, prefix: "=", columnSpan: 2);

        ///////////////////////////

        panel.Append(uGrid);
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

    public UIButton<string> AppendButton(UIExLayout layout, int prefix, int rowSpan = 1, int columnSpan = 1)
        => AppendButton(layout, prefix.ToString(), rowSpan, columnSpan);
    public UIButton<string> AppendButton(UIExLayout layout, string prefix, int rowSpan = 1, int columnSpan = 1)
    {
        UIButton<string> button = new(prefix);

        StyleLayoutChildUniformGrid styleGrid = button.StyleLayoutChild().UniformGrid();
        styleGrid.Set(rowSpan, columnSpan);

        layout.Append(button);
        return button;
    }
}