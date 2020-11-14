using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.IO;
using TestMod.DimensionLogic;

namespace TestMod.DimensionExample
{
    public abstract class TagCompoundParser<TDimension>: DataParser<TDimension> where TDimension : Dimension
    {
        internal static TagCompound OnWorldSave()
        {
            var dimensionsTag = new TagCompound();
            foreach (var name in DimensionLoader.RegisteredDimension.GetNames())
            {
                var parser = DimensionLoader.RegisteredDimension.GetParser(name);
                if (parser is TagCompoundParser<TDimension> tagCompoundParser)
                {
                    dimensionsTag.Set(name, tagCompoundParser.TagToSave);
                }
            }

            return dimensionsTag;
        }

        internal TagCompound TagToSave { get; set; }

        /// <summary>
        /// Do not override this method to class work correctly. Override <see cref="Load(TagCompound)"/> instead.
        /// </summary>
        /// <returns></returns>
        public override TDimension Load()
        {
            var tag = TestWorldMod.DimensionsTag.GetCompound(Name);
            return tag != null ? Load(tag) : InitializeInternal();
        }

        /// <summary>
        /// Do not override this method to class work correctly.
        /// </summary>
        /// <param name="dimension"></param>
        public override void Save(TDimension dimension)
        {
            TagToSave = new TagCompound();
            Save(dimension, TagToSave);
        }

        internal TDimension InitializeInternal()
        {
            var dimension = Initialize();
            Save(dimension);
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
