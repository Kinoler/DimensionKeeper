using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using TestMod.LevelSystem;

namespace TestMod
{
    public class MyPlayer : ModPlayer
    {
        public LevelingService Leveling { get; set; }

        public override void Initialize()
        {
            Leveling = new LevelingService();
        }

        public override TagCompound Save()
        {
            return new TagCompound {
                {nameof(Leveling), Leveling}
            };
        }

        public override void Load(TagCompound tag)
        {
            Leveling = tag.Get<LevelingService>(nameof(Leveling));
        }
	}
}
