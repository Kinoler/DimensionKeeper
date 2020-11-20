using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using DimensionKeeper.TagSerializers;
using Terraria.ModLoader.IO;
using Terraria.Utilities;

namespace DimensionKeeper.DimensionService.DefaultStorages
{
    public class ResourceManagerStorage<TDimension>: DimensionStorage<TDimension> where TDimension : Dimension, new()
    {
        private string FileResourcePath => Path.Combine(ResourceFolderName, ResourceFileName);

        public virtual string ResourceFolderName => "Resources";
        public virtual string ResourceFileName => $"{Type}-{Id}";

        public override TDimension Load()
        {
            var tagCompound = TagIO.FromFile(FileResourcePath);
            return TagIO.Deserialize<TDimension>(tagCompound);
        }

        public override void Save(TDimension dimension)
        {
            if (!Directory.Exists(ResourceFolderName)) 
                Directory.CreateDirectory(ResourceFolderName);
            if (File.Exists(FileResourcePath))
                File.Delete(FileResourcePath);

            var tag = (TagCompound)TagIO.Serialize(dimension);
            TagIO.ToFile(tag, FileResourcePath);
        }
    }
}
