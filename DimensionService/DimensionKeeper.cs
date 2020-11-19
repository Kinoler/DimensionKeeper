using System.Collections.Generic;
using Terraria.ModLoader.IO;

namespace DimensionKeeper.DimensionService
{
    public class DimensionKeeper
    {
        private static DimensionKeeper _instance;

        public static DimensionKeeper Instance
        {
            get => _instance ?? (_instance = new DimensionKeeper());
            internal set => _instance = value;
        }

        private Dictionary<string, SingleEntryDimension> SingleEntryDimensions { get; set; } = 
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
            var tag = new TagCompound();

            foreach (var entry in SingleEntryDimensions)
            {
                var entryCompound = entry.Value.Save();
                if (entryCompound != null) 
                    tag.Add(entry.Key, entryCompound);
            }

            return tag;
        }

        internal void Load(TagCompound tag)
        {
            SingleEntryDimensions.Clear();
            
            foreach (var valuePair in tag)
            {
                var key = valuePair.Key;
                var entryCompound = tag.GetCompound(key);

                if (entryCompound.Count == 0)
                    continue;

                var entry = new SingleEntryDimension();
                entry.Load(entryCompound);

                SingleEntryDimensions.Add(key, entry);
            }
        }
    }
}
