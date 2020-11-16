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

        internal static TagCompound DimensionsTag { get; set; }

        public override void Initialize()
        {
            DimensionLoader.Clear();
        }

        public override void Load(TagCompound tag)
        {
            DimensionsTag = tag.GetCompound(DimensionListTagName);
            DimensionLoader.Load(tag.GetCompound(CurrentDimensionTagName));
        }

        public override TagCompound Save()
        {
            var dimensionsTag = new TagCompound();
            dimensionsTag.Set(DimensionListTagName, TagCompoundParser<Dimension>.OnWorldSave());
            dimensionsTag.Set(CurrentDimensionTagName, DimensionLoader.Save());

            return dimensionsTag;
        }
    }
}
