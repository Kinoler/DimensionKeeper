using System.IO;
using DimensionKeeper.DimensionService.Configuration;
using DimensionKeeper.Interfaces;
using Terraria.ModLoader.IO;

namespace DimensionKeeper.HelperImplementations.Storages
{
    /// <summary>
    /// Allow you to store the dimensions in the world tag compound.
    /// </summary>
    /// <typeparam name="TDimension">The dimension class. Be sure that it have a tag serializer.</typeparam>
    public abstract class TagCompoundStorage<TDimension>: DimensionStorage<TDimension>, ITagCompoundStorage 
        where TDimension : Dimension, new()
    {
        /// <summary>
        /// The tag compound.
        /// </summary>
        public TagCompound SavedDimensionsTag { get; set; }

        /// <summary>
        /// Do not override this method to class work correctly.
        /// </summary>
        /// <returns></returns>
        public override TDimension Load()
        {
            SavedDimensionsTag = SavedDimensionsTag ?? new TagCompound();
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

        public override void Send(BinaryWriter writer)
        {
            TagIO.Write(SavedDimensionsTag, writer);
        }

        public override void Receive(BinaryReader reader)
        {
            SavedDimensionsTag = TagIO.Read(reader);
        }

        /// <summary>
        /// Called whenever dimension is loading provided that the world tag compound does not contain the dimension associated with the name.
        /// This used to initialize dimension in the first time.
        /// </summary>
        /// <returns>A new dimension</returns>
        public abstract TDimension InitializeTag();
    }
}