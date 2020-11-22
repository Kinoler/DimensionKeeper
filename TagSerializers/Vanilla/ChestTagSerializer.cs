using System.Linq;
using Terraria;
using Terraria.ModLoader.IO;

namespace DimensionKeeper.TagSerializers.Vanilla
{
    public class ChestTagSerializer: TagSerializer<Chest, TagCompound>
    {
        public override TagCompound Serialize(Chest chest)
        {
            return new TagCompound()
            {
                {nameof(chest.x), chest.x},
                {nameof(chest.y), chest.y},
                {nameof(chest.frame), chest.frame},
                {nameof(chest.frameCounter), chest.frameCounter},
                {nameof(chest.bankChest), chest.bankChest},
                {nameof(chest.name), chest.name},
                {nameof(chest.item), chest.item.ToList()},
            };
        }

        public override Chest Deserialize(TagCompound tag)
        {
            var chest = new Chest();
            chest.x = tag.GetInt(nameof(chest.x));
            chest.y = tag.GetInt(nameof(chest.y));
            chest.frame = tag.GetInt(nameof(chest.frame));
            chest.frameCounter = tag.GetInt(nameof(chest.frameCounter));
            chest.bankChest = tag.GetBool(nameof(chest.bankChest));
            chest.name = tag.GetString(nameof(chest.name));
            chest.item = tag.GetList<Item>(nameof(chest.item)).ToArray();

            return chest;
        }
    }
}
