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
        where TDimension : class, IDimension, new()
    {
        /// <summary>
        /// The tag compound.
        /// </summary>
        public TagCompound SavedDimensionsTag { get; set; } = new TagCompound();

        /// <summary>
        /// Do not override this method to class work correctly.
        /// </summary>
        public override TDimension Load()
        {
            if (!SavedDimensionsTag.ContainsKey(Id))
                return InitializeTag();

            return SavedDimensionsTag.Get<TDimension>(Id);
        }

        /// <summary>
        /// Do not override this method to class work correctly.
        /// </summary>
        public override void Save(TDimension dimension)
        {
            SavedDimensionsTag.Set(Id, dimension, true);
        }

        /// <summary>
        /// Do not override this method to class work correctly.
        /// </summary>
        public override void Send(BinaryWriter writer)
        {
            TagIO.Write(SavedDimensionsTag, writer);
        }

        /// <summary>
        /// Do not override this method to class work correctly.
        /// </summary>
        public override void Receive(BinaryReader reader)
        {
            SavedDimensionsTag = TagIO.Read(reader) ?? new TagCompound();
        }

        /// <summary>
        /// Called whenever dimension is loading provided that the world tag compound does not contain saved the dimension with this id.
        /// This used to initialize dimension in the first time.
        /// </summary>
        /// <returns>A new dimension</returns>
        public virtual TDimension InitializeTag()
        {
            var tagCompound = GetTagCompound();

            SavedDimensionsTag.Add(Id, tagCompound);
            var dimension = TagIO.Deserialize<TDimension>(tagCompound);
            return dimension;
        }

        /// <summary>
        /// Called whenever InitializeTag is called.
        /// </summary>
        /// <returns>The tag compound from anywhere.</returns>
        public abstract TagCompound GetTagCompound();
    }
}