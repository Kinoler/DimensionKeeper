using System.Linq;
using DimensionKeeper.DimensionService;
using Terraria.ModLoader.IO;

namespace DimensionKeeper.TagSerializers
{
    public class SingleEntryFactoryTagSerializer : TagSerializer<SingleEntryFactory, TagCompound>
    {
        public override TagCompound Serialize(SingleEntryFactory factory)
        {
            return new TagCompound
            {
                {
                    $"{nameof(factory.SingleEntryDimensions)}.{nameof(factory.SingleEntryDimensions.Keys)}",
                    factory.SingleEntryDimensions.Keys.ToList()
                },
                {
                    $"{nameof(factory.SingleEntryDimensions)}.{nameof(factory.SingleEntryDimensions.Values)}",
                    factory.SingleEntryDimensions.Values.ToList()
                },
            };
        }

        public override SingleEntryFactory Deserialize(TagCompound tag)
        {
            var factory = SingleEntryFactory.Instance;
            var keys = tag.GetList<string>($"{nameof(factory.SingleEntryDimensions)}.{nameof(factory.SingleEntryDimensions.Keys)}");
            var values = tag.GetList<SingleEntryDimension>($"{nameof(factory.SingleEntryDimensions)}.{nameof(factory.SingleEntryDimensions.Values)}");

            factory.SingleEntryDimensions = keys.Zip(values, (key, value) => new { Key = key, Value = value }).ToDictionary(x => x.Key, x => x.Value);

            return factory.SingleEntryDimensions.Any() ? factory : null;
        }
    }
}
