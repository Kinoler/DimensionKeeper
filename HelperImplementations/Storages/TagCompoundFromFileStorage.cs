using System.IO;
using DimensionKeeper.DimensionService.Configuration;
using DimensionKeeper.Interfaces;
using Terraria.ModLoader.IO;

namespace DimensionKeeper.HelperImplementations.Storages
{
    /// <summary>
    /// Allows you to gets the dimension from the file.
    /// </summary>
    /// <typeparam name="TDimension">The dimension class. Be sure that it have a tag serializer.</typeparam>
    public class TagCompoundFromFileStorage<TDimension> : TagCompoundStorage<TDimension> 
        where TDimension : class, IDimension, new()
    {
        public string FileResourcePath => Path.Combine(ResourceFolderName, ResourceFileName);

        public virtual string ResourceFolderName => "Resources";
        public virtual string ResourceFileName => $"{Type}-{Id}";

        public override TagCompound GetTagCompound()
        {
            return TagIO.FromFile(FileResourcePath);
        }
    }
}
