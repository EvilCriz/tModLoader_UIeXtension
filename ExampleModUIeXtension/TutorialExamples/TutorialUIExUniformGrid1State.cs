using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI;
using Terraria.UI;
using UIeXtension;
using UIeXtension.Enums;
using UIeXtension.MethodsExtensions;
using UIeXtension.Styles;

namespace UIeXtensionExample;

public class TutorialUIExUniformGrid1State : UIState
{
    // The OnActivate() method is used here only
    // for the "hot code reload" feature in the development environment.
    // For a real interface, it would be more appropriate to use
    // the OnInitialize() method..
    public override void OnActivate()
    {
        RemoveAllChildren();

        // A greatly simplified Minesweeper game layout.

        // Creates a panel occupying 60% of the screen and positioned in the center.
        UIPanel panel = new();
        Append(panel);

        // A method written for demonstration purposes. NOT defined in the library itself.
        SetPrecentSize(panel, left: 0.2f, top: 0.2f, width: 0.6f, height: 0.6f);


        /////////////////////////////////


        // Creates a 10x10 UniformGrid and adds buttons to it.
        // The Grid panel is placed inside panel and occupies width: 50%; height: 80% of panel.
        UIExUniformGrid grid = new();
        panel.Append(grid);

        SetPrecentSize(grid, left: 0.25f, top: 0.1f, width: 0.5f, height: 0.8f);

        int rows = 10, columns = 10;


        /////////////////////////////////


        // Specifies that the buttons added to the UniformGrid should occupy 100% of each cell.
        grid.StyleLayout.JustifyContent = UIExAlignment.Stretch;
        grid.StyleLayout.AlignItems = UIExAlignment.Stretch;

        for (int row = 0; row < rows; row++)
            for (int column = 0; column < columns; column++)
            {
                UIButton<string> button = new($"[{row},{column}]");
                grid.Append(button);
            }

        // Creates the text. It is added to the grid.
        UIText text = new("GAME OVER");
        text.TextColor = Color.DarkRed;
        StyleLayoutChild styleText = text.StyleLayoutChild();

        // Specifies that the UniformGrid should not control the layout of text.
        // This means that the tModLoader Canvas will be used.
        styleText.WithoutLayout = true;

        // Specifies that the text should be exactly in the center of the grid.
        text.HAlign = 0.5f;
        text.VAlign = 0.5f;
        grid.Append(text);
    }

    public void SetPrecentSize(UIElement element, float left, float top, float width, float height)
    {
        element.Left.Set(0f, left);
        element.Top.Set(0f, top);
        element.Width.Set(0f, width);
        element.Height.Set(0f, height);
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