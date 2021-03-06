﻿using System.IO;
using DimensionKeeper.DimensionService.Configuration;
using DimensionKeeper.Interfaces;
using Terraria.ModLoader.IO;

namespace DimensionKeeper.HelperImplementations.Storages
{
    /// <summary>
    /// Allow you to store the tag compound in the file. (Load and save it)
    /// </summary>
    /// <typeparam name="TDimension">The dimension class. Be sure that it have a tag serializer.</typeparam>
    public class FileTagCompoundStorage<TDimension>: DimensionStorage<TDimension> 
        where TDimension : class, IDimension, new()
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
