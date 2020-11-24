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
                {nameof(factory.SingleEntryDimensions), factory.SingleEntryDimensions.Values.ToList()},
            };
        }

        public override SingleEntryFactory Deserialize(TagCompound tag)
        {
            var factory = SingleEntryFactory.Instance;
            var values = tag.GetList<SingleEntryDimension>(nameof(factory.SingleEntryDimensions));

            factory.SingleEntryDimensions = values.Where(el => el != null).ToDictionary(x => x.EntryName, x => x);
            return factory.SingleEntryDimensions.Any() ? factory : null;
        }
    }
}
