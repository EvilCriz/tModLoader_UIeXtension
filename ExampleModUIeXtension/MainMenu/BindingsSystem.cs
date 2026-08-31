using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace ExampleModUIeXtension.MainMenu;

public class BindingsModSystem : ModSystem
{
    public static ModKeybind MainMenuKeybind;
    public static UIMainMenuState MainMenuState;

    public override void Load()
    {
        MainMenuKeybind = KeybindLoader.RegisterKeybind(Mod, "Main Menu", "X");
    }

    public override void Unload()
    {
        MainMenuKeybind = null;
        MainMenuState = null;
    }
}

public class BindingsModPlayer : ModPlayer
{
    public override void OnEnterWorld()
    {
        bool hasKey = BindingsModSystem.MainMenuKeybind.GetAssignedKeys().Count > 0;

        string message = $"[{ExampleModUIeXtension.Instance.DisplayName}]: ";
        message += "Assign a hotkey to open the main menu";

        if (!hasKey)
            Main.NewText(message);
    }

    public override void ProcessTriggers(TriggersSet triggersSet)
    {
        if (BindingsModSystem.MainMenuKeybind.JustReleased)
        {
            UIMainMenuState state = BindingsModSystem.MainMenuState;
            UIMainMenuBar system = ModContent.GetInstance<UIMainMenuBar>();

            if (state is null)
                state = new UIMainMenuState();

            if (system.GetState())
                system.SetUIState(null);
            else
                system.SetUIState(state);
        }
    }
}