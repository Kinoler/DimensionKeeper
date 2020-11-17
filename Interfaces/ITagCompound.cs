using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.IO;
using TestMod.DimensionLogic;

namespace TestMod.Interfaces
{
    internal interface ITagCompound
    {
        TagCompound Save();

        void Load(TagCompound tag);
    }
}
