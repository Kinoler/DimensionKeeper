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

            var tag = new TagCompound
            {
                {nameof(entry.LocationToLoad), entry.LocationToLoad}
            };

            if (TryGetSerializer(typeof(DimensionEntity), out var serializer))
                tag.Add(nameof(entry.CurrentEntity), serializer.Serialize(entry.CurrentEntity));
            
            return tag;
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
