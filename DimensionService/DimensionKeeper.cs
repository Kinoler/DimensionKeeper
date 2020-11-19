using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader.IO;

namespace DimensionKeeper.DimensionService
{
    public class DimensionsKeeper
    {
        private static DimensionsKeeper _instance;

        public static DimensionsKeeper Instance
        {
            get => _instance ?? (_instance = new DimensionsKeeper());
            internal set => _instance = value;
        }

        internal Dictionary<string, SingleEntryDimension> SingleEntryDimensions { get; set; } = 
            new Dictionary<string, SingleEntryDimension>();

        public SingleEntryDimension GetEntry(string entryName)
        {
            if (!SingleEntryDimensions.ContainsKey(entryName))
            {
                SingleEntryDimensions.Add(entryName, new SingleEntryDimension());
            }

            return SingleEntryDimensions[entryName];
        }

        internal TagCompound Save()
        {
            return new TagCompound
            {
                {"scoreNames", SingleEntryDimensions.Keys.ToList()},
                {"scoreValues", SingleEntryDimensions.Values.ToList()}
            };
        }

        internal void Load(TagCompound tag)
        {
            var keys = tag.Get<List<string>>($"{nameof(SingleEntryDimensions)}.{nameof(SingleEntryDimensions.Keys)}");
            var values = tag.Get<List<SingleEntryDimension>>($"{nameof(SingleEntryDimensions)}.{nameof(SingleEntryDimensions.Values)}");

            SingleEntryDimensions = keys.Zip(values, (key, value) => new { Key = key, Value = value }).ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
