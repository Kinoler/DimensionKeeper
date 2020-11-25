using System.Linq;
using Terraria;
using Terraria.ModLoader.IO;

namespace DimensionKeeper.TagSerializers.Vanilla
{
    public class NPCTagSerializer : TagSerializer<NPC, TagCompound>
    {
        public override TagCompound Serialize(NPC npc)
        {
            var tag = new TagCompound()
            {
                [nameof(npc.type)] = npc.type,
                [nameof(npc.GivenName)] = npc.GivenName,
                [nameof(npc.position.X)] = npc.position.X,
                [nameof(npc.position.Y)] = npc.position.Y,
                [nameof(npc.life)] = npc.life,
                [nameof(npc.target)] = npc.target,
                [nameof(npc.ai)] = npc.ai.ToList(),
                [nameof(npc.timeLeft)] = npc.timeLeft,
                [nameof(npc.wet)] = npc.wet,
                [nameof(npc.scale)] = npc.scale,
                [nameof(npc.width)] = npc.width,
                [nameof(npc.height)] = npc.height,
            };

            if (npc.townNPC)
            {
                tag.Add(nameof(npc.homeless), npc.homeless);
                tag.Add(nameof(npc.homeTileX), npc.homeTileX);
                tag.Add(nameof(npc.homeTileY), npc.homeTileY);
            }

            return tag;
        }

        public override NPC Deserialize(TagCompound tag)
        {
            var npc = new NPC();
            var type = tag.GetInt(nameof(npc.type));

            npc.SetDefaults(type, -1f);
            npc.active = true;

            npc.GivenName = tag.GetString(nameof(npc.GivenName));
            npc.position.X = tag.GetFloat(nameof(npc.position.X));
            npc.position.Y = tag.GetFloat(nameof(npc.position.Y));
            npc.life = tag.GetInt(nameof(npc.life));
            npc.timeLeft = tag.GetInt(nameof(npc.timeLeft));
            npc.wet = tag.GetBool(nameof(npc.wet));
            npc.ai = tag.GetList<float>(nameof(npc.ai)).ToArray();
            npc.target = tag.GetInt(nameof(npc.target));
            npc.scale = tag.GetFloat(nameof(npc.scale));
            npc.width = tag.GetInt(nameof(npc.width));
            npc.height = tag.GetInt(nameof(npc.height));

            if (npc.townNPC)
            {
                npc.homeless = tag.GetBool(nameof(npc.homeless));
                npc.homeTileX = tag.GetInt(nameof(npc.homeTileX));
                npc.homeTileY = tag.GetInt(nameof(npc.homeTileY));
            }

            return npc;
        }
    }
}