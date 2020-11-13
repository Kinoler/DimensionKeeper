using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace TestMod.Globals
{
    public class LevelGlobalNPC : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            var leveling = Main.LocalPlayer.GetModPlayer<MyPlayer>().Leveling;

            var gainedExp = leveling.CalculateExp(npc);
            leveling.AddExp(gainedExp);
        }
    }
}
