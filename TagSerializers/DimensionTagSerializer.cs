using System.Linq;
using DimensionKeeper.DimensionService.Configuration;
using Terraria;
using Terraria.ModLoader.IO;

namespace DimensionKeeper.TagSerializers
{
    public class DimensionTagSerializer: TagSerializer<Dimension, TagCompound>
    {
        public override TagCompound Serialize(Dimension dimension)
        {
            var tag = new TagCompound()
            {
                {nameof(dimension.Chests), dimension.Chests.ToList()},
                {nameof(dimension.Tiles), dimension.Tiles},
                {nameof(dimension.NPCIndexes), dimension.NPCIndexes.ToList()},
                {nameof(dimension.NPCs), dimension.NPCs.ToList()},
            };

            return tag;
        }

        public override Dimension Deserialize(TagCompound tag)
        {
            var dimension = new Dimension();
            dimension.Chests = tag.GetList<Chest>(nameof(dimension.Chests)).ToArray();
            dimension.Tiles = tag.Get<Tile[,]>(nameof(dimension.Tiles));
            dimension.NPCIndexes = tag.GetList<int>(nameof(dimension.NPCIndexes)).ToArray();
            dimension.NPCs = tag.GetList<NPC>(nameof(dimension.NPCs)).ToArray();

            return dimension;
        }
    }
}
