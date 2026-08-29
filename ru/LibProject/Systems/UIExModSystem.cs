using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace UIeXtension;

/// <summary>
///     Вспомогательный класс, созданный для упрощенного создания базовой реализации ModSystem, 
///     требующейся для создания пользовательского интерфейса.
/// </summary>
/// <typeparam name="TUIState">
///     UIState, жизненным циклом которого будет управлять данный класс
/// </typeparam>
[Autoload(Side = ModSide.Client)]
public abstract class UIExModSystem<TUIState> : ModSystem 
    where TUIState : UIState, new()
{

    /// <summary>
    ///     Пользовательский объект UIState
    /// </summary>
    protected TUIState State { get; set; }

    /// <summary>
    ///     Управляет обновлением и отрисовкой <see cref="State"/>
    /// </summary>
    protected UserInterface InterfaceState { get; set; }



    /// <summary>
    ///     Хранит последнее состояние <see cref="State"/> / <see cref="InterfaceState"/> (отображается / скрыт)
    /// </summary>
    protected bool LastState { get; set; }



    /// <summary>
    ///     Хранит значение, использующееся в <see cref="ModifyInterfaceLayers(List{GameInterfaceLayer})"/>
    /// </summary>
    protected virtual string InterfaceLayerName => "Vanilla: Mouse Text";

    /// <summary>
    ///     Хранит значение, использующееся в <see cref="ModifyInterfaceLayers(List{GameInterfaceLayer})"/>
    /// </summary>
    protected virtual string InterfaceLayerDescription => "YourMod: A Description";



    /// <summary>
    ///     Позволяет задать состояние (отображается / скрыт) пользовательского интерфейса
    /// </summary>
    /// <param name="state">
    ///     Новое состояние (отображается / скрыт) пользовательского интерфейса
    /// </param>
    public virtual void SetState(bool state)
    {
        if (LastState == state)
            return;

        LastState = !LastState;
        InterfaceState.SetState(LastState ? State : null);
    }

    /// <summary>
    ///     Меняет состояние пользовательского интерфейса. Если отображается - скрывает; если скрыт - отображает.
    /// </summary>
    public virtual void SwitchState() => SetState(!LastState);

    /// <summary>
    ///     Отображает пользовательский интерфейс, если он скрыт.
    /// </summary>
    public virtual void Show() => SetState(true);

    /// <summary>
    ///     Скрывает пользовательский интерфейс, если он отображается.
    /// </summary>
    public virtual void Hide() => SetState(false);

    /// <summary>
    ///     Возвращает состояние <see cref="State"/> (скрыт/отображается)
    /// </summary>
    public virtual bool GetState() => LastState;

    /// <summary>
    ///     Возвращает <see cref="State"/>. 
    /// </summary>
    public virtual TUIState GetUIState() => State;

    /// <summary>
    ///     Позволяет прямо задать <typeparamref name="TUIState"/> пользовательского интерфейса.
    /// </summary>
    public virtual void SetUIState(TUIState state) => InterfaceState.SetState(state);

    /// <summary>
    ///     Базовая реализация <see cref="ModSystem"/> для загрузки пользовательского интерфейса
    /// </summary>
    public override void Load()
    {
        State = new TUIState();
        State.Activate();
        InterfaceState = new UserInterface();
    }

    /// <summary>
    ///     Базовая реализация <see cref="ModSystem"/> для обновления пользовательского интерфейса
    /// </summary>
    public override void UpdateUI(GameTime gameTime) => InterfaceState?.Update(gameTime);

    /// <summary>
    ///     Базовая реализация <see cref="ModSystem"/> для вывода пользовательского интерфейса
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