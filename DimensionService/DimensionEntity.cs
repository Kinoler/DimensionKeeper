using DimensionKeeper.DimensionService.Configuration;
using DimensionKeeper.Interfaces;
using Microsoft.Xna.Framework;

namespace DimensionKeeper.DimensionService
{

    /// <summary>
    /// Represents the dimension which will be or already loaded into the world.
    /// </summary>
    public abstract class DimensionEntity
    {
        /// <summary>
        /// The dimension type.
        /// </summary>
        public string Type { get; internal set; }

        /// <summary>
        /// The dimension id.
        /// </summary>
        public string Id { get; internal set; }

        /// <summary>
        /// The tile location. Points to the left up corner of dimension.
        /// </summary>
        public Point Location { get; internal set; }

        /// <summary>
        /// The size of dimension.
        /// </summary>
        public Point Size { get; internal set; }

        public int Width => Size.X;
        public int Height => Size.Y;

        /// <summary>
        /// Gets the dimension.
        /// </summary>
        /// <typeparam name="TDimension">The class of dimension.</typeparam>
        /// <returns>The dimension.</returns>
        public TDimension GetDimension<TDimension>() where TDimension : class, IDimension
        {
            return DimensionInternal as TDimension;
        }

        public void CopyFrom(DimensionEntity otherEntity)
        {
            DimensionInternal = otherEntity.DimensionInternal;
            Location = otherEntity.Location;
            Type = otherEntity.Type;
            Id = otherEntity.Id;
            Size = otherEntity.Size;
        }

        internal abstract IDimension DimensionInternal { get; set; }

        public override string ToString()
        {
            return $"{nameof(Type)}: {Type}; {nameof(Id)}: {Id}; {nameof(Location)}: {Location}; {nameof(Size)}: {Size}; {nameof(DimensionInternal)}: {DimensionInternal};";
        }
    }

    /// <summary>
    /// Represents the dimension which will be or already loaded into the world.
    /// </summary>
    public sealed class DimensionEntity<TDimension>: DimensionEntity 
        where TDimension: IDimension
    {
        /// <summary>
        /// The dimension.
        /// </summary>
        public TDimension Dimension { get; internal set; }

        internal override IDimension DimensionInternal
        {
            get => Dimension;
            set => Dimension = (TDimension)value;
        }
    }
}
