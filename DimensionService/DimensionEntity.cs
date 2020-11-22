using DimensionKeeper.DimensionService.Configuration;
using Microsoft.Xna.Framework;

namespace DimensionKeeper.DimensionService
{
    public abstract class DimensionEntity
    {
        public string Type { get; internal set; }
        public string Id { get; internal set; }

        /// <summary>
        /// Points to the left up corner.
        /// </summary>
        public Point Location { get; internal set; }
        public Point Size { get; internal set; }

        public int Width => Size.X;
        public int Height => Size.Y;

        internal void CopyFrom(DimensionEntity otherEntity)
        {
            DimensionInternal = otherEntity.DimensionInternal;
            Location = otherEntity.Location;
            Type = otherEntity.Type;
            Id = otherEntity.Id;
            Size = otherEntity.Size;
        }

        internal abstract Dimension DimensionInternal { get; set; }

        public override string ToString()
        {
            return $"{nameof(Type)}: {Type}; {nameof(Id)}: {Id}; {nameof(Location)}: {Location}; {nameof(Size)}: {Size}; {nameof(DimensionInternal)}: {DimensionInternal};";
        }
    }

    /// <summary>
    /// Represents the dimension which will be or already loaded into the world.
    /// </summary>
    public sealed class DimensionEntity<TDimension>: DimensionEntity where TDimension: Dimension
    {
        public TDimension Dimension { get; internal set; }

        internal override Dimension DimensionInternal
        {
            get => Dimension;
            set => Dimension = (TDimension)value;
        }
    }
}
