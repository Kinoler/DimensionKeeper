using System.Collections.Generic;
using Terraria.ModLoader.IO;

namespace DimensionKeeper.DimensionService.DefaultStorages
{
    internal interface ITagCompoundStorage
    {
        TagCompound SavedDimensionsTag { get; set; }
    }

    public abstract class TagCompoundStorage<TDimension>: DimensionStorage<TDimension>, ITagCompoundStorage 
        where TDimension : Dimension, new()
    {
        public TagCompound SavedDimensionsTag { get; set; }

        /// <summary>
        /// Do not override this method to class work correctly.
        /// </summary>
        /// <returns></returns>
        public override TDimension Load()
        {
            if (!SavedDimensionsTag.ContainsKey(Id))
                return InitializeTag();

            return SavedDimensionsTag.Get<TDimension>(Id);
        }

        /// <summary>
        /// Do not override this method to class work correctly.
        /// </summary>
        /// <param name="dimension"></param>
        public override void Save(TDimension dimension)
        {
            SavedDimensionsTag = SavedDimensionsTag ?? new TagCompound();
            SavedDimensionsTag.Set(Id, dimension, true);
        }

        /// <summary>
        /// Called whenever dimension is loading provided that the world Tag Compound does not contain the dimension associated with the name.
        /// This used to initialize dimension in the first time.
        /// </summary>
        /// <returns>A new dimension</returns>
        public abstract TDimension InitializeTag();
    }
}
