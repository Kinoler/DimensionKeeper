using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DimensionKeeper.DimensionService;
using DimensionKeeper.DimensionService.InternalClasses;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader.IO;

namespace DimensionKeeper.TagSerializers
{
    public class DimensionEntityTagSerializer: TagSerializer<DimensionEntity, TagCompound>
    {
        public override TagCompound Serialize(DimensionEntity entity)
        {
            return new TagCompound
            {
                {nameof(entity.Type), entity.Type},
                {nameof(entity.Id), entity.Id},
                {nameof(entity.Location), entity.Location},
                {nameof(entity.Size), entity.Size}
            };
        }

        public override DimensionEntity Deserialize(TagCompound tag)
        {
            var type = tag.Get<string>("Type");

            var entity = DimensionRegister.Instance.GetStorage(type).CreateEmptyEntity(Point.Zero, Point.Zero);
            entity.Id = tag.Get<string>(nameof(entity.Id));
            entity.Location = tag.Get<Point>(nameof(entity.Location));
            entity.Size = tag.Get<Point>(nameof(entity.Size));

            return entity;
        }
    }
}
