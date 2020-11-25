using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DimensionKeeper.DimensionService;
using DimensionKeeper.DimensionService.Configuration;
using Microsoft.Xna.Framework;
using Terraria;

namespace DimensionKeeper.HelperImplementations.Phases
{
    /// <summary>
    /// Handles the NPCs.
    /// </summary>
    public class NPCPhase : DimensionPhase<Dimension>
    {
        public override void ExecuteLoadPhase(DimensionEntity<Dimension> entity)
        {
            var locationToLoadWorld = entity.Location.ToWorldCoordinates();
            var index = 0;

            for (var i = 0; i < entity.Dimension.NPCs.Length; i++)
            {
                while (index < 200 && Main.npc[index].active)
                    ++index;
                if (index >= 200)
                    break;

                var npc = (NPC) entity.Dimension.NPCs[i].Clone();
                npc.position.X += locationToLoadWorld.X;
                npc.position.Y += locationToLoadWorld.Y;

                Main.npc[index] = npc;
                Main.npc[index].whoAmI = index;
                entity.Dimension.NPCIndexes[i] = index;
            }
        }

        public override void ExecuteSynchronizePhase(DimensionEntity<Dimension> entity)
        {
            var locationToLoadWorld = entity.Location.ToWorldCoordinates();
            var npcList = new List<NPC>();
            var indexList = new List<int>();

            for (var i = 0; i < 200; i++)
            {
                if (!Main.npc[i].active)
                    continue;

                var npc = Main.npc[i];
                if (!npc.position.Between(
                    entity.Location.ToWorldCoordinates(),
                    Vector2.Add(entity.Location.ToWorldCoordinates(), entity.Size.ToWorldCoordinates())))
                    continue;

                var npcCloned = (NPC)npc.Clone();
                npcCloned.position.X -= locationToLoadWorld.X;
                npcCloned.position.Y -= locationToLoadWorld.Y;

                npcList.Add(npcCloned);
                indexList.Add(i);
            }

            entity.Dimension.NPCs = npcList.ToArray();
            entity.Dimension.NPCIndexes = indexList.ToArray();
        }

        public override void ExecuteClearPhase(DimensionEntity<Dimension> entity)
        {
            foreach (var index in entity.Dimension.NPCIndexes)
            {
                Main.npc[index] = new NPC();
                Main.npc[index].whoAmI = index;
            }
        }
    }
}
