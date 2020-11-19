using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.IO;

namespace DimensionKeeper.TagSerializers
{
    public class PointTagSerializer: TagSerializer<Point, TagCompound>
    {
        public override TagCompound Serialize(Point point)
        {
            return new TagCompound()
            {
                {nameof(point.X), point.X},
                {nameof(point.Y), point.Y}
            };
        }

        public override Point Deserialize(TagCompound tag)
        {
            var point = new Point();
            point.X = tag.GetInt(nameof(point.X));
            point.Y = tag.GetInt(nameof(point.Y));

            return point;
        }
    }
}
