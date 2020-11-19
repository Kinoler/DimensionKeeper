using Microsoft.Xna.Framework;
using TestMod.DimensionService.InternalHelperClasses;

namespace TestMod.DimensionService
{
    /// <summary>
    /// The class that allows you to handle storage of dimensions.
    /// </summary>
    /// <typeparam name="TDimension">The dimension type that should be storing.</typeparam>
    public abstract class DimensionStorage<TDimension>: DimensionStorageInternal where TDimension: Dimension, new()
    {        
        /// <summary>
        /// The type of registered dimension.
        /// </summary>
        public string Type { get; internal set; }
        public string Id { get; private set; }

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

        internal override DimensionEntity CreateEmptyEntity(Point location, Point size)
        {
            return new DimensionEntity<TDimension>
            {
                Type = Type,
                Location = location,
                Size = size,
                Dimension = new TDimension(),
            };
        }

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
            Id = entity.Id;

            Save((TDimension)entity.DimensionInternal);
        }
    }
}
