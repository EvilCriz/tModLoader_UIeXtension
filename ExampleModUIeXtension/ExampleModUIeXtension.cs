using Terraria.ModLoader;

namespace ExampleModUIeXtension;

public class ExampleModUIeXtension : Mod
{
    public static ExampleModUIeXtension Instance;

    public override void Load()
    {
        Instance = this;
    }
}