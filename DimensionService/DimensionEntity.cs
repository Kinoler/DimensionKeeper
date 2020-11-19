using TestMod.DimensionService.InternalHelperClasses;

namespace TestMod.DimensionService
{
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

        internal void CopyFrom(DimensionEntity otherEntity)
        {
            DimensionInternal = otherEntity.DimensionInternal;
            Location = otherEntity.Location;
            Type = otherEntity.Type;
            Id = otherEntity.Id;
            Size = otherEntity.Size;
        }
    }
}
