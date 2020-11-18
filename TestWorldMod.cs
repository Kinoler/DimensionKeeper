using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using TestMod.DimensionExample;
using TestMod.DimensionLogic;
using TestMod.DimensionLogic.DefaultParsers;

namespace TestMod
{
    public class TestWorldMod: ModWorld
    {
        private const string DimensionListTagName = "DimensionList";
        private const string CurrentDimensionTagName = "CurentDimension";
        private const string SingleEntryTagName = "SingleEntries";

        internal static TagCompound DimensionsTag { get; set; }

        public override void Initialize()
        {
            DimensionStorageExample.Initialize();
        }

        public override void Load(TagCompound tag)
        {
            DimensionsTag = tag.GetCompound(DimensionListTagName);
            DimensionKeeper.Instance.Load(tag.GetCompound(SingleEntryTagName));
        }

        public override TagCompound Save()
        {
            var dimensionsTag = new TagCompound
            {
                {SingleEntryTagName, DimensionKeeper.Instance.Save()}
            };
            //dimensionsTag.Set(DimensionListTagName, TagCompoundStorage<Dimension>.OnWorldSave());

            return dimensionsTag;
        }
    }
}
