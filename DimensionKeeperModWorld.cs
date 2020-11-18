using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using TestMod.DimensionLogic;
using TestMod.DimensionLogic.DefaultParsers;

namespace TestMod
{
    public class DimensionKeeperModWorld: ModWorld
    {
        private const string DimensionListTagName = "DimensionList";
        private const string SingleEntryTagName = "SingleEntries";

        internal static TagCompound DimensionsTag { get; set; }

        public override void Load(TagCompound tag)
        {
            DimensionsTag = tag.GetCompound(DimensionListTagName);
            var singleEntryTags = tag.GetCompound(SingleEntryTagName);
            DimensionKeeper.Instance.Load(singleEntryTags);
        }

        public override TagCompound Save()
        {
            return new TagCompound
            {
                {SingleEntryTagName, DimensionKeeper.Instance.Save()},
                {DimensionListTagName, SaveTagCompoundStorage()}
            };
        }

        internal TagCompound SaveTagCompoundStorage()
        {
            var dimensionsTag = new TagCompound();
            foreach (var name in DimensionLoader.RegisteredDimension.GetNames())
            {
                var parser = DimensionLoader.RegisteredDimension.GetParser(name);
                if (parser is ITagCompoundStorage tagCompoundParser)
                {
                    dimensionsTag.Add(name, tagCompoundParser.SavedDimensionTags);
                }
            }

            return dimensionsTag;
        }
    }
}
