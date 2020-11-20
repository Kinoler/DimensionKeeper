using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.IO;

namespace DimensionKeeper.DimensionService.DefaultStorages
{
    public class TagCompoundFromFileStorage<TDimension> : TagCompoundStorage<TDimension> 
        where TDimension : Dimension, new()
    {
        private string FileResourcePath => Path.Combine(ResourceFolderName, ResourceFileName);

        public virtual string ResourceFolderName => "Resources";
        public virtual string ResourceFileName => $"{Type}-{Id}";

        public override TDimension InitializeTag()
        {
            var tagCompound = TagIO.FromFile(FileResourcePath);
            return TagIO.Deserialize<TDimension>(tagCompound);
        }
    }
}
