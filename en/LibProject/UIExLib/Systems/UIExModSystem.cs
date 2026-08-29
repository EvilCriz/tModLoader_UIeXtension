using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace UIeXtension;

/// <summary>
///     An auxiliary class designed to simplify the creation of a basic implementation ModSystem. 
///     required to create a user interface.
/// </summary>
/// <typeparam name="TUIState">
///     UIStateThe life cycle of which this class will govern
/// </typeparam>
[Autoload(Side = ModSide.Client)]
public abstract class UIExModSystem<TUIState> : ModSystem 
    where TUIState : UIState, new()
{

    /// <summary>
    ///     User object UIState
    /// </summary>
    protected TUIState State { get; set; }

    /// <summary>
    ///     Manages updating and rendering <see cref="State"/>
    /// </summary>
    protected UserInterface InterfaceState { get; set; }



    /// <summary>
    ///     Keeps the last fortune. <see cref="State"/> / <see cref="InterfaceState"/> (displayed/hidden)
    /// </summary>
    protected bool LastState { get; set; }



    /// <summary>
    ///     Stores the value used in <see cref="ModifyInterfaceLayers(List{GameInterfaceLayer})"/>
    /// </summary>
    protected virtual string InterfaceLayerName => "Vanilla: Mouse Text";

    /// <summary>
    ///     Stores the value used in <see cref="ModifyInterfaceLayers(List{GameInterfaceLayer})"/>
    /// </summary>
    protected virtual string InterfaceLayerDescription => "YourMod: A Description";



    /// <summary>
    ///     Allows you to set the state (displayed / hidden) of the user interface
    /// </summary>
    /// <param name="state">
    ///     New state (displayed/hidden) of the user interface
    /// </param>
    public virtual void SetState(bool state)
    {
        if (LastState == state)
            return;

        LastState = !LastState;
        InterfaceState.SetState(LastState ? State : null);
    }

    /// <summary>
    ///     It changes the state of the user interface. If it's displayed, it hides; if it's hidden, it displays.
    /// </summary>
    public virtual void SwitchState() => SetState(!LastState);

    /// <summary>
    ///     It displays the user interface if it is hidden.
    /// </summary>
    public virtual void Show() => SetState(true);

    /// <summary>
    ///     Hide the user interface if it is displayed.
    /// </summary>
    public virtual void Hide() => SetState(false);

    /// <summary>
    ///     Returns the fortune <see cref="State"/> (hidden/displayed)
    /// </summary>
    public virtual bool GetState() => LastState;

    /// <summary>
    ///     Returns. <see cref="State"/>. 
    /// </summary>
    public virtual TUIState GetUIState() => State;

    /// <summary>
    ///     Allows you to ask directly. <typeparamref name="TUIState"/> The user interface.
    /// </summary>
    public virtual void SetUIState(TUIState state) => InterfaceState.SetState(state);

    /// <summary>
    ///     Basic implementation <see cref="ModSystem"/> loader
    /// </summary>
    public override void Load()
    {
        State = new TUIState();
        State.Activate();
        InterfaceState = new UserInterface();
    }

    /// <summary>
    ///     Basic implementation <see cref="ModSystem"/> to update the user interface
    /// </summary>
    public override void UpdateUI(GameTime gameTime) => InterfaceState?.Update(gameTime);

    /// <summary>
    ///     Basic implementation <see cref="ModSystem"/> user interface
    /// </summary>
    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
    {
        int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals(InterfaceLayerName));
        if (mouseTextIndex != -1)
        {
            layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                InterfaceLayerDescription,
                () =>
                {
                    InterfaceState.Draw(Main.spriteBatch, new GameTime());
                    return true;
                },
                InterfaceScaleType.UI)
            );
        }
    }
}