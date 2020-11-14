using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using TestMod.DimensionLogic;

namespace TestMod
{
    public class TestWorldMod: ModWorld
    {
        private const string DimensionsTag = "Dimensions";

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Load(TagCompound tag)
        {
            var dimensionsTag = tag.GetCompound(DimensionsTag);
            if (dimensionsTag != null) 
                DimensionLoader.DimensionsTag = dimensionsTag;
        }

        public override TagCompound Save()
        {
            var dimensionsTag = new TagCompound();
            foreach (var name in DimensionLoader.RegisteredDimension.GetNames())
            {
                var parser = DimensionLoader.RegisteredDimension.GetParser(name);
                dimensionsTag.Set(name, parser.TagToSave);
            }

            return base.Save();
        }
    }
}
