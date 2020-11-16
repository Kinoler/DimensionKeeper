using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Mono.CompilerServices.SymbolWriter;

namespace TestMod.DimensionLogic
{
    public abstract class DimensionEntity
    {
        public string TypeName { get; internal set; }
        public string Id { get; internal set; }

        public Point Location { get; internal set; }

        internal abstract Dimension DimensionInternal { get; set; }
    }

    /// <summary>
    /// Represent the loaded dimension into the world.
    /// </summary>
    public class DimensionEntity<TDimension>: DimensionEntity where TDimension: Dimension
    {
        public TDimension Dimension { get; internal set; }

        public void CopyFrom(DimensionEntity otherEntity)
        {
            DimensionInternal = otherEntity.DimensionInternal;
            Location = otherEntity.Location;
            TypeName = otherEntity.TypeName;
            Id = otherEntity.Id;
        }

        internal override Dimension DimensionInternal
        {
            get => Dimension;
            set => Dimension = (TDimension)value;
        }

    }
}
