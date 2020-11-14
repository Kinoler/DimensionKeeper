using System.Xml;
using Terraria;
using Terraria.Map;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace TestMod.DimensionLogic
{
    public abstract class DataParser
    {
        private Dimension _cachedDimension;

        internal TagCompound TagToSave { get; set; }

        internal Dimension GetDimension(string name)
        {
            if (AlwaysNewInternal || _cachedDimension == null)
            {
                _cachedDimension = LoadInternal(name);
            }

            return _cachedDimension;
        }

        internal abstract bool AlwaysNewInternal { get; set; }

        internal abstract Dimension LoadInternal(string tag);

        internal abstract void SaveInternal(Dimension dimension);
    }

    /// <summary>
    /// The class that allows you to handle storage of dimensions.
    /// </summary>
    /// <typeparam name="TDimension">The dimension type that should be storing.</typeparam>
    public abstract class DataParser<TDimension>: DataParser where TDimension: Dimension
    {
        /// <summary>
        /// Should the loading method be called instead of using the cached dimension.
        /// </summary>
        public virtual bool AlwaysNew => false;

        /// <summary>
        /// The name with which the <see cref="DimensionLoader.LoadDimension"/> method was called.
        /// </summary>
        public string Name { get; private set; }

        internal override bool AlwaysNewInternal => AlwaysNew;

        internal override Dimension LoadInternal(string name)
        {
            Name = name;
            var tag = DimensionLoader.DimensionsTag.GetCompound(name);
            return tag != null ? Load(tag) : InitializeInternal();
        }

        internal override void SaveInternal(Dimension dimension)
        {
            TagToSave = new TagCompound();
            Save((TDimension)dimension, TagToSave);
        }

        internal TDimension InitializeInternal()
        {
            var dimension = Initialize();
            SaveInternal(dimension);
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
        /// <param name="tag">The <see cref="TagCompound"/> which was saved in the past</param>
        /// <returns>A new dimension</returns>
        public abstract TDimension Load(TagCompound tag);

        /// <summary>
        /// Allow you to save the dimension to the <see cref="TagCompound"/>.
        /// To the load it in the future session.
        /// </summary>
        /// <param name="dimension">The current dimension which should be saving</param>
        /// <param name="tag">The <see cref="TagCompound"/> which will be store in the world <see cref="TagCompound"/>.</param>
        public abstract void Save(TDimension dimension, TagCompound tag);
    }
}
