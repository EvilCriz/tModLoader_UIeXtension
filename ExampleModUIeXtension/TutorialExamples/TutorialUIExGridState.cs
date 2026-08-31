using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI;
using Terraria.UI;
using UIeXtension;
using UIeXtension.Enums;
using UIeXtension.MethodsExtensions;
using UIeXtension.Styles;

namespace UIeXtensionExample;

public class TutorialUIExGridState : UIState
{
    // the OnActivate() method is used in this case only
    // for the "hot code reload" functionality in the development environment.
    // For a real interface, it would be more appropriate to use
    // the OnInitialize() method.
    public override void OnActivate()
    {
        RemoveAllChildren();

        UIPanel panel = new UIPanel();
        SetPixelsSizeCenter(width: 450f, height: 650f, panel);

        UIExGrid grid = new();
        grid.Stretch();

        ////////////////////////////

        StyleLayoutContainer styleLayout = grid.StyleLayout;
        StyleLayoutContainerGrid styleLayoutGrid = styleLayout.Grid();

        styleLayout.Set(
            justifyContent: UIExAlignment.Stretch,
            alignItems: UIExAlignment.Stretch);

        styleLayoutGrid.AddRowDefinition(UIExGridLength.FromFr(1f), repeat: 7);
        styleLayoutGrid.AddColumnDefinition(UIExGridLength.FromFr(1f), repeat: 4);

        styleLayoutGrid.Set(
            rowsSpace: StyleDimension.FromPixels(10f),
            columnSpace: StyleDimension.FromPixels(15f));

        ///////////////////////////

        AppendButton(grid, prefix: "RESULT:", row: 0, column: 0, rowSpan: 2, columnSpan: 4);

        AppendButton(grid, prefix: "AC", row: 2, column: 0);
        AppendButton(grid, prefix: "%", row: 2, column: 1);
        AppendButton(grid, prefix: "<<", row: 2, column: 2);
        AppendButton(grid, prefix: "/", row: 2, column: 3);

        AppendButton(grid, prefix: 7, row: 3, column: 0);
        AppendButton(grid, prefix: 8, row: 3, column: 1);
        AppendButton(grid, prefix: 9, row: 3, column: 2);
        AppendButton(grid, prefix: "*", row: 3, column: 3);

        AppendButton(grid, prefix: 4, row: 4, column: 0);
        AppendButton(grid, prefix: 5, row: 4, column: 1);
        AppendButton(grid, prefix: 6, row: 4, column: 2);
        AppendButton(grid, prefix: "-", row: 4, column: 3);

        AppendButton(grid, prefix: 1, row: 5, column: 0);
        AppendButton(grid, prefix: 2, row: 5, column: 1);
        AppendButton(grid, prefix: 3, row: 5, column: 2);
        AppendButton(grid, prefix: "+", row: 5, column: 3);

        AppendButton(grid, prefix: 0, row: 6, column: 0);
        AppendButton(grid, prefix: ",", row: 6, column: 1);
        AppendButton(grid, prefix: "=", row: 6, column: 2, rowSpan: 1, columnSpan: 2);

        ///////////////////////////

        panel.Append(grid);
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

    public UIButton<string> AppendButton(UIExLayout layout, int prefix, int row, int column, int rowSpan = 1, int columnSpan = 1)
        => AppendButton(layout, prefix.ToString(), row, column, rowSpan, columnSpan);
    public UIButton<string> AppendButton(UIExLayout layout, string prefix, int row, int column, int rowSpan = 1, int columnSpan = 1)
    {
        UIButton<string> button = new(prefix);

        StyleLayoutChildGrid styleGrid = button.StyleLayoutChild().Grid();
        styleGrid.Set(row, column, rowSpan, columnSpan);

        layout.Append(button);
        return button;
    }
}