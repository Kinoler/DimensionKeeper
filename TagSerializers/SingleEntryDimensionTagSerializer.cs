using DimensionKeeper.DimensionService;
using DimensionKeeper.DimensionService.InternalClasses;
using Microsoft.Xna.Framework;
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
                {nameof(entry.EntryName), entry.EntryName},
                {nameof(entry.LocationToLoad), entry.LocationToLoad}
            };

            if (TryGetSerializer(typeof(DimensionEntity), out var serializer))
                tag.Add(nameof(entry.CurrentEntity), serializer.Serialize(entry.CurrentEntity));
            
            return tag;
        }

        public override SingleEntryDimension Deserialize(TagCompound tag)
        {
            var entry = new SingleEntryDimension();
            entry.EntryName = tag.GetString(nameof(entry.EntryName));
            entry.CurrentEntity = tag.Get<DimensionEntity>(nameof(entry.CurrentEntity));
            entry.LocationToLoad = tag.Get<Point>(nameof(entry.LocationToLoad));

            return entry;
        }
    }
}
