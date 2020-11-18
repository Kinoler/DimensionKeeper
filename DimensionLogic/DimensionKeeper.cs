using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.IO;
using TestMod.Interfaces;

namespace TestMod.DimensionLogic
{
    public class DimensionKeeper: ITagCompound
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


        public TagCompound Save()
        {
            var tag = new TagCompound();

            foreach (var entry in SingleEntryDimensions)
            {
                var entryCompound = entry.Value.Save();
                if (entryCompound != null)
                {
                    tag.Add(entry.Key, entryCompound);
                }
            }

            return tag;
        }

        public void Load(TagCompound tag)
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
