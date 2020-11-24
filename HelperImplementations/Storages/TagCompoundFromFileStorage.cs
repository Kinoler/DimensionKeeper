using System.IO;
using DimensionKeeper.DimensionService.Configuration;
using Terraria.ModLoader.IO;

namespace DimensionKeeper.HelperImplementations.Storages
{
    /// <summary>
    /// Allows you to gets the dimension from the file.
    /// </summary>
    /// <typeparam name="TDimension">The dimension class. Be sure that it have a tag serializer.</typeparam>
    public class TagCompoundFromFileStorage<TDimension> : TagCompoundStorage<TDimension> 
        where TDimension : Dimension, new()
    {
        private string FileResourcePath => Path.Combine(ResourceFolderName, ResourceFileName);

        public virtual string ResourceFolderName => "Resources";
        public virtual string ResourceFileName => $"{Type}-{Id}";

        public override TDimension InitializeTag()
        {
            var tagCompound = TagIO.FromFile(FileResourcePath);

            SavedDimensionsTag = SavedDimensionsTag ?? new TagCompound();
            SavedDimensionsTag.Add(Id, tagCompound);

            var dimension = TagIO.Deserialize<TDimension>(tagCompound);

            return dimension;
        }
    }
}
