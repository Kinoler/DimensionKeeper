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

        internal Dimension GetDimension(string name)
        {
            if (AlwaysNewInternal || _cachedDimension == null)
            {
                _cachedDimension = LoadInternal(name);
            }

            return _cachedDimension;
        }

        internal abstract bool AlwaysNewInternal { get; }

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
            return Load();
        }

        internal override void SaveInternal(Dimension dimension)
        {
            Save((TDimension)dimension);
        }

        /// <summary>
        /// Allow you to load a dimension from the storage.
        /// </summary>
        /// <returns>A new dimension</returns>
        public abstract TDimension Load();

        /// <summary>
        /// Allow you to save the dimension to the storage.
        /// </summary>
        /// <param name="dimension">The dimension which should be saving</param>
        public abstract void Save(TDimension dimension);
    }
}
