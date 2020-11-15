﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using TestMod.DimensionExample;
using TestMod.DimensionLogic;

namespace TestMod
{
    public class TestWorldMod: ModWorld
    {
        private const string DimensionsTagName = "Dimensions";
        internal static TagCompound DimensionsTag { get; set; }

        public override void Load(TagCompound tag)
        {
            DimensionsTag = tag.GetCompound(DimensionsTagName);
        }

        public override TagCompound Save()
        {
            var dimensionsTag = new TagCompound();
            dimensionsTag.Set(DimensionsTagName, TagCompoundParser<Dimension>.OnWorldSave());

            return dimensionsTag;
        }
    }
}