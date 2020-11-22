using System.IO;
using DimensionKeeper.DimensionService.Configuration;
using Terraria.ModLoader.IO;

namespace DimensionKeeper.HelperImplementations.Storages
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
