using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DimensionKeeper.Interfaces;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DimensionKeeper.HelperImplementations.Storages
{
    public abstract class TagCompoundFromModStorage<TDimension> : TagCompoundFromFileStorage<TDimension>
        where TDimension : class, IDimension, new()
    {
        public abstract Mod Mod { get; } //ModContent.GetInstance<DimensionKeeperMod>();

        public override TagCompound GetTagCompound()
        {
            var bytes = Mod.GetFileBytes(FileResourcePath);
            var stream = new MemoryStream(bytes);
            var tagCompound = TagIO.FromStream(stream);

            return tagCompound;
        }
    }
}