using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.IO;

namespace DimensionKeeper.Interfaces
{
    internal interface ITagCompoundStorage
    {
        TagCompound SavedDimensionsTag { get; set; }
    }
}
