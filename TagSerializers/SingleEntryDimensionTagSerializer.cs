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
    public class SingleEntryDimensionTagSerializer: TagSerializer<SingleEntryDimension, TagCompound>
    {
        public override TagCompound Serialize(SingleEntryDimension entry)
        {
            if (!DimensionHelpers.ValidateDimension(entry.CurrentEntity))
                return null;

            return new TagCompound
            {
                {nameof(entry.CurrentEntity), entry.CurrentEntity},
                {nameof(entry.LocationToLoad), entry.LocationToLoad}
            };
        }

        public override SingleEntryDimension Deserialize(TagCompound tag)
        {
            var entry = new SingleEntryDimension();
            entry.CurrentEntity = tag.Get<DimensionEntity>(nameof(entry.CurrentEntity));
            entry.LocationToLoad = tag.Get<Point>(nameof(entry.LocationToLoad));

            return entry;
        }
    }
}
