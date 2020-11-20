using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DimensionKeeper.DimensionService;
using Terraria.ModLoader.IO;

namespace DimensionKeeper.TagSerializers
{
    public class DimensionsKeeperTagSerializer : TagSerializer<SingleEntryFactory, TagCompound>
    {
        public override TagCompound Serialize(SingleEntryFactory keeper)
        {
            return new TagCompound
            {
                {
                    $"{nameof(keeper.SingleEntryDimensions)}.{nameof(keeper.SingleEntryDimensions.Keys)}",
                    keeper.SingleEntryDimensions.Keys.ToList()
                },
                {
                    $"{nameof(keeper.SingleEntryDimensions)}.{nameof(keeper.SingleEntryDimensions.Values)}",
                    keeper.SingleEntryDimensions.Values.ToList()
                },
            };
        }

        public override SingleEntryFactory Deserialize(TagCompound tag)
        {
            var keeper = new SingleEntryFactory();
            var keys = tag.GetList<string>($"{nameof(keeper.SingleEntryDimensions)}.{nameof(keeper.SingleEntryDimensions.Keys)}");
            var values = tag.GetList<SingleEntryDimension>($"{nameof(keeper.SingleEntryDimensions)}.{nameof(keeper.SingleEntryDimensions.Values)}");

            keeper.SingleEntryDimensions = keys.Zip(values, (key, value) => new { Key = key, Value = value }).ToDictionary(x => x.Key, x => x.Value);

            return keeper.SingleEntryDimensions.Any() ? keeper : null;
        }
    }
}
