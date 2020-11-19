﻿using System;
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
    public class DimensionEntityTagSerializer: TagSerializer<DimensionEntityInternal, TagCompound>
    {
        public override TagCompound Serialize(DimensionEntityInternal entity)
        {
            return new TagCompound
            {
                {nameof(entity.Type), entity.Type},
                {nameof(entity.Id), entity.Id},
                {nameof(entity.Location), entity.Location},
                {nameof(entity.Size), entity.Size}
            };
        }

        public override DimensionEntityInternal Deserialize(TagCompound tag)
        {
            var type = tag.Get<string>("Type");
            var id = tag.Get<string>("Id");

            var entity = DimensionRegister.Instance.GetStorage(type).LoadInternal(id);
            entity.Location = tag.Get<Point>(nameof(entity.Location));
            entity.Size = tag.Get<Point>(nameof(entity.Size));

            return entity;
        }
    }
}