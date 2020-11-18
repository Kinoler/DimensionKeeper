using System;
using System.IO;
using Terraria.ModLoader;
using TestMod.DimensionExample;
using TestMod.DimensionLogic;

namespace TestMod
{
    public class ModExample: Mod
    {
        public override void Load()
        {
            DimensionsRegister.SetupDimensionTypesRegister<DimensionRegisterExample>();
        }

        public override void Unload()
        {
            DimensionStorageExample.Dimensions = null;
        }
    }
}