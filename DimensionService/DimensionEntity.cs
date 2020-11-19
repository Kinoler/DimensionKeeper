using DimensionKeeper.DimensionService.InternalClasses;

namespace DimensionKeeper.DimensionService
{
    /// <summary>
    /// Represents the dimension which will be or already loaded into the world.
    /// </summary>
    public sealed class DimensionEntity<TDimension>: DimensionEntityInternal where TDimension: Dimension
    {
        public TDimension Dimension { get; internal set; }

        internal override Dimension DimensionInternal
        {
            get => Dimension;
            set => Dimension = (TDimension)value;
        }
    }
}
