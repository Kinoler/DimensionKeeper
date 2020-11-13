using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace TestMod.LevelSystem
{
    public class LevelingService
    {
        public int Level { get; set; }
        public int Experience { get; set; }

        public void AddExp(int exp)
        {
            Experience += exp;

            while (Experience >= ExperienceToLevel())
            {
                Experience -= ExperienceToLevel();
                LevelUp();
            }
        }

        public void LevelUp()
        {
            Level += 1;
            Main.NewText("Congratulations! You are now level " + Level, 255, 223, 63);
        }

        public int ExperienceToLevel()
        {
            //TODO
            return 5;

            if (Level < 5)
                return 80 + Level * 20;
            if (Level < 10)
                return Level * 40;
            if (Level < 163)
                return (int)(280 * Math.Pow(1.09, Level - 5) + 3 * Level);
            return (int)(2000000000 - 288500000000 / Level);
        }

        public int CalculateExp(NPC npc)
        {
            //TODO
            return 2;

            var character = Main.LocalPlayer.GetModPlayer<MyPlayer>();
            var life =
                npc.type == NPCID.SolarCrawltipedeTail ||
                npc.type == NPCID.SolarCrawltipedeBody ||
                npc.type == NPCID.SolarCrawltipedeHead
                    ? npc.lifeMax / 8
                    : npc.lifeMax;
            var defFactor = npc.defense < 0 ? 1 : npc.defense * life / (character.Leveling.Level + 10);
            var baseExp = Main.rand.Next((life + defFactor) / 5) + (life + defFactor) / 6;
            var scaled = Main.expertMode ? (int)(baseExp * 0.5) : baseExp;

            return scaled;
        }
    }
}
