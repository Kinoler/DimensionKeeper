using System;
using System.IO;
using DimensionKeeper.Interfaces;
using DimensionKeeper.Interfaces.Internal;
using Microsoft.Xna.Framework;

namespace DimensionKeeper.DimensionService.Configuration
{
    /// <summary>
    /// The class that allows you to handle storage of dimensions.
    /// </summary>
    /// <typeparam name="TDimension">The dimension type that should be storing.</typeparam>
    public abstract class DimensionStorage<TDimension>: IDimensionStorage 
        where TDimension: class, IDimension, new()
    {        
        /// <summary>
        /// The type with which storage was registered.
        /// </summary>
        public string Type { get; internal set; }

        /// <summary>
        /// The id with which Load/Save method was called.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Allows you to specify the way of loading a dimension.
        /// </summary>
        /// <returns>A new dimension</returns>
        public abstract TDimension Load();

        /// <summary>
        /// Allows you to specify the way of saving a dimension.
        /// </summary>
        /// <param name="dimension">The dimension which should be saving</param>
        public abstract void Save(TDimension dimension);

        /// <summary>
        /// Calls in the Client/Server mode when new player entered to the world.
        /// Works on the server side.
        /// Write the dimension data to the writer.
        /// </summary>
        /// <param name="writer">The writer for the dimension data.</param>
        public virtual void Send(BinaryWriter writer)
        {
        }

        /// <summary>
        /// Calls in the Client/Server mode when new player entered to the world.
        /// Works on the client side.
        /// Read the dimension data from the reader.
        /// </summary>
        /// <param name="reader">The reader with the dimension data</param>
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
            };

            try
            {
                entity.Dimension = Load() ?? new TDimension();
            }
            catch (Exception e)
            {
                entity.Dimension = new TDimension();
            }

            entity.Size = new Point(
                entity.Dimension.Width, 
                entity.Dimension.Height);

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
