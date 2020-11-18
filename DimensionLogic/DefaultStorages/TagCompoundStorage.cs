using System.Collections.Generic;
using Terraria.ModLoader.IO;

namespace TestMod.DimensionLogic.DefaultParsers
{
    internal interface ITagCompoundStorage
    {
        Dictionary<string, TagCompound> TagsToSave { get; set; }
    }

    public abstract class TagCompoundStorage<TDimension>: DimensionStorage<TDimension>, ITagCompoundStorage where TDimension : Dimension, new()
    {
        internal static TagCompound OnWorldSave()
        {
            var dimensionsTag = new TagCompound();
            foreach (var name in DimensionLoader.RegisteredDimension.GetNames())
            {
                var parser = DimensionLoader.RegisteredDimension.GetParser(name);
                if (parser is ITagCompoundStorage tagCompoundParser)
                {
                    dimensionsTag.Add(name, tagCompoundParser.TagsToSave);
                }
            }

            return dimensionsTag;
        }

        public Dictionary<string, TagCompound> TagsToSave { get; set; }

        /// <summary>
        /// Do not override this method to class work correctly. Override <see cref="Load(TagCompound)"/> instead.
        /// </summary>
        /// <returns></returns>
        public override TDimension Load()
        {
            if (AlwaysNew || !TestWorldMod.DimensionsTag.ContainsKey(Id))
                return InitializeInternal();

            var tag = TestWorldMod.DimensionsTag.GetCompound(Id);
            return Load(tag);

        }

        /// <summary>
        /// Do not override this method to class work correctly. Override <see cref="Save(TDimension, TagCompound)"/> instead.
        /// </summary>
        /// <param name="dimension"></param>
        public override void Save(TDimension dimension)
        {
            var tagToSave = new TagCompound();
            Save(dimension, tagToSave);

            TagsToSave.Add(Id, tagToSave);
        }

        internal TDimension InitializeInternal()
        {
            var dimension = Initialize();
            if (!AlwaysNew)
            {
                Save(dimension);
            }
            return dimension;
        }

        /// <summary>
        /// Called whenever dimension is loading provided that the world Tag Compound does not contain the dimension associated with the name.
        /// This used to initialize dimension in the first time.
        /// </summary>
        /// <returns>A new dimension</returns>
        public abstract TDimension Initialize();

        /// <summary>
        /// Allow you to load a new dimension from the <see cref="TagCompound"/> class.
        /// Which was saved in the past.
        /// </summary>
        /// <param name="tag">The <see cref="TagCompound"/> which was saved in the past.</param>
        /// <returns>A new dimension.</returns>
        public abstract TDimension Load(TagCompound tag);

        /// <summary>
        /// Allow you to save the dimension to the <see cref="TagCompound"/>.
        /// To the load it in the future session.
        /// </summary>
        /// <param name="dimension">The current dimension which should be saving.</param>
        /// <param name="tag">The <see cref="TagCompound"/> which will be store in the world <see cref="TagCompound"/>.</param>
        public abstract void Save(TDimension dimension, TagCompound tag);
    }
}
