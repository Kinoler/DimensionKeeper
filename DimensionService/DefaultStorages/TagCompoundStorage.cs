using System.Collections.Generic;
using Terraria.ModLoader.IO;

namespace TestMod.DimensionLogic.DefaultParsers
{
    internal interface ITagCompoundStorage
    {
        Dictionary<string, TagCompound> SavedDimensionTags { get; set; }
    }

    public abstract class TagCompoundStorage<TDimension>: DimensionStorage<TDimension>, ITagCompoundStorage where TDimension : Dimension, new()
    {
        public Dictionary<string, TagCompound> SavedDimensionTags { get; set; }

        /// <summary>
        /// Do not override this method to class work correctly. Override the <see cref="LoadTag(TagCompound)"/> instead.
        /// </summary>
        /// <returns></returns>
        public override TDimension Load()
        {
            if (!DimensionKeeperModWorld.DimensionsTag.ContainsKey(Id))
                return InitializeTag();

            var tag = DimensionKeeperModWorld.DimensionsTag.GetCompound(Id);
            return LoadTag(tag);
        }

        /// <summary>
        /// Do not override this method to class work correctly. Override the <see cref="SaveTag(TDimension)"/> instead.
        /// </summary>
        /// <param name="dimension"></param>
        public override void Save(TDimension dimension)
        {
            var tagToSave = SaveTag(dimension);
            SavedDimensionTags.Add(Id, tagToSave);
        }

        /// <summary>
        /// Called whenever dimension is loading provided that the world Tag Compound does not contain the dimension associated with the name.
        /// This used to initialize dimension in the first time.
        /// </summary>
        /// <returns>A new dimension</returns>
        public abstract TDimension InitializeTag();

        /// <summary>
        /// Allow you to load a new dimension from the <see cref="TagCompound"/> class.
        /// Which was saved in the past.
        /// </summary>
        /// <param name="tag">The <see cref="TagCompound"/> which was saved in the past.</param>
        /// <returns>A new dimension.</returns>
        public abstract TDimension LoadTag(TagCompound tag);

        /// <summary>
        /// Allow you to save the dimension to the <see cref="TagCompound"/>.
        /// To the load it in the future session.
        /// </summary>
        /// <param name="dimension">The current dimension which should be saving.</param>
        public abstract TagCompound SaveTag(TDimension dimension);
    }
}
