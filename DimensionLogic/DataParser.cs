using System.Xml;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Map;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using TestMod.DimensionLogic.InternalHelperClasses;
using TestMod.Interfaces;

namespace TestMod.DimensionLogic
{
    /// <summary>
    /// The class that allows you to handle storage of dimensions.
    /// </summary>
    /// <typeparam name="TDimension">The dimension type that should be storing.</typeparam>
    public abstract class DataParser<TDimension>: DataParser where TDimension: Dimension, new()
    {        
        /// <summary>
        /// The type of registered dimension.
        /// </summary>
        public string Type { get; internal set; }
        public string Id { get; private set; }

        /// <summary>
        /// Should the loading method be called instead of using the cached dimension.
        /// </summary>
        public virtual bool AlwaysNew => false;

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

        internal override bool AlwaysNewInternal => AlwaysNew;

        internal override DimensionEntity LoadInternal(string id)
        {
            Id = id;

            var entity = new DimensionEntity<TDimension>
            {
                Type = Type,
                Id = Id,
                Dimension = Load() ?? new TDimension(),
            };

            entity.Size = new Point(
                entity.Dimension.Tiles?.GetLength(0) ?? 0, 
                entity.Dimension.Tiles?.GetLength(1) ?? 0);

            return entity;
        }

        internal override void SaveInternal(DimensionEntity entity)
        {
            Save((TDimension)entity.DimensionInternal);
        }
    }
}
