using Terraria.ModLoader;
using TestMod.DimensionExample;

namespace TestMod
{
    public class ModWorldExample: ModWorld
    {
        public override void Initialize()
        {
            DimensionStorageExample.Initialize();
        }
    }
}