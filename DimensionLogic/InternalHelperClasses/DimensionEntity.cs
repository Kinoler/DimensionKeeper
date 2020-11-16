using Microsoft.Xna.Framework;

namespace TestMod.DimensionLogic.InternalHelperClasses
{
    public abstract class DimensionEntity
    {
        public string TypeName { get; internal set; }

        public string Id { get; internal set; }

        public Point Location { get; internal set; }

        internal abstract Dimension DimensionInternal { get; set; }
    }
}