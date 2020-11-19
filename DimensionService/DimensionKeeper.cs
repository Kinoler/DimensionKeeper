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

        public void RemoveEntry(string entryName)
        {
            if (SingleEntryDimensions.ContainsKey(entryName))
            {
                SingleEntryDimensions.Remove(entryName);
            }
        }


    }
}
