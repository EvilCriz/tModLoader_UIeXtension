using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;
using Terraria.UI;
using UIeXtension;
using UIeXtension.Enums;
using UIeXtension.MethodsExtensions;
using UIeXtensionExample;
using UIeXtensionsExample;

namespace ExampleModUIeXtension;

public class UIMainMenuBar : UIExModSystem<UIState> { }

public class UIMainMenuState : UIState
{
    private enum ButtonNavigationClickType
    { 
        MainMenu,
        Tutorial
    }

    private enum ButtonTutorialClickType
    {
        TCanvas,
        TStackPanel,
        TWrapPanel1,
        TWrapPanel2,
        TDockPanel,
        TGrid,
        TUniformGrid1,
        TUniformGrid2
    }

    private Dictionary<UIElement, ButtonNavigationClickType> _buttonsNavigationClickInfo = new();
    private Dictionary<UIElement, ButtonTutorialClickType> _buttonsTutorailClickType = new();

    private UIExStackPanel _stackPanelMainMenu;

    public override void OnInitialize()
        => UIExLayout.BeginLayoutPreparation(this);

    public override void OnDeactivate()
    {
        RemoveAllChildren();
        _buttonsNavigationClickInfo.Clear();
        _buttonsTutorailClickType.Clear();

        _stackPanelMainMenu = null;
    }

    // the OnActivate() method is used in this case only
    // for the «hot reload» feature in the development environment.
    // for a real interface, it would be more appropriate to use
    // the OnInitialize() method
    public override void OnActivate()
    {
        // Main panel:

        UIExCanvas canvas = new();
        Append(canvas);
        canvas.Stretch();
		canvas.StyleLayout.JustifyContent = UIExAlignment.Center;
		canvas.StyleLayout.AlignItems = UIExAlignment.Center;
        canvas.StyleLayout.Canvas().AllowOverflow = true;

        // Dock panel:

        UIExDockPanel dockPanel = new();
        canvas.Append(dockPanel);
        dockPanel.StyleDisplay.tModLoaderStyle = true;

        var styleChildDP = dockPanel.StyleLayoutChild();
        styleChildDP.Width.Precent = 0.8f;
        styleChildDP.Height.Precent = 0.8f;

        /////////////////////////// SIDE TOP

        var styleSideTop = dockPanel.StyleLayout.DockPanel().SideTop;
        styleSideTop.JustifyContent = UIExAlignment.Stretch;
        styleSideTop.AlignItems = UIExAlignment.Stretch;

        UITextPanel<string> text = new("MAIN MENU");
        text.TextScale = 2f;
        text.StyleLayoutChild().DockPanel().Side = UIExSide.Top;
        dockPanel.Append(text);


        //////////////////////// SIDE LEFT


        var styleSideLeft = dockPanel.StyleLayout.DockPanel().SideLeft;
        styleSideLeft.Padding.SetPrecent(0.025f);
        styleSideLeft.JustifyContent = UIExAlignment.Stretch;
        styleSideLeft.AlignItems = UIExAlignment.Stretch;

        UIExStackPanel stackPanelNavigation = new();
        stackPanelNavigation.StyleDisplay.tModLoaderStyle = true;
        dockPanel.Append(stackPanelNavigation);

        stackPanelNavigation.StyleLayoutChild().DockPanel().Side = UIExSide.Left;
        stackPanelNavigation.StyleLayoutChild().Width.Precent = 0.15f;

        stackPanelNavigation.StyleLayout.AlignItems = UIExAlignment.Stretch;
        stackPanelNavigation.StyleLayout.StackPanel().Spacing.Pixels = 10f;

        var buttonNavMM = AppendButton(stackPanelNavigation, "Main Menu", ButtonNavigationClickType.MainMenu);
        var buttonNavTutorial = AppendButton(stackPanelNavigation, "Tutorial", ButtonNavigationClickType.Tutorial);


        ///////////////////////// FILL


        var styleSideFill = dockPanel.StyleLayout.DockPanel().SideFill;
        styleSideFill.Padding.SetPrecent(0.025f);
        styleSideFill.JustifyContent = UIExAlignment.Stretch;
        styleSideFill.AlignItems = UIExAlignment.Stretch;


        _stackPanelMainMenu = new();
        _stackPanelMainMenu.StyleDisplay.tModLoaderStyle = true;
        dockPanel.Append(_stackPanelMainMenu);

        _stackPanelMainMenu.StyleLayoutChild().DockPanel().Side = UIExSide.Fill;

        _stackPanelMainMenu.StyleLayout.AlignItems = UIExAlignment.Stretch;
        _stackPanelMainMenu.StyleLayout.StackPanel().Spacing.Pixels = 10f;


        /////////////////////////////// START LAYOUT


        UIExLayout.EndLayoutPreparation(this);
        UIExLayout.BeginLayoutPreparation(this);
    }

