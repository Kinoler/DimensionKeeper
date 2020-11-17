using Microsoft.Xna.Framework;

namespace TestMod.DimensionLogic.InternalHelperClasses
{
    public abstract class DimensionEntity
    {
        public string Type { get; internal set; }
        public string Id { get; internal set; }
        public Point Location { get; internal set; }
        public Point Size { get; internal set; }

        public int Width => Size.X;
        public int Height => Size.Y;

        internal abstract Dimension DimensionInternal { get; set; }
    }
}