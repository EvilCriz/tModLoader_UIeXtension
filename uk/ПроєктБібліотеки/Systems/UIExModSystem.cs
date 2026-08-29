using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace UIeXtension;

/// <summary>
///     Додатковий клас, призначений для спрощення створення базової реалізації ModSystem. . 
///     необхідний для створення інтерфейсу користувача.
/// </summary>
/// <typeparam name="TUIState">
///     UIStateThe життєвий цикл якого в цьому класі буде керуватися
/// </typeparam>
[Autoload(Side = ModSide.Client)]
public abstract class UIExModSystem<TUIState> : ModSystem 
    where TUIState : UIState, new()
{

    /// <summary>
    ///     Об'єкт користувача UIState
    /// </summary>
    protected TUIState State { get; set; }

    /// <summary>
    ///     Оновлення та рендеринг <see cref="State"/>
    /// </summary>
    protected UserInterface InterfaceState { get; set; }



    /// <summary>
    ///     Зберігає останній стан <see cref="State"/> та <see cref="InterfaceState"/> (відображений/прихований)
    /// </summary>
    protected bool LastState { get; set; }



    /// <summary>
    ///     Зберігати значення, що використовується в <see cref="ModifyInterfaceLayers(List{GameInterfaceLayer})"/>
    /// </summary>
    protected virtual string InterfaceLayerName => "Vanilla: Mouse Text";

    /// <summary>
    ///     Зберігати значення, що використовується в <see cref="ModifyInterfaceLayers(List{GameInterfaceLayer})"/>
    /// </summary>
    protected virtual string InterfaceLayerDescription => "YourMod: A Description";



    /// <summary>
    ///     Дозволяє встановлювати стан (ображений / прихований) інтерфейсу користувача
    /// </summary>
    /// <param name="state">
    ///     Новий стан (показаний/прихований) інтерфейсу користувача
    /// </param>
    public virtual void SetState(bool state)
    {
        if (LastState == state)
            return;

        LastState = !LastState;
        InterfaceState.SetState(LastState ? State : null);
    }

    /// <summary>
    ///     Він змінює стан інтерфейсу користувача. Якщо він відображається, він приховує; якщо він прихований, він відображає.
    /// </summary>
    public virtual void SwitchState() => SetState(!LastState);

    /// <summary>
    ///     Він відображає інтерфейс користувача, якщо він прихований.
    /// </summary>
    public virtual void Show() => SetState(true);

    /// <summary>
    ///     Приховати інтерфейс користувача, якщо він відображається.
    /// </summary>
    public virtual void Hide() => SetState(false);

    /// <summary>
    ///     Повернення фортуни <see cref="State"/> (прихований/виражений)
    /// </summary>
    public virtual bool GetState() => LastState;

    /// <summary>
    ///     Повернення. <see cref="State"/>. . 
    /// </summary>
    public virtual TUIState GetUIState() => State;

    /// <summary>
    ///     Дозволяє запитати безпосередньо. <typeparamref name="TUIState"/> Інтерфейс користувача.
    /// </summary>
    public virtual void SetUIState(TUIState state) => InterfaceState.SetState(state);

    /// <summary>
    ///     Базова реалізація <see cref="ModSystem"/> навантажувачі
    /// </summary>
    public override void Load()
    {
        State = new TUIState();
        State.Activate();
        InterfaceState = new UserInterface();
    }

    /// <summary>
    ///     Базова реалізація <see cref="ModSystem"/> оновити інтерфейс користувача
    /// </summary>
    public override void UpdateUI(GameTime gameTime) => InterfaceState?.Update(gameTime);

    /// <summary>
    ///     Базова реалізація <see cref="ModSystem"/> інтерфейс користувача
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