    private UIButton<string> AppendButton(UIElement element, string text, ButtonNavigationClickType clickType)
    {
        var button = AppendButton(element, text);
        _buttonsNavigationClickInfo[button] = clickType;
        return button;
    }

    private UIButton<string> AppendButton(UIElement element, string text, ButtonTutorialClickType clickType)
    {
        var button = AppendButton(element, text);
        _buttonsTutorailClickType[button] = clickType;
        return button;
    }

    private UIButton<string> AppendButton(UIElement element, string text)
    {
        UIButton<string> button = new(text);
        element.Append(button);

        button.TextScale = 2f;
        button.TextColor = Color.DarkOrange;

        button.OnLeftClick += OnClickButton;

        var style = button.StyleLayoutChild();
        style.Height.Pixels = 50f;
        style.Margin.Left.Precent = 0.2f;
        style.Margin.Right.Precent = 0.2f;

        return button;
    }

    public void AddTutorialButtonsInMainMenu()
    {
        AppendButton(_stackPanelMainMenu, "[Tutorial]: UIExCanvas", ButtonTutorialClickType.TCanvas);
        AppendButton(_stackPanelMainMenu, "[Tutorial]: UIExStackPanel", ButtonTutorialClickType.TStackPanel);
        AppendButton(_stackPanelMainMenu, "[Tutorial]: UIExWrapPanel 1", ButtonTutorialClickType.TWrapPanel1);
        AppendButton(_stackPanelMainMenu, "[Tutorial]: UIExWrapPanel 2", ButtonTutorialClickType.TWrapPanel2);
        AppendButton(_stackPanelMainMenu, "[Tutorial]: UIExDockPanel", ButtonTutorialClickType.TDockPanel);
        AppendButton(_stackPanelMainMenu, "[Tutorial]: UIExGrid", ButtonTutorialClickType.TGrid);
        AppendButton(_stackPanelMainMenu, "[Tutorial]: UIExUniformGrid 1", ButtonTutorialClickType.TUniformGrid1);
        AppendButton(_stackPanelMainMenu, "[Tutorial]: UIExUniformGrid 2", ButtonTutorialClickType.TUniformGrid2);
    }

    private void OnClickButton(UIMouseEvent evt, UIElement listeningElement)
    {
        _stackPanelMainMenu.RemoveAllChildren();

        var button = (UIButton<string>)listeningElement;

        if (_buttonsNavigationClickInfo.ContainsKey(button))
        {
            ButtonNavigationClickType type = _buttonsNavigationClickInfo[button];

            switch (type)
            {
                case ButtonNavigationClickType.Tutorial:
                    AddTutorialButtonsInMainMenu();
                    break;
            }

            UIExLayout.BeginLayoutPreparation(this);
            UIExLayout.EndLayoutPreparation(this);
        }
        else if (_buttonsTutorailClickType.ContainsKey(button))
        {
            ButtonTutorialClickType type = _buttonsTutorailClickType[button];

            UIMainMenuBar menuBar = ModContent.GetInstance<UIMainMenuBar>();

            if (TrySetNewState<TutorialUIExCanvasState>(ButtonTutorialClickType.TCanvas))
                return;
            if (TrySetNewState<TutorialUIExStackPanelState>(ButtonTutorialClickType.TStackPanel))
                return;
            if (TrySetNewState<TutorialUIExWrapPanel1State>(ButtonTutorialClickType.TWrapPanel1))
                return;
            if (TrySetNewState<TutorialUIExWrapPanel2State>(ButtonTutorialClickType.TWrapPanel2))
                return;
            if (TrySetNewState<TutorialUIExDockPanelState>(ButtonTutorialClickType.TDockPanel))
                return;
            if (TrySetNewState<TutorialUIExGridState>(ButtonTutorialClickType.TGrid))
                return;
            if (TrySetNewState<TutorialUIExUniformGrid1State>(ButtonTutorialClickType.TUniformGrid1))
                return;
            if (TrySetNewState<TutorialUIExUniformGrid2State>(ButtonTutorialClickType.TUniformGrid2))
                return;

            bool TrySetNewState<T>(ButtonTutorialClickType t) where T : UIState, new()
            {
                if (type != t)
                    return false;

                menuBar.SetUIState(new T());
                return true;
            }
        }
    }
}