using Microsoft.Xna.Framework;

namespace DimensionKeeper.DimensionService.InternalClasses
{
    public abstract class DimensionEntityInternal
    {
        public string Type { get; internal set; }
        public string Id { get; internal set; }
        public Point Location { get; internal set; }
        public Point Size { get; internal set; }

        public int Width => Size.X;
        public int Height => Size.Y;

        internal void CopyFrom(DimensionEntityInternal otherEntity)
        {
            DimensionInternal = otherEntity.DimensionInternal;
            Location = otherEntity.Location;
            Type = otherEntity.Type;
            Id = otherEntity.Id;
            Size = otherEntity.Size;
        }

        internal abstract Dimension DimensionInternal { get; set; }
    }
}