using System.IO;
using DimensionKeeper.DimensionService.InternalClasses;
using DimensionKeeper.Interfaces.Internal;
using Microsoft.Xna.Framework;

namespace DimensionKeeper.DimensionService.Configuration
{
    /// <summary>
    /// The class that allows you to handle storage of dimensions.
    /// </summary>
    /// <typeparam name="TDimension">The dimension type that should be storing.</typeparam>
    public abstract class DimensionStorage<TDimension>: IDimensionStorage where TDimension: Dimension, new()
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

        public virtual void Send(BinaryWriter writer)
        {
        }

        public virtual void Receive(BinaryReader reader)
        {
        }

        DimensionEntity IDimensionStorage.CreateEmptyEntity(Point location, Point size)
        {
            return new DimensionEntity<TDimension>
            {
                Type = Type,
                Id = Id ?? Type,
                Location = location,
                Size = size,
                Dimension = new TDimension(),
            };
        }

        DimensionEntity IDimensionStorage.LoadInternal(string id)
        {
            Id = id ?? Type;

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

        void IDimensionStorage.SaveInternal(DimensionEntity entity)
        {
            Id = entity.Id;

            Save((TDimension)entity.DimensionInternal);
        }

        void IDimensionStorage.SendInternal(BinaryWriter writer)
        {
            Send(writer);
        }

        void IDimensionStorage.ReceiveInternal(BinaryReader reader)
        {
            Receive(reader);
        }
    }
}
