using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DimensionKeeper.DimensionService;
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
            };


            return tag;
        }

        public override Dimension Deserialize(TagCompound tag)
        {
            var dimension = new Dimension();
            dimension.Chests = tag.GetList<Chest>(nameof(dimension.Chests)).ToArray();
            dimension.Tiles = tag.Get<Tile[,]>(nameof(dimension.Tiles));

            return dimension;
        }
    }


    //TODO Move to separate project
    public class MyDimension: Dimension
    {
        public bool BossDefeated { get; set; }
    }

    public class MyDimensionTagSerializer : TagSerializer<MyDimension, TagCompound>
    {
        public override TagCompound Serialize(MyDimension dimension)
        {
            return new TagCompound()
            {
                {nameof(Dimension), (Dimension)dimension},
                {nameof(dimension.BossDefeated), dimension.BossDefeated},
            };
        }

        public override MyDimension Deserialize(TagCompound tag)
        {
            var dimension = new MyDimension();
            dimension.CopyFrom(tag.Get<Dimension>(nameof(Dimension)));
            dimension.BossDefeated = tag.GetBool(nameof(dimension.BossDefeated));

            return dimension;
        }
    }
}
